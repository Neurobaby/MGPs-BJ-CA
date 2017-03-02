Imports BJ_CA.BJCAShared

Public Class BJCAResultsForm
    Inherits System.Windows.Forms.Form

#Region " Declarations "

#Region " General Declarations "
    Private PreviousObject As System.Object
    Private PreviousGroup As System.Object

    Public Results As New BJCA
    Public Filenames As New BJCAFileSetClass
    Private Constants As New BJCAGlobalsClass
    Private ResultsLoaded As Boolean

    Private ForcedRule As New BJCAForcedRulesClass
    Public FormRules As New BJCAFormRulesClass

    Private HAHand As New BJCAHandClass

    Private DAHand As New BJCAHandClass
    Private DoubleAnalysisArray(2, 9) As IndexedTextBox

    Private EHand As New BJCAHandClass
    Private CurrentEList() As Integer
    Private CurrentNCEList() As Integer

    Private UCEVsArray(4, 9) As IndexedTextBox
    Private PCardEVsArray(4, 9) As IndexedTextBox

    Private SuitedHandEVsArray(5, 4) As IndexedTextBox

    Private HandSizeAnalysisArray(20, 5) As IndexedTextBox

    Private PDTiesArray(5) As IndexedTextBox
    Private SplitsAllowedArray(10) As IndexedTextBox
    Private ShoeArray(5, 11) As IndexedTextBox
    Private SplitsArray(4, 2, 9) As IndexedTextBox

    Private EORNcards As Integer
    Private EORHand As New BJCAHandClass
    Private EORs(11) As BJCAEORsClass
    Private EOREVsArray(3, 10) As IndexedTextBox
    Private EORNetEVsArray(3, 10) As IndexedTextBox
    Private EORHandArray(6, 9) As IndexedTextBox
    Private EORHardTotalArray(18, 15) As IndexedTextBox
    Private EORSoftTotalArray(10, 15) As IndexedTextBox
    Private HardEORLabelArray(18) As IndexedLabel
    Private SoftEORLabelArray(10) As IndexedLabel

    Private HardTDStratTableArray(17, 9) As IndexedTextBox
    Private SoftTDStratTableArray(9, 9) As IndexedTextBox
    Private HardCDStratTableArray(35, 9) As IndexedTextBox
    Private SoftCDStratTableArray(8, 9) As IndexedTextBox
    Private PairCDStratTableArray(9, 9) As IndexedTextBox
    Friend WithEvents HardTDStratTableArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents SoftTDStratTableArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents HardCDStratTableArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents SoftCDStratTableArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents PairCDStratTableArrayHandler As System.Windows.Forms.TextBox

    Private StratColorBoxArray() As IndexedTextBox
    Friend WithEvents StratColorBoxArrayHandler As System.Windows.Forms.TextBox

#End Region

#Region " Forced Rules Declarations "
    Private ForcedRulesHandArray(9) As IndexedTextBox
    Private HardTDForcedTableArray(16, 9) As IndexedTextBox
    Private SoftTDForcedTableArray(8, 9) As IndexedTextBox
    Private HardCDForcedTableArray(35, 9) As IndexedTextBox
    Private SoftCDForcedTableArray(8, 9) As IndexedTextBox
    Private PairCDForcedTableArray(9, 9) As IndexedTextBox

    Private HardTDForcedTableLabelArray(16) As IndexedLabel
    Private SoftTDForcedTableLabelArray(8) As IndexedLabel
    Private HardCDForcedTableLabelArray(35) As IndexedLabel
    Private SoftCDForcedTableLabelArray(8) As IndexedLabel
    Private PairCDForcedTableLabelArray(9) As IndexedLabel

    Friend WithEvents ForcedRulesHandArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents HardTDForcedTableArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents SoftTDForcedTableArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents HardCDForcedTableArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents SoftCDForcedTableArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents PairCDForcedTableArrayHandler As System.Windows.Forms.TextBox

    Friend WithEvents HardTDForcedTableLabelArrayHandler As System.Windows.Forms.Label
    Friend WithEvents SoftTDForcedTableLabelArrayHandler As System.Windows.Forms.Label
    Friend WithEvents HardCDForcedTableLabelArrayHandler As System.Windows.Forms.Label
    Friend WithEvents SoftCDForcedTableLabelArrayHandler As System.Windows.Forms.Label
    Friend WithEvents PairCDForcedTableLabelArrayHandler As System.Windows.Forms.Label
    Private CurrentForcedRuleStrat As Integer
#End Region

#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeForm()

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ResultsFormTabControl As System.Windows.Forms.TabControl
    Friend WithEvents SummaryTab As System.Windows.Forms.TabPage
    Friend WithEvents SaveResultsButtonSummTab As System.Windows.Forms.Button
    Friend WithEvents PrintToExelButtonSummTab As System.Windows.Forms.Button
    Friend WithEvents LoadResultsButtonSummTab As System.Windows.Forms.Button
    Friend WithEvents TDEVBoxSummTab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedEVBoxSummTab As System.Windows.Forms.TextBox
    Friend WithEvents CDEVBoxSummTab As System.Windows.Forms.TextBox
    Friend WithEvents TCEVBoxSummTab As System.Windows.Forms.TextBox
    Friend WithEvents TDStrat2LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents ForcedStrat2LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents CDStrat2LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents TCStrat2LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents DealerUCLabelSummTab As System.Windows.Forms.Label
    Friend WithEvents PlayerLabelSummTab As System.Windows.Forms.Label
    Friend WithEvents Prob1LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents Prob2LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents TCStrat3LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents CDStrat3LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents ForcedStrat3LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents TDStrat3LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents TCStrat1LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents CDStrat1LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents ForcedStrat1LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents TDStrat1LabelSummTab As System.Windows.Forms.Label
    Friend WithEvents NetEVLabelSummTab As System.Windows.Forms.Label
    Friend WithEvents StratTab As System.Windows.Forms.TabPage
    Friend WithEvents RulesTab As System.Windows.Forms.TabPage
    Friend WithEvents SplitsTab As System.Windows.Forms.TabPage
    Friend WithEvents ResultsFilenameBoxSummTab As System.Windows.Forms.TextBox
    Friend WithEvents SixtoTButtonSplitTab As System.Windows.Forms.RadioButton
    Friend WithEvents Ato5ButtonSplitTab As System.Windows.Forms.RadioButton
    Friend WithEvents CDButtonSplitTab As System.Windows.Forms.RadioButton
    Friend WithEvents TCButtonSplitTab As System.Windows.Forms.RadioButton
    Friend WithEvents ForcedButtonSplitTab As System.Windows.Forms.RadioButton
    Friend WithEvents TDButtonSplitTab As System.Windows.Forms.RadioButton
    Friend WithEvents Card5LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents Card3LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents Card4LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents Card2LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents Card1LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents PaircardsGroupSplitTab As System.Windows.Forms.GroupBox
    Friend WithEvents StrategyGroupSplitTab As System.Windows.Forms.GroupBox
    Friend WithEvents StandsOnSoftBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents BJPaysBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents BJPaysLabelRTab As System.Windows.Forms.Label
    Friend WithEvents StandsOnSoftLabelRTab As System.Windows.Forms.Label
    Friend WithEvents NDecksLabelRTab As System.Windows.Forms.Label
    Friend WithEvents NDecksBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SplitAllowedLabelRTab As System.Windows.Forms.Label
    Friend WithEvents BJRuleBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents DBJRuleLabelRTab As System.Windows.Forms.Label
    Friend WithEvents DTypeBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents DTypeLabelRTab As System.Windows.Forms.Label
    Friend WithEvents DASBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents DASLabelRTab As System.Windows.Forms.Label
    Friend WithEvents SurrTypeLabelRTab As System.Windows.Forms.Label
    Friend WithEvents SPLBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SPLRLabelTab As System.Windows.Forms.Label
    Friend WithEvents SurrPaysBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SurrPaysLabelRTab As System.Windows.Forms.Label
    Friend WithEvents DSoftHardBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents DANBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SSABoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SSARLabelTab As System.Windows.Forms.Label
    Friend WithEvents DSABoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents DSARLabelTab As System.Windows.Forms.Label
    Friend WithEvents HSABoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SANBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SANLabelRTab As System.Windows.Forms.Label
    Friend WithEvents HSARLabelTab As System.Windows.Forms.Label
    Friend WithEvents SMALabelRTab As System.Windows.Forms.Label
    Friend WithEvents SMABoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SASBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SASLabelRTab As System.Windows.Forms.Label
    Friend WithEvents MacauBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents MacauLabelRTab As System.Windows.Forms.Label
    Friend WithEvents SurrPaysDBJBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents SurrPaysDBJLabelRTab As System.Windows.Forms.Label
    Friend WithEvents CheckAceBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents CheckAceLabelRTab As System.Windows.Forms.Label
    Friend WithEvents CheckTenBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents CheckTenLabelRTab As System.Windows.Forms.Label
    Friend WithEvents BJSplitTensLabelRTab As System.Windows.Forms.Label
    Friend WithEvents BJSplitAcesBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents BJSplitAcesLabelRTab As System.Windows.Forms.Label
    Friend WithEvents CDTypeBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents CDTypeLabelRTab As System.Windows.Forms.Label
    Friend WithEvents PDTiesLabelRTab As System.Windows.Forms.Label
    Friend WithEvents T19LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T20LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T18LabelRTab As System.Windows.Forms.Label
    Friend WithEvents TBJLabelRTab As System.Windows.Forms.Label
    Friend WithEvents T17LabelRTab As System.Windows.Forms.Label
    Friend WithEvents BonusRulesBoxRTab As System.Windows.Forms.ListBox
    Friend WithEvents ForcedRulesBoxRTab As System.Windows.Forms.ListBox
    Friend WithEvents BonusRulesLabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedRulesLabelRTab As System.Windows.Forms.Label
    Friend WithEvents SurrTypeBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents BJSplitTensBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents T21TiesLabelRTab As System.Windows.Forms.Label
    Friend WithEvents T31LabelRTab As System.Windows.Forms.Label
    Friend WithEvents TTen1LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T41LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T51LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T21LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T61LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T71LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T81LabelRTab As System.Windows.Forms.Label
    Friend WithEvents T91LabelRTab As System.Windows.Forms.Label
    Friend WithEvents TAce1LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ShoeGroupRTab As System.Windows.Forms.GroupBox
    Friend WithEvents HardSoftTDTabSTab As System.Windows.Forms.TabPage
    Friend WithEvents HardCDTabSTab As System.Windows.Forms.TabPage
    Friend WithEvents SoftPairsCDTabSTab As System.Windows.Forms.TabPage
    Friend WithEvents StratTabControlSTab As System.Windows.Forms.TabControl
    Friend WithEvents SoftTDGroupSTab As System.Windows.Forms.GroupBox
    Friend WithEvents HardTDGroupSTab As System.Windows.Forms.GroupBox
    Friend WithEvents PairCDGroupSTab As System.Windows.Forms.GroupBox
    Friend WithEvents SoftCDGroupSTab As System.Windows.Forms.GroupBox
    Friend WithEvents StratGroupTDHardSoftTab As System.Windows.Forms.GroupBox
    Friend WithEvents CDButtonTDHardSoftTab As System.Windows.Forms.RadioButton
    Friend WithEvents TCButtonTDHardSoftTab As System.Windows.Forms.RadioButton
    Friend WithEvents ForcedButtonTDHardSoftTab As System.Windows.Forms.RadioButton
    Friend WithEvents TDButtonTDHardSoftTab As System.Windows.Forms.RadioButton
    Friend WithEvents StratGroupCDHardTab As System.Windows.Forms.GroupBox
    Friend WithEvents CDButtonCDHardTab As System.Windows.Forms.RadioButton
    Friend WithEvents TCButtonCDHardTab As System.Windows.Forms.RadioButton
    Friend WithEvents ForcedButtonCDHardTab As System.Windows.Forms.RadioButton
    Friend WithEvents TDButtonCDHardTab As System.Windows.Forms.RadioButton
    Friend WithEvents StratGroupTDSoftPairsTab As System.Windows.Forms.GroupBox
    Friend WithEvents CDButtonCDSoftPairsTab As System.Windows.Forms.RadioButton
    Friend WithEvents TCButtonCDSoftPairsTab As System.Windows.Forms.RadioButton
    Friend WithEvents ForcedButtonCDSoftPairsTab As System.Windows.Forms.RadioButton
    Friend WithEvents TDButtonCDSoftPairsTab As System.Windows.Forms.RadioButton
    Friend WithEvents SplitEVBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents SurrEVBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents SurrLabelBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents ProbLabelBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents DoubleEVBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents HitEVBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents StandEVBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents ProbBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents StandLabelBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents HitLabelBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents DoubleLabelBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents SplitLabelBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents EVsBoxSTab As System.Windows.Forms.GroupBox
    Friend WithEvents ResultsFileLabelSummTab As System.Windows.Forms.Label
    Friend WithEvents NoteLabelSTab As System.Windows.Forms.Label
    Friend WithEvents DSHardLabelRTab As System.Windows.Forms.Label
    Friend WithEvents DANLabelRTab As System.Windows.Forms.Label
    Friend WithEvents NetSuitLabelRTab As System.Windows.Forms.Label
    Friend WithEvents SpadesLabelRTab As System.Windows.Forms.Label
    Friend WithEvents ClubsLabelRTab As System.Windows.Forms.Label
    Friend WithEvents HeartsLabelRTab As System.Windows.Forms.Label
    Friend WithEvents DiamondsLabelRTab As System.Windows.Forms.Label
    Friend WithEvents NetForcedCardsLabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks3LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks10LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks4LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks5LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks2LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks6LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks7LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks8LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecks9LabelRTab As System.Windows.Forms.Label
    Friend WithEvents ForcedDecksALabelRTab As System.Windows.Forms.Label
    Friend WithEvents UC10LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UC9LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UC8LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UC7LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UC6LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UC5LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UC4LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UC3LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UC2LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents UCALabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL25LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL35LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL15LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL23LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL33LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL13LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL24LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL34LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL14LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL22LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL32LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL12LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL21LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL31LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents SPL11LabelSplitTab As System.Windows.Forms.Label
    Friend WithEvents ForcedTab As System.Windows.Forms.TabPage
    Friend WithEvents ForcedStratTabControlFSTab As System.Windows.Forms.TabControl
    Friend WithEvents OptionsTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents PairsRuleApplyLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ForcedTablePostCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents ForcedTablePreCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents ForcednCDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ForcednCDBoxFSTab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedWarningLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HardSoftTDTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents RowClickLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents PairCDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents SoftCDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents HardCDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents SoftTDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents HardTDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents SoftTDGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents ForcedTableComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents HardTDGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents HardCDTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents SoftPairsCDTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents PairCDGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents SoftCDGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents OtherTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents DeleteAllForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents RenameRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents LoadForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents UncheckAllForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents SavedForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents RestoreDefaultForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents MoveForcedRulesDownButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents MoveForcedRulesUpButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents UpdateForcedRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents DeleteForcedRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents ForcedRuleLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ForcedRuleDetailsGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents ForcedRuleStratBoxFSTab As System.Windows.Forms.TextBox
    Friend WithEvents StrategyComboBoxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents DealerUpcardLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents StrategyLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ExactMatchCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents OrLessCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents ClearForcedRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents PostSplitCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents PreSplitCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents ForcedRuleNameBoxFSTab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedRuleNameLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HandCompCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandTotalSizeCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents TotalBoxFSTab As System.Windows.Forms.TextBox
    Friend WithEvents NCardsBoxFSTab As System.Windows.Forms.TextBox
    Friend WithEvents OrMoreCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandNCardsLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents TotalLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents SoftLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents NCardsLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C3LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C10LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C4LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C5LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C2LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C6LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C7LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C8LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents C9LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents CAceLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HandSoftCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandSoftLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HandTotalLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents AddForcedRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents ForcedRulesCheckListBoxFSTab As System.Windows.Forms.CheckedListBox
    Friend WithEvents LoadForcedTablesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents SaveForcedTablesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents ClearForcedTablesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents CopyTDButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents CopyCDButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents CopyPairsButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents RecalcForcedStratButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents EORTab As System.Windows.Forms.TabPage
    Friend WithEvents OtherTab As System.Windows.Forms.TabPage
    Friend WithEvents ColorTableGroupOTab As System.Windows.Forms.GroupBox
    Friend WithEvents RestoreDefaultColorTableButtonOTab As System.Windows.Forms.Button
    Friend WithEvents SaveColorTableFileButtonOTab As System.Windows.Forms.Button
    Friend WithEvents LoadColorTableFileButtonOTab As System.Windows.Forms.Button
    Friend WithEvents SummaryEORTab As System.Windows.Forms.TabPage
    Friend WithEvents NoteLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents HATabEORTab As System.Windows.Forms.TabPage
    Friend WithEvents CalcEORsButtonEORTab As System.Windows.Forms.Button
    Friend WithEvents CDEVBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedEVBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents EORCardComboboxEORTab As System.Windows.Forms.ComboBox
    Friend WithEvents TDEVBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents EORTabControlEORTab As System.Windows.Forms.TabControl
    Friend WithEvents TDLabel1SummEORTab As System.Windows.Forms.Label
    Friend WithEvents CDLabel3SummEORTab As System.Windows.Forms.Label
    Friend WithEvents TDLabel3SummEORTab As System.Windows.Forms.Label
    Friend WithEvents ForcedLabel3SummEORTab As System.Windows.Forms.Label
    Friend WithEvents NetLabelSummEORTab As System.Windows.Forms.Label
    Friend WithEvents CDLabel1SummEORTab As System.Windows.Forms.Label
    Friend WithEvents ForcedLabel1SummEORTab As System.Windows.Forms.Label
    Friend WithEvents CardLabel2SummEORTab As System.Windows.Forms.Label
    Friend WithEvents CardLabel1SummEORTab As System.Windows.Forms.Label
    Friend WithEvents ProbLabel2SummEORTab As System.Windows.Forms.Label
    Friend WithEvents ProbLabel1SummEORTab As System.Windows.Forms.Label
    Friend WithEvents EORLabelSummEORTab As System.Windows.Forms.Label
    Friend WithEvents NetEORLabelSummEORTab As System.Windows.Forms.Label
    Friend WithEvents CDLabel2SummEORTab As System.Windows.Forms.Label
    Friend WithEvents TDLabel2SummEORTab As System.Windows.Forms.Label
    Friend WithEvents ForcedLabel2SummEORTab As System.Windows.Forms.Label
    Friend WithEvents CardRemovedLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents ExactMatchCheckEORTab As System.Windows.Forms.CheckBox
    Friend WithEvents ListSizeLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents ListSizeBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents HardOnlyCheckEORTab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftOnlyCheckEORTab As System.Windows.Forms.CheckBox
    Friend WithEvents OrLessCheckEORTab As System.Windows.Forms.CheckBox
    Friend WithEvents OrMoreCheckEORTab As System.Windows.Forms.CheckBox
    Friend WithEvents EitherCheckEORTab As System.Windows.Forms.CheckBox
    Friend WithEvents IncludesLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents HandBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents NCardLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents UCLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents SoftLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents HandListBoxEORTab As System.Windows.Forms.ListBox
    Friend WithEvents HandDetailsGroupEORTab As System.Windows.Forms.GroupBox
    Friend WithEvents Diff3DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents New3DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents Orig3DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents Diff2DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents New2DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents Orig2DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents Diff1DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents New1DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents Orig1DetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents UCDetailsBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents UCDetailsLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailsBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents SoftDetailsCheckEORTab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftDetailsLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents CDDetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents ForcedDetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents TDDetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents SplitLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents StratLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents HitLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents SurrLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents DoubleLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents StandLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents HandLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents HandNameBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents TotalDetailsLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents TotalDetailsBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents CardRemovedDetailsBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents CardRemovedDetailsLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents TotalTabEORTab As System.Windows.Forms.TabPage
    Friend WithEvents HandTypeGroupEORTab As System.Windows.Forms.GroupBox
    Friend WithEvents SoftButtonEORTab As System.Windows.Forms.RadioButton
    Friend WithEvents HardButtonEORTab As System.Windows.Forms.RadioButton
    Friend WithEvents Diff2TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents New2TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Orig2TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Diff4TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents New4TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Orig4TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Diff5TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents New5TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Orig5TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Diff3TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents New3TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Orig3TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Diff1TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents New1TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents Orig1TotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents StratTotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents HitTotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents SurrenderTotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents DoubleTotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents StandTotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents StrategyGroupEORTab As System.Windows.Forms.GroupBox
    Friend WithEvents ForcedButtonEORTab As System.Windows.Forms.RadioButton
    Friend WithEvents TDButtonEORTab As System.Windows.Forms.RadioButton
    Friend WithEvents CardRemovedTotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents UCTotalLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents UpcardComboBoxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents UCComboBoxEORTab As System.Windows.Forms.ComboBox
    Friend WithEvents UCComboBoxHATab As System.Windows.Forms.ComboBox
    Friend WithEvents ExactMatchCheckHATab As System.Windows.Forms.CheckBox
    Friend WithEvents ListSizeLabelHATab As System.Windows.Forms.Label
    Friend WithEvents ListSizeBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents HardOnlyCheckHATab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftOnlyCheckHATab As System.Windows.Forms.CheckBox
    Friend WithEvents OrLessCheckHATab As System.Windows.Forms.CheckBox
    Friend WithEvents OrMoreCheckHATab As System.Windows.Forms.CheckBox
    Friend WithEvents EitherCheckHATab As System.Windows.Forms.CheckBox
    Friend WithEvents IncludesLabelHATab As System.Windows.Forms.Label
    Friend WithEvents HandBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents NCardLabelHATab As System.Windows.Forms.Label
    Friend WithEvents UCLabelHATab As System.Windows.Forms.Label
    Friend WithEvents SoftLabelHATab As System.Windows.Forms.Label
    Friend WithEvents TotalLabelHATab As System.Windows.Forms.Label
    Friend WithEvents HandListBoxHATab As System.Windows.Forms.ListBox
    Friend WithEvents HandDetailsGroupHATab As System.Windows.Forms.GroupBox
    Friend WithEvents UCDetailsBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents UCDetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailLabelHATab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailsBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents SoftDetailsCheckHATab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftDetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents UsedLabelHATab As System.Windows.Forms.Label
    Friend WithEvents UsedTCLabelHATab As System.Windows.Forms.Label
    Friend WithEvents UsedCDLabelHATab As System.Windows.Forms.Label
    Friend WithEvents UsedForcedLabelHATab As System.Windows.Forms.Label
    Friend WithEvents UsedTDLabelHATab As System.Windows.Forms.Label
    Friend WithEvents StratTCLabelHATab As System.Windows.Forms.Label
    Friend WithEvents StratCDLabelHATab As System.Windows.Forms.Label
    Friend WithEvents StratForcedLabelHATab As System.Windows.Forms.Label
    Friend WithEvents StratTDLabelHATab As System.Windows.Forms.Label
    Friend WithEvents StandBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents DoubleBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents SurrenderBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents TDSplitBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents TCSplitBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents CDSplitBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedSplitBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedHitBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents CDHitBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents TCHitBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents TDHitBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents ProbBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents TDSplitLabelHATab As System.Windows.Forms.Label
    Friend WithEvents ForcedSplitLabelHATab As System.Windows.Forms.Label
    Friend WithEvents ForcedHitLabelHATab As System.Windows.Forms.Label
    Friend WithEvents StratLabelHATab As System.Windows.Forms.Label
    Friend WithEvents CDHitLabelHATab As System.Windows.Forms.Label
    Friend WithEvents TDHitLabelHATab As System.Windows.Forms.Label
    Friend WithEvents TCHitLabelHATab As System.Windows.Forms.Label
    Friend WithEvents SurrLabelHATab As System.Windows.Forms.Label
    Friend WithEvents TCSplitLabelHATab As System.Windows.Forms.Label
    Friend WithEvents CDSplitLabelHATab As System.Windows.Forms.Label
    Friend WithEvents DoubleLabelHATab As System.Windows.Forms.Label
    Friend WithEvents StandLabelHATab As System.Windows.Forms.Label
    Friend WithEvents ProbLabelHATab As System.Windows.Forms.Label
    Friend WithEvents HandLabelHATab As System.Windows.Forms.Label
    Friend WithEvents HandNameBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents TotalDetailsLabelHATab As System.Windows.Forms.Label
    Friend WithEvents TotalDetailsBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents HandAnalysisTab As System.Windows.Forms.TabPage
    Friend WithEvents AnalysisTab As System.Windows.Forms.TabPage
    Friend WithEvents HandSizeAnalysisTab As System.Windows.Forms.TabPage
    Friend WithEvents UCComboBoxHSATab As System.Windows.Forms.ComboBox
    Friend WithEvents Note2LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents OrLessCheckHSATab As System.Windows.Forms.CheckBox
    Friend WithEvents OrMoreCheckHSATab As System.Windows.Forms.CheckBox
    Friend WithEvents SplitEVBoxHSATab As System.Windows.Forms.TextBox
    Friend WithEvents SplitEVLabelHSATab As System.Windows.Forms.Label
    Friend WithEvents NoteLabelHSATab As System.Windows.Forms.Label
    Friend WithEvents HandUsedCheckHSATab As System.Windows.Forms.CheckBox
    Friend WithEvents StrategyGroupHSATab As System.Windows.Forms.GroupBox
    Friend WithEvents CDButtonHSATab As System.Windows.Forms.RadioButton
    Friend WithEvents TCButtonHSATab As System.Windows.Forms.RadioButton
    Friend WithEvents ForcedButtonHSATab As System.Windows.Forms.RadioButton
    Friend WithEvents TDButtonHSATab As System.Windows.Forms.RadioButton
    Friend WithEvents NCards2LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents SEV2LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents HEV2LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents DEV2LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents Strat2LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents SurrEV2LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents SEV1LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents HEV1LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents DEV1LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents Strat1LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents SurrEV1LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C18LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C19LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C20LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C11LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C12LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C13LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C14LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C15LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C16LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C17LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C10LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents NCards1LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents AnyLabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C3LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C4LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C5LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C6LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C7LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C8LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C9LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents C2LabelHSATab As System.Windows.Forms.Label
    Friend WithEvents UCLabelHSATab As System.Windows.Forms.Label
    Friend WithEvents SoftCheckHSATab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftLabelHSATab As System.Windows.Forms.Label
    Friend WithEvents TotalLabelHSATab As System.Windows.Forms.Label
    Friend WithEvents UCComboBoxETab As System.Windows.Forms.ComboBox
    Friend WithEvents PostSplitCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents PreSplitCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents ETypeLabelETab As System.Windows.Forms.Label
    Friend WithEvents ExTypeComboBoxETab As System.Windows.Forms.ComboBox
    Friend WithEvents ExactMatchCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents ListSizeLabelETab As System.Windows.Forms.Label
    Friend WithEvents ListSizeBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents HardOnlyCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftOnlyCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents OrLessCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents OrMoreCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents EitherCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents IncludesLabelETab As System.Windows.Forms.Label
    Friend WithEvents HandBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents NCardLabelETab As System.Windows.Forms.Label
    Friend WithEvents UCLabelETab As System.Windows.Forms.Label
    Friend WithEvents SoftLabelETab As System.Windows.Forms.Label
    Friend WithEvents TotalLabelETab As System.Windows.Forms.Label
    Friend WithEvents HandListBoxETab As System.Windows.Forms.ListBox
    Friend WithEvents PaircardBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents PaircardLabelETab As System.Windows.Forms.Label
    Friend WithEvents ExSurrenderBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ExStandBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ExDoubleBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents StateBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ExTypeBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ExNameBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseNameBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ExStratDetailsLabelETab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailLabelETab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailsBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents SoftDetailsCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftDetailsLabelETab As System.Windows.Forms.Label
    Friend WithEvents UsedLabelETab As System.Windows.Forms.Label
    Friend WithEvents StratNameDetailsLabelETab As System.Windows.Forms.Label
    Friend WithEvents StratLabelETab As System.Windows.Forms.Label
    Friend WithEvents ExStratETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseStandBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseDoubleBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseSurrenderBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseSplitBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ExSplitBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ExHitBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseHitBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ProbBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseStratETab As System.Windows.Forms.TextBox
    Friend WithEvents SplitLabelETab As System.Windows.Forms.Label
    Friend WithEvents BaseStratDetailsLabelETab As System.Windows.Forms.Label
    Friend WithEvents HitLabelETab As System.Windows.Forms.Label
    Friend WithEvents StateLabelETab As System.Windows.Forms.Label
    Friend WithEvents SurrLabelETab As System.Windows.Forms.Label
    Friend WithEvents DoubleLabelETab As System.Windows.Forms.Label
    Friend WithEvents StandLabelETab As System.Windows.Forms.Label
    Friend WithEvents ProbLabelETab As System.Windows.Forms.Label
    Friend WithEvents HandLabelETab As System.Windows.Forms.Label
    Friend WithEvents HandNameBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents TotalDetailsLabelETab As System.Windows.Forms.Label
    Friend WithEvents TotalDetailsBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents UCDetailsBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents UCDetailsLabelETab As System.Windows.Forms.Label
    Friend WithEvents AnalysisTabControl As System.Windows.Forms.TabControl
    Friend WithEvents AllExceptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents ExceptionsTabControl As System.Windows.Forms.TabControl
    Friend WithEvents ExceptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents NCardExceptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents ExceptionDetailsGroupETab As System.Windows.Forms.GroupBox
    Friend WithEvents ExTypeLabelETab As System.Windows.Forms.Label
    Friend WithEvents NoteLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents UCComboBoxNCETab As System.Windows.Forms.ComboBox
    Friend WithEvents ETypeLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents ExTypeComboBoxNCETab As System.Windows.Forms.ComboBox
    Friend WithEvents ListSizeLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents HardOnlyCheckNCETab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftOnlyCheckNCETab As System.Windows.Forms.CheckBox
    Friend WithEvents EitherCheckNCETab As System.Windows.Forms.CheckBox
    Friend WithEvents NCardLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents UCLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents SoftLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents TotalLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents HandListBoxNCETab As System.Windows.Forms.ListBox
    Friend WithEvents ExceptionDetailsGroupNCETab As System.Windows.Forms.GroupBox
    Friend WithEvents ExRuleNameBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents ExRuleNameLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents ExTypeBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents ExNameBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseNameBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents ExStratDetailsLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailsBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents SoftDetailsCheckNCETab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftDetailsLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents StratNameDetailsLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents StratLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents ExStratNCETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseStandBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseStratNCETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseStratDetailsLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents ExTypeLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents StandLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents TotalDetailsLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents TotalDetailsBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents UCDetailsBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents UCDetailsLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents ExForcedButtonNCETab As System.Windows.Forms.Button
    Friend WithEvents ExSurrenderBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents ExStandBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents ExDoubleBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseDoubleBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseSurrenderBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents ExHitBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents BaseHitBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents HitLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents SurrLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents DoubleLabelNCETab As System.Windows.Forms.Label
    Friend WithEvents BaseHandUsedCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents ExHandUsedCheckETab As System.Windows.Forms.CheckBox
    Friend WithEvents ListSizeBoxNCETab As System.Windows.Forms.TextBox
    Friend WithEvents ExRuleNameBoxETab As System.Windows.Forms.TextBox
    Friend WithEvents ExRuleNameLabelETab As System.Windows.Forms.Label
    Friend WithEvents ExForcedButtonETab As System.Windows.Forms.Button
    Friend WithEvents BJStandBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents BJStandLabelHATab As System.Windows.Forms.Label
    Friend WithEvents BJStandEVBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents BJStandLabelBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents UCTotalComboBoxEORTab As System.Windows.Forms.ComboBox
    Friend WithEvents CardRemovedTotalComboBoxEORTab As System.Windows.Forms.ComboBox
    Friend WithEvents CardRemovedComboBoxEORTab As System.Windows.Forms.ComboBox
    Friend WithEvents SuitedTabSTab As System.Windows.Forms.TabPage
    Friend WithEvents DiamondsLabelSTab As System.Windows.Forms.Label
    Friend WithEvents ClubsLabelSTab As System.Windows.Forms.Label
    Friend WithEvents HeartsLabelSTab As System.Windows.Forms.Label
    Friend WithEvents SpadesLabelSTab As System.Windows.Forms.Label
    Friend WithEvents SplitLabelSTab As System.Windows.Forms.Label
    Friend WithEvents HitLabelSTab As System.Windows.Forms.Label
    Friend WithEvents UCComboBoxSTab As System.Windows.Forms.ComboBox
    Friend WithEvents ListSizeLabelSTab As System.Windows.Forms.Label
    Friend WithEvents ListSizeBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents UpcardLabelSTab As System.Windows.Forms.Label
    Friend WithEvents HandListBoxSTab As System.Windows.Forms.ListBox
    Friend WithEvents HandDetailsGroupSTab As System.Windows.Forms.GroupBox
    Friend WithEvents BJStandLabelSTab As System.Windows.Forms.Label
    Friend WithEvents UCDetailsBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents UCDetailsLabelSTab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailLabelSTab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailsBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents SoftDetailsCheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftDetailsLabelSTab As System.Windows.Forms.Label
    Friend WithEvents CDLabelSTab As System.Windows.Forms.Label
    Friend WithEvents StratLabelSTab As System.Windows.Forms.Label
    Friend WithEvents SurrLabelSTab As System.Windows.Forms.Label
    Friend WithEvents DoubleLabelSTab As System.Windows.Forms.Label
    Friend WithEvents StandLabelSTab As System.Windows.Forms.Label
    Friend WithEvents ProbLabelSTab As System.Windows.Forms.Label
    Friend WithEvents HandLabelSTab As System.Windows.Forms.Label
    Friend WithEvents HandNameBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents TotalDetailsLabelSTab As System.Windows.Forms.Label
    Friend WithEvents TotalDetailsBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents BJStandBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents StandBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents DoubleBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents SurrenderBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents CDSplitBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents CDHitBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents NetEVBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents NetEVLabelSTab As System.Windows.Forms.Label
    Friend WithEvents CDStratBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents NonSuitedProbDetailsBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents NetProbLabelSTab As System.Windows.Forms.Label
    Friend WithEvents ProbDetailsBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents NetBJEVLabelSTab As System.Windows.Forms.Label
    Friend WithEvents NetBJEVBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents RDABoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents RDALabelRTab As System.Windows.Forms.Label
    Friend WithEvents DDRBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents DDRLabelRTab As System.Windows.Forms.Label
    Friend WithEvents DoubleAnalysisTab As System.Windows.Forms.TabPage
    Friend WithEvents UCComboBoxDATab As System.Windows.Forms.ComboBox
    Friend WithEvents ExactMatchCheckDATab As System.Windows.Forms.CheckBox
    Friend WithEvents HardOnlyCheckDATab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftOnlyCheckDATab As System.Windows.Forms.CheckBox
    Friend WithEvents OrLessCheckDATab As System.Windows.Forms.CheckBox
    Friend WithEvents OrMoreCheckDATab As System.Windows.Forms.CheckBox
    Friend WithEvents EitherCheckDATab As System.Windows.Forms.CheckBox
    Friend WithEvents IncludesLabelDATab As System.Windows.Forms.Label
    Friend WithEvents HandBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents NCardLabelDATab As System.Windows.Forms.Label
    Friend WithEvents UCLabelDATab As System.Windows.Forms.Label
    Friend WithEvents SoftLabelDATab As System.Windows.Forms.Label
    Friend WithEvents TotalLabelDATab As System.Windows.Forms.Label
    Friend WithEvents HandListBoxDATab As System.Windows.Forms.ListBox
    Friend WithEvents HandDetailsGroupDATab As System.Windows.Forms.GroupBox
    Friend WithEvents SoftDetailsCheckDATab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftDetailsLabelDATab As System.Windows.Forms.Label
    Friend WithEvents HandLabelDATab As System.Windows.Forms.Label
    Friend WithEvents HandNameBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents TotalDetailsLabelDATab As System.Windows.Forms.Label
    Friend WithEvents TotalDetailsBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents TCStratBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents CDStratBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedStratBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents TDStratBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents CardStratLabelDATab As System.Windows.Forms.Label
    Friend WithEvents NextCardLabelDATab As System.Windows.Forms.Label
    Friend WithEvents BreakdownLabelDATab As System.Windows.Forms.Label
    Friend WithEvents PostDoubleLabelDATab As System.Windows.Forms.Label
    Friend WithEvents UCDetailsBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents UCDetailsLabelDATab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailLabelDATab As System.Windows.Forms.Label
    Friend WithEvents NCardsDetailsBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents StandBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents DoubleBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents SurrenderBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents StratBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents StratLabelDATab As System.Windows.Forms.Label
    Friend WithEvents SurrLabelDATab As System.Windows.Forms.Label
    Friend WithEvents DoubleLabelDATab As System.Windows.Forms.Label
    Friend WithEvents StandLabelDATab As System.Windows.Forms.Label
    Friend WithEvents CardProbLabelDATab As System.Windows.Forms.Label
    Friend WithEvents CardStratEVLabelDATab As System.Windows.Forms.Label
    Friend WithEvents C3LabelDATab As System.Windows.Forms.Label
    Friend WithEvents CTLabelDATab As System.Windows.Forms.Label
    Friend WithEvents C4LabelDATab As System.Windows.Forms.Label
    Friend WithEvents C5LabelDATab As System.Windows.Forms.Label
    Friend WithEvents C2LabelDATab As System.Windows.Forms.Label
    Friend WithEvents C6LabelDATab As System.Windows.Forms.Label
    Friend WithEvents C7LabelDATab As System.Windows.Forms.Label
    Friend WithEvents C8LabelDATab As System.Windows.Forms.Label
    Friend WithEvents C9LabelDATab As System.Windows.Forms.Label
    Friend WithEvents CALabelDATab As System.Windows.Forms.Label
    Friend WithEvents DoubleBox2DATab As System.Windows.Forms.TextBox
    Friend WithEvents DoubleLabel2DATab As System.Windows.Forms.Label
    Friend WithEvents ListSizeLabelDATab As System.Windows.Forms.Label
    Friend WithEvents ListSizeBoxDATab As System.Windows.Forms.TextBox
    Friend WithEvents DAllowedLabelDATab As System.Windows.Forms.Label
    Friend WithEvents DAllowedCheckDATab As System.Windows.Forms.CheckBox
    Friend WithEvents ExAllForcedButtonNCETab As System.Windows.Forms.Button
    Friend WithEvents TotalComboBoxHSATab As System.Windows.Forms.ComboBox
    Friend WithEvents NCardsComboBoxHATab As System.Windows.Forms.ComboBox
    Friend WithEvents TotalComboBoxHATab As System.Windows.Forms.ComboBox
    Friend WithEvents TotalComboBoxDATab As System.Windows.Forms.ComboBox
    Friend WithEvents NCardsComboBoxDATab As System.Windows.Forms.ComboBox
    Friend WithEvents NCardsComboBoxETab As System.Windows.Forms.ComboBox
    Friend WithEvents TotalComboBoxETab As System.Windows.Forms.ComboBox
    Friend WithEvents TotalComboBoxNCETab As System.Windows.Forms.ComboBox
    Friend WithEvents NCardsComboBoxNCETab As System.Windows.Forms.ComboBox
    Friend WithEvents HandTotalComboBoxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents HandNCardsComboBoxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents TotalComboBoxEORTab As System.Windows.Forms.ComboBox
    Friend WithEvents NCardsComboBoxEORTab As System.Windows.Forms.ComboBox
    Friend WithEvents BonusBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents BonusLabelHATab As System.Windows.Forms.Label
    Friend WithEvents BonusBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents BonusLabelSTab As System.Windows.Forms.Label
    Friend WithEvents TDMultiplierBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents TCMultiplierBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents CDMultiplierBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedMultiplierBoxHATab As System.Windows.Forms.TextBox
    Friend WithEvents CalcNCardStratButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents ClearEORsButtonEORTab As System.Windows.Forms.Button
    Friend WithEvents NetLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents ForcedPreCheckBoxHATab As System.Windows.Forms.CheckBox
    Friend WithEvents ForcedPostCheckBoxHATab As System.Windows.Forms.CheckBox
    Friend WithEvents ForcedPreLabelHATab As System.Windows.Forms.Label
    Friend WithEvents ForcedPostLabelHATab As System.Windows.Forms.Label
    Friend WithEvents ChangedOnlyCheckBoxEORTab As System.Windows.Forms.CheckBox
    Friend WithEvents DDRTypeBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents P21AutowinBoxRTab As System.Windows.Forms.TextBox
    Friend WithEvents P21AutowinLabelRTab As System.Windows.Forms.Label
    Friend WithEvents NCardEORBoxEORTab As System.Windows.Forms.TextBox
    Friend WithEvents NCardEORLabelEORTab As System.Windows.Forms.Label
    Friend WithEvents TextButtonSummTab As System.Windows.Forms.Button
    Friend WithEvents HardCDHand2LabelSTab As System.Windows.Forms.Label
    Friend WithEvents HardCDHand1LabelSTab As System.Windows.Forms.Label
    Friend WithEvents TextButton1SummTab As System.Windows.Forms.Button
    Friend WithEvents TextButton2SummTab As System.Windows.Forms.Button
    Friend WithEvents TextButton3STab As System.Windows.Forms.Button
    Friend WithEvents TextButtonEORTab As System.Windows.Forms.Button
    Friend WithEvents HardCDHand2LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HardCDHand1LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents SoftTotalTDLabelSTab As System.Windows.Forms.Label
    Friend WithEvents HardTotalTDLabelSTab As System.Windows.Forms.Label
    Friend WithEvents PairForcedCDLabelSTab As System.Windows.Forms.Label
    Friend WithEvents TotalForcedCDLabelSTab As System.Windows.Forms.Label
    Friend WithEvents SoftTotalTDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HardTotalTDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents PairForcedCDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents TotalForcedCDLabelFSTab As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(BJCAResultsForm))
Me.ResultsFormTabControl = New System.Windows.Forms.TabControl
Me.SummaryTab = New System.Windows.Forms.TabPage
Me.TextButtonSummTab = New System.Windows.Forms.Button
Me.ResultsFileLabelSummTab = New System.Windows.Forms.Label
Me.ResultsFilenameBoxSummTab = New System.Windows.Forms.TextBox
Me.NetEVLabelSummTab = New System.Windows.Forms.Label
Me.TCStrat1LabelSummTab = New System.Windows.Forms.Label
Me.CDStrat1LabelSummTab = New System.Windows.Forms.Label
Me.ForcedStrat1LabelSummTab = New System.Windows.Forms.Label
Me.TDStrat1LabelSummTab = New System.Windows.Forms.Label
Me.Prob2LabelSummTab = New System.Windows.Forms.Label
Me.TCStrat3LabelSummTab = New System.Windows.Forms.Label
Me.CDStrat3LabelSummTab = New System.Windows.Forms.Label
Me.ForcedStrat3LabelSummTab = New System.Windows.Forms.Label
Me.TDStrat3LabelSummTab = New System.Windows.Forms.Label
Me.Prob1LabelSummTab = New System.Windows.Forms.Label
Me.PlayerLabelSummTab = New System.Windows.Forms.Label
Me.DealerUCLabelSummTab = New System.Windows.Forms.Label
Me.TCStrat2LabelSummTab = New System.Windows.Forms.Label
Me.CDStrat2LabelSummTab = New System.Windows.Forms.Label
Me.ForcedStrat2LabelSummTab = New System.Windows.Forms.Label
Me.TDStrat2LabelSummTab = New System.Windows.Forms.Label
Me.TCEVBoxSummTab = New System.Windows.Forms.TextBox
Me.CDEVBoxSummTab = New System.Windows.Forms.TextBox
Me.ForcedEVBoxSummTab = New System.Windows.Forms.TextBox
Me.TDEVBoxSummTab = New System.Windows.Forms.TextBox
Me.LoadResultsButtonSummTab = New System.Windows.Forms.Button
Me.PrintToExelButtonSummTab = New System.Windows.Forms.Button
Me.SaveResultsButtonSummTab = New System.Windows.Forms.Button
Me.StratTab = New System.Windows.Forms.TabPage
Me.StratTabControlSTab = New System.Windows.Forms.TabControl
Me.HardSoftTDTabSTab = New System.Windows.Forms.TabPage
Me.TextButton1SummTab = New System.Windows.Forms.Button
Me.NoteLabelSTab = New System.Windows.Forms.Label
Me.StratGroupTDHardSoftTab = New System.Windows.Forms.GroupBox
Me.CDButtonTDHardSoftTab = New System.Windows.Forms.RadioButton
Me.TCButtonTDHardSoftTab = New System.Windows.Forms.RadioButton
Me.ForcedButtonTDHardSoftTab = New System.Windows.Forms.RadioButton
Me.TDButtonTDHardSoftTab = New System.Windows.Forms.RadioButton
Me.SoftTDGroupSTab = New System.Windows.Forms.GroupBox
Me.SoftTotalTDLabelSTab = New System.Windows.Forms.Label
Me.HardTDGroupSTab = New System.Windows.Forms.GroupBox
Me.HardTotalTDLabelSTab = New System.Windows.Forms.Label
Me.SoftPairsCDTabSTab = New System.Windows.Forms.TabPage
Me.TextButton2SummTab = New System.Windows.Forms.Button
Me.EVsBoxSTab = New System.Windows.Forms.GroupBox
Me.BJStandEVBoxSTab = New System.Windows.Forms.TextBox
Me.BJStandLabelBoxSTab = New System.Windows.Forms.TextBox
Me.SplitEVBoxSTab = New System.Windows.Forms.TextBox
Me.SurrEVBoxSTab = New System.Windows.Forms.TextBox
Me.SurrLabelBoxSTab = New System.Windows.Forms.TextBox
Me.ProbLabelBoxSTab = New System.Windows.Forms.TextBox
Me.DoubleEVBoxSTab = New System.Windows.Forms.TextBox
Me.HitEVBoxSTab = New System.Windows.Forms.TextBox
Me.StandEVBoxSTab = New System.Windows.Forms.TextBox
Me.ProbBoxSTab = New System.Windows.Forms.TextBox
Me.StandLabelBoxSTab = New System.Windows.Forms.TextBox
Me.HitLabelBoxSTab = New System.Windows.Forms.TextBox
Me.DoubleLabelBoxSTab = New System.Windows.Forms.TextBox
Me.SplitLabelBoxSTab = New System.Windows.Forms.TextBox
Me.StratGroupTDSoftPairsTab = New System.Windows.Forms.GroupBox
Me.CDButtonCDSoftPairsTab = New System.Windows.Forms.RadioButton
Me.TCButtonCDSoftPairsTab = New System.Windows.Forms.RadioButton
Me.ForcedButtonCDSoftPairsTab = New System.Windows.Forms.RadioButton
Me.TDButtonCDSoftPairsTab = New System.Windows.Forms.RadioButton
Me.PairCDGroupSTab = New System.Windows.Forms.GroupBox
Me.PairForcedCDLabelSTab = New System.Windows.Forms.Label
Me.SoftCDGroupSTab = New System.Windows.Forms.GroupBox
Me.TotalForcedCDLabelSTab = New System.Windows.Forms.Label
Me.HardCDTabSTab = New System.Windows.Forms.TabPage
Me.TextButton3STab = New System.Windows.Forms.Button
Me.StratGroupCDHardTab = New System.Windows.Forms.GroupBox
Me.CDButtonCDHardTab = New System.Windows.Forms.RadioButton
Me.TCButtonCDHardTab = New System.Windows.Forms.RadioButton
Me.ForcedButtonCDHardTab = New System.Windows.Forms.RadioButton
Me.TDButtonCDHardTab = New System.Windows.Forms.RadioButton
Me.HardCDHand2LabelSTab = New System.Windows.Forms.Label
Me.HardCDHand1LabelSTab = New System.Windows.Forms.Label
Me.SuitedTabSTab = New System.Windows.Forms.TabPage
Me.UCComboBoxSTab = New System.Windows.Forms.ComboBox
Me.ListSizeLabelSTab = New System.Windows.Forms.Label
Me.ListSizeBoxSTab = New System.Windows.Forms.TextBox
Me.UpcardLabelSTab = New System.Windows.Forms.Label
Me.HandListBoxSTab = New System.Windows.Forms.ListBox
Me.HandDetailsGroupSTab = New System.Windows.Forms.GroupBox
Me.BonusBoxSTab = New System.Windows.Forms.TextBox
Me.BonusLabelSTab = New System.Windows.Forms.Label
Me.NetBJEVLabelSTab = New System.Windows.Forms.Label
Me.NetBJEVBoxSTab = New System.Windows.Forms.TextBox
Me.NetProbLabelSTab = New System.Windows.Forms.Label
Me.ProbDetailsBoxSTab = New System.Windows.Forms.TextBox
Me.NetEVLabelSTab = New System.Windows.Forms.Label
Me.NetEVBoxSTab = New System.Windows.Forms.TextBox
Me.SpadesLabelSTab = New System.Windows.Forms.Label
Me.BJStandBoxSTab = New System.Windows.Forms.TextBox
Me.BJStandLabelSTab = New System.Windows.Forms.Label
Me.UCDetailsBoxSTab = New System.Windows.Forms.TextBox
Me.UCDetailsLabelSTab = New System.Windows.Forms.Label
Me.NCardsDetailLabelSTab = New System.Windows.Forms.Label
Me.NCardsDetailsBoxSTab = New System.Windows.Forms.TextBox
Me.SoftDetailsCheckSTab = New System.Windows.Forms.CheckBox
Me.SoftDetailsLabelSTab = New System.Windows.Forms.Label
Me.DiamondsLabelSTab = New System.Windows.Forms.Label
Me.CDLabelSTab = New System.Windows.Forms.Label
Me.ClubsLabelSTab = New System.Windows.Forms.Label
Me.HeartsLabelSTab = New System.Windows.Forms.Label
Me.CDStratBoxSTab = New System.Windows.Forms.TextBox
Me.StandBoxSTab = New System.Windows.Forms.TextBox
Me.DoubleBoxSTab = New System.Windows.Forms.TextBox
Me.SurrenderBoxSTab = New System.Windows.Forms.TextBox
Me.CDSplitBoxSTab = New System.Windows.Forms.TextBox
Me.CDHitBoxSTab = New System.Windows.Forms.TextBox
Me.NonSuitedProbDetailsBoxSTab = New System.Windows.Forms.TextBox
Me.SplitLabelSTab = New System.Windows.Forms.Label
Me.StratLabelSTab = New System.Windows.Forms.Label
Me.HitLabelSTab = New System.Windows.Forms.Label
Me.SurrLabelSTab = New System.Windows.Forms.Label
Me.DoubleLabelSTab = New System.Windows.Forms.Label
Me.StandLabelSTab = New System.Windows.Forms.Label
Me.ProbLabelSTab = New System.Windows.Forms.Label
Me.HandLabelSTab = New System.Windows.Forms.Label
Me.HandNameBoxSTab = New System.Windows.Forms.TextBox
Me.TotalDetailsLabelSTab = New System.Windows.Forms.Label
Me.TotalDetailsBoxSTab = New System.Windows.Forms.TextBox
Me.RulesTab = New System.Windows.Forms.TabPage
Me.P21AutowinBoxRTab = New System.Windows.Forms.TextBox
Me.P21AutowinLabelRTab = New System.Windows.Forms.Label
Me.DDRTypeBoxRTab = New System.Windows.Forms.TextBox
Me.DDRBoxRTab = New System.Windows.Forms.TextBox
Me.DDRLabelRTab = New System.Windows.Forms.Label
Me.RDABoxRTab = New System.Windows.Forms.TextBox
Me.RDALabelRTab = New System.Windows.Forms.Label
Me.ForcedRulesLabelRTab = New System.Windows.Forms.Label
Me.BonusRulesLabelRTab = New System.Windows.Forms.Label
Me.ForcedRulesBoxRTab = New System.Windows.Forms.ListBox
Me.BonusRulesBoxRTab = New System.Windows.Forms.ListBox
Me.T31LabelRTab = New System.Windows.Forms.Label
Me.TTen1LabelRTab = New System.Windows.Forms.Label
Me.T41LabelRTab = New System.Windows.Forms.Label
Me.T51LabelRTab = New System.Windows.Forms.Label
Me.T21LabelRTab = New System.Windows.Forms.Label
Me.T61LabelRTab = New System.Windows.Forms.Label
Me.T71LabelRTab = New System.Windows.Forms.Label
Me.T81LabelRTab = New System.Windows.Forms.Label
Me.T91LabelRTab = New System.Windows.Forms.Label
Me.TAce1LabelRTab = New System.Windows.Forms.Label
Me.PDTiesLabelRTab = New System.Windows.Forms.Label
Me.T19LabelRTab = New System.Windows.Forms.Label
Me.T20LabelRTab = New System.Windows.Forms.Label
Me.T21TiesLabelRTab = New System.Windows.Forms.Label
Me.T18LabelRTab = New System.Windows.Forms.Label
Me.TBJLabelRTab = New System.Windows.Forms.Label
Me.T17LabelRTab = New System.Windows.Forms.Label
Me.CheckAceBoxRTab = New System.Windows.Forms.TextBox
Me.CheckAceLabelRTab = New System.Windows.Forms.Label
Me.CheckTenBoxRTab = New System.Windows.Forms.TextBox
Me.CheckTenLabelRTab = New System.Windows.Forms.Label
Me.BJSplitTensBoxRTab = New System.Windows.Forms.TextBox
Me.BJSplitTensLabelRTab = New System.Windows.Forms.Label
Me.BJSplitAcesBoxRTab = New System.Windows.Forms.TextBox
Me.BJSplitAcesLabelRTab = New System.Windows.Forms.Label
Me.CDTypeBoxRTab = New System.Windows.Forms.TextBox
Me.CDTypeLabelRTab = New System.Windows.Forms.Label
Me.SASBoxRTab = New System.Windows.Forms.TextBox
Me.SASLabelRTab = New System.Windows.Forms.Label
Me.MacauBoxRTab = New System.Windows.Forms.TextBox
Me.MacauLabelRTab = New System.Windows.Forms.Label
Me.SurrPaysDBJBoxRTab = New System.Windows.Forms.TextBox
Me.SurrPaysDBJLabelRTab = New System.Windows.Forms.Label
Me.SurrPaysBoxRTab = New System.Windows.Forms.TextBox
Me.SurrPaysLabelRTab = New System.Windows.Forms.Label
Me.DSoftHardBoxRTab = New System.Windows.Forms.TextBox
Me.DSHardLabelRTab = New System.Windows.Forms.Label
Me.DANBoxRTab = New System.Windows.Forms.TextBox
Me.DANLabelRTab = New System.Windows.Forms.Label
Me.SSABoxRTab = New System.Windows.Forms.TextBox
Me.SSARLabelTab = New System.Windows.Forms.Label
Me.DSABoxRTab = New System.Windows.Forms.TextBox
Me.DSARLabelTab = New System.Windows.Forms.Label
Me.HSABoxRTab = New System.Windows.Forms.TextBox
Me.SANBoxRTab = New System.Windows.Forms.TextBox
Me.SANLabelRTab = New System.Windows.Forms.Label
Me.HSARLabelTab = New System.Windows.Forms.Label
Me.SMALabelRTab = New System.Windows.Forms.Label
Me.SMABoxRTab = New System.Windows.Forms.TextBox
Me.SPLBoxRTab = New System.Windows.Forms.TextBox
Me.SPLRLabelTab = New System.Windows.Forms.Label
Me.SurrTypeBoxRTab = New System.Windows.Forms.TextBox
Me.SurrTypeLabelRTab = New System.Windows.Forms.Label
Me.DASBoxRTab = New System.Windows.Forms.TextBox
Me.DASLabelRTab = New System.Windows.Forms.Label
Me.DTypeBoxRTab = New System.Windows.Forms.TextBox
Me.DTypeLabelRTab = New System.Windows.Forms.Label
Me.BJRuleBoxRTab = New System.Windows.Forms.TextBox
Me.DBJRuleLabelRTab = New System.Windows.Forms.Label
Me.StandsOnSoftBoxRTab = New System.Windows.Forms.TextBox
Me.BJPaysBoxRTab = New System.Windows.Forms.TextBox
Me.BJPaysLabelRTab = New System.Windows.Forms.Label
Me.StandsOnSoftLabelRTab = New System.Windows.Forms.Label
Me.NDecksLabelRTab = New System.Windows.Forms.Label
Me.NDecksBoxRTab = New System.Windows.Forms.TextBox
Me.SplitAllowedLabelRTab = New System.Windows.Forms.Label
Me.ShoeGroupRTab = New System.Windows.Forms.GroupBox
Me.NetSuitLabelRTab = New System.Windows.Forms.Label
Me.SpadesLabelRTab = New System.Windows.Forms.Label
Me.ClubsLabelRTab = New System.Windows.Forms.Label
Me.HeartsLabelRTab = New System.Windows.Forms.Label
Me.DiamondsLabelRTab = New System.Windows.Forms.Label
Me.NetForcedCardsLabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks3LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks10LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks4LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks5LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks2LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks6LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks7LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks8LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecks9LabelRTab = New System.Windows.Forms.Label
Me.ForcedDecksALabelRTab = New System.Windows.Forms.Label
Me.AnalysisTab = New System.Windows.Forms.TabPage
Me.AnalysisTabControl = New System.Windows.Forms.TabControl
Me.HandAnalysisTab = New System.Windows.Forms.TabPage
Me.NCardsComboBoxHATab = New System.Windows.Forms.ComboBox
Me.TotalComboBoxHATab = New System.Windows.Forms.ComboBox
Me.UCComboBoxHATab = New System.Windows.Forms.ComboBox
Me.ExactMatchCheckHATab = New System.Windows.Forms.CheckBox
Me.ListSizeLabelHATab = New System.Windows.Forms.Label
Me.ListSizeBoxHATab = New System.Windows.Forms.TextBox
Me.HardOnlyCheckHATab = New System.Windows.Forms.CheckBox
Me.SoftOnlyCheckHATab = New System.Windows.Forms.CheckBox
Me.OrLessCheckHATab = New System.Windows.Forms.CheckBox
Me.OrMoreCheckHATab = New System.Windows.Forms.CheckBox
Me.EitherCheckHATab = New System.Windows.Forms.CheckBox
Me.IncludesLabelHATab = New System.Windows.Forms.Label
Me.HandBoxHATab = New System.Windows.Forms.TextBox
Me.NCardLabelHATab = New System.Windows.Forms.Label
Me.UCLabelHATab = New System.Windows.Forms.Label
Me.SoftLabelHATab = New System.Windows.Forms.Label
Me.TotalLabelHATab = New System.Windows.Forms.Label
Me.HandListBoxHATab = New System.Windows.Forms.ListBox
Me.HandDetailsGroupHATab = New System.Windows.Forms.GroupBox
Me.ForcedPostLabelHATab = New System.Windows.Forms.Label
Me.ForcedPreLabelHATab = New System.Windows.Forms.Label
Me.ForcedPostCheckBoxHATab = New System.Windows.Forms.CheckBox
Me.ForcedPreCheckBoxHATab = New System.Windows.Forms.CheckBox
Me.ForcedMultiplierBoxHATab = New System.Windows.Forms.TextBox
Me.CDMultiplierBoxHATab = New System.Windows.Forms.TextBox
Me.TCMultiplierBoxHATab = New System.Windows.Forms.TextBox
Me.TDMultiplierBoxHATab = New System.Windows.Forms.TextBox
Me.BonusBoxHATab = New System.Windows.Forms.TextBox
Me.BonusLabelHATab = New System.Windows.Forms.Label
Me.BJStandBoxHATab = New System.Windows.Forms.TextBox
Me.BJStandLabelHATab = New System.Windows.Forms.Label
Me.UCDetailsBoxHATab = New System.Windows.Forms.TextBox
Me.UCDetailsLabelHATab = New System.Windows.Forms.Label
Me.NCardsDetailLabelHATab = New System.Windows.Forms.Label
Me.NCardsDetailsBoxHATab = New System.Windows.Forms.TextBox
Me.SoftDetailsCheckHATab = New System.Windows.Forms.CheckBox
Me.SoftDetailsLabelHATab = New System.Windows.Forms.Label
Me.UsedLabelHATab = New System.Windows.Forms.Label
Me.UsedTCLabelHATab = New System.Windows.Forms.Label
Me.UsedCDLabelHATab = New System.Windows.Forms.Label
Me.UsedForcedLabelHATab = New System.Windows.Forms.Label
Me.UsedTDLabelHATab = New System.Windows.Forms.Label
Me.StratTCLabelHATab = New System.Windows.Forms.Label
Me.StratCDLabelHATab = New System.Windows.Forms.Label
Me.StratForcedLabelHATab = New System.Windows.Forms.Label
Me.StratTDLabelHATab = New System.Windows.Forms.Label
Me.TCStratBoxHATab = New System.Windows.Forms.TextBox
Me.CDStratBoxHATab = New System.Windows.Forms.TextBox
Me.ForcedStratBoxHATab = New System.Windows.Forms.TextBox
Me.StandBoxHATab = New System.Windows.Forms.TextBox
Me.DoubleBoxHATab = New System.Windows.Forms.TextBox
Me.SurrenderBoxHATab = New System.Windows.Forms.TextBox
Me.TDSplitBoxHATab = New System.Windows.Forms.TextBox
Me.TCSplitBoxHATab = New System.Windows.Forms.TextBox
Me.CDSplitBoxHATab = New System.Windows.Forms.TextBox
Me.ForcedSplitBoxHATab = New System.Windows.Forms.TextBox
Me.ForcedHitBoxHATab = New System.Windows.Forms.TextBox
Me.CDHitBoxHATab = New System.Windows.Forms.TextBox
Me.TCHitBoxHATab = New System.Windows.Forms.TextBox
Me.TDHitBoxHATab = New System.Windows.Forms.TextBox
Me.ProbBoxHATab = New System.Windows.Forms.TextBox
Me.TDStratBoxHATab = New System.Windows.Forms.TextBox
Me.TDSplitLabelHATab = New System.Windows.Forms.Label
Me.ForcedSplitLabelHATab = New System.Windows.Forms.Label
Me.ForcedHitLabelHATab = New System.Windows.Forms.Label
Me.StratLabelHATab = New System.Windows.Forms.Label
Me.CDHitLabelHATab = New System.Windows.Forms.Label
Me.TDHitLabelHATab = New System.Windows.Forms.Label
Me.TCHitLabelHATab = New System.Windows.Forms.Label
Me.SurrLabelHATab = New System.Windows.Forms.Label
Me.TCSplitLabelHATab = New System.Windows.Forms.Label
Me.CDSplitLabelHATab = New System.Windows.Forms.Label
Me.DoubleLabelHATab = New System.Windows.Forms.Label
Me.StandLabelHATab = New System.Windows.Forms.Label
Me.ProbLabelHATab = New System.Windows.Forms.Label
Me.HandLabelHATab = New System.Windows.Forms.Label
Me.HandNameBoxHATab = New System.Windows.Forms.TextBox
Me.TotalDetailsLabelHATab = New System.Windows.Forms.Label
Me.TotalDetailsBoxHATab = New System.Windows.Forms.TextBox
Me.HandSizeAnalysisTab = New System.Windows.Forms.TabPage
Me.TotalComboBoxHSATab = New System.Windows.Forms.ComboBox
Me.UCComboBoxHSATab = New System.Windows.Forms.ComboBox
Me.Note2LabelHSATab = New System.Windows.Forms.Label
Me.OrLessCheckHSATab = New System.Windows.Forms.CheckBox
Me.OrMoreCheckHSATab = New System.Windows.Forms.CheckBox
Me.SplitEVBoxHSATab = New System.Windows.Forms.TextBox
Me.SplitEVLabelHSATab = New System.Windows.Forms.Label
Me.NoteLabelHSATab = New System.Windows.Forms.Label
Me.HandUsedCheckHSATab = New System.Windows.Forms.CheckBox
Me.StrategyGroupHSATab = New System.Windows.Forms.GroupBox
Me.CDButtonHSATab = New System.Windows.Forms.RadioButton
Me.TCButtonHSATab = New System.Windows.Forms.RadioButton
Me.ForcedButtonHSATab = New System.Windows.Forms.RadioButton
Me.TDButtonHSATab = New System.Windows.Forms.RadioButton
Me.NCards2LabelHSATab = New System.Windows.Forms.Label
Me.SEV2LabelHSATab = New System.Windows.Forms.Label
Me.HEV2LabelHSATab = New System.Windows.Forms.Label
Me.DEV2LabelHSATab = New System.Windows.Forms.Label
Me.Strat2LabelHSATab = New System.Windows.Forms.Label
Me.SurrEV2LabelHSATab = New System.Windows.Forms.Label
Me.SEV1LabelHSATab = New System.Windows.Forms.Label
Me.HEV1LabelHSATab = New System.Windows.Forms.Label
Me.DEV1LabelHSATab = New System.Windows.Forms.Label
Me.Strat1LabelHSATab = New System.Windows.Forms.Label
Me.SurrEV1LabelHSATab = New System.Windows.Forms.Label
Me.C18LabelHSATab = New System.Windows.Forms.Label
Me.C19LabelHSATab = New System.Windows.Forms.Label
Me.C20LabelHSATab = New System.Windows.Forms.Label
Me.C11LabelHSATab = New System.Windows.Forms.Label
Me.C12LabelHSATab = New System.Windows.Forms.Label
Me.C13LabelHSATab = New System.Windows.Forms.Label
Me.C14LabelHSATab = New System.Windows.Forms.Label
Me.C15LabelHSATab = New System.Windows.Forms.Label
Me.C16LabelHSATab = New System.Windows.Forms.Label
Me.C17LabelHSATab = New System.Windows.Forms.Label
Me.C10LabelHSATab = New System.Windows.Forms.Label
Me.NCards1LabelHSATab = New System.Windows.Forms.Label
Me.AnyLabelHSATab = New System.Windows.Forms.Label
Me.C3LabelHSATab = New System.Windows.Forms.Label
Me.C4LabelHSATab = New System.Windows.Forms.Label
Me.C5LabelHSATab = New System.Windows.Forms.Label
Me.C6LabelHSATab = New System.Windows.Forms.Label
Me.C7LabelHSATab = New System.Windows.Forms.Label
Me.C8LabelHSATab = New System.Windows.Forms.Label
Me.C9LabelHSATab = New System.Windows.Forms.Label
Me.C2LabelHSATab = New System.Windows.Forms.Label
Me.UCLabelHSATab = New System.Windows.Forms.Label
Me.SoftCheckHSATab = New System.Windows.Forms.CheckBox
Me.SoftLabelHSATab = New System.Windows.Forms.Label
Me.TotalLabelHSATab = New System.Windows.Forms.Label
Me.DoubleAnalysisTab = New System.Windows.Forms.TabPage
Me.NCardsComboBoxDATab = New System.Windows.Forms.ComboBox
Me.TotalComboBoxDATab = New System.Windows.Forms.ComboBox
Me.UCComboBoxDATab = New System.Windows.Forms.ComboBox
Me.ExactMatchCheckDATab = New System.Windows.Forms.CheckBox
Me.ListSizeLabelDATab = New System.Windows.Forms.Label
Me.ListSizeBoxDATab = New System.Windows.Forms.TextBox
Me.HardOnlyCheckDATab = New System.Windows.Forms.CheckBox
Me.SoftOnlyCheckDATab = New System.Windows.Forms.CheckBox
Me.OrLessCheckDATab = New System.Windows.Forms.CheckBox
Me.OrMoreCheckDATab = New System.Windows.Forms.CheckBox
Me.EitherCheckDATab = New System.Windows.Forms.CheckBox
Me.IncludesLabelDATab = New System.Windows.Forms.Label
Me.HandBoxDATab = New System.Windows.Forms.TextBox
Me.NCardLabelDATab = New System.Windows.Forms.Label
Me.UCLabelDATab = New System.Windows.Forms.Label
Me.SoftLabelDATab = New System.Windows.Forms.Label
Me.TotalLabelDATab = New System.Windows.Forms.Label
Me.HandListBoxDATab = New System.Windows.Forms.ListBox
Me.HandDetailsGroupDATab = New System.Windows.Forms.GroupBox
Me.DAllowedCheckDATab = New System.Windows.Forms.CheckBox
Me.DAllowedLabelDATab = New System.Windows.Forms.Label
Me.DoubleBox2DATab = New System.Windows.Forms.TextBox
Me.DoubleLabel2DATab = New System.Windows.Forms.Label
Me.C3LabelDATab = New System.Windows.Forms.Label
Me.CTLabelDATab = New System.Windows.Forms.Label
Me.C4LabelDATab = New System.Windows.Forms.Label
Me.C5LabelDATab = New System.Windows.Forms.Label
Me.C2LabelDATab = New System.Windows.Forms.Label
Me.C6LabelDATab = New System.Windows.Forms.Label
Me.C7LabelDATab = New System.Windows.Forms.Label
Me.C8LabelDATab = New System.Windows.Forms.Label
Me.C9LabelDATab = New System.Windows.Forms.Label
Me.CALabelDATab = New System.Windows.Forms.Label
Me.CardStratEVLabelDATab = New System.Windows.Forms.Label
Me.CardStratLabelDATab = New System.Windows.Forms.Label
Me.NextCardLabelDATab = New System.Windows.Forms.Label
Me.BreakdownLabelDATab = New System.Windows.Forms.Label
Me.PostDoubleLabelDATab = New System.Windows.Forms.Label
Me.UCDetailsBoxDATab = New System.Windows.Forms.TextBox
Me.UCDetailsLabelDATab = New System.Windows.Forms.Label
Me.NCardsDetailLabelDATab = New System.Windows.Forms.Label
Me.NCardsDetailsBoxDATab = New System.Windows.Forms.TextBox
Me.SoftDetailsCheckDATab = New System.Windows.Forms.CheckBox
Me.SoftDetailsLabelDATab = New System.Windows.Forms.Label
Me.StandBoxDATab = New System.Windows.Forms.TextBox
Me.DoubleBoxDATab = New System.Windows.Forms.TextBox
Me.SurrenderBoxDATab = New System.Windows.Forms.TextBox
Me.StratBoxDATab = New System.Windows.Forms.TextBox
Me.StratLabelDATab = New System.Windows.Forms.Label
Me.SurrLabelDATab = New System.Windows.Forms.Label
Me.DoubleLabelDATab = New System.Windows.Forms.Label
Me.StandLabelDATab = New System.Windows.Forms.Label
Me.CardProbLabelDATab = New System.Windows.Forms.Label
Me.HandLabelDATab = New System.Windows.Forms.Label
Me.HandNameBoxDATab = New System.Windows.Forms.TextBox
Me.TotalDetailsLabelDATab = New System.Windows.Forms.Label
Me.TotalDetailsBoxDATab = New System.Windows.Forms.TextBox
Me.AllExceptionsTab = New System.Windows.Forms.TabPage
Me.ExceptionsTabControl = New System.Windows.Forms.TabControl
Me.ExceptionsTab = New System.Windows.Forms.TabPage
Me.TotalComboBoxETab = New System.Windows.Forms.ComboBox
Me.NCardsComboBoxETab = New System.Windows.Forms.ComboBox
Me.UCComboBoxETab = New System.Windows.Forms.ComboBox
Me.PostSplitCheckETab = New System.Windows.Forms.CheckBox
Me.PreSplitCheckETab = New System.Windows.Forms.CheckBox
Me.ETypeLabelETab = New System.Windows.Forms.Label
Me.ExTypeComboBoxETab = New System.Windows.Forms.ComboBox
Me.ExactMatchCheckETab = New System.Windows.Forms.CheckBox
Me.ListSizeLabelETab = New System.Windows.Forms.Label
Me.ListSizeBoxETab = New System.Windows.Forms.TextBox
Me.HardOnlyCheckETab = New System.Windows.Forms.CheckBox
Me.SoftOnlyCheckETab = New System.Windows.Forms.CheckBox
Me.OrLessCheckETab = New System.Windows.Forms.CheckBox
Me.OrMoreCheckETab = New System.Windows.Forms.CheckBox
Me.EitherCheckETab = New System.Windows.Forms.CheckBox
Me.IncludesLabelETab = New System.Windows.Forms.Label
Me.HandBoxETab = New System.Windows.Forms.TextBox
Me.NCardLabelETab = New System.Windows.Forms.Label
Me.UCLabelETab = New System.Windows.Forms.Label
Me.SoftLabelETab = New System.Windows.Forms.Label
Me.TotalLabelETab = New System.Windows.Forms.Label
Me.HandListBoxETab = New System.Windows.Forms.ListBox
Me.ExceptionDetailsGroupETab = New System.Windows.Forms.GroupBox
Me.ExRuleNameBoxETab = New System.Windows.Forms.TextBox
Me.ExRuleNameLabelETab = New System.Windows.Forms.Label
Me.ExForcedButtonETab = New System.Windows.Forms.Button
Me.PaircardBoxETab = New System.Windows.Forms.TextBox
Me.PaircardLabelETab = New System.Windows.Forms.Label
Me.ExSurrenderBoxETab = New System.Windows.Forms.TextBox
Me.ExStandBoxETab = New System.Windows.Forms.TextBox
Me.ExDoubleBoxETab = New System.Windows.Forms.TextBox
Me.StateBoxETab = New System.Windows.Forms.TextBox
Me.ExTypeBoxETab = New System.Windows.Forms.TextBox
Me.ExNameBoxETab = New System.Windows.Forms.TextBox
Me.BaseNameBoxETab = New System.Windows.Forms.TextBox
Me.ExStratDetailsLabelETab = New System.Windows.Forms.Label
Me.BaseHandUsedCheckETab = New System.Windows.Forms.CheckBox
Me.ExHandUsedCheckETab = New System.Windows.Forms.CheckBox
Me.NCardsDetailLabelETab = New System.Windows.Forms.Label
Me.NCardsDetailsBoxETab = New System.Windows.Forms.TextBox
Me.SoftDetailsCheckETab = New System.Windows.Forms.CheckBox
Me.SoftDetailsLabelETab = New System.Windows.Forms.Label
Me.UsedLabelETab = New System.Windows.Forms.Label
Me.StratNameDetailsLabelETab = New System.Windows.Forms.Label
Me.StratLabelETab = New System.Windows.Forms.Label
Me.ExStratETab = New System.Windows.Forms.TextBox
Me.BaseStandBoxETab = New System.Windows.Forms.TextBox
Me.BaseDoubleBoxETab = New System.Windows.Forms.TextBox
Me.BaseSurrenderBoxETab = New System.Windows.Forms.TextBox
Me.BaseSplitBoxETab = New System.Windows.Forms.TextBox
Me.ExSplitBoxETab = New System.Windows.Forms.TextBox
Me.ExHitBoxETab = New System.Windows.Forms.TextBox
Me.BaseHitBoxETab = New System.Windows.Forms.TextBox
Me.ProbBoxETab = New System.Windows.Forms.TextBox
Me.BaseStratETab = New System.Windows.Forms.TextBox
Me.SplitLabelETab = New System.Windows.Forms.Label
Me.BaseStratDetailsLabelETab = New System.Windows.Forms.Label
Me.HitLabelETab = New System.Windows.Forms.Label
Me.StateLabelETab = New System.Windows.Forms.Label
Me.SurrLabelETab = New System.Windows.Forms.Label
Me.ExTypeLabelETab = New System.Windows.Forms.Label
Me.DoubleLabelETab = New System.Windows.Forms.Label
Me.StandLabelETab = New System.Windows.Forms.Label
Me.ProbLabelETab = New System.Windows.Forms.Label
Me.HandLabelETab = New System.Windows.Forms.Label
Me.HandNameBoxETab = New System.Windows.Forms.TextBox
Me.TotalDetailsLabelETab = New System.Windows.Forms.Label
Me.TotalDetailsBoxETab = New System.Windows.Forms.TextBox
Me.UCDetailsBoxETab = New System.Windows.Forms.TextBox
Me.UCDetailsLabelETab = New System.Windows.Forms.Label
Me.NCardExceptionsTab = New System.Windows.Forms.TabPage
Me.NCardsComboBoxNCETab = New System.Windows.Forms.ComboBox
Me.TotalComboBoxNCETab = New System.Windows.Forms.ComboBox
Me.ExAllForcedButtonNCETab = New System.Windows.Forms.Button
Me.NoteLabelNCETab = New System.Windows.Forms.Label
Me.UCComboBoxNCETab = New System.Windows.Forms.ComboBox
Me.ETypeLabelNCETab = New System.Windows.Forms.Label
Me.ExTypeComboBoxNCETab = New System.Windows.Forms.ComboBox
Me.ListSizeLabelNCETab = New System.Windows.Forms.Label
Me.ListSizeBoxNCETab = New System.Windows.Forms.TextBox
Me.HardOnlyCheckNCETab = New System.Windows.Forms.CheckBox
Me.SoftOnlyCheckNCETab = New System.Windows.Forms.CheckBox
Me.EitherCheckNCETab = New System.Windows.Forms.CheckBox
Me.NCardLabelNCETab = New System.Windows.Forms.Label
Me.UCLabelNCETab = New System.Windows.Forms.Label
Me.SoftLabelNCETab = New System.Windows.Forms.Label
Me.TotalLabelNCETab = New System.Windows.Forms.Label
Me.HandListBoxNCETab = New System.Windows.Forms.ListBox
Me.ExceptionDetailsGroupNCETab = New System.Windows.Forms.GroupBox
Me.ExRuleNameBoxNCETab = New System.Windows.Forms.TextBox
Me.ExRuleNameLabelNCETab = New System.Windows.Forms.Label
Me.ExForcedButtonNCETab = New System.Windows.Forms.Button
Me.ExSurrenderBoxNCETab = New System.Windows.Forms.TextBox
Me.ExStandBoxNCETab = New System.Windows.Forms.TextBox
Me.ExDoubleBoxNCETab = New System.Windows.Forms.TextBox
Me.ExTypeBoxNCETab = New System.Windows.Forms.TextBox
Me.ExNameBoxNCETab = New System.Windows.Forms.TextBox
Me.BaseNameBoxNCETab = New System.Windows.Forms.TextBox
Me.ExStratDetailsLabelNCETab = New System.Windows.Forms.Label
Me.NCardsDetailLabelNCETab = New System.Windows.Forms.Label
Me.NCardsDetailsBoxNCETab = New System.Windows.Forms.TextBox
Me.SoftDetailsCheckNCETab = New System.Windows.Forms.CheckBox
Me.SoftDetailsLabelNCETab = New System.Windows.Forms.Label
Me.StratNameDetailsLabelNCETab = New System.Windows.Forms.Label
Me.StratLabelNCETab = New System.Windows.Forms.Label
Me.ExStratNCETab = New System.Windows.Forms.TextBox
Me.BaseStandBoxNCETab = New System.Windows.Forms.TextBox
Me.BaseDoubleBoxNCETab = New System.Windows.Forms.TextBox
Me.BaseSurrenderBoxNCETab = New System.Windows.Forms.TextBox
Me.ExHitBoxNCETab = New System.Windows.Forms.TextBox
Me.BaseHitBoxNCETab = New System.Windows.Forms.TextBox
Me.BaseStratNCETab = New System.Windows.Forms.TextBox
Me.BaseStratDetailsLabelNCETab = New System.Windows.Forms.Label
Me.HitLabelNCETab = New System.Windows.Forms.Label
Me.SurrLabelNCETab = New System.Windows.Forms.Label
Me.ExTypeLabelNCETab = New System.Windows.Forms.Label
Me.DoubleLabelNCETab = New System.Windows.Forms.Label
Me.StandLabelNCETab = New System.Windows.Forms.Label
Me.TotalDetailsLabelNCETab = New System.Windows.Forms.Label
Me.TotalDetailsBoxNCETab = New System.Windows.Forms.TextBox
Me.UCDetailsBoxNCETab = New System.Windows.Forms.TextBox
Me.UCDetailsLabelNCETab = New System.Windows.Forms.Label
Me.ForcedTab = New System.Windows.Forms.TabPage
Me.CopyPairsButtonFSTab = New System.Windows.Forms.Button
Me.CopyTDButtonFSTab = New System.Windows.Forms.Button
Me.ForcedStratTabControlFSTab = New System.Windows.Forms.TabControl
Me.OptionsTabFSTab = New System.Windows.Forms.TabPage
Me.CalcNCardStratButtonFSTab = New System.Windows.Forms.Button
Me.PairsRuleApplyLabelFSTab = New System.Windows.Forms.Label
Me.ForcedTablePostCheckFSTab = New System.Windows.Forms.CheckBox
Me.ForcedTablePreCheckFSTab = New System.Windows.Forms.CheckBox
Me.ForcednCDLabelFSTab = New System.Windows.Forms.Label
Me.ForcednCDBoxFSTab = New System.Windows.Forms.TextBox
Me.ForcedWarningLabelFSTab = New System.Windows.Forms.Label
Me.HardSoftTDTabFSTab = New System.Windows.Forms.TabPage
Me.RowClickLabelFSTab = New System.Windows.Forms.Label
Me.PairCDLabelComboboxFSTab = New System.Windows.Forms.ComboBox
Me.SoftCDLabelComboboxFSTab = New System.Windows.Forms.ComboBox
Me.HardCDLabelComboboxFSTab = New System.Windows.Forms.ComboBox
Me.SoftTDLabelComboboxFSTab = New System.Windows.Forms.ComboBox
Me.HardTDLabelComboboxFSTab = New System.Windows.Forms.ComboBox
Me.SoftTDGroupFSTab = New System.Windows.Forms.GroupBox
Me.SoftTotalTDLabelFSTab = New System.Windows.Forms.Label
Me.ForcedTableComboboxFSTab = New System.Windows.Forms.ComboBox
Me.HardTDGroupFSTab = New System.Windows.Forms.GroupBox
Me.HardTotalTDLabelFSTab = New System.Windows.Forms.Label
Me.SoftPairsCDTabFSTab = New System.Windows.Forms.TabPage
Me.PairCDGroupFSTab = New System.Windows.Forms.GroupBox
Me.PairForcedCDLabelFSTab = New System.Windows.Forms.Label
Me.SoftCDGroupFSTab = New System.Windows.Forms.GroupBox
Me.TotalForcedCDLabelFSTab = New System.Windows.Forms.Label
Me.HardCDTabFSTab = New System.Windows.Forms.TabPage
Me.HardCDHand2LabelFSTab = New System.Windows.Forms.Label
Me.HardCDHand1LabelFSTab = New System.Windows.Forms.Label
Me.OtherTabFSTab = New System.Windows.Forms.TabPage
Me.DeleteAllForcedRulesButtonFSTab = New System.Windows.Forms.Button
Me.RenameRuleButtonFSTab = New System.Windows.Forms.Button
Me.LoadForcedRulesButtonFSTab = New System.Windows.Forms.Button
Me.UncheckAllForcedRulesButtonFSTab = New System.Windows.Forms.Button
Me.SavedForcedRulesButtonFSTab = New System.Windows.Forms.Button
Me.RestoreDefaultForcedRulesButtonFSTab = New System.Windows.Forms.Button
Me.MoveForcedRulesDownButtonFSTab = New System.Windows.Forms.Button
Me.MoveForcedRulesUpButtonFSTab = New System.Windows.Forms.Button
Me.UpdateForcedRuleButtonFSTab = New System.Windows.Forms.Button
Me.DeleteForcedRuleButtonFSTab = New System.Windows.Forms.Button
Me.ForcedRuleLabelFSTab = New System.Windows.Forms.Label
Me.ForcedRuleDetailsGroupFSTab = New System.Windows.Forms.GroupBox
Me.HandNCardsComboBoxFSTab = New System.Windows.Forms.ComboBox
Me.HandTotalComboBoxFSTab = New System.Windows.Forms.ComboBox
Me.UpcardComboBoxFSTab = New System.Windows.Forms.ComboBox
Me.ForcedRuleStratBoxFSTab = New System.Windows.Forms.TextBox
Me.StrategyComboBoxFSTab = New System.Windows.Forms.ComboBox
Me.DealerUpcardLabelFSTab = New System.Windows.Forms.Label
Me.StrategyLabelFSTab = New System.Windows.Forms.Label
Me.ExactMatchCheckFSTab = New System.Windows.Forms.CheckBox
Me.SoftCheckFSTab = New System.Windows.Forms.CheckBox
Me.OrLessCheckFSTab = New System.Windows.Forms.CheckBox
Me.ClearForcedRuleButtonFSTab = New System.Windows.Forms.Button
Me.PostSplitCheckFSTab = New System.Windows.Forms.CheckBox
Me.PreSplitCheckFSTab = New System.Windows.Forms.CheckBox
Me.ForcedRuleNameBoxFSTab = New System.Windows.Forms.TextBox
Me.ForcedRuleNameLabelFSTab = New System.Windows.Forms.Label
Me.HandCompCheckFSTab = New System.Windows.Forms.CheckBox
Me.HandTotalSizeCheckFSTab = New System.Windows.Forms.CheckBox
Me.TotalBoxFSTab = New System.Windows.Forms.TextBox
Me.NCardsBoxFSTab = New System.Windows.Forms.TextBox
Me.OrMoreCheckFSTab = New System.Windows.Forms.CheckBox
Me.HandNCardsLabelFSTab = New System.Windows.Forms.Label
Me.TotalLabelFSTab = New System.Windows.Forms.Label
Me.SoftLabelFSTab = New System.Windows.Forms.Label
Me.NCardsLabelFSTab = New System.Windows.Forms.Label
Me.C3LabelFSTab = New System.Windows.Forms.Label
Me.C10LabelFSTab = New System.Windows.Forms.Label
Me.C4LabelFSTab = New System.Windows.Forms.Label
Me.C5LabelFSTab = New System.Windows.Forms.Label
Me.C2LabelFSTab = New System.Windows.Forms.Label
Me.C6LabelFSTab = New System.Windows.Forms.Label
Me.C7LabelFSTab = New System.Windows.Forms.Label
Me.C8LabelFSTab = New System.Windows.Forms.Label
Me.C9LabelFSTab = New System.Windows.Forms.Label
Me.CAceLabelFSTab = New System.Windows.Forms.Label
Me.HandSoftCheckFSTab = New System.Windows.Forms.CheckBox
Me.HandSoftLabelFSTab = New System.Windows.Forms.Label
Me.HandTotalLabelFSTab = New System.Windows.Forms.Label
Me.AddForcedRuleButtonFSTab = New System.Windows.Forms.Button
Me.ForcedRulesCheckListBoxFSTab = New System.Windows.Forms.CheckedListBox
Me.LoadForcedTablesButtonFSTab = New System.Windows.Forms.Button
Me.SaveForcedTablesButtonFSTab = New System.Windows.Forms.Button
Me.ClearForcedTablesButtonFSTab = New System.Windows.Forms.Button
Me.CopyCDButtonFSTab = New System.Windows.Forms.Button
Me.RecalcForcedStratButtonFSTab = New System.Windows.Forms.Button
Me.EORTab = New System.Windows.Forms.TabPage
Me.EORTabControlEORTab = New System.Windows.Forms.TabControl
Me.SummaryEORTab = New System.Windows.Forms.TabPage
Me.TextButtonEORTab = New System.Windows.Forms.Button
Me.NCardEORLabelEORTab = New System.Windows.Forms.Label
Me.NCardEORBoxEORTab = New System.Windows.Forms.TextBox
Me.NetLabelEORTab = New System.Windows.Forms.Label
Me.ClearEORsButtonEORTab = New System.Windows.Forms.Button
Me.TDLabel1SummEORTab = New System.Windows.Forms.Label
Me.TDEVBoxEORTab = New System.Windows.Forms.TextBox
Me.EORCardComboboxEORTab = New System.Windows.Forms.ComboBox
Me.CDLabel3SummEORTab = New System.Windows.Forms.Label
Me.TDLabel3SummEORTab = New System.Windows.Forms.Label
Me.ForcedLabel3SummEORTab = New System.Windows.Forms.Label
Me.NetLabelSummEORTab = New System.Windows.Forms.Label
Me.CDLabel1SummEORTab = New System.Windows.Forms.Label
Me.ForcedLabel1SummEORTab = New System.Windows.Forms.Label
Me.CDEVBoxEORTab = New System.Windows.Forms.TextBox
Me.ForcedEVBoxEORTab = New System.Windows.Forms.TextBox
Me.CardLabel2SummEORTab = New System.Windows.Forms.Label
Me.CardLabel1SummEORTab = New System.Windows.Forms.Label
Me.CalcEORsButtonEORTab = New System.Windows.Forms.Button
Me.NoteLabelEORTab = New System.Windows.Forms.Label
Me.ProbLabel2SummEORTab = New System.Windows.Forms.Label
Me.ProbLabel1SummEORTab = New System.Windows.Forms.Label
Me.EORLabelSummEORTab = New System.Windows.Forms.Label
Me.NetEORLabelSummEORTab = New System.Windows.Forms.Label
Me.CDLabel2SummEORTab = New System.Windows.Forms.Label
Me.TDLabel2SummEORTab = New System.Windows.Forms.Label
Me.ForcedLabel2SummEORTab = New System.Windows.Forms.Label
Me.HATabEORTab = New System.Windows.Forms.TabPage
Me.ChangedOnlyCheckBoxEORTab = New System.Windows.Forms.CheckBox
Me.NCardsComboBoxEORTab = New System.Windows.Forms.ComboBox
Me.TotalComboBoxEORTab = New System.Windows.Forms.ComboBox
Me.CardRemovedComboBoxEORTab = New System.Windows.Forms.ComboBox
Me.UCComboBoxEORTab = New System.Windows.Forms.ComboBox
Me.CardRemovedLabelEORTab = New System.Windows.Forms.Label
Me.ExactMatchCheckEORTab = New System.Windows.Forms.CheckBox
Me.ListSizeLabelEORTab = New System.Windows.Forms.Label
Me.ListSizeBoxEORTab = New System.Windows.Forms.TextBox
Me.HardOnlyCheckEORTab = New System.Windows.Forms.CheckBox
Me.SoftOnlyCheckEORTab = New System.Windows.Forms.CheckBox
Me.OrLessCheckEORTab = New System.Windows.Forms.CheckBox
Me.OrMoreCheckEORTab = New System.Windows.Forms.CheckBox
Me.EitherCheckEORTab = New System.Windows.Forms.CheckBox
Me.IncludesLabelEORTab = New System.Windows.Forms.Label
Me.HandBoxEORTab = New System.Windows.Forms.TextBox
Me.NCardLabelEORTab = New System.Windows.Forms.Label
Me.UCLabelEORTab = New System.Windows.Forms.Label
Me.SoftLabelEORTab = New System.Windows.Forms.Label
Me.TotalLabelEORTab = New System.Windows.Forms.Label
Me.HandListBoxEORTab = New System.Windows.Forms.ListBox
Me.HandDetailsGroupEORTab = New System.Windows.Forms.GroupBox
Me.CardRemovedDetailsBoxEORTab = New System.Windows.Forms.TextBox
Me.CardRemovedDetailsLabelEORTab = New System.Windows.Forms.Label
Me.Diff3DetailsLabelHATab = New System.Windows.Forms.Label
Me.New3DetailsLabelHATab = New System.Windows.Forms.Label
Me.Orig3DetailsLabelHATab = New System.Windows.Forms.Label
Me.Diff2DetailsLabelHATab = New System.Windows.Forms.Label
Me.New2DetailsLabelHATab = New System.Windows.Forms.Label
Me.Orig2DetailsLabelHATab = New System.Windows.Forms.Label
Me.Diff1DetailsLabelHATab = New System.Windows.Forms.Label
Me.New1DetailsLabelHATab = New System.Windows.Forms.Label
Me.Orig1DetailsLabelHATab = New System.Windows.Forms.Label
Me.UCDetailsBoxEORTab = New System.Windows.Forms.TextBox
Me.UCDetailsLabelEORTab = New System.Windows.Forms.Label
Me.NCardsDetailLabelEORTab = New System.Windows.Forms.Label
Me.NCardsDetailsBoxEORTab = New System.Windows.Forms.TextBox
Me.SoftDetailsCheckEORTab = New System.Windows.Forms.CheckBox
Me.SoftDetailsLabelEORTab = New System.Windows.Forms.Label
Me.CDDetailsLabelHATab = New System.Windows.Forms.Label
Me.ForcedDetailsLabelHATab = New System.Windows.Forms.Label
Me.TDDetailsLabelHATab = New System.Windows.Forms.Label
Me.SplitLabelEORTab = New System.Windows.Forms.Label
Me.StratLabelEORTab = New System.Windows.Forms.Label
Me.HitLabelEORTab = New System.Windows.Forms.Label
Me.SurrLabelEORTab = New System.Windows.Forms.Label
Me.DoubleLabelEORTab = New System.Windows.Forms.Label
Me.StandLabelEORTab = New System.Windows.Forms.Label
Me.HandLabelEORTab = New System.Windows.Forms.Label
Me.HandNameBoxEORTab = New System.Windows.Forms.TextBox
Me.TotalDetailsLabelEORTab = New System.Windows.Forms.Label
Me.TotalDetailsBoxEORTab = New System.Windows.Forms.TextBox
Me.TotalTabEORTab = New System.Windows.Forms.TabPage
Me.CardRemovedTotalComboBoxEORTab = New System.Windows.Forms.ComboBox
Me.UCTotalComboBoxEORTab = New System.Windows.Forms.ComboBox
Me.CardRemovedTotalLabelEORTab = New System.Windows.Forms.Label
Me.UCTotalLabelEORTab = New System.Windows.Forms.Label
Me.HandTypeGroupEORTab = New System.Windows.Forms.GroupBox
Me.SoftButtonEORTab = New System.Windows.Forms.RadioButton
Me.HardButtonEORTab = New System.Windows.Forms.RadioButton
Me.Diff2TotalLabelEORTab = New System.Windows.Forms.Label
Me.New2TotalLabelEORTab = New System.Windows.Forms.Label
Me.Orig2TotalLabelEORTab = New System.Windows.Forms.Label
Me.Diff4TotalLabelEORTab = New System.Windows.Forms.Label
Me.New4TotalLabelEORTab = New System.Windows.Forms.Label
Me.Orig4TotalLabelEORTab = New System.Windows.Forms.Label
Me.Diff5TotalLabelEORTab = New System.Windows.Forms.Label
Me.New5TotalLabelEORTab = New System.Windows.Forms.Label
Me.Orig5TotalLabelEORTab = New System.Windows.Forms.Label
Me.Diff3TotalLabelEORTab = New System.Windows.Forms.Label
Me.New3TotalLabelEORTab = New System.Windows.Forms.Label
Me.Orig3TotalLabelEORTab = New System.Windows.Forms.Label
Me.Diff1TotalLabelEORTab = New System.Windows.Forms.Label
Me.New1TotalLabelEORTab = New System.Windows.Forms.Label
Me.Orig1TotalLabelEORTab = New System.Windows.Forms.Label
Me.StratTotalLabelEORTab = New System.Windows.Forms.Label
Me.HitTotalLabelEORTab = New System.Windows.Forms.Label
Me.SurrenderTotalLabelEORTab = New System.Windows.Forms.Label
Me.DoubleTotalLabelEORTab = New System.Windows.Forms.Label
Me.StandTotalLabelEORTab = New System.Windows.Forms.Label
Me.StrategyGroupEORTab = New System.Windows.Forms.GroupBox
Me.ForcedButtonEORTab = New System.Windows.Forms.RadioButton
Me.TDButtonEORTab = New System.Windows.Forms.RadioButton
Me.SplitsTab = New System.Windows.Forms.TabPage
Me.UC10LabelSplitTab = New System.Windows.Forms.Label
Me.UC9LabelSplitTab = New System.Windows.Forms.Label
Me.UC8LabelSplitTab = New System.Windows.Forms.Label
Me.UC7LabelSplitTab = New System.Windows.Forms.Label
Me.UC6LabelSplitTab = New System.Windows.Forms.Label
Me.UC5LabelSplitTab = New System.Windows.Forms.Label
Me.UC4LabelSplitTab = New System.Windows.Forms.Label
Me.UC3LabelSplitTab = New System.Windows.Forms.Label
Me.UC2LabelSplitTab = New System.Windows.Forms.Label
Me.UCALabelSplitTab = New System.Windows.Forms.Label
Me.Card5LabelSplitTab = New System.Windows.Forms.Label
Me.SPL25LabelSplitTab = New System.Windows.Forms.Label
Me.SPL35LabelSplitTab = New System.Windows.Forms.Label
Me.SPL15LabelSplitTab = New System.Windows.Forms.Label
Me.Card3LabelSplitTab = New System.Windows.Forms.Label
Me.SPL23LabelSplitTab = New System.Windows.Forms.Label
Me.SPL33LabelSplitTab = New System.Windows.Forms.Label
Me.SPL13LabelSplitTab = New System.Windows.Forms.Label
Me.Card4LabelSplitTab = New System.Windows.Forms.Label
Me.SPL24LabelSplitTab = New System.Windows.Forms.Label
Me.SPL34LabelSplitTab = New System.Windows.Forms.Label
Me.SPL14LabelSplitTab = New System.Windows.Forms.Label
Me.Card2LabelSplitTab = New System.Windows.Forms.Label
Me.SPL22LabelSplitTab = New System.Windows.Forms.Label
Me.SPL32LabelSplitTab = New System.Windows.Forms.Label
Me.SPL12LabelSplitTab = New System.Windows.Forms.Label
Me.Card1LabelSplitTab = New System.Windows.Forms.Label
Me.SPL21LabelSplitTab = New System.Windows.Forms.Label
Me.SPL31LabelSplitTab = New System.Windows.Forms.Label
Me.SPL11LabelSplitTab = New System.Windows.Forms.Label
Me.PaircardsGroupSplitTab = New System.Windows.Forms.GroupBox
Me.SixtoTButtonSplitTab = New System.Windows.Forms.RadioButton
Me.Ato5ButtonSplitTab = New System.Windows.Forms.RadioButton
Me.StrategyGroupSplitTab = New System.Windows.Forms.GroupBox
Me.CDButtonSplitTab = New System.Windows.Forms.RadioButton
Me.TCButtonSplitTab = New System.Windows.Forms.RadioButton
Me.ForcedButtonSplitTab = New System.Windows.Forms.RadioButton
Me.TDButtonSplitTab = New System.Windows.Forms.RadioButton
Me.OtherTab = New System.Windows.Forms.TabPage
Me.ColorTableGroupOTab = New System.Windows.Forms.GroupBox
Me.RestoreDefaultColorTableButtonOTab = New System.Windows.Forms.Button
Me.SaveColorTableFileButtonOTab = New System.Windows.Forms.Button
Me.LoadColorTableFileButtonOTab = New System.Windows.Forms.Button
Me.ResultsFormTabControl.SuspendLayout
Me.SummaryTab.SuspendLayout
Me.StratTab.SuspendLayout
Me.StratTabControlSTab.SuspendLayout
Me.HardSoftTDTabSTab.SuspendLayout
Me.StratGroupTDHardSoftTab.SuspendLayout
Me.SoftTDGroupSTab.SuspendLayout
Me.HardTDGroupSTab.SuspendLayout
Me.SoftPairsCDTabSTab.SuspendLayout
Me.EVsBoxSTab.SuspendLayout
Me.StratGroupTDSoftPairsTab.SuspendLayout
Me.PairCDGroupSTab.SuspendLayout
Me.SoftCDGroupSTab.SuspendLayout
Me.HardCDTabSTab.SuspendLayout
Me.StratGroupCDHardTab.SuspendLayout
Me.SuitedTabSTab.SuspendLayout
Me.HandDetailsGroupSTab.SuspendLayout
Me.RulesTab.SuspendLayout
Me.ShoeGroupRTab.SuspendLayout
Me.AnalysisTab.SuspendLayout
Me.AnalysisTabControl.SuspendLayout
Me.HandAnalysisTab.SuspendLayout
Me.HandDetailsGroupHATab.SuspendLayout
Me.HandSizeAnalysisTab.SuspendLayout
Me.StrategyGroupHSATab.SuspendLayout
Me.DoubleAnalysisTab.SuspendLayout
Me.HandDetailsGroupDATab.SuspendLayout
Me.AllExceptionsTab.SuspendLayout
Me.ExceptionsTabControl.SuspendLayout
Me.ExceptionsTab.SuspendLayout
Me.ExceptionDetailsGroupETab.SuspendLayout
Me.NCardExceptionsTab.SuspendLayout
Me.ExceptionDetailsGroupNCETab.SuspendLayout
Me.ForcedTab.SuspendLayout
Me.ForcedStratTabControlFSTab.SuspendLayout
Me.OptionsTabFSTab.SuspendLayout
Me.HardSoftTDTabFSTab.SuspendLayout
Me.SoftTDGroupFSTab.SuspendLayout
Me.HardTDGroupFSTab.SuspendLayout
Me.SoftPairsCDTabFSTab.SuspendLayout
Me.PairCDGroupFSTab.SuspendLayout
Me.SoftCDGroupFSTab.SuspendLayout
Me.HardCDTabFSTab.SuspendLayout
Me.OtherTabFSTab.SuspendLayout
Me.ForcedRuleDetailsGroupFSTab.SuspendLayout
Me.EORTab.SuspendLayout
Me.EORTabControlEORTab.SuspendLayout
Me.SummaryEORTab.SuspendLayout
Me.HATabEORTab.SuspendLayout
Me.HandDetailsGroupEORTab.SuspendLayout
Me.TotalTabEORTab.SuspendLayout
Me.HandTypeGroupEORTab.SuspendLayout
Me.StrategyGroupEORTab.SuspendLayout
Me.SplitsTab.SuspendLayout
Me.PaircardsGroupSplitTab.SuspendLayout
Me.StrategyGroupSplitTab.SuspendLayout
Me.OtherTab.SuspendLayout
Me.ColorTableGroupOTab.SuspendLayout
Me.SuspendLayout
'
'ResultsFormTabControl
'
Me.ResultsFormTabControl.Controls.Add(Me.SummaryTab)
Me.ResultsFormTabControl.Controls.Add(Me.RulesTab)
Me.ResultsFormTabControl.Controls.Add(Me.StratTab)
Me.ResultsFormTabControl.Controls.Add(Me.AnalysisTab)
Me.ResultsFormTabControl.Controls.Add(Me.AllExceptionsTab)
Me.ResultsFormTabControl.Controls.Add(Me.ForcedTab)
Me.ResultsFormTabControl.Controls.Add(Me.EORTab)
Me.ResultsFormTabControl.Controls.Add(Me.SplitsTab)
Me.ResultsFormTabControl.Controls.Add(Me.OtherTab)
Me.ResultsFormTabControl.Location = New System.Drawing.Point(0, 0)
Me.ResultsFormTabControl.Name = "ResultsFormTabControl"
Me.ResultsFormTabControl.SelectedIndex = 0
Me.ResultsFormTabControl.Size = New System.Drawing.Size(854, 655)
Me.ResultsFormTabControl.TabIndex = 0
'
'SummaryTab
'
Me.SummaryTab.Controls.Add(Me.TextButtonSummTab)
Me.SummaryTab.Controls.Add(Me.ResultsFileLabelSummTab)
Me.SummaryTab.Controls.Add(Me.ResultsFilenameBoxSummTab)
Me.SummaryTab.Controls.Add(Me.NetEVLabelSummTab)
Me.SummaryTab.Controls.Add(Me.TCStrat1LabelSummTab)
Me.SummaryTab.Controls.Add(Me.CDStrat1LabelSummTab)
Me.SummaryTab.Controls.Add(Me.ForcedStrat1LabelSummTab)
Me.SummaryTab.Controls.Add(Me.TDStrat1LabelSummTab)
Me.SummaryTab.Controls.Add(Me.Prob2LabelSummTab)
Me.SummaryTab.Controls.Add(Me.TCStrat3LabelSummTab)
Me.SummaryTab.Controls.Add(Me.CDStrat3LabelSummTab)
Me.SummaryTab.Controls.Add(Me.ForcedStrat3LabelSummTab)
Me.SummaryTab.Controls.Add(Me.TDStrat3LabelSummTab)
Me.SummaryTab.Controls.Add(Me.Prob1LabelSummTab)
Me.SummaryTab.Controls.Add(Me.PlayerLabelSummTab)
Me.SummaryTab.Controls.Add(Me.DealerUCLabelSummTab)
Me.SummaryTab.Controls.Add(Me.TCStrat2LabelSummTab)
Me.SummaryTab.Controls.Add(Me.CDStrat2LabelSummTab)
Me.SummaryTab.Controls.Add(Me.ForcedStrat2LabelSummTab)
Me.SummaryTab.Controls.Add(Me.TDStrat2LabelSummTab)
Me.SummaryTab.Controls.Add(Me.TCEVBoxSummTab)
Me.SummaryTab.Controls.Add(Me.CDEVBoxSummTab)
Me.SummaryTab.Controls.Add(Me.ForcedEVBoxSummTab)
Me.SummaryTab.Controls.Add(Me.TDEVBoxSummTab)
Me.SummaryTab.Controls.Add(Me.LoadResultsButtonSummTab)
Me.SummaryTab.Controls.Add(Me.PrintToExelButtonSummTab)
Me.SummaryTab.Controls.Add(Me.SaveResultsButtonSummTab)
Me.SummaryTab.Location = New System.Drawing.Point(4, 25)
Me.SummaryTab.Name = "SummaryTab"
Me.SummaryTab.Size = New System.Drawing.Size(846, 626)
Me.SummaryTab.TabIndex = 0
Me.SummaryTab.Text = "Summary"
'
'TextButtonSummTab
'
Me.TextButtonSummTab.Location = New System.Drawing.Point(744, 568)
Me.TextButtonSummTab.Name = "TextButtonSummTab"
Me.TextButtonSummTab.Size = New System.Drawing.Size(72, 24)
Me.TextButtonSummTab.TabIndex = 181
Me.TextButtonSummTab.Text = "Text"
'
'ResultsFileLabelSummTab
'
Me.ResultsFileLabelSummTab.Location = New System.Drawing.Point(48, 526)
Me.ResultsFileLabelSummTab.Name = "ResultsFileLabelSummTab"
Me.ResultsFileLabelSummTab.Size = New System.Drawing.Size(163, 19)
Me.ResultsFileLabelSummTab.TabIndex = 180
Me.ResultsFileLabelSummTab.Text = "Results File Filename"
Me.ResultsFileLabelSummTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
Me.ResultsFileLabelSummTab.Visible = false
'
'ResultsFilenameBoxSummTab
'
Me.ResultsFilenameBoxSummTab.Location = New System.Drawing.Point(232, 526)
Me.ResultsFilenameBoxSummTab.Name = "ResultsFilenameBoxSummTab"
Me.ResultsFilenameBoxSummTab.ReadOnly = true
Me.ResultsFilenameBoxSummTab.Size = New System.Drawing.Size(154, 22)
Me.ResultsFilenameBoxSummTab.TabIndex = 179
Me.ResultsFilenameBoxSummTab.TabStop = false
Me.ResultsFilenameBoxSummTab.Text = ""
Me.ResultsFilenameBoxSummTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
Me.ResultsFilenameBoxSummTab.Visible = false
'
'NetEVLabelSummTab
'
Me.NetEVLabelSummTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.NetEVLabelSummTab.Location = New System.Drawing.Point(400, 9)
Me.NetEVLabelSummTab.Name = "NetEVLabelSummTab"
Me.NetEVLabelSummTab.Size = New System.Drawing.Size(168, 19)
Me.NetEVLabelSummTab.TabIndex = 38
Me.NetEVLabelSummTab.Text = "Net EV"
Me.NetEVLabelSummTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'TCStrat1LabelSummTab
'
Me.TCStrat1LabelSummTab.Location = New System.Drawing.Point(211, 65)
Me.TCStrat1LabelSummTab.Name = "TCStrat1LabelSummTab"
Me.TCStrat1LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.TCStrat1LabelSummTab.TabIndex = 37
Me.TCStrat1LabelSummTab.Text = "2 Card Dependent Strategy"
'
'CDStrat1LabelSummTab
'
Me.CDStrat1LabelSummTab.Location = New System.Drawing.Point(211, 92)
Me.CDStrat1LabelSummTab.Name = "CDStrat1LabelSummTab"
Me.CDStrat1LabelSummTab.Size = New System.Drawing.Size(173, 19)
Me.CDStrat1LabelSummTab.TabIndex = 36
Me.CDStrat1LabelSummTab.Text = "Composition Dependent"
'
'ForcedStrat1LabelSummTab
'
Me.ForcedStrat1LabelSummTab.Location = New System.Drawing.Point(211, 120)
Me.ForcedStrat1LabelSummTab.Name = "ForcedStrat1LabelSummTab"
Me.ForcedStrat1LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.ForcedStrat1LabelSummTab.TabIndex = 35
Me.ForcedStrat1LabelSummTab.Text = "Forced Strategy"
'
'TDStrat1LabelSummTab
'
Me.TDStrat1LabelSummTab.Location = New System.Drawing.Point(211, 37)
Me.TDStrat1LabelSummTab.Name = "TDStrat1LabelSummTab"
Me.TDStrat1LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.TDStrat1LabelSummTab.TabIndex = 34
Me.TDStrat1LabelSummTab.Text = "Total Dependent Strategy"
'
'Prob2LabelSummTab
'
Me.Prob2LabelSummTab.Location = New System.Drawing.Point(48, 369)
Me.Prob2LabelSummTab.Name = "Prob2LabelSummTab"
Me.Prob2LabelSummTab.Size = New System.Drawing.Size(173, 19)
Me.Prob2LabelSummTab.TabIndex = 33
Me.Prob2LabelSummTab.Text = "Probability"
'
'TCStrat3LabelSummTab
'
Me.TCStrat3LabelSummTab.Location = New System.Drawing.Point(48, 425)
Me.TCStrat3LabelSummTab.Name = "TCStrat3LabelSummTab"
Me.TCStrat3LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.TCStrat3LabelSummTab.TabIndex = 32
Me.TCStrat3LabelSummTab.Text = "2 Card Dependent Strategy"
'
'CDStrat3LabelSummTab
'
Me.CDStrat3LabelSummTab.Location = New System.Drawing.Point(48, 453)
Me.CDStrat3LabelSummTab.Name = "CDStrat3LabelSummTab"
Me.CDStrat3LabelSummTab.Size = New System.Drawing.Size(173, 19)
Me.CDStrat3LabelSummTab.TabIndex = 31
Me.CDStrat3LabelSummTab.Text = "Composition Dependent"
'
'ForcedStrat3LabelSummTab
'
Me.ForcedStrat3LabelSummTab.Location = New System.Drawing.Point(48, 481)
Me.ForcedStrat3LabelSummTab.Name = "ForcedStrat3LabelSummTab"
Me.ForcedStrat3LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.ForcedStrat3LabelSummTab.TabIndex = 30
Me.ForcedStrat3LabelSummTab.Text = "Forced Strategy"
'
'TDStrat3LabelSummTab
'
Me.TDStrat3LabelSummTab.Location = New System.Drawing.Point(48, 397)
Me.TDStrat3LabelSummTab.Name = "TDStrat3LabelSummTab"
Me.TDStrat3LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.TDStrat3LabelSummTab.TabIndex = 29
Me.TDStrat3LabelSummTab.Text = "Total Dependent Strategy"
'
'Prob1LabelSummTab
'
Me.Prob1LabelSummTab.Location = New System.Drawing.Point(48, 186)
Me.Prob1LabelSummTab.Name = "Prob1LabelSummTab"
Me.Prob1LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.Prob1LabelSummTab.TabIndex = 28
Me.Prob1LabelSummTab.Text = "Probability"
'
'PlayerLabelSummTab
'
Me.PlayerLabelSummTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.PlayerLabelSummTab.Location = New System.Drawing.Point(48, 342)
Me.PlayerLabelSummTab.Name = "PlayerLabelSummTab"
Me.PlayerLabelSummTab.Size = New System.Drawing.Size(134, 18)
Me.PlayerLabelSummTab.TabIndex = 27
Me.PlayerLabelSummTab.Text = "Player's First Card"
'
'DealerUCLabelSummTab
'
Me.DealerUCLabelSummTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.DealerUCLabelSummTab.Location = New System.Drawing.Point(48, 157)
Me.DealerUCLabelSummTab.Name = "DealerUCLabelSummTab"
Me.DealerUCLabelSummTab.Size = New System.Drawing.Size(106, 18)
Me.DealerUCLabelSummTab.TabIndex = 26
Me.DealerUCLabelSummTab.Text = "Dealer's Upcard"
'
'TCStrat2LabelSummTab
'
Me.TCStrat2LabelSummTab.Location = New System.Drawing.Point(48, 240)
Me.TCStrat2LabelSummTab.Name = "TCStrat2LabelSummTab"
Me.TCStrat2LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.TCStrat2LabelSummTab.TabIndex = 17
Me.TCStrat2LabelSummTab.Text = "2 Card Dependent Strategy"
'
'CDStrat2LabelSummTab
'
Me.CDStrat2LabelSummTab.Location = New System.Drawing.Point(48, 268)
Me.CDStrat2LabelSummTab.Name = "CDStrat2LabelSummTab"
Me.CDStrat2LabelSummTab.Size = New System.Drawing.Size(173, 18)
Me.CDStrat2LabelSummTab.TabIndex = 16
Me.CDStrat2LabelSummTab.Text = "Composition Dependent"
'
'ForcedStrat2LabelSummTab
'
Me.ForcedStrat2LabelSummTab.Location = New System.Drawing.Point(48, 296)
Me.ForcedStrat2LabelSummTab.Name = "ForcedStrat2LabelSummTab"
Me.ForcedStrat2LabelSummTab.Size = New System.Drawing.Size(173, 19)
Me.ForcedStrat2LabelSummTab.TabIndex = 15
Me.ForcedStrat2LabelSummTab.Text = "Forced Strategy"
'
'TDStrat2LabelSummTab
'
Me.TDStrat2LabelSummTab.Location = New System.Drawing.Point(48, 212)
Me.TDStrat2LabelSummTab.Name = "TDStrat2LabelSummTab"
Me.TDStrat2LabelSummTab.Size = New System.Drawing.Size(173, 19)
Me.TDStrat2LabelSummTab.TabIndex = 14
Me.TDStrat2LabelSummTab.Text = "Total Dependent Strategy"
'
'TCEVBoxSummTab
'
Me.TCEVBoxSummTab.Location = New System.Drawing.Point(394, 65)
Me.TCEVBoxSummTab.Name = "TCEVBoxSummTab"
Me.TCEVBoxSummTab.ReadOnly = true
Me.TCEVBoxSummTab.Size = New System.Drawing.Size(172, 22)
Me.TCEVBoxSummTab.TabIndex = 13
Me.TCEVBoxSummTab.TabStop = false
Me.TCEVBoxSummTab.Text = ""
Me.TCEVBoxSummTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CDEVBoxSummTab
'
Me.CDEVBoxSummTab.Location = New System.Drawing.Point(394, 92)
Me.CDEVBoxSummTab.Name = "CDEVBoxSummTab"
Me.CDEVBoxSummTab.ReadOnly = true
Me.CDEVBoxSummTab.Size = New System.Drawing.Size(172, 22)
Me.CDEVBoxSummTab.TabIndex = 12
Me.CDEVBoxSummTab.TabStop = false
Me.CDEVBoxSummTab.Text = ""
Me.CDEVBoxSummTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ForcedEVBoxSummTab
'
Me.ForcedEVBoxSummTab.Location = New System.Drawing.Point(394, 120)
Me.ForcedEVBoxSummTab.Name = "ForcedEVBoxSummTab"
Me.ForcedEVBoxSummTab.ReadOnly = true
Me.ForcedEVBoxSummTab.Size = New System.Drawing.Size(172, 22)
Me.ForcedEVBoxSummTab.TabIndex = 11
Me.ForcedEVBoxSummTab.TabStop = false
Me.ForcedEVBoxSummTab.Text = ""
Me.ForcedEVBoxSummTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TDEVBoxSummTab
'
Me.TDEVBoxSummTab.Location = New System.Drawing.Point(394, 37)
Me.TDEVBoxSummTab.Name = "TDEVBoxSummTab"
Me.TDEVBoxSummTab.ReadOnly = true
Me.TDEVBoxSummTab.Size = New System.Drawing.Size(172, 22)
Me.TDEVBoxSummTab.TabIndex = 10
Me.TDEVBoxSummTab.TabStop = false
Me.TDEVBoxSummTab.Text = ""
Me.TDEVBoxSummTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'LoadResultsButtonSummTab
'
Me.LoadResultsButtonSummTab.Location = New System.Drawing.Point(208, 563)
Me.LoadResultsButtonSummTab.Name = "LoadResultsButtonSummTab"
Me.LoadResultsButtonSummTab.Size = New System.Drawing.Size(154, 37)
Me.LoadResultsButtonSummTab.TabIndex = 1
Me.LoadResultsButtonSummTab.Text = "Load Results File"
Me.LoadResultsButtonSummTab.Visible = false
'
'PrintToExelButtonSummTab
'
Me.PrintToExelButtonSummTab.Location = New System.Drawing.Point(400, 560)
Me.PrintToExelButtonSummTab.Name = "PrintToExelButtonSummTab"
Me.PrintToExelButtonSummTab.Size = New System.Drawing.Size(154, 37)
Me.PrintToExelButtonSummTab.TabIndex = 2
Me.PrintToExelButtonSummTab.Text = "Send Results to Excel"
'
'SaveResultsButtonSummTab
'
Me.SaveResultsButtonSummTab.Location = New System.Drawing.Point(48, 563)
Me.SaveResultsButtonSummTab.Name = "SaveResultsButtonSummTab"
Me.SaveResultsButtonSummTab.Size = New System.Drawing.Size(153, 37)
Me.SaveResultsButtonSummTab.TabIndex = 0
Me.SaveResultsButtonSummTab.Text = "Save Results File"
Me.SaveResultsButtonSummTab.Visible = false
'
'StratTab
'
Me.StratTab.Controls.Add(Me.StratTabControlSTab)
Me.StratTab.Location = New System.Drawing.Point(4, 25)
Me.StratTab.Name = "StratTab"
Me.StratTab.Size = New System.Drawing.Size(846, 626)
Me.StratTab.TabIndex = 2
Me.StratTab.Text = "Strategies"
'
'StratTabControlSTab
'
Me.StratTabControlSTab.Controls.Add(Me.HardSoftTDTabSTab)
Me.StratTabControlSTab.Controls.Add(Me.SoftPairsCDTabSTab)
Me.StratTabControlSTab.Controls.Add(Me.HardCDTabSTab)
Me.StratTabControlSTab.Controls.Add(Me.SuitedTabSTab)
Me.StratTabControlSTab.Location = New System.Drawing.Point(10, 9)
Me.StratTabControlSTab.Name = "StratTabControlSTab"
Me.StratTabControlSTab.SelectedIndex = 0
Me.StratTabControlSTab.Size = New System.Drawing.Size(825, 609)
Me.StratTabControlSTab.TabIndex = 127
'
'HardSoftTDTabSTab
'
Me.HardSoftTDTabSTab.Controls.Add(Me.TextButton1SummTab)
Me.HardSoftTDTabSTab.Controls.Add(Me.NoteLabelSTab)
Me.HardSoftTDTabSTab.Controls.Add(Me.StratGroupTDHardSoftTab)
Me.HardSoftTDTabSTab.Controls.Add(Me.SoftTDGroupSTab)
Me.HardSoftTDTabSTab.Controls.Add(Me.HardTDGroupSTab)
Me.HardSoftTDTabSTab.Location = New System.Drawing.Point(4, 25)
Me.HardSoftTDTabSTab.Name = "HardSoftTDTabSTab"
Me.HardSoftTDTabSTab.Size = New System.Drawing.Size(817, 580)
Me.HardSoftTDTabSTab.TabIndex = 5
Me.HardSoftTDTabSTab.Text = "TD Hard/Soft"
Me.HardSoftTDTabSTab.Visible = false
'
'TextButton1SummTab
'
Me.TextButton1SummTab.Location = New System.Drawing.Point(728, 536)
Me.TextButton1SummTab.Name = "TextButton1SummTab"
Me.TextButton1SummTab.Size = New System.Drawing.Size(72, 24)
Me.TextButton1SummTab.TabIndex = 227
Me.TextButton1SummTab.Text = "Text"
'
'NoteLabelSTab
'
Me.NoteLabelSTab.Location = New System.Drawing.Point(442, 328)
Me.NoteLabelSTab.Name = "NoteLabelSTab"
Me.NoteLabelSTab.Size = New System.Drawing.Size(355, 18)
Me.NoteLabelSTab.TabIndex = 226
Me.NoteLabelSTab.Text = "*Note: Click on a strategy to get the corresponding EVs."
'
'StratGroupTDHardSoftTab
'
Me.StratGroupTDHardSoftTab.Controls.Add(Me.CDButtonTDHardSoftTab)
Me.StratGroupTDHardSoftTab.Controls.Add(Me.TCButtonTDHardSoftTab)
Me.StratGroupTDHardSoftTab.Controls.Add(Me.ForcedButtonTDHardSoftTab)
Me.StratGroupTDHardSoftTab.Controls.Add(Me.TDButtonTDHardSoftTab)
Me.StratGroupTDHardSoftTab.Location = New System.Drawing.Point(424, 480)
Me.StratGroupTDHardSoftTab.Name = "StratGroupTDHardSoftTab"
Me.StratGroupTDHardSoftTab.Size = New System.Drawing.Size(288, 83)
Me.StratGroupTDHardSoftTab.TabIndex = 0
Me.StratGroupTDHardSoftTab.TabStop = false
Me.StratGroupTDHardSoftTab.Text = "Strategy"
'
'CDButtonTDHardSoftTab
'
Me.CDButtonTDHardSoftTab.Location = New System.Drawing.Point(16, 56)
Me.CDButtonTDHardSoftTab.Name = "CDButtonTDHardSoftTab"
Me.CDButtonTDHardSoftTab.Size = New System.Drawing.Size(105, 19)
Me.CDButtonTDHardSoftTab.TabIndex = 2
Me.CDButtonTDHardSoftTab.Text = "CD Strategy"
'
'TCButtonTDHardSoftTab
'
Me.TCButtonTDHardSoftTab.Location = New System.Drawing.Point(152, 24)
Me.TCButtonTDHardSoftTab.Name = "TCButtonTDHardSoftTab"
Me.TCButtonTDHardSoftTab.Size = New System.Drawing.Size(125, 18)
Me.TCButtonTDHardSoftTab.TabIndex = 1
Me.TCButtonTDHardSoftTab.Text = "2-Card Strategy"
'
'ForcedButtonTDHardSoftTab
'
Me.ForcedButtonTDHardSoftTab.Location = New System.Drawing.Point(152, 56)
Me.ForcedButtonTDHardSoftTab.Name = "ForcedButtonTDHardSoftTab"
Me.ForcedButtonTDHardSoftTab.Size = New System.Drawing.Size(125, 19)
Me.ForcedButtonTDHardSoftTab.TabIndex = 3
Me.ForcedButtonTDHardSoftTab.Text = "Forced Strategy"
'
'TDButtonTDHardSoftTab
'
Me.TDButtonTDHardSoftTab.Location = New System.Drawing.Point(16, 24)
Me.TDButtonTDHardSoftTab.Name = "TDButtonTDHardSoftTab"
Me.TDButtonTDHardSoftTab.Size = New System.Drawing.Size(105, 18)
Me.TDButtonTDHardSoftTab.TabIndex = 0
Me.TDButtonTDHardSoftTab.Text = "TD Strategy"
'
'SoftTDGroupSTab
'
Me.SoftTDGroupSTab.Controls.Add(Me.SoftTotalTDLabelSTab)
Me.SoftTDGroupSTab.Location = New System.Drawing.Point(432, 9)
Me.SoftTDGroupSTab.Name = "SoftTDGroupSTab"
Me.SoftTDGroupSTab.Size = New System.Drawing.Size(355, 303)
Me.SoftTDGroupSTab.TabIndex = 2
Me.SoftTDGroupSTab.TabStop = false
Me.SoftTDGroupSTab.Text = "Soft TD Strategy"
'
'SoftTotalTDLabelSTab
'
Me.SoftTotalTDLabelSTab.Location = New System.Drawing.Point(10, 18)
Me.SoftTotalTDLabelSTab.Name = "SoftTotalTDLabelSTab"
Me.SoftTotalTDLabelSTab.Size = New System.Drawing.Size(38, 19)
Me.SoftTotalTDLabelSTab.TabIndex = 50
Me.SoftTotalTDLabelSTab.Text = "Total"
'
'HardTDGroupSTab
'
Me.HardTDGroupSTab.Controls.Add(Me.HardTotalTDLabelSTab)
Me.HardTDGroupSTab.Location = New System.Drawing.Point(29, 9)
Me.HardTDGroupSTab.Name = "HardTDGroupSTab"
Me.HardTDGroupSTab.Size = New System.Drawing.Size(355, 495)
Me.HardTDGroupSTab.TabIndex = 1
Me.HardTDGroupSTab.TabStop = false
Me.HardTDGroupSTab.Text = "Hard TD Strategy"
'
'HardTotalTDLabelSTab
'
Me.HardTotalTDLabelSTab.Location = New System.Drawing.Point(10, 18)
Me.HardTotalTDLabelSTab.Name = "HardTotalTDLabelSTab"
Me.HardTotalTDLabelSTab.Size = New System.Drawing.Size(38, 19)
Me.HardTotalTDLabelSTab.TabIndex = 222
Me.HardTotalTDLabelSTab.Text = "Total"
Me.HardTotalTDLabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'SoftPairsCDTabSTab
'
Me.SoftPairsCDTabSTab.Controls.Add(Me.TextButton2SummTab)
Me.SoftPairsCDTabSTab.Controls.Add(Me.EVsBoxSTab)
Me.SoftPairsCDTabSTab.Controls.Add(Me.StratGroupTDSoftPairsTab)
Me.SoftPairsCDTabSTab.Controls.Add(Me.PairCDGroupSTab)
Me.SoftPairsCDTabSTab.Controls.Add(Me.SoftCDGroupSTab)
Me.SoftPairsCDTabSTab.Location = New System.Drawing.Point(4, 25)
Me.SoftPairsCDTabSTab.Name = "SoftPairsCDTabSTab"
Me.SoftPairsCDTabSTab.Size = New System.Drawing.Size(817, 580)
Me.SoftPairsCDTabSTab.TabIndex = 1
Me.SoftPairsCDTabSTab.Text = "CD Soft/Pairs"
Me.SoftPairsCDTabSTab.Visible = false
'
'TextButton2SummTab
'
Me.TextButton2SummTab.Location = New System.Drawing.Point(728, 536)
Me.TextButton2SummTab.Name = "TextButton2SummTab"
Me.TextButton2SummTab.Size = New System.Drawing.Size(72, 24)
Me.TextButton2SummTab.TabIndex = 228
Me.TextButton2SummTab.Text = "Text"
'
'EVsBoxSTab
'
Me.EVsBoxSTab.Controls.Add(Me.BJStandEVBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.BJStandLabelBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.SplitEVBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.SurrEVBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.SurrLabelBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.ProbLabelBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.DoubleEVBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.HitEVBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.StandEVBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.ProbBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.StandLabelBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.HitLabelBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.DoubleLabelBoxSTab)
Me.EVsBoxSTab.Controls.Add(Me.SplitLabelBoxSTab)
Me.EVsBoxSTab.Location = New System.Drawing.Point(29, 360)
Me.EVsBoxSTab.Name = "EVsBoxSTab"
Me.EVsBoxSTab.Size = New System.Drawing.Size(211, 157)
Me.EVsBoxSTab.TabIndex = 0
Me.EVsBoxSTab.TabStop = false
Me.EVsBoxSTab.Visible = false
'
'BJStandEVBoxSTab
'
Me.BJStandEVBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.BJStandEVBoxSTab.Location = New System.Drawing.Point(106, 129)
Me.BJStandEVBoxSTab.Name = "BJStandEVBoxSTab"
Me.BJStandEVBoxSTab.ReadOnly = true
Me.BJStandEVBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.BJStandEVBoxSTab.TabIndex = 25
Me.BJStandEVBoxSTab.TabStop = false
Me.BJStandEVBoxSTab.Text = ""
'
'BJStandLabelBoxSTab
'
Me.BJStandLabelBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.BJStandLabelBoxSTab.Location = New System.Drawing.Point(10, 129)
Me.BJStandLabelBoxSTab.Name = "BJStandLabelBoxSTab"
Me.BJStandLabelBoxSTab.ReadOnly = true
Me.BJStandLabelBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.BJStandLabelBoxSTab.TabIndex = 24
Me.BJStandLabelBoxSTab.TabStop = false
Me.BJStandLabelBoxSTab.Text = ""
'
'SplitEVBoxSTab
'
Me.SplitEVBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.SplitEVBoxSTab.Location = New System.Drawing.Point(106, 111)
Me.SplitEVBoxSTab.Name = "SplitEVBoxSTab"
Me.SplitEVBoxSTab.ReadOnly = true
Me.SplitEVBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.SplitEVBoxSTab.TabIndex = 23
Me.SplitEVBoxSTab.TabStop = false
Me.SplitEVBoxSTab.Text = ""
'
'SurrEVBoxSTab
'
Me.SurrEVBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.SurrEVBoxSTab.Location = New System.Drawing.Point(106, 92)
Me.SurrEVBoxSTab.Name = "SurrEVBoxSTab"
Me.SurrEVBoxSTab.ReadOnly = true
Me.SurrEVBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.SurrEVBoxSTab.TabIndex = 22
Me.SurrEVBoxSTab.TabStop = false
Me.SurrEVBoxSTab.Text = ""
'
'SurrLabelBoxSTab
'
Me.SurrLabelBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.SurrLabelBoxSTab.Location = New System.Drawing.Point(10, 92)
Me.SurrLabelBoxSTab.Name = "SurrLabelBoxSTab"
Me.SurrLabelBoxSTab.ReadOnly = true
Me.SurrLabelBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.SurrLabelBoxSTab.TabIndex = 21
Me.SurrLabelBoxSTab.TabStop = false
Me.SurrLabelBoxSTab.Text = ""
'
'ProbLabelBoxSTab
'
Me.ProbLabelBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.ProbLabelBoxSTab.Location = New System.Drawing.Point(10, 18)
Me.ProbLabelBoxSTab.Name = "ProbLabelBoxSTab"
Me.ProbLabelBoxSTab.ReadOnly = true
Me.ProbLabelBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.ProbLabelBoxSTab.TabIndex = 13
Me.ProbLabelBoxSTab.TabStop = false
Me.ProbLabelBoxSTab.Text = ""
'
'DoubleEVBoxSTab
'
Me.DoubleEVBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.DoubleEVBoxSTab.Location = New System.Drawing.Point(106, 74)
Me.DoubleEVBoxSTab.Name = "DoubleEVBoxSTab"
Me.DoubleEVBoxSTab.ReadOnly = true
Me.DoubleEVBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.DoubleEVBoxSTab.TabIndex = 20
Me.DoubleEVBoxSTab.TabStop = false
Me.DoubleEVBoxSTab.Text = ""
'
'HitEVBoxSTab
'
Me.HitEVBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.HitEVBoxSTab.Location = New System.Drawing.Point(106, 55)
Me.HitEVBoxSTab.Name = "HitEVBoxSTab"
Me.HitEVBoxSTab.ReadOnly = true
Me.HitEVBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.HitEVBoxSTab.TabIndex = 19
Me.HitEVBoxSTab.TabStop = false
Me.HitEVBoxSTab.Text = ""
'
'StandEVBoxSTab
'
Me.StandEVBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.StandEVBoxSTab.Location = New System.Drawing.Point(106, 37)
Me.StandEVBoxSTab.Name = "StandEVBoxSTab"
Me.StandEVBoxSTab.ReadOnly = true
Me.StandEVBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.StandEVBoxSTab.TabIndex = 18
Me.StandEVBoxSTab.TabStop = false
Me.StandEVBoxSTab.Text = ""
'
'ProbBoxSTab
'
Me.ProbBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.ProbBoxSTab.Location = New System.Drawing.Point(106, 18)
Me.ProbBoxSTab.Name = "ProbBoxSTab"
Me.ProbBoxSTab.ReadOnly = true
Me.ProbBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.ProbBoxSTab.TabIndex = 17
Me.ProbBoxSTab.TabStop = false
Me.ProbBoxSTab.Text = ""
'
'StandLabelBoxSTab
'
Me.StandLabelBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.StandLabelBoxSTab.Location = New System.Drawing.Point(10, 37)
Me.StandLabelBoxSTab.Name = "StandLabelBoxSTab"
Me.StandLabelBoxSTab.ReadOnly = true
Me.StandLabelBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.StandLabelBoxSTab.TabIndex = 14
Me.StandLabelBoxSTab.TabStop = false
Me.StandLabelBoxSTab.Text = ""
'
'HitLabelBoxSTab
'
Me.HitLabelBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.HitLabelBoxSTab.Location = New System.Drawing.Point(10, 55)
Me.HitLabelBoxSTab.Name = "HitLabelBoxSTab"
Me.HitLabelBoxSTab.ReadOnly = true
Me.HitLabelBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.HitLabelBoxSTab.TabIndex = 15
Me.HitLabelBoxSTab.TabStop = false
Me.HitLabelBoxSTab.Text = ""
'
'DoubleLabelBoxSTab
'
Me.DoubleLabelBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.DoubleLabelBoxSTab.Location = New System.Drawing.Point(10, 74)
Me.DoubleLabelBoxSTab.Name = "DoubleLabelBoxSTab"
Me.DoubleLabelBoxSTab.ReadOnly = true
Me.DoubleLabelBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.DoubleLabelBoxSTab.TabIndex = 16
Me.DoubleLabelBoxSTab.TabStop = false
Me.DoubleLabelBoxSTab.Text = ""
'
'SplitLabelBoxSTab
'
Me.SplitLabelBoxSTab.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.SplitLabelBoxSTab.Location = New System.Drawing.Point(10, 111)
Me.SplitLabelBoxSTab.Name = "SplitLabelBoxSTab"
Me.SplitLabelBoxSTab.ReadOnly = true
Me.SplitLabelBoxSTab.Size = New System.Drawing.Size(96, 15)
Me.SplitLabelBoxSTab.TabIndex = 22
Me.SplitLabelBoxSTab.TabStop = false
Me.SplitLabelBoxSTab.Text = ""
'
'StratGroupTDSoftPairsTab
'
Me.StratGroupTDSoftPairsTab.Controls.Add(Me.CDButtonCDSoftPairsTab)
Me.StratGroupTDSoftPairsTab.Controls.Add(Me.TCButtonCDSoftPairsTab)
Me.StratGroupTDSoftPairsTab.Controls.Add(Me.ForcedButtonCDSoftPairsTab)
Me.StratGroupTDSoftPairsTab.Controls.Add(Me.TDButtonCDSoftPairsTab)
Me.StratGroupTDSoftPairsTab.Location = New System.Drawing.Point(125, 526)
Me.StratGroupTDSoftPairsTab.Name = "StratGroupTDSoftPairsTab"
Me.StratGroupTDSoftPairsTab.Size = New System.Drawing.Size(576, 46)
Me.StratGroupTDSoftPairsTab.TabIndex = 0
Me.StratGroupTDSoftPairsTab.TabStop = false
Me.StratGroupTDSoftPairsTab.Text = "Strategy"
'
'CDButtonCDSoftPairsTab
'
Me.CDButtonCDSoftPairsTab.Location = New System.Drawing.Point(298, 18)
Me.CDButtonCDSoftPairsTab.Name = "CDButtonCDSoftPairsTab"
Me.CDButtonCDSoftPairsTab.Size = New System.Drawing.Size(105, 19)
Me.CDButtonCDSoftPairsTab.TabIndex = 2
Me.CDButtonCDSoftPairsTab.Text = "CD Strategy"
'
'TCButtonCDSoftPairsTab
'
Me.TCButtonCDSoftPairsTab.Location = New System.Drawing.Point(163, 18)
Me.TCButtonCDSoftPairsTab.Name = "TCButtonCDSoftPairsTab"
Me.TCButtonCDSoftPairsTab.Size = New System.Drawing.Size(125, 19)
Me.TCButtonCDSoftPairsTab.TabIndex = 1
Me.TCButtonCDSoftPairsTab.Text = "2-Card Strategy"
'
'ForcedButtonCDSoftPairsTab
'
Me.ForcedButtonCDSoftPairsTab.Location = New System.Drawing.Point(432, 18)
Me.ForcedButtonCDSoftPairsTab.Name = "ForcedButtonCDSoftPairsTab"
Me.ForcedButtonCDSoftPairsTab.Size = New System.Drawing.Size(125, 19)
Me.ForcedButtonCDSoftPairsTab.TabIndex = 3
Me.ForcedButtonCDSoftPairsTab.Text = "Forced Strategy"
'
'TDButtonCDSoftPairsTab
'
Me.TDButtonCDSoftPairsTab.Location = New System.Drawing.Point(29, 18)
Me.TDButtonCDSoftPairsTab.Name = "TDButtonCDSoftPairsTab"
Me.TDButtonCDSoftPairsTab.Size = New System.Drawing.Size(105, 19)
Me.TDButtonCDSoftPairsTab.TabIndex = 0
Me.TDButtonCDSoftPairsTab.Text = "TD Strategy"
'
'PairCDGroupSTab
'
Me.PairCDGroupSTab.Controls.Add(Me.PairForcedCDLabelSTab)
Me.PairCDGroupSTab.Location = New System.Drawing.Point(432, 9)
Me.PairCDGroupSTab.Name = "PairCDGroupSTab"
Me.PairCDGroupSTab.Size = New System.Drawing.Size(355, 303)
Me.PairCDGroupSTab.TabIndex = 11
Me.PairCDGroupSTab.TabStop = false
Me.PairCDGroupSTab.Text = "Pairs CD Strategy"
'
'PairForcedCDLabelSTab
'
Me.PairForcedCDLabelSTab.Location = New System.Drawing.Point(10, 18)
Me.PairForcedCDLabelSTab.Name = "PairForcedCDLabelSTab"
Me.PairForcedCDLabelSTab.Size = New System.Drawing.Size(38, 19)
Me.PairForcedCDLabelSTab.TabIndex = 50
Me.PairForcedCDLabelSTab.Text = "Pair"
'
'SoftCDGroupSTab
'
Me.SoftCDGroupSTab.Controls.Add(Me.TotalForcedCDLabelSTab)
Me.SoftCDGroupSTab.Location = New System.Drawing.Point(29, 9)
Me.SoftCDGroupSTab.Name = "SoftCDGroupSTab"
Me.SoftCDGroupSTab.Size = New System.Drawing.Size(355, 279)
Me.SoftCDGroupSTab.TabIndex = 10
Me.SoftCDGroupSTab.TabStop = false
Me.SoftCDGroupSTab.Text = "Soft CD Strategy"
'
'TotalForcedCDLabelSTab
'
Me.TotalForcedCDLabelSTab.Location = New System.Drawing.Point(10, 18)
Me.TotalForcedCDLabelSTab.Name = "TotalForcedCDLabelSTab"
Me.TotalForcedCDLabelSTab.Size = New System.Drawing.Size(38, 19)
Me.TotalForcedCDLabelSTab.TabIndex = 50
Me.TotalForcedCDLabelSTab.Text = "Total"
'
'HardCDTabSTab
'
Me.HardCDTabSTab.Controls.Add(Me.TextButton3STab)
Me.HardCDTabSTab.Controls.Add(Me.StratGroupCDHardTab)
Me.HardCDTabSTab.Controls.Add(Me.HardCDHand2LabelSTab)
Me.HardCDTabSTab.Controls.Add(Me.HardCDHand1LabelSTab)
Me.HardCDTabSTab.Location = New System.Drawing.Point(4, 25)
Me.HardCDTabSTab.Name = "HardCDTabSTab"
Me.HardCDTabSTab.Size = New System.Drawing.Size(817, 580)
Me.HardCDTabSTab.TabIndex = 0
Me.HardCDTabSTab.Text = "CD Hard"
Me.HardCDTabSTab.Visible = false
'
'TextButton3STab
'
Me.TextButton3STab.Location = New System.Drawing.Point(728, 536)
Me.TextButton3STab.Name = "TextButton3STab"
Me.TextButton3STab.Size = New System.Drawing.Size(72, 24)
Me.TextButton3STab.TabIndex = 228
Me.TextButton3STab.Text = "Text"
'
'StratGroupCDHardTab
'
Me.StratGroupCDHardTab.Controls.Add(Me.CDButtonCDHardTab)
Me.StratGroupCDHardTab.Controls.Add(Me.TCButtonCDHardTab)
Me.StratGroupCDHardTab.Controls.Add(Me.ForcedButtonCDHardTab)
Me.StratGroupCDHardTab.Controls.Add(Me.TDButtonCDHardTab)
Me.StratGroupCDHardTab.Location = New System.Drawing.Point(125, 526)
Me.StratGroupCDHardTab.Name = "StratGroupCDHardTab"
Me.StratGroupCDHardTab.Size = New System.Drawing.Size(576, 46)
Me.StratGroupCDHardTab.TabIndex = 0
Me.StratGroupCDHardTab.TabStop = false
Me.StratGroupCDHardTab.Text = "Strategy"
'
'CDButtonCDHardTab
'
Me.CDButtonCDHardTab.Location = New System.Drawing.Point(298, 18)
Me.CDButtonCDHardTab.Name = "CDButtonCDHardTab"
Me.CDButtonCDHardTab.Size = New System.Drawing.Size(105, 19)
Me.CDButtonCDHardTab.TabIndex = 2
Me.CDButtonCDHardTab.Text = "CD Strategy"
'
'TCButtonCDHardTab
'
Me.TCButtonCDHardTab.Location = New System.Drawing.Point(163, 18)
Me.TCButtonCDHardTab.Name = "TCButtonCDHardTab"
Me.TCButtonCDHardTab.Size = New System.Drawing.Size(125, 19)
Me.TCButtonCDHardTab.TabIndex = 1
Me.TCButtonCDHardTab.Text = "2-Card Strategy"
'
'ForcedButtonCDHardTab
'
Me.ForcedButtonCDHardTab.Location = New System.Drawing.Point(432, 18)
Me.ForcedButtonCDHardTab.Name = "ForcedButtonCDHardTab"
Me.ForcedButtonCDHardTab.Size = New System.Drawing.Size(125, 19)
Me.ForcedButtonCDHardTab.TabIndex = 3
Me.ForcedButtonCDHardTab.Text = "Forced Strategy"
'
'TDButtonCDHardTab
'
Me.TDButtonCDHardTab.Location = New System.Drawing.Point(29, 18)
Me.TDButtonCDHardTab.Name = "TDButtonCDHardTab"
Me.TDButtonCDHardTab.Size = New System.Drawing.Size(105, 19)
Me.TDButtonCDHardTab.TabIndex = 0
Me.TDButtonCDHardTab.Text = "TD Strategy"
'
'HardCDHand2LabelSTab
'
Me.HardCDHand2LabelSTab.Location = New System.Drawing.Point(440, 16)
Me.HardCDHand2LabelSTab.Name = "HardCDHand2LabelSTab"
Me.HardCDHand2LabelSTab.Size = New System.Drawing.Size(38, 19)
Me.HardCDHand2LabelSTab.TabIndex = 168
Me.HardCDHand2LabelSTab.Text = "Hand"
Me.HardCDHand2LabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'HardCDHand1LabelSTab
'
Me.HardCDHand1LabelSTab.Location = New System.Drawing.Point(24, 16)
Me.HardCDHand1LabelSTab.Name = "HardCDHand1LabelSTab"
Me.HardCDHand1LabelSTab.Size = New System.Drawing.Size(38, 19)
Me.HardCDHand1LabelSTab.TabIndex = 113
Me.HardCDHand1LabelSTab.Text = "Hand"
Me.HardCDHand1LabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'SuitedTabSTab
'
Me.SuitedTabSTab.Controls.Add(Me.UCComboBoxSTab)
Me.SuitedTabSTab.Controls.Add(Me.ListSizeLabelSTab)
Me.SuitedTabSTab.Controls.Add(Me.ListSizeBoxSTab)
Me.SuitedTabSTab.Controls.Add(Me.UpcardLabelSTab)
Me.SuitedTabSTab.Controls.Add(Me.HandListBoxSTab)
Me.SuitedTabSTab.Controls.Add(Me.HandDetailsGroupSTab)
Me.SuitedTabSTab.Location = New System.Drawing.Point(4, 25)
Me.SuitedTabSTab.Name = "SuitedTabSTab"
Me.SuitedTabSTab.Size = New System.Drawing.Size(817, 580)
Me.SuitedTabSTab.TabIndex = 6
Me.SuitedTabSTab.Text = "Suited Hands"
'
'UCComboBoxSTab
'
Me.UCComboBoxSTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UCComboBoxSTab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UCComboBoxSTab.Location = New System.Drawing.Point(413, 18)
Me.UCComboBoxSTab.Name = "UCComboBoxSTab"
Me.UCComboBoxSTab.Size = New System.Drawing.Size(57, 24)
Me.UCComboBoxSTab.TabIndex = 96
'
'ListSizeLabelSTab
'
Me.ListSizeLabelSTab.Location = New System.Drawing.Point(298, 185)
Me.ListSizeLabelSTab.Name = "ListSizeLabelSTab"
Me.ListSizeLabelSTab.Size = New System.Drawing.Size(182, 27)
Me.ListSizeLabelSTab.TabIndex = 107
Me.ListSizeLabelSTab.Text = "Hands meeting above criteria"
Me.ListSizeLabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ListSizeBoxSTab
'
Me.ListSizeBoxSTab.Location = New System.Drawing.Point(480, 185)
Me.ListSizeBoxSTab.Name = "ListSizeBoxSTab"
Me.ListSizeBoxSTab.ReadOnly = true
Me.ListSizeBoxSTab.Size = New System.Drawing.Size(48, 22)
Me.ListSizeBoxSTab.TabIndex = 106
Me.ListSizeBoxSTab.TabStop = false
Me.ListSizeBoxSTab.Text = ""
Me.ListSizeBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UpcardLabelSTab
'
Me.UpcardLabelSTab.Location = New System.Drawing.Point(346, 18)
Me.UpcardLabelSTab.Name = "UpcardLabelSTab"
Me.UpcardLabelSTab.Size = New System.Drawing.Size(57, 19)
Me.UpcardLabelSTab.TabIndex = 103
Me.UpcardLabelSTab.Text = "Upcard"
'
'HandListBoxSTab
'
Me.HandListBoxSTab.ItemHeight = 16
Me.HandListBoxSTab.Location = New System.Drawing.Point(221, 55)
Me.HandListBoxSTab.Name = "HandListBoxSTab"
Me.HandListBoxSTab.Size = New System.Drawing.Size(384, 116)
Me.HandListBoxSTab.TabIndex = 99
'
'HandDetailsGroupSTab
'
Me.HandDetailsGroupSTab.Controls.Add(Me.BonusBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.BonusLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.NetBJEVLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.NetBJEVBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.NetProbLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.ProbDetailsBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.NetEVLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.NetEVBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.SpadesLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.BJStandBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.BJStandLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.UCDetailsBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.UCDetailsLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.NCardsDetailLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.NCardsDetailsBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.SoftDetailsCheckSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.SoftDetailsLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.DiamondsLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.CDLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.ClubsLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.HeartsLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.CDStratBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.StandBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.DoubleBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.SurrenderBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.CDSplitBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.CDHitBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.NonSuitedProbDetailsBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.SplitLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.StratLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.HitLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.SurrLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.DoubleLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.StandLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.ProbLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.HandLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.HandNameBoxSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.TotalDetailsLabelSTab)
Me.HandDetailsGroupSTab.Controls.Add(Me.TotalDetailsBoxSTab)
Me.HandDetailsGroupSTab.Location = New System.Drawing.Point(10, 222)
Me.HandDetailsGroupSTab.Name = "HandDetailsGroupSTab"
Me.HandDetailsGroupSTab.Size = New System.Drawing.Size(796, 350)
Me.HandDetailsGroupSTab.TabIndex = 100
Me.HandDetailsGroupSTab.TabStop = false
Me.HandDetailsGroupSTab.Text = "Hand Details"
'
'BonusBoxSTab
'
Me.BonusBoxSTab.Location = New System.Drawing.Point(144, 260)
Me.BonusBoxSTab.Name = "BonusBoxSTab"
Me.BonusBoxSTab.ReadOnly = true
Me.BonusBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.BonusBoxSTab.TabIndex = 94
Me.BonusBoxSTab.TabStop = false
Me.BonusBoxSTab.Text = ""
Me.BonusBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BonusLabelSTab
'
Me.BonusLabelSTab.Location = New System.Drawing.Point(38, 260)
Me.BonusLabelSTab.Name = "BonusLabelSTab"
Me.BonusLabelSTab.Size = New System.Drawing.Size(96, 19)
Me.BonusLabelSTab.TabIndex = 93
Me.BonusLabelSTab.Text = "Bonus EV"
'
'NetBJEVLabelSTab
'
Me.NetBJEVLabelSTab.Location = New System.Drawing.Point(288, 65)
Me.NetBJEVLabelSTab.Name = "NetBJEVLabelSTab"
Me.NetBJEVLabelSTab.Size = New System.Drawing.Size(67, 18)
Me.NetBJEVLabelSTab.TabIndex = 92
Me.NetBJEVLabelSTab.Text = "Net BJ EV"
'
'NetBJEVBoxSTab
'
Me.NetBJEVBoxSTab.Location = New System.Drawing.Point(365, 65)
Me.NetBJEVBoxSTab.Name = "NetBJEVBoxSTab"
Me.NetBJEVBoxSTab.ReadOnly = true
Me.NetBJEVBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.NetBJEVBoxSTab.TabIndex = 91
Me.NetBJEVBoxSTab.TabStop = false
Me.NetBJEVBoxSTab.Text = ""
Me.NetBJEVBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'NetProbLabelSTab
'
Me.NetProbLabelSTab.Location = New System.Drawing.Point(509, 65)
Me.NetProbLabelSTab.Name = "NetProbLabelSTab"
Me.NetProbLabelSTab.Size = New System.Drawing.Size(67, 18)
Me.NetProbLabelSTab.TabIndex = 90
Me.NetProbLabelSTab.Text = "Net Prob"
'
'ProbDetailsBoxSTab
'
Me.ProbDetailsBoxSTab.Location = New System.Drawing.Point(576, 65)
Me.ProbDetailsBoxSTab.Name = "ProbDetailsBoxSTab"
Me.ProbDetailsBoxSTab.ReadOnly = true
Me.ProbDetailsBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.ProbDetailsBoxSTab.TabIndex = 89
Me.ProbDetailsBoxSTab.TabStop = false
Me.ProbDetailsBoxSTab.Text = ""
Me.ProbDetailsBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'NetEVLabelSTab
'
Me.NetEVLabelSTab.Location = New System.Drawing.Point(86, 65)
Me.NetEVLabelSTab.Name = "NetEVLabelSTab"
Me.NetEVLabelSTab.Size = New System.Drawing.Size(48, 18)
Me.NetEVLabelSTab.TabIndex = 88
Me.NetEVLabelSTab.Text = "Net EV"
'
'NetEVBoxSTab
'
Me.NetEVBoxSTab.Location = New System.Drawing.Point(144, 65)
Me.NetEVBoxSTab.Name = "NetEVBoxSTab"
Me.NetEVBoxSTab.ReadOnly = true
Me.NetEVBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.NetEVBoxSTab.TabIndex = 87
Me.NetEVBoxSTab.TabStop = false
Me.NetEVBoxSTab.Text = ""
Me.NetEVBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SpadesLabelSTab
'
Me.SpadesLabelSTab.Location = New System.Drawing.Point(272, 96)
Me.SpadesLabelSTab.Name = "SpadesLabelSTab"
Me.SpadesLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.SpadesLabelSTab.TabIndex = 86
Me.SpadesLabelSTab.Text = "Spades"
Me.SpadesLabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'BJStandBoxSTab
'
Me.BJStandBoxSTab.Location = New System.Drawing.Point(144, 232)
Me.BJStandBoxSTab.Name = "BJStandBoxSTab"
Me.BJStandBoxSTab.ReadOnly = true
Me.BJStandBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.BJStandBoxSTab.TabIndex = 84
Me.BJStandBoxSTab.TabStop = false
Me.BJStandBoxSTab.Text = ""
Me.BJStandBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BJStandLabelSTab
'
Me.BJStandLabelSTab.Location = New System.Drawing.Point(38, 232)
Me.BJStandLabelSTab.Name = "BJStandLabelSTab"
Me.BJStandLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.BJStandLabelSTab.TabIndex = 83
Me.BJStandLabelSTab.Text = "BJ Stand EV"
'
'UCDetailsBoxSTab
'
Me.UCDetailsBoxSTab.Location = New System.Drawing.Point(701, 28)
Me.UCDetailsBoxSTab.Name = "UCDetailsBoxSTab"
Me.UCDetailsBoxSTab.ReadOnly = true
Me.UCDetailsBoxSTab.Size = New System.Drawing.Size(38, 22)
Me.UCDetailsBoxSTab.TabIndex = 82
Me.UCDetailsBoxSTab.TabStop = false
Me.UCDetailsBoxSTab.Text = ""
Me.UCDetailsBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UCDetailsLabelSTab
'
Me.UCDetailsLabelSTab.Location = New System.Drawing.Point(634, 28)
Me.UCDetailsLabelSTab.Name = "UCDetailsLabelSTab"
Me.UCDetailsLabelSTab.Size = New System.Drawing.Size(57, 18)
Me.UCDetailsLabelSTab.TabIndex = 81
Me.UCDetailsLabelSTab.Text = "Upcard"
'
'NCardsDetailLabelSTab
'
Me.NCardsDetailLabelSTab.Location = New System.Drawing.Point(509, 28)
Me.NCardsDetailLabelSTab.Name = "NCardsDetailLabelSTab"
Me.NCardsDetailLabelSTab.Size = New System.Drawing.Size(57, 18)
Me.NCardsDetailLabelSTab.TabIndex = 75
Me.NCardsDetailLabelSTab.Text = "N Cards"
'
'NCardsDetailsBoxSTab
'
Me.NCardsDetailsBoxSTab.Location = New System.Drawing.Point(576, 28)
Me.NCardsDetailsBoxSTab.Name = "NCardsDetailsBoxSTab"
Me.NCardsDetailsBoxSTab.ReadOnly = true
Me.NCardsDetailsBoxSTab.Size = New System.Drawing.Size(38, 22)
Me.NCardsDetailsBoxSTab.TabIndex = 76
Me.NCardsDetailsBoxSTab.TabStop = false
Me.NCardsDetailsBoxSTab.Text = ""
Me.NCardsDetailsBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SoftDetailsCheckSTab
'
Me.SoftDetailsCheckSTab.Enabled = false
Me.SoftDetailsCheckSTab.Location = New System.Drawing.Point(470, 28)
Me.SoftDetailsCheckSTab.Name = "SoftDetailsCheckSTab"
Me.SoftDetailsCheckSTab.Size = New System.Drawing.Size(20, 18)
Me.SoftDetailsCheckSTab.TabIndex = 74
Me.SoftDetailsCheckSTab.TabStop = false
'
'SoftDetailsLabelSTab
'
Me.SoftDetailsLabelSTab.Location = New System.Drawing.Point(422, 28)
Me.SoftDetailsLabelSTab.Name = "SoftDetailsLabelSTab"
Me.SoftDetailsLabelSTab.Size = New System.Drawing.Size(29, 18)
Me.SoftDetailsLabelSTab.TabIndex = 73
Me.SoftDetailsLabelSTab.Text = "Soft"
'
'DiamondsLabelSTab
'
Me.DiamondsLabelSTab.Location = New System.Drawing.Point(528, 96)
Me.DiamondsLabelSTab.Name = "DiamondsLabelSTab"
Me.DiamondsLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.DiamondsLabelSTab.TabIndex = 35
Me.DiamondsLabelSTab.Text = "Diamonds"
Me.DiamondsLabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'CDLabelSTab
'
Me.CDLabelSTab.Location = New System.Drawing.Point(144, 97)
Me.CDLabelSTab.Name = "CDLabelSTab"
Me.CDLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.CDLabelSTab.TabIndex = 34
Me.CDLabelSTab.Text = "Non-Suited CD"
Me.CDLabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ClubsLabelSTab
'
Me.ClubsLabelSTab.Location = New System.Drawing.Point(656, 96)
Me.ClubsLabelSTab.Name = "ClubsLabelSTab"
Me.ClubsLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.ClubsLabelSTab.TabIndex = 33
Me.ClubsLabelSTab.Text = "Clubs"
Me.ClubsLabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'HeartsLabelSTab
'
Me.HeartsLabelSTab.Location = New System.Drawing.Point(400, 96)
Me.HeartsLabelSTab.Name = "HeartsLabelSTab"
Me.HeartsLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.HeartsLabelSTab.TabIndex = 32
Me.HeartsLabelSTab.Text = "Hearts"
Me.HeartsLabelSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'CDStratBoxSTab
'
Me.CDStratBoxSTab.Location = New System.Drawing.Point(172, 120)
Me.CDStratBoxSTab.Name = "CDStratBoxSTab"
Me.CDStratBoxSTab.ReadOnly = true
Me.CDStratBoxSTab.Size = New System.Drawing.Size(40, 22)
Me.CDStratBoxSTab.TabIndex = 30
Me.CDStratBoxSTab.TabStop = false
Me.CDStratBoxSTab.Text = ""
Me.CDStratBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'StandBoxSTab
'
Me.StandBoxSTab.Location = New System.Drawing.Point(144, 176)
Me.StandBoxSTab.Name = "StandBoxSTab"
Me.StandBoxSTab.ReadOnly = true
Me.StandBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.StandBoxSTab.TabIndex = 28
Me.StandBoxSTab.TabStop = false
Me.StandBoxSTab.Text = ""
Me.StandBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DoubleBoxSTab
'
Me.DoubleBoxSTab.Location = New System.Drawing.Point(144, 314)
Me.DoubleBoxSTab.Name = "DoubleBoxSTab"
Me.DoubleBoxSTab.ReadOnly = true
Me.DoubleBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.DoubleBoxSTab.TabIndex = 27
Me.DoubleBoxSTab.TabStop = false
Me.DoubleBoxSTab.Text = ""
Me.DoubleBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SurrenderBoxSTab
'
Me.SurrenderBoxSTab.Location = New System.Drawing.Point(413, 314)
Me.SurrenderBoxSTab.Name = "SurrenderBoxSTab"
Me.SurrenderBoxSTab.ReadOnly = true
Me.SurrenderBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.SurrenderBoxSTab.TabIndex = 26
Me.SurrenderBoxSTab.TabStop = false
Me.SurrenderBoxSTab.Text = ""
Me.SurrenderBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CDSplitBoxSTab
'
Me.CDSplitBoxSTab.Location = New System.Drawing.Point(682, 314)
Me.CDSplitBoxSTab.Name = "CDSplitBoxSTab"
Me.CDSplitBoxSTab.ReadOnly = true
Me.CDSplitBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.CDSplitBoxSTab.TabIndex = 25
Me.CDSplitBoxSTab.TabStop = false
Me.CDSplitBoxSTab.Text = ""
Me.CDSplitBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CDHitBoxSTab
'
Me.CDHitBoxSTab.Location = New System.Drawing.Point(144, 204)
Me.CDHitBoxSTab.Name = "CDHitBoxSTab"
Me.CDHitBoxSTab.ReadOnly = true
Me.CDHitBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.CDHitBoxSTab.TabIndex = 18
Me.CDHitBoxSTab.TabStop = false
Me.CDHitBoxSTab.Text = ""
Me.CDHitBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'NonSuitedProbDetailsBoxSTab
'
Me.NonSuitedProbDetailsBoxSTab.Location = New System.Drawing.Point(144, 148)
Me.NonSuitedProbDetailsBoxSTab.Name = "NonSuitedProbDetailsBoxSTab"
Me.NonSuitedProbDetailsBoxSTab.ReadOnly = true
Me.NonSuitedProbDetailsBoxSTab.Size = New System.Drawing.Size(96, 22)
Me.NonSuitedProbDetailsBoxSTab.TabIndex = 17
Me.NonSuitedProbDetailsBoxSTab.TabStop = false
Me.NonSuitedProbDetailsBoxSTab.Text = ""
Me.NonSuitedProbDetailsBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SplitLabelSTab
'
Me.SplitLabelSTab.Location = New System.Drawing.Point(595, 314)
Me.SplitLabelSTab.Name = "SplitLabelSTab"
Me.SplitLabelSTab.Size = New System.Drawing.Size(58, 18)
Me.SplitLabelSTab.TabIndex = 14
Me.SplitLabelSTab.Text = "Split EV"
'
'StratLabelSTab
'
Me.StratLabelSTab.Location = New System.Drawing.Point(38, 120)
Me.StratLabelSTab.Name = "StratLabelSTab"
Me.StratLabelSTab.Size = New System.Drawing.Size(68, 18)
Me.StratLabelSTab.TabIndex = 11
Me.StratLabelSTab.Text = "Strategy"
'
'HitLabelSTab
'
Me.HitLabelSTab.Location = New System.Drawing.Point(38, 204)
Me.HitLabelSTab.Name = "HitLabelSTab"
Me.HitLabelSTab.Size = New System.Drawing.Size(96, 19)
Me.HitLabelSTab.TabIndex = 9
Me.HitLabelSTab.Text = "Hit EV"
'
'SurrLabelSTab
'
Me.SurrLabelSTab.Location = New System.Drawing.Point(288, 314)
Me.SurrLabelSTab.Name = "SurrLabelSTab"
Me.SurrLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.SurrLabelSTab.TabIndex = 7
Me.SurrLabelSTab.Text = "Surrender EV"
'
'DoubleLabelSTab
'
Me.DoubleLabelSTab.Location = New System.Drawing.Point(38, 314)
Me.DoubleLabelSTab.Name = "DoubleLabelSTab"
Me.DoubleLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.DoubleLabelSTab.TabIndex = 4
Me.DoubleLabelSTab.Text = "Double EV"
'
'StandLabelSTab
'
Me.StandLabelSTab.Location = New System.Drawing.Point(38, 176)
Me.StandLabelSTab.Name = "StandLabelSTab"
Me.StandLabelSTab.Size = New System.Drawing.Size(96, 19)
Me.StandLabelSTab.TabIndex = 3
Me.StandLabelSTab.Text = "Stand EV"
'
'ProbLabelSTab
'
Me.ProbLabelSTab.Location = New System.Drawing.Point(38, 148)
Me.ProbLabelSTab.Name = "ProbLabelSTab"
Me.ProbLabelSTab.Size = New System.Drawing.Size(96, 18)
Me.ProbLabelSTab.TabIndex = 2
Me.ProbLabelSTab.Text = "Probability"
'
'HandLabelSTab
'
Me.HandLabelSTab.Location = New System.Drawing.Point(38, 28)
Me.HandLabelSTab.Name = "HandLabelSTab"
Me.HandLabelSTab.Size = New System.Drawing.Size(39, 18)
Me.HandLabelSTab.TabIndex = 1
Me.HandLabelSTab.Text = "Hand"
'
'HandNameBoxSTab
'
Me.HandNameBoxSTab.Location = New System.Drawing.Point(86, 28)
Me.HandNameBoxSTab.Name = "HandNameBoxSTab"
Me.HandNameBoxSTab.ReadOnly = true
Me.HandNameBoxSTab.Size = New System.Drawing.Size(212, 22)
Me.HandNameBoxSTab.TabIndex = 0
Me.HandNameBoxSTab.TabStop = false
Me.HandNameBoxSTab.Text = ""
Me.HandNameBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TotalDetailsLabelSTab
'
Me.TotalDetailsLabelSTab.Location = New System.Drawing.Point(317, 28)
Me.TotalDetailsLabelSTab.Name = "TotalDetailsLabelSTab"
Me.TotalDetailsLabelSTab.Size = New System.Drawing.Size(38, 18)
Me.TotalDetailsLabelSTab.TabIndex = 71
Me.TotalDetailsLabelSTab.Text = "Total"
'
'TotalDetailsBoxSTab
'
Me.TotalDetailsBoxSTab.Location = New System.Drawing.Point(365, 28)
Me.TotalDetailsBoxSTab.Name = "TotalDetailsBoxSTab"
Me.TotalDetailsBoxSTab.ReadOnly = true
Me.TotalDetailsBoxSTab.Size = New System.Drawing.Size(38, 22)
Me.TotalDetailsBoxSTab.TabIndex = 72
Me.TotalDetailsBoxSTab.TabStop = false
Me.TotalDetailsBoxSTab.Text = ""
Me.TotalDetailsBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'RulesTab
'
Me.RulesTab.Controls.Add(Me.P21AutowinBoxRTab)
Me.RulesTab.Controls.Add(Me.P21AutowinLabelRTab)
Me.RulesTab.Controls.Add(Me.DDRTypeBoxRTab)
Me.RulesTab.Controls.Add(Me.DDRBoxRTab)
Me.RulesTab.Controls.Add(Me.DDRLabelRTab)
Me.RulesTab.Controls.Add(Me.RDABoxRTab)
Me.RulesTab.Controls.Add(Me.RDALabelRTab)
Me.RulesTab.Controls.Add(Me.ForcedRulesLabelRTab)
Me.RulesTab.Controls.Add(Me.BonusRulesLabelRTab)
Me.RulesTab.Controls.Add(Me.ForcedRulesBoxRTab)
Me.RulesTab.Controls.Add(Me.BonusRulesBoxRTab)
Me.RulesTab.Controls.Add(Me.T31LabelRTab)
Me.RulesTab.Controls.Add(Me.TTen1LabelRTab)
Me.RulesTab.Controls.Add(Me.T41LabelRTab)
Me.RulesTab.Controls.Add(Me.T51LabelRTab)
Me.RulesTab.Controls.Add(Me.T21LabelRTab)
Me.RulesTab.Controls.Add(Me.T61LabelRTab)
Me.RulesTab.Controls.Add(Me.T71LabelRTab)
Me.RulesTab.Controls.Add(Me.T81LabelRTab)
Me.RulesTab.Controls.Add(Me.T91LabelRTab)
Me.RulesTab.Controls.Add(Me.TAce1LabelRTab)
Me.RulesTab.Controls.Add(Me.PDTiesLabelRTab)
Me.RulesTab.Controls.Add(Me.T19LabelRTab)
Me.RulesTab.Controls.Add(Me.T20LabelRTab)
Me.RulesTab.Controls.Add(Me.T21TiesLabelRTab)
Me.RulesTab.Controls.Add(Me.T18LabelRTab)
Me.RulesTab.Controls.Add(Me.TBJLabelRTab)
Me.RulesTab.Controls.Add(Me.T17LabelRTab)
Me.RulesTab.Controls.Add(Me.CheckAceBoxRTab)
Me.RulesTab.Controls.Add(Me.CheckAceLabelRTab)
Me.RulesTab.Controls.Add(Me.CheckTenBoxRTab)
Me.RulesTab.Controls.Add(Me.CheckTenLabelRTab)
Me.RulesTab.Controls.Add(Me.BJSplitTensBoxRTab)
Me.RulesTab.Controls.Add(Me.BJSplitTensLabelRTab)
Me.RulesTab.Controls.Add(Me.BJSplitAcesBoxRTab)
Me.RulesTab.Controls.Add(Me.BJSplitAcesLabelRTab)
Me.RulesTab.Controls.Add(Me.CDTypeBoxRTab)
Me.RulesTab.Controls.Add(Me.CDTypeLabelRTab)
Me.RulesTab.Controls.Add(Me.SASBoxRTab)
Me.RulesTab.Controls.Add(Me.SASLabelRTab)
Me.RulesTab.Controls.Add(Me.MacauBoxRTab)
Me.RulesTab.Controls.Add(Me.MacauLabelRTab)
Me.RulesTab.Controls.Add(Me.SurrPaysDBJBoxRTab)
Me.RulesTab.Controls.Add(Me.SurrPaysDBJLabelRTab)
Me.RulesTab.Controls.Add(Me.SurrPaysBoxRTab)
Me.RulesTab.Controls.Add(Me.SurrPaysLabelRTab)
Me.RulesTab.Controls.Add(Me.DSoftHardBoxRTab)
Me.RulesTab.Controls.Add(Me.DSHardLabelRTab)
Me.RulesTab.Controls.Add(Me.DANBoxRTab)
Me.RulesTab.Controls.Add(Me.DANLabelRTab)
Me.RulesTab.Controls.Add(Me.SSABoxRTab)
Me.RulesTab.Controls.Add(Me.SSARLabelTab)
Me.RulesTab.Controls.Add(Me.DSABoxRTab)
Me.RulesTab.Controls.Add(Me.DSARLabelTab)
Me.RulesTab.Controls.Add(Me.HSABoxRTab)
Me.RulesTab.Controls.Add(Me.SANBoxRTab)
Me.RulesTab.Controls.Add(Me.SANLabelRTab)
Me.RulesTab.Controls.Add(Me.HSARLabelTab)
Me.RulesTab.Controls.Add(Me.SMALabelRTab)
Me.RulesTab.Controls.Add(Me.SMABoxRTab)
Me.RulesTab.Controls.Add(Me.SPLBoxRTab)
Me.RulesTab.Controls.Add(Me.SPLRLabelTab)
Me.RulesTab.Controls.Add(Me.SurrTypeBoxRTab)
Me.RulesTab.Controls.Add(Me.SurrTypeLabelRTab)
Me.RulesTab.Controls.Add(Me.DASBoxRTab)
Me.RulesTab.Controls.Add(Me.DASLabelRTab)
Me.RulesTab.Controls.Add(Me.DTypeBoxRTab)
Me.RulesTab.Controls.Add(Me.DTypeLabelRTab)
Me.RulesTab.Controls.Add(Me.BJRuleBoxRTab)
Me.RulesTab.Controls.Add(Me.DBJRuleLabelRTab)
Me.RulesTab.Controls.Add(Me.StandsOnSoftBoxRTab)
Me.RulesTab.Controls.Add(Me.BJPaysBoxRTab)
Me.RulesTab.Controls.Add(Me.BJPaysLabelRTab)
Me.RulesTab.Controls.Add(Me.StandsOnSoftLabelRTab)
Me.RulesTab.Controls.Add(Me.NDecksLabelRTab)
Me.RulesTab.Controls.Add(Me.NDecksBoxRTab)
Me.RulesTab.Controls.Add(Me.SplitAllowedLabelRTab)
Me.RulesTab.Controls.Add(Me.ShoeGroupRTab)
Me.RulesTab.Location = New System.Drawing.Point(4, 25)
Me.RulesTab.Name = "RulesTab"
Me.RulesTab.Size = New System.Drawing.Size(846, 626)
Me.RulesTab.TabIndex = 4
Me.RulesTab.Text = "Rules"
'
'P21AutowinBoxRTab
'
Me.P21AutowinBoxRTab.Location = New System.Drawing.Point(576, 130)
Me.P21AutowinBoxRTab.Name = "P21AutowinBoxRTab"
Me.P21AutowinBoxRTab.ReadOnly = true
Me.P21AutowinBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.P21AutowinBoxRTab.TabIndex = 143
Me.P21AutowinBoxRTab.TabStop = false
Me.P21AutowinBoxRTab.Text = ""
Me.P21AutowinBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'P21AutowinLabelRTab
'
Me.P21AutowinLabelRTab.Location = New System.Drawing.Point(432, 130)
Me.P21AutowinLabelRTab.Name = "P21AutowinLabelRTab"
Me.P21AutowinLabelRTab.Size = New System.Drawing.Size(144, 19)
Me.P21AutowinLabelRTab.TabIndex = 144
Me.P21AutowinLabelRTab.Text = "Player 21 Autowin"
'
'DDRTypeBoxRTab
'
Me.DDRTypeBoxRTab.Location = New System.Drawing.Point(416, 214)
Me.DDRTypeBoxRTab.Name = "DDRTypeBoxRTab"
Me.DDRTypeBoxRTab.ReadOnly = true
Me.DDRTypeBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.DDRTypeBoxRTab.TabIndex = 142
Me.DDRTypeBoxRTab.TabStop = false
Me.DDRTypeBoxRTab.Text = ""
Me.DDRTypeBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DDRBoxRTab
'
Me.DDRBoxRTab.Location = New System.Drawing.Point(360, 214)
Me.DDRBoxRTab.Name = "DDRBoxRTab"
Me.DDRBoxRTab.ReadOnly = true
Me.DDRBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.DDRBoxRTab.TabIndex = 141
Me.DDRBoxRTab.TabStop = false
Me.DDRBoxRTab.Text = ""
Me.DDRBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DDRLabelRTab
'
Me.DDRLabelRTab.Location = New System.Drawing.Point(221, 214)
Me.DDRLabelRTab.Name = "DDRLabelRTab"
Me.DDRLabelRTab.Size = New System.Drawing.Size(134, 19)
Me.DDRLabelRTab.TabIndex = 140
Me.DDRLabelRTab.Text = "DD Rescue"
'
'RDABoxRTab
'
Me.RDABoxRTab.Location = New System.Drawing.Point(360, 186)
Me.RDABoxRTab.Name = "RDABoxRTab"
Me.RDABoxRTab.ReadOnly = true
Me.RDABoxRTab.Size = New System.Drawing.Size(56, 22)
Me.RDABoxRTab.TabIndex = 139
Me.RDABoxRTab.TabStop = false
Me.RDABoxRTab.Text = ""
Me.RDABoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'RDALabelRTab
'
Me.RDALabelRTab.Location = New System.Drawing.Point(221, 186)
Me.RDALabelRTab.Name = "RDALabelRTab"
Me.RDALabelRTab.Size = New System.Drawing.Size(134, 18)
Me.RDALabelRTab.TabIndex = 138
Me.RDALabelRTab.Text = "Redouble"
'
'ForcedRulesLabelRTab
'
Me.ForcedRulesLabelRTab.Location = New System.Drawing.Point(658, 256)
Me.ForcedRulesLabelRTab.Name = "ForcedRulesLabelRTab"
Me.ForcedRulesLabelRTab.Size = New System.Drawing.Size(86, 18)
Me.ForcedRulesLabelRTab.TabIndex = 137
Me.ForcedRulesLabelRTab.Text = "Forced Rules"
'
'BonusRulesLabelRTab
'
Me.BonusRulesLabelRTab.Location = New System.Drawing.Point(656, 160)
Me.BonusRulesLabelRTab.Name = "BonusRulesLabelRTab"
Me.BonusRulesLabelRTab.Size = New System.Drawing.Size(86, 19)
Me.BonusRulesLabelRTab.TabIndex = 136
Me.BonusRulesLabelRTab.Text = "Bonus Rules"
'
'ForcedRulesBoxRTab
'
Me.ForcedRulesBoxRTab.ItemHeight = 16
Me.ForcedRulesBoxRTab.Location = New System.Drawing.Point(568, 280)
Me.ForcedRulesBoxRTab.Name = "ForcedRulesBoxRTab"
Me.ForcedRulesBoxRTab.Size = New System.Drawing.Size(259, 68)
Me.ForcedRulesBoxRTab.TabIndex = 1
'
'BonusRulesBoxRTab
'
Me.BonusRulesBoxRTab.ItemHeight = 16
Me.BonusRulesBoxRTab.Location = New System.Drawing.Point(568, 184)
Me.BonusRulesBoxRTab.Name = "BonusRulesBoxRTab"
Me.BonusRulesBoxRTab.Size = New System.Drawing.Size(259, 68)
Me.BonusRulesBoxRTab.TabIndex = 0
'
'T31LabelRTab
'
Me.T31LabelRTab.Location = New System.Drawing.Point(206, 351)
Me.T31LabelRTab.Name = "T31LabelRTab"
Me.T31LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.T31LabelRTab.TabIndex = 123
Me.T31LabelRTab.Text = "3"
Me.T31LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'TTen1LabelRTab
'
Me.TTen1LabelRTab.Location = New System.Drawing.Point(542, 351)
Me.TTen1LabelRTab.Name = "TTen1LabelRTab"
Me.TTen1LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.TTen1LabelRTab.TabIndex = 122
Me.TTen1LabelRTab.Text = "Ten"
Me.TTen1LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T41LabelRTab
'
Me.T41LabelRTab.Location = New System.Drawing.Point(254, 351)
Me.T41LabelRTab.Name = "T41LabelRTab"
Me.T41LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.T41LabelRTab.TabIndex = 121
Me.T41LabelRTab.Text = "4"
Me.T41LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T51LabelRTab
'
Me.T51LabelRTab.Location = New System.Drawing.Point(302, 351)
Me.T51LabelRTab.Name = "T51LabelRTab"
Me.T51LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.T51LabelRTab.TabIndex = 120
Me.T51LabelRTab.Text = "5"
Me.T51LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T21LabelRTab
'
Me.T21LabelRTab.Location = New System.Drawing.Point(158, 351)
Me.T21LabelRTab.Name = "T21LabelRTab"
Me.T21LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.T21LabelRTab.TabIndex = 119
Me.T21LabelRTab.Text = "2"
Me.T21LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T61LabelRTab
'
Me.T61LabelRTab.Location = New System.Drawing.Point(350, 351)
Me.T61LabelRTab.Name = "T61LabelRTab"
Me.T61LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.T61LabelRTab.TabIndex = 118
Me.T61LabelRTab.Text = "6"
Me.T61LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T71LabelRTab
'
Me.T71LabelRTab.Location = New System.Drawing.Point(398, 351)
Me.T71LabelRTab.Name = "T71LabelRTab"
Me.T71LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.T71LabelRTab.TabIndex = 117
Me.T71LabelRTab.Text = "7"
Me.T71LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T81LabelRTab
'
Me.T81LabelRTab.Location = New System.Drawing.Point(446, 351)
Me.T81LabelRTab.Name = "T81LabelRTab"
Me.T81LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.T81LabelRTab.TabIndex = 116
Me.T81LabelRTab.Text = "8"
Me.T81LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T91LabelRTab
'
Me.T91LabelRTab.Location = New System.Drawing.Point(494, 351)
Me.T91LabelRTab.Name = "T91LabelRTab"
Me.T91LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.T91LabelRTab.TabIndex = 115
Me.T91LabelRTab.Text = "9"
Me.T91LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'TAce1LabelRTab
'
Me.TAce1LabelRTab.Location = New System.Drawing.Point(110, 351)
Me.TAce1LabelRTab.Name = "TAce1LabelRTab"
Me.TAce1LabelRTab.Size = New System.Drawing.Size(30, 16)
Me.TAce1LabelRTab.TabIndex = 114
Me.TAce1LabelRTab.Text = "Ace"
Me.TAce1LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'PDTiesLabelRTab
'
Me.PDTiesLabelRTab.Location = New System.Drawing.Point(14, 312)
Me.PDTiesLabelRTab.Name = "PDTiesLabelRTab"
Me.PDTiesLabelRTab.Size = New System.Drawing.Size(125, 19)
Me.PDTiesLabelRTab.TabIndex = 107
Me.PDTiesLabelRTab.Text = "Player-Dealer Ties"
'
'T19LabelRTab
'
Me.T19LabelRTab.Location = New System.Drawing.Point(290, 288)
Me.T19LabelRTab.Name = "T19LabelRTab"
Me.T19LabelRTab.Size = New System.Drawing.Size(24, 16)
Me.T19LabelRTab.TabIndex = 106
Me.T19LabelRTab.Text = "19"
Me.T19LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T20LabelRTab
'
Me.T20LabelRTab.Location = New System.Drawing.Point(346, 288)
Me.T20LabelRTab.Name = "T20LabelRTab"
Me.T20LabelRTab.Size = New System.Drawing.Size(24, 16)
Me.T20LabelRTab.TabIndex = 105
Me.T20LabelRTab.Text = "20"
Me.T20LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T21TiesLabelRTab
'
Me.T21TiesLabelRTab.Location = New System.Drawing.Point(402, 288)
Me.T21TiesLabelRTab.Name = "T21TiesLabelRTab"
Me.T21TiesLabelRTab.Size = New System.Drawing.Size(24, 16)
Me.T21TiesLabelRTab.TabIndex = 104
Me.T21TiesLabelRTab.Text = "21"
Me.T21TiesLabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T18LabelRTab
'
Me.T18LabelRTab.Location = New System.Drawing.Point(234, 288)
Me.T18LabelRTab.Name = "T18LabelRTab"
Me.T18LabelRTab.Size = New System.Drawing.Size(24, 16)
Me.T18LabelRTab.TabIndex = 103
Me.T18LabelRTab.Text = "18"
Me.T18LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'TBJLabelRTab
'
Me.TBJLabelRTab.Location = New System.Drawing.Point(458, 288)
Me.TBJLabelRTab.Name = "TBJLabelRTab"
Me.TBJLabelRTab.Size = New System.Drawing.Size(24, 16)
Me.TBJLabelRTab.TabIndex = 102
Me.TBJLabelRTab.Text = "BJ"
Me.TBJLabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'T17LabelRTab
'
Me.T17LabelRTab.Location = New System.Drawing.Point(178, 288)
Me.T17LabelRTab.Name = "T17LabelRTab"
Me.T17LabelRTab.Size = New System.Drawing.Size(24, 16)
Me.T17LabelRTab.TabIndex = 101
Me.T17LabelRTab.Text = "17"
Me.T17LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'CheckAceBoxRTab
'
Me.CheckAceBoxRTab.Location = New System.Drawing.Point(782, 130)
Me.CheckAceBoxRTab.Name = "CheckAceBoxRTab"
Me.CheckAceBoxRTab.ReadOnly = true
Me.CheckAceBoxRTab.Size = New System.Drawing.Size(48, 22)
Me.CheckAceBoxRTab.TabIndex = 100
Me.CheckAceBoxRTab.TabStop = false
Me.CheckAceBoxRTab.Text = ""
Me.CheckAceBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CheckAceLabelRTab
'
Me.CheckAceLabelRTab.Location = New System.Drawing.Point(648, 130)
Me.CheckAceLabelRTab.Name = "CheckAceLabelRTab"
Me.CheckAceLabelRTab.Size = New System.Drawing.Size(130, 19)
Me.CheckAceLabelRTab.TabIndex = 99
Me.CheckAceLabelRTab.Text = "Dealer Checks Ace"
'
'CheckTenBoxRTab
'
Me.CheckTenBoxRTab.Location = New System.Drawing.Point(782, 102)
Me.CheckTenBoxRTab.Name = "CheckTenBoxRTab"
Me.CheckTenBoxRTab.ReadOnly = true
Me.CheckTenBoxRTab.Size = New System.Drawing.Size(48, 22)
Me.CheckTenBoxRTab.TabIndex = 98
Me.CheckTenBoxRTab.TabStop = false
Me.CheckTenBoxRTab.Text = ""
Me.CheckTenBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CheckTenLabelRTab
'
Me.CheckTenLabelRTab.Location = New System.Drawing.Point(648, 102)
Me.CheckTenLabelRTab.Name = "CheckTenLabelRTab"
Me.CheckTenLabelRTab.Size = New System.Drawing.Size(130, 18)
Me.CheckTenLabelRTab.TabIndex = 97
Me.CheckTenLabelRTab.Text = "Dealer Checks Ten"
'
'BJSplitTensBoxRTab
'
Me.BJSplitTensBoxRTab.Location = New System.Drawing.Point(782, 74)
Me.BJSplitTensBoxRTab.Name = "BJSplitTensBoxRTab"
Me.BJSplitTensBoxRTab.ReadOnly = true
Me.BJSplitTensBoxRTab.Size = New System.Drawing.Size(48, 22)
Me.BJSplitTensBoxRTab.TabIndex = 96
Me.BJSplitTensBoxRTab.TabStop = false
Me.BJSplitTensBoxRTab.Text = ""
Me.BJSplitTensBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BJSplitTensLabelRTab
'
Me.BJSplitTensLabelRTab.Location = New System.Drawing.Point(648, 74)
Me.BJSplitTensLabelRTab.Name = "BJSplitTensLabelRTab"
Me.BJSplitTensLabelRTab.Size = New System.Drawing.Size(130, 18)
Me.BJSplitTensLabelRTab.TabIndex = 95
Me.BJSplitTensLabelRTab.Text = "BJ Bonus Split Tens"
'
'BJSplitAcesBoxRTab
'
Me.BJSplitAcesBoxRTab.Location = New System.Drawing.Point(782, 46)
Me.BJSplitAcesBoxRTab.Name = "BJSplitAcesBoxRTab"
Me.BJSplitAcesBoxRTab.ReadOnly = true
Me.BJSplitAcesBoxRTab.Size = New System.Drawing.Size(48, 22)
Me.BJSplitAcesBoxRTab.TabIndex = 94
Me.BJSplitAcesBoxRTab.TabStop = false
Me.BJSplitAcesBoxRTab.Text = ""
Me.BJSplitAcesBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BJSplitAcesLabelRTab
'
Me.BJSplitAcesLabelRTab.Location = New System.Drawing.Point(648, 46)
Me.BJSplitAcesLabelRTab.Name = "BJSplitAcesLabelRTab"
Me.BJSplitAcesLabelRTab.Size = New System.Drawing.Size(130, 19)
Me.BJSplitAcesLabelRTab.TabIndex = 93
Me.BJSplitAcesLabelRTab.Text = "BJ Bonus Split Aces"
'
'CDTypeBoxRTab
'
Me.CDTypeBoxRTab.Location = New System.Drawing.Point(782, 18)
Me.CDTypeBoxRTab.Name = "CDTypeBoxRTab"
Me.CDTypeBoxRTab.ReadOnly = true
Me.CDTypeBoxRTab.Size = New System.Drawing.Size(48, 22)
Me.CDTypeBoxRTab.TabIndex = 92
Me.CDTypeBoxRTab.TabStop = false
Me.CDTypeBoxRTab.Text = ""
Me.CDTypeBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CDTypeLabelRTab
'
Me.CDTypeLabelRTab.Location = New System.Drawing.Point(648, 18)
Me.CDTypeLabelRTab.Name = "CDTypeLabelRTab"
Me.CDTypeLabelRTab.Size = New System.Drawing.Size(130, 19)
Me.CDTypeLabelRTab.TabIndex = 91
Me.CDTypeLabelRTab.Text = "CD Split Type"
'
'SASBoxRTab
'
Me.SASBoxRTab.Location = New System.Drawing.Point(576, 102)
Me.SASBoxRTab.Name = "SASBoxRTab"
Me.SASBoxRTab.ReadOnly = true
Me.SASBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.SASBoxRTab.TabIndex = 90
Me.SASBoxRTab.TabStop = false
Me.SASBoxRTab.Text = ""
Me.SASBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SASLabelRTab
'
Me.SASLabelRTab.Location = New System.Drawing.Point(432, 102)
Me.SASLabelRTab.Name = "SASLabelRTab"
Me.SASLabelRTab.Size = New System.Drawing.Size(139, 19)
Me.SASLabelRTab.TabIndex = 89
Me.SASLabelRTab.Text = "Surrender After Split"
'
'MacauBoxRTab
'
Me.MacauBoxRTab.Location = New System.Drawing.Point(576, 74)
Me.MacauBoxRTab.Name = "MacauBoxRTab"
Me.MacauBoxRTab.ReadOnly = true
Me.MacauBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.MacauBoxRTab.TabIndex = 88
Me.MacauBoxRTab.TabStop = false
Me.MacauBoxRTab.Text = ""
Me.MacauBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'MacauLabelRTab
'
Me.MacauLabelRTab.Location = New System.Drawing.Point(432, 74)
Me.MacauLabelRTab.Name = "MacauLabelRTab"
Me.MacauLabelRTab.Size = New System.Drawing.Size(139, 18)
Me.MacauLabelRTab.TabIndex = 87
Me.MacauLabelRTab.Text = "Macau Surrender"
'
'SurrPaysDBJBoxRTab
'
Me.SurrPaysDBJBoxRTab.Location = New System.Drawing.Point(576, 46)
Me.SurrPaysDBJBoxRTab.Name = "SurrPaysDBJBoxRTab"
Me.SurrPaysDBJBoxRTab.ReadOnly = true
Me.SurrPaysDBJBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.SurrPaysDBJBoxRTab.TabIndex = 86
Me.SurrPaysDBJBoxRTab.TabStop = false
Me.SurrPaysDBJBoxRTab.Text = ""
Me.SurrPaysDBJBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SurrPaysDBJLabelRTab
'
Me.SurrPaysDBJLabelRTab.Location = New System.Drawing.Point(432, 46)
Me.SurrPaysDBJLabelRTab.Name = "SurrPaysDBJLabelRTab"
Me.SurrPaysDBJLabelRTab.Size = New System.Drawing.Size(139, 18)
Me.SurrPaysDBJLabelRTab.TabIndex = 85
Me.SurrPaysDBJLabelRTab.Text = "Surr Pays Dealer BJ"
'
'SurrPaysBoxRTab
'
Me.SurrPaysBoxRTab.Location = New System.Drawing.Point(154, 242)
Me.SurrPaysBoxRTab.Name = "SurrPaysBoxRTab"
Me.SurrPaysBoxRTab.ReadOnly = true
Me.SurrPaysBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.SurrPaysBoxRTab.TabIndex = 84
Me.SurrPaysBoxRTab.TabStop = false
Me.SurrPaysBoxRTab.Text = ""
Me.SurrPaysBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SurrPaysLabelRTab
'
Me.SurrPaysLabelRTab.Location = New System.Drawing.Point(14, 242)
Me.SurrPaysLabelRTab.Name = "SurrPaysLabelRTab"
Me.SurrPaysLabelRTab.Size = New System.Drawing.Size(139, 19)
Me.SurrPaysLabelRTab.TabIndex = 83
Me.SurrPaysLabelRTab.Text = "Surrender Pays"
'
'DSoftHardBoxRTab
'
Me.DSoftHardBoxRTab.Location = New System.Drawing.Point(360, 158)
Me.DSoftHardBoxRTab.Name = "DSoftHardBoxRTab"
Me.DSoftHardBoxRTab.ReadOnly = true
Me.DSoftHardBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.DSoftHardBoxRTab.TabIndex = 82
Me.DSoftHardBoxRTab.TabStop = false
Me.DSoftHardBoxRTab.Text = ""
Me.DSoftHardBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DSHardLabelRTab
'
Me.DSHardLabelRTab.Location = New System.Drawing.Point(221, 158)
Me.DSHardLabelRTab.Name = "DSHardLabelRTab"
Me.DSHardLabelRTab.Size = New System.Drawing.Size(139, 18)
Me.DSHardLabelRTab.TabIndex = 81
Me.DSHardLabelRTab.Text = "Double Softs are Hard"
'
'DANBoxRTab
'
Me.DANBoxRTab.Location = New System.Drawing.Point(360, 130)
Me.DANBoxRTab.Name = "DANBoxRTab"
Me.DANBoxRTab.ReadOnly = true
Me.DANBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.DANBoxRTab.TabIndex = 80
Me.DANBoxRTab.TabStop = false
Me.DANBoxRTab.Text = ""
Me.DANBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DANLabelRTab
'
Me.DANLabelRTab.Location = New System.Drawing.Point(221, 130)
Me.DANLabelRTab.Name = "DANLabelRTab"
Me.DANLabelRTab.Size = New System.Drawing.Size(139, 19)
Me.DANLabelRTab.TabIndex = 79
Me.DANLabelRTab.Text = "Double Any Number"
'
'SSABoxRTab
'
Me.SSABoxRTab.Location = New System.Drawing.Point(360, 102)
Me.SSABoxRTab.Name = "SSABoxRTab"
Me.SSABoxRTab.ReadOnly = true
Me.SSABoxRTab.Size = New System.Drawing.Size(56, 22)
Me.SSABoxRTab.TabIndex = 78
Me.SSABoxRTab.TabStop = false
Me.SSABoxRTab.Text = ""
Me.SSABoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SSARLabelTab
'
Me.SSARLabelTab.Location = New System.Drawing.Point(221, 102)
Me.SSARLabelTab.Name = "SSARLabelTab"
Me.SSARLabelTab.Size = New System.Drawing.Size(139, 18)
Me.SSARLabelTab.TabIndex = 77
Me.SSARLabelTab.Text = "Surrender Split Aces"
'
'DSABoxRTab
'
Me.DSABoxRTab.Location = New System.Drawing.Point(360, 74)
Me.DSABoxRTab.Name = "DSABoxRTab"
Me.DSABoxRTab.ReadOnly = true
Me.DSABoxRTab.Size = New System.Drawing.Size(56, 22)
Me.DSABoxRTab.TabIndex = 76
Me.DSABoxRTab.TabStop = false
Me.DSABoxRTab.Text = ""
Me.DSABoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DSARLabelTab
'
Me.DSARLabelTab.Location = New System.Drawing.Point(221, 74)
Me.DSARLabelTab.Name = "DSARLabelTab"
Me.DSARLabelTab.Size = New System.Drawing.Size(139, 18)
Me.DSARLabelTab.TabIndex = 75
Me.DSARLabelTab.Text = "Double Split Aces"
'
'HSABoxRTab
'
Me.HSABoxRTab.Location = New System.Drawing.Point(360, 46)
Me.HSABoxRTab.Name = "HSABoxRTab"
Me.HSABoxRTab.ReadOnly = true
Me.HSABoxRTab.Size = New System.Drawing.Size(56, 22)
Me.HSABoxRTab.TabIndex = 74
Me.HSABoxRTab.TabStop = false
Me.HSABoxRTab.Text = ""
Me.HSABoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SANBoxRTab
'
Me.SANBoxRTab.Location = New System.Drawing.Point(576, 18)
Me.SANBoxRTab.Name = "SANBoxRTab"
Me.SANBoxRTab.ReadOnly = true
Me.SANBoxRTab.Size = New System.Drawing.Size(56, 22)
Me.SANBoxRTab.TabIndex = 71
Me.SANBoxRTab.TabStop = false
Me.SANBoxRTab.Text = ""
Me.SANBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SANLabelRTab
'
Me.SANLabelRTab.Location = New System.Drawing.Point(432, 18)
Me.SANLabelRTab.Name = "SANLabelRTab"
Me.SANLabelRTab.Size = New System.Drawing.Size(144, 19)
Me.SANLabelRTab.TabIndex = 72
Me.SANLabelRTab.Text = "Surrender Any Number"
'
'HSARLabelTab
'
Me.HSARLabelTab.Location = New System.Drawing.Point(221, 46)
Me.HSARLabelTab.Name = "HSARLabelTab"
Me.HSARLabelTab.Size = New System.Drawing.Size(139, 19)
Me.HSARLabelTab.TabIndex = 73
Me.HSARLabelTab.Text = "Hit Split Aces"
'
'SMALabelRTab
'
Me.SMALabelRTab.Location = New System.Drawing.Point(221, 18)
Me.SMALabelRTab.Name = "SMALabelRTab"
Me.SMALabelRTab.Size = New System.Drawing.Size(139, 19)
Me.SMALabelRTab.TabIndex = 69
Me.SMALabelRTab.Text = "Split Multiple Aces"
'
'SMABoxRTab
'
Me.SMABoxRTab.Location = New System.Drawing.Point(360, 18)
Me.SMABoxRTab.Name = "SMABoxRTab"
Me.SMABoxRTab.ReadOnly = true
Me.SMABoxRTab.Size = New System.Drawing.Size(56, 22)
Me.SMABoxRTab.TabIndex = 70
Me.SMABoxRTab.TabStop = false
Me.SMABoxRTab.Text = ""
Me.SMABoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SPLBoxRTab
'
Me.SPLBoxRTab.Location = New System.Drawing.Point(154, 186)
Me.SPLBoxRTab.Name = "SPLBoxRTab"
Me.SPLBoxRTab.ReadOnly = true
Me.SPLBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.SPLBoxRTab.TabIndex = 68
Me.SPLBoxRTab.TabStop = false
Me.SPLBoxRTab.Text = ""
Me.SPLBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SPLRLabelTab
'
Me.SPLRLabelTab.Location = New System.Drawing.Point(14, 186)
Me.SPLRLabelTab.Name = "SPLRLabelTab"
Me.SPLRLabelTab.Size = New System.Drawing.Size(135, 18)
Me.SPLRLabelTab.TabIndex = 67
Me.SPLRLabelTab.Text = "Splits Allowed"
'
'SurrTypeBoxRTab
'
Me.SurrTypeBoxRTab.Location = New System.Drawing.Point(154, 158)
Me.SurrTypeBoxRTab.Name = "SurrTypeBoxRTab"
Me.SurrTypeBoxRTab.ReadOnly = true
Me.SurrTypeBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.SurrTypeBoxRTab.TabIndex = 66
Me.SurrTypeBoxRTab.TabStop = false
Me.SurrTypeBoxRTab.Text = ""
Me.SurrTypeBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SurrTypeLabelRTab
'
Me.SurrTypeLabelRTab.Location = New System.Drawing.Point(14, 158)
Me.SurrTypeLabelRTab.Name = "SurrTypeLabelRTab"
Me.SurrTypeLabelRTab.Size = New System.Drawing.Size(135, 18)
Me.SurrTypeLabelRTab.TabIndex = 65
Me.SurrTypeLabelRTab.Text = "Surrender Allowed"
'
'DASBoxRTab
'
Me.DASBoxRTab.Location = New System.Drawing.Point(154, 130)
Me.DASBoxRTab.Name = "DASBoxRTab"
Me.DASBoxRTab.ReadOnly = true
Me.DASBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.DASBoxRTab.TabIndex = 64
Me.DASBoxRTab.TabStop = false
Me.DASBoxRTab.Text = ""
Me.DASBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DASLabelRTab
'
Me.DASLabelRTab.Location = New System.Drawing.Point(14, 130)
Me.DASLabelRTab.Name = "DASLabelRTab"
Me.DASLabelRTab.Size = New System.Drawing.Size(135, 19)
Me.DASLabelRTab.TabIndex = 63
Me.DASLabelRTab.Text = "Double After Split"
'
'DTypeBoxRTab
'
Me.DTypeBoxRTab.Location = New System.Drawing.Point(154, 102)
Me.DTypeBoxRTab.Name = "DTypeBoxRTab"
Me.DTypeBoxRTab.ReadOnly = true
Me.DTypeBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.DTypeBoxRTab.TabIndex = 62
Me.DTypeBoxRTab.TabStop = false
Me.DTypeBoxRTab.Text = ""
Me.DTypeBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DTypeLabelRTab
'
Me.DTypeLabelRTab.Location = New System.Drawing.Point(14, 102)
Me.DTypeLabelRTab.Name = "DTypeLabelRTab"
Me.DTypeLabelRTab.Size = New System.Drawing.Size(135, 18)
Me.DTypeLabelRTab.TabIndex = 61
Me.DTypeLabelRTab.Text = "Double Allowed"
'
'BJRuleBoxRTab
'
Me.BJRuleBoxRTab.Location = New System.Drawing.Point(154, 74)
Me.BJRuleBoxRTab.Name = "BJRuleBoxRTab"
Me.BJRuleBoxRTab.ReadOnly = true
Me.BJRuleBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.BJRuleBoxRTab.TabIndex = 60
Me.BJRuleBoxRTab.TabStop = false
Me.BJRuleBoxRTab.Text = ""
Me.BJRuleBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'DBJRuleLabelRTab
'
Me.DBJRuleLabelRTab.Location = New System.Drawing.Point(14, 74)
Me.DBJRuleLabelRTab.Name = "DBJRuleLabelRTab"
Me.DBJRuleLabelRTab.Size = New System.Drawing.Size(135, 18)
Me.DBJRuleLabelRTab.TabIndex = 59
Me.DBJRuleLabelRTab.Text = "Dealer BJ Rule"
'
'StandsOnSoftBoxRTab
'
Me.StandsOnSoftBoxRTab.Location = New System.Drawing.Point(154, 46)
Me.StandsOnSoftBoxRTab.Name = "StandsOnSoftBoxRTab"
Me.StandsOnSoftBoxRTab.ReadOnly = true
Me.StandsOnSoftBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.StandsOnSoftBoxRTab.TabIndex = 58
Me.StandsOnSoftBoxRTab.TabStop = false
Me.StandsOnSoftBoxRTab.Text = ""
Me.StandsOnSoftBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BJPaysBoxRTab
'
Me.BJPaysBoxRTab.Location = New System.Drawing.Point(154, 214)
Me.BJPaysBoxRTab.Name = "BJPaysBoxRTab"
Me.BJPaysBoxRTab.ReadOnly = true
Me.BJPaysBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.BJPaysBoxRTab.TabIndex = 54
Me.BJPaysBoxRTab.TabStop = false
Me.BJPaysBoxRTab.Text = ""
Me.BJPaysBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BJPaysLabelRTab
'
Me.BJPaysLabelRTab.Location = New System.Drawing.Point(14, 214)
Me.BJPaysLabelRTab.Name = "BJPaysLabelRTab"
Me.BJPaysLabelRTab.Size = New System.Drawing.Size(135, 19)
Me.BJPaysLabelRTab.TabIndex = 55
Me.BJPaysLabelRTab.Text = "Blackjack Pays"
'
'StandsOnSoftLabelRTab
'
Me.StandsOnSoftLabelRTab.Location = New System.Drawing.Point(14, 46)
Me.StandsOnSoftLabelRTab.Name = "StandsOnSoftLabelRTab"
Me.StandsOnSoftLabelRTab.Size = New System.Drawing.Size(140, 19)
Me.StandsOnSoftLabelRTab.TabIndex = 57
Me.StandsOnSoftLabelRTab.Text = "Dealer Stands on Soft"
'
'NDecksLabelRTab
'
Me.NDecksLabelRTab.Location = New System.Drawing.Point(14, 18)
Me.NDecksLabelRTab.Name = "NDecksLabelRTab"
Me.NDecksLabelRTab.Size = New System.Drawing.Size(135, 19)
Me.NDecksLabelRTab.TabIndex = 52
Me.NDecksLabelRTab.Text = "Number of Decks"
'
'NDecksBoxRTab
'
Me.NDecksBoxRTab.Location = New System.Drawing.Point(154, 18)
Me.NDecksBoxRTab.Name = "NDecksBoxRTab"
Me.NDecksBoxRTab.ReadOnly = true
Me.NDecksBoxRTab.Size = New System.Drawing.Size(54, 22)
Me.NDecksBoxRTab.TabIndex = 53
Me.NDecksBoxRTab.TabStop = false
Me.NDecksBoxRTab.Text = ""
Me.NDecksBoxRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SplitAllowedLabelRTab
'
Me.SplitAllowedLabelRTab.Location = New System.Drawing.Point(14, 378)
Me.SplitAllowedLabelRTab.Name = "SplitAllowedLabelRTab"
Me.SplitAllowedLabelRTab.Size = New System.Drawing.Size(87, 19)
Me.SplitAllowedLabelRTab.TabIndex = 51
Me.SplitAllowedLabelRTab.Text = "Split Allowed"
'
'ShoeGroupRTab
'
Me.ShoeGroupRTab.Controls.Add(Me.NetSuitLabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.SpadesLabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ClubsLabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.HeartsLabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.DiamondsLabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.NetForcedCardsLabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks3LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks10LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks4LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks5LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks2LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks6LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks7LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks8LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecks9LabelRTab)
Me.ShoeGroupRTab.Controls.Add(Me.ForcedDecksALabelRTab)
Me.ShoeGroupRTab.Location = New System.Drawing.Point(14, 415)
Me.ShoeGroupRTab.Name = "ShoeGroupRTab"
Me.ShoeGroupRTab.Size = New System.Drawing.Size(816, 194)
Me.ShoeGroupRTab.TabIndex = 3
Me.ShoeGroupRTab.TabStop = false
Me.ShoeGroupRTab.Text = "Shoe"
'
'NetSuitLabelRTab
'
Me.NetSuitLabelRTab.Location = New System.Drawing.Point(58, 46)
Me.NetSuitLabelRTab.Name = "NetSuitLabelRTab"
Me.NetSuitLabelRTab.Size = New System.Drawing.Size(48, 19)
Me.NetSuitLabelRTab.TabIndex = 104
Me.NetSuitLabelRTab.Text = "Net"
'
'SpadesLabelRTab
'
Me.SpadesLabelRTab.Location = New System.Drawing.Point(58, 74)
Me.SpadesLabelRTab.Name = "SpadesLabelRTab"
Me.SpadesLabelRTab.Size = New System.Drawing.Size(67, 18)
Me.SpadesLabelRTab.TabIndex = 103
Me.SpadesLabelRTab.Text = "Spades"
'
'ClubsLabelRTab
'
Me.ClubsLabelRTab.Location = New System.Drawing.Point(58, 158)
Me.ClubsLabelRTab.Name = "ClubsLabelRTab"
Me.ClubsLabelRTab.Size = New System.Drawing.Size(67, 18)
Me.ClubsLabelRTab.TabIndex = 102
Me.ClubsLabelRTab.Text = "Clubs"
'
'HeartsLabelRTab
'
Me.HeartsLabelRTab.Location = New System.Drawing.Point(58, 102)
Me.HeartsLabelRTab.Name = "HeartsLabelRTab"
Me.HeartsLabelRTab.Size = New System.Drawing.Size(67, 18)
Me.HeartsLabelRTab.TabIndex = 101
Me.HeartsLabelRTab.Text = "Hearts"
'
'DiamondsLabelRTab
'
Me.DiamondsLabelRTab.Location = New System.Drawing.Point(58, 130)
Me.DiamondsLabelRTab.Name = "DiamondsLabelRTab"
Me.DiamondsLabelRTab.Size = New System.Drawing.Size(67, 19)
Me.DiamondsLabelRTab.TabIndex = 100
Me.DiamondsLabelRTab.Text = "Diamonds"
'
'NetForcedCardsLabelRTab
'
Me.NetForcedCardsLabelRTab.Location = New System.Drawing.Point(728, 24)
Me.NetForcedCardsLabelRTab.Name = "NetForcedCardsLabelRTab"
Me.NetForcedCardsLabelRTab.Size = New System.Drawing.Size(72, 20)
Me.NetForcedCardsLabelRTab.TabIndex = 44
Me.NetForcedCardsLabelRTab.Text = "Net Cards"
Me.NetForcedCardsLabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks3LabelRTab
'
Me.ForcedDecks3LabelRTab.Location = New System.Drawing.Point(264, 24)
Me.ForcedDecks3LabelRTab.Name = "ForcedDecks3LabelRTab"
Me.ForcedDecks3LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks3LabelRTab.TabIndex = 40
Me.ForcedDecks3LabelRTab.Text = "3"
Me.ForcedDecks3LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks10LabelRTab
'
Me.ForcedDecks10LabelRTab.Location = New System.Drawing.Point(656, 24)
Me.ForcedDecks10LabelRTab.Name = "ForcedDecks10LabelRTab"
Me.ForcedDecks10LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks10LabelRTab.TabIndex = 38
Me.ForcedDecks10LabelRTab.Text = "Ten"
Me.ForcedDecks10LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks4LabelRTab
'
Me.ForcedDecks4LabelRTab.Location = New System.Drawing.Point(320, 24)
Me.ForcedDecks4LabelRTab.Name = "ForcedDecks4LabelRTab"
Me.ForcedDecks4LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks4LabelRTab.TabIndex = 36
Me.ForcedDecks4LabelRTab.Text = "4"
Me.ForcedDecks4LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks5LabelRTab
'
Me.ForcedDecks5LabelRTab.Location = New System.Drawing.Point(376, 24)
Me.ForcedDecks5LabelRTab.Name = "ForcedDecks5LabelRTab"
Me.ForcedDecks5LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks5LabelRTab.TabIndex = 34
Me.ForcedDecks5LabelRTab.Text = "5"
Me.ForcedDecks5LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks2LabelRTab
'
Me.ForcedDecks2LabelRTab.Location = New System.Drawing.Point(208, 24)
Me.ForcedDecks2LabelRTab.Name = "ForcedDecks2LabelRTab"
Me.ForcedDecks2LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks2LabelRTab.TabIndex = 32
Me.ForcedDecks2LabelRTab.Text = "2"
Me.ForcedDecks2LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks6LabelRTab
'
Me.ForcedDecks6LabelRTab.Location = New System.Drawing.Point(432, 24)
Me.ForcedDecks6LabelRTab.Name = "ForcedDecks6LabelRTab"
Me.ForcedDecks6LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks6LabelRTab.TabIndex = 30
Me.ForcedDecks6LabelRTab.Text = "6"
Me.ForcedDecks6LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks7LabelRTab
'
Me.ForcedDecks7LabelRTab.Location = New System.Drawing.Point(488, 24)
Me.ForcedDecks7LabelRTab.Name = "ForcedDecks7LabelRTab"
Me.ForcedDecks7LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks7LabelRTab.TabIndex = 28
Me.ForcedDecks7LabelRTab.Text = "7"
Me.ForcedDecks7LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks8LabelRTab
'
Me.ForcedDecks8LabelRTab.Location = New System.Drawing.Point(544, 24)
Me.ForcedDecks8LabelRTab.Name = "ForcedDecks8LabelRTab"
Me.ForcedDecks8LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks8LabelRTab.TabIndex = 26
Me.ForcedDecks8LabelRTab.Text = "8"
Me.ForcedDecks8LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecks9LabelRTab
'
Me.ForcedDecks9LabelRTab.Location = New System.Drawing.Point(600, 24)
Me.ForcedDecks9LabelRTab.Name = "ForcedDecks9LabelRTab"
Me.ForcedDecks9LabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecks9LabelRTab.TabIndex = 24
Me.ForcedDecks9LabelRTab.Text = "9"
Me.ForcedDecks9LabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedDecksALabelRTab
'
Me.ForcedDecksALabelRTab.Location = New System.Drawing.Point(152, 24)
Me.ForcedDecksALabelRTab.Name = "ForcedDecksALabelRTab"
Me.ForcedDecksALabelRTab.Size = New System.Drawing.Size(32, 16)
Me.ForcedDecksALabelRTab.TabIndex = 3
Me.ForcedDecksALabelRTab.Text = "Ace"
Me.ForcedDecksALabelRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'AnalysisTab
'
Me.AnalysisTab.Controls.Add(Me.AnalysisTabControl)
Me.AnalysisTab.Location = New System.Drawing.Point(4, 25)
Me.AnalysisTab.Name = "AnalysisTab"
Me.AnalysisTab.Size = New System.Drawing.Size(846, 626)
Me.AnalysisTab.TabIndex = 12
Me.AnalysisTab.Text = "Analysis"
'
'AnalysisTabControl
'
Me.AnalysisTabControl.Controls.Add(Me.HandAnalysisTab)
Me.AnalysisTabControl.Controls.Add(Me.HandSizeAnalysisTab)
Me.AnalysisTabControl.Controls.Add(Me.DoubleAnalysisTab)
Me.AnalysisTabControl.Location = New System.Drawing.Point(10, 9)
Me.AnalysisTabControl.Name = "AnalysisTabControl"
Me.AnalysisTabControl.SelectedIndex = 0
Me.AnalysisTabControl.Size = New System.Drawing.Size(825, 609)
Me.AnalysisTabControl.TabIndex = 0
'
'HandAnalysisTab
'
Me.HandAnalysisTab.Controls.Add(Me.NCardsComboBoxHATab)
Me.HandAnalysisTab.Controls.Add(Me.TotalComboBoxHATab)
Me.HandAnalysisTab.Controls.Add(Me.UCComboBoxHATab)
Me.HandAnalysisTab.Controls.Add(Me.ExactMatchCheckHATab)
Me.HandAnalysisTab.Controls.Add(Me.ListSizeLabelHATab)
Me.HandAnalysisTab.Controls.Add(Me.ListSizeBoxHATab)
Me.HandAnalysisTab.Controls.Add(Me.HardOnlyCheckHATab)
Me.HandAnalysisTab.Controls.Add(Me.SoftOnlyCheckHATab)
Me.HandAnalysisTab.Controls.Add(Me.OrLessCheckHATab)
Me.HandAnalysisTab.Controls.Add(Me.OrMoreCheckHATab)
Me.HandAnalysisTab.Controls.Add(Me.EitherCheckHATab)
Me.HandAnalysisTab.Controls.Add(Me.IncludesLabelHATab)
Me.HandAnalysisTab.Controls.Add(Me.HandBoxHATab)
Me.HandAnalysisTab.Controls.Add(Me.NCardLabelHATab)
Me.HandAnalysisTab.Controls.Add(Me.UCLabelHATab)
Me.HandAnalysisTab.Controls.Add(Me.SoftLabelHATab)
Me.HandAnalysisTab.Controls.Add(Me.TotalLabelHATab)
Me.HandAnalysisTab.Controls.Add(Me.HandListBoxHATab)
Me.HandAnalysisTab.Controls.Add(Me.HandDetailsGroupHATab)
Me.HandAnalysisTab.Location = New System.Drawing.Point(4, 25)
Me.HandAnalysisTab.Name = "HandAnalysisTab"
Me.HandAnalysisTab.Size = New System.Drawing.Size(817, 580)
Me.HandAnalysisTab.TabIndex = 0
Me.HandAnalysisTab.Text = "Individual Hands"
'
'NCardsComboBoxHATab
'
Me.NCardsComboBoxHATab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.NCardsComboBoxHATab.Items.AddRange(New Object() {"Any", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.NCardsComboBoxHATab.Location = New System.Drawing.Point(442, 37)
Me.NCardsComboBoxHATab.Name = "NCardsComboBoxHATab"
Me.NCardsComboBoxHATab.Size = New System.Drawing.Size(67, 24)
Me.NCardsComboBoxHATab.TabIndex = 89
'
'TotalComboBoxHATab
'
Me.TotalComboBoxHATab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.TotalComboBoxHATab.Items.AddRange(New Object() {"Any", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.TotalComboBoxHATab.Location = New System.Drawing.Point(240, 37)
Me.TotalComboBoxHATab.Name = "TotalComboBoxHATab"
Me.TotalComboBoxHATab.Size = New System.Drawing.Size(67, 24)
Me.TotalComboBoxHATab.TabIndex = 70
'
'UCComboBoxHATab
'
Me.UCComboBoxHATab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UCComboBoxHATab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UCComboBoxHATab.Location = New System.Drawing.Point(538, 37)
Me.UCComboBoxHATab.Name = "UCComboBoxHATab"
Me.UCComboBoxHATab.Size = New System.Drawing.Size(57, 24)
Me.UCComboBoxHATab.TabIndex = 77
'
'ExactMatchCheckHATab
'
Me.ExactMatchCheckHATab.Location = New System.Drawing.Point(499, 111)
Me.ExactMatchCheckHATab.Name = "ExactMatchCheckHATab"
Me.ExactMatchCheckHATab.Size = New System.Drawing.Size(106, 18)
Me.ExactMatchCheckHATab.TabIndex = 79
Me.ExactMatchCheckHATab.Text = "Exact Match"
'
'ListSizeLabelHATab
'
Me.ListSizeLabelHATab.Location = New System.Drawing.Point(298, 240)
Me.ListSizeLabelHATab.Name = "ListSizeLabelHATab"
Me.ListSizeLabelHATab.Size = New System.Drawing.Size(182, 28)
Me.ListSizeLabelHATab.TabIndex = 88
Me.ListSizeLabelHATab.Text = "Hands meeting above criteria"
Me.ListSizeLabelHATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ListSizeBoxHATab
'
Me.ListSizeBoxHATab.Location = New System.Drawing.Point(480, 240)
Me.ListSizeBoxHATab.Name = "ListSizeBoxHATab"
Me.ListSizeBoxHATab.ReadOnly = true
Me.ListSizeBoxHATab.Size = New System.Drawing.Size(48, 22)
Me.ListSizeBoxHATab.TabIndex = 87
Me.ListSizeBoxHATab.TabStop = false
Me.ListSizeBoxHATab.Text = ""
Me.ListSizeBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'HardOnlyCheckHATab
'
Me.HardOnlyCheckHATab.Location = New System.Drawing.Point(336, 65)
Me.HardOnlyCheckHATab.Name = "HardOnlyCheckHATab"
Me.HardOnlyCheckHATab.Size = New System.Drawing.Size(96, 18)
Me.HardOnlyCheckHATab.TabIndex = 72
Me.HardOnlyCheckHATab.Text = "Hard Only"
'
'SoftOnlyCheckHATab
'
Me.SoftOnlyCheckHATab.Location = New System.Drawing.Point(336, 83)
Me.SoftOnlyCheckHATab.Name = "SoftOnlyCheckHATab"
Me.SoftOnlyCheckHATab.Size = New System.Drawing.Size(86, 19)
Me.SoftOnlyCheckHATab.TabIndex = 73
Me.SoftOnlyCheckHATab.Text = "Soft Only"
'
'OrLessCheckHATab
'
Me.OrLessCheckHATab.Location = New System.Drawing.Point(442, 83)
Me.OrLessCheckHATab.Name = "OrLessCheckHATab"
Me.OrLessCheckHATab.Size = New System.Drawing.Size(86, 19)
Me.OrLessCheckHATab.TabIndex = 76
Me.OrLessCheckHATab.Text = "Or Less"
'
'OrMoreCheckHATab
'
Me.OrMoreCheckHATab.Location = New System.Drawing.Point(442, 65)
Me.OrMoreCheckHATab.Name = "OrMoreCheckHATab"
Me.OrMoreCheckHATab.Size = New System.Drawing.Size(86, 18)
Me.OrMoreCheckHATab.TabIndex = 75
Me.OrMoreCheckHATab.Text = "Or More"
'
'EitherCheckHATab
'
Me.EitherCheckHATab.Location = New System.Drawing.Point(336, 37)
Me.EitherCheckHATab.Name = "EitherCheckHATab"
Me.EitherCheckHATab.Size = New System.Drawing.Size(67, 18)
Me.EitherCheckHATab.TabIndex = 71
Me.EitherCheckHATab.Text = "Either"
'
'IncludesLabelHATab
'
Me.IncludesLabelHATab.Location = New System.Drawing.Point(211, 111)
Me.IncludesLabelHATab.Name = "IncludesLabelHATab"
Me.IncludesLabelHATab.Size = New System.Drawing.Size(96, 18)
Me.IncludesLabelHATab.TabIndex = 86
Me.IncludesLabelHATab.Text = "Hand Includes"
Me.IncludesLabelHATab.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'
'HandBoxHATab
'
Me.HandBoxHATab.Location = New System.Drawing.Point(317, 111)
Me.HandBoxHATab.Name = "HandBoxHATab"
Me.HandBoxHATab.Size = New System.Drawing.Size(163, 22)
Me.HandBoxHATab.TabIndex = 78
Me.HandBoxHATab.TabStop = false
Me.HandBoxHATab.Text = ""
Me.HandBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'NCardLabelHATab
'
Me.NCardLabelHATab.Location = New System.Drawing.Point(442, 9)
Me.NCardLabelHATab.Name = "NCardLabelHATab"
Me.NCardLabelHATab.Size = New System.Drawing.Size(57, 19)
Me.NCardLabelHATab.TabIndex = 85
Me.NCardLabelHATab.Text = "N Cards"
'
'UCLabelHATab
'
Me.UCLabelHATab.Location = New System.Drawing.Point(538, 9)
Me.UCLabelHATab.Name = "UCLabelHATab"
Me.UCLabelHATab.Size = New System.Drawing.Size(57, 19)
Me.UCLabelHATab.TabIndex = 84
Me.UCLabelHATab.Text = "Upcard"
'
'SoftLabelHATab
'
Me.SoftLabelHATab.Location = New System.Drawing.Point(336, 9)
Me.SoftLabelHATab.Name = "SoftLabelHATab"
Me.SoftLabelHATab.Size = New System.Drawing.Size(67, 19)
Me.SoftLabelHATab.TabIndex = 83
Me.SoftLabelHATab.Text = "Hand Soft"
'
'TotalLabelHATab
'
Me.TotalLabelHATab.Location = New System.Drawing.Point(240, 9)
Me.TotalLabelHATab.Name = "TotalLabelHATab"
Me.TotalLabelHATab.Size = New System.Drawing.Size(77, 19)
Me.TotalLabelHATab.TabIndex = 82
Me.TotalLabelHATab.Text = "Hand Total"
'
'HandListBoxHATab
'
Me.HandListBoxHATab.ItemHeight = 16
Me.HandListBoxHATab.Location = New System.Drawing.Point(221, 148)
Me.HandListBoxHATab.Name = "HandListBoxHATab"
Me.HandListBoxHATab.Size = New System.Drawing.Size(384, 68)
Me.HandListBoxHATab.TabIndex = 80
'
'HandDetailsGroupHATab
'
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedPostLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedPreLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedPostCheckBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedPreCheckBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedMultiplierBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.CDMultiplierBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TCMultiplierBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TDMultiplierBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.BonusBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.BonusLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.BJStandBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.BJStandLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.UCDetailsBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.UCDetailsLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.NCardsDetailLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.NCardsDetailsBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.SoftDetailsCheckHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.SoftDetailsLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.UsedLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.UsedTCLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.UsedCDLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.UsedForcedLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.UsedTDLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.StratTCLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.StratCDLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.StratForcedLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.StratTDLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TCStratBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.CDStratBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedStratBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.StandBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.DoubleBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.SurrenderBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TDSplitBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TCSplitBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.CDSplitBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedSplitBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedHitBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.CDHitBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TCHitBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TDHitBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ProbBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TDStratBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TDSplitLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedSplitLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ForcedHitLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.StratLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.CDHitLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TDHitLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TCHitLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.SurrLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TCSplitLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.CDSplitLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.DoubleLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.StandLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.ProbLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.HandLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.HandNameBoxHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TotalDetailsLabelHATab)
Me.HandDetailsGroupHATab.Controls.Add(Me.TotalDetailsBoxHATab)
Me.HandDetailsGroupHATab.Location = New System.Drawing.Point(10, 268)
Me.HandDetailsGroupHATab.Name = "HandDetailsGroupHATab"
Me.HandDetailsGroupHATab.Size = New System.Drawing.Size(796, 304)
Me.HandDetailsGroupHATab.TabIndex = 81
Me.HandDetailsGroupHATab.TabStop = false
Me.HandDetailsGroupHATab.Text = "Hand Details"
'
'ForcedPostLabelHATab
'
Me.ForcedPostLabelHATab.Location = New System.Drawing.Point(600, 144)
Me.ForcedPostLabelHATab.Name = "ForcedPostLabelHATab"
Me.ForcedPostLabelHATab.Size = New System.Drawing.Size(112, 16)
Me.ForcedPostLabelHATab.TabIndex = 94
Me.ForcedPostLabelHATab.Text = "Post Split Forced"
'
'ForcedPreLabelHATab
'
Me.ForcedPreLabelHATab.Location = New System.Drawing.Point(600, 120)
Me.ForcedPreLabelHATab.Name = "ForcedPreLabelHATab"
Me.ForcedPreLabelHATab.Size = New System.Drawing.Size(112, 16)
Me.ForcedPreLabelHATab.TabIndex = 93
Me.ForcedPreLabelHATab.Text = "Pre Split Forced"
'
'ForcedPostCheckBoxHATab
'
Me.ForcedPostCheckBoxHATab.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
Me.ForcedPostCheckBoxHATab.Enabled = false
Me.ForcedPostCheckBoxHATab.Location = New System.Drawing.Point(720, 144)
Me.ForcedPostCheckBoxHATab.Name = "ForcedPostCheckBoxHATab"
Me.ForcedPostCheckBoxHATab.Size = New System.Drawing.Size(16, 16)
Me.ForcedPostCheckBoxHATab.TabIndex = 92
'
'ForcedPreCheckBoxHATab
'
Me.ForcedPreCheckBoxHATab.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
Me.ForcedPreCheckBoxHATab.Enabled = false
Me.ForcedPreCheckBoxHATab.Location = New System.Drawing.Point(720, 120)
Me.ForcedPreCheckBoxHATab.Name = "ForcedPreCheckBoxHATab"
Me.ForcedPreCheckBoxHATab.Size = New System.Drawing.Size(16, 16)
Me.ForcedPreCheckBoxHATab.TabIndex = 91
'
'ForcedMultiplierBoxHATab
'
Me.ForcedMultiplierBoxHATab.Location = New System.Drawing.Point(701, 92)
Me.ForcedMultiplierBoxHATab.Name = "ForcedMultiplierBoxHATab"
Me.ForcedMultiplierBoxHATab.ReadOnly = true
Me.ForcedMultiplierBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.ForcedMultiplierBoxHATab.TabIndex = 90
Me.ForcedMultiplierBoxHATab.TabStop = false
Me.ForcedMultiplierBoxHATab.Text = ""
Me.ForcedMultiplierBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CDMultiplierBoxHATab
'
Me.CDMultiplierBoxHATab.Location = New System.Drawing.Point(528, 92)
Me.CDMultiplierBoxHATab.Name = "CDMultiplierBoxHATab"
Me.CDMultiplierBoxHATab.ReadOnly = true
Me.CDMultiplierBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.CDMultiplierBoxHATab.TabIndex = 89
Me.CDMultiplierBoxHATab.TabStop = false
Me.CDMultiplierBoxHATab.Text = ""
Me.CDMultiplierBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TCMultiplierBoxHATab
'
Me.TCMultiplierBoxHATab.Location = New System.Drawing.Point(384, 92)
Me.TCMultiplierBoxHATab.Name = "TCMultiplierBoxHATab"
Me.TCMultiplierBoxHATab.ReadOnly = true
Me.TCMultiplierBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.TCMultiplierBoxHATab.TabIndex = 88
Me.TCMultiplierBoxHATab.TabStop = false
Me.TCMultiplierBoxHATab.Text = ""
Me.TCMultiplierBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TDMultiplierBoxHATab
'
Me.TDMultiplierBoxHATab.Location = New System.Drawing.Point(221, 92)
Me.TDMultiplierBoxHATab.Name = "TDMultiplierBoxHATab"
Me.TDMultiplierBoxHATab.ReadOnly = true
Me.TDMultiplierBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.TDMultiplierBoxHATab.TabIndex = 87
Me.TDMultiplierBoxHATab.TabStop = false
Me.TDMultiplierBoxHATab.Text = ""
Me.TDMultiplierBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BonusBoxHATab
'
Me.BonusBoxHATab.Location = New System.Drawing.Point(144, 268)
Me.BonusBoxHATab.Name = "BonusBoxHATab"
Me.BonusBoxHATab.ReadOnly = true
Me.BonusBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.BonusBoxHATab.TabIndex = 86
Me.BonusBoxHATab.TabStop = false
Me.BonusBoxHATab.Text = ""
'
'BonusLabelHATab
'
Me.BonusLabelHATab.Location = New System.Drawing.Point(38, 268)
Me.BonusLabelHATab.Name = "BonusLabelHATab"
Me.BonusLabelHATab.Size = New System.Drawing.Size(96, 18)
Me.BonusLabelHATab.TabIndex = 85
Me.BonusLabelHATab.Text = "Bonus EV"
'
'BJStandBoxHATab
'
Me.BJStandBoxHATab.Location = New System.Drawing.Point(144, 240)
Me.BJStandBoxHATab.Name = "BJStandBoxHATab"
Me.BJStandBoxHATab.ReadOnly = true
Me.BJStandBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.BJStandBoxHATab.TabIndex = 84
Me.BJStandBoxHATab.TabStop = false
Me.BJStandBoxHATab.Text = ""
'
'BJStandLabelHATab
'
Me.BJStandLabelHATab.Location = New System.Drawing.Point(38, 240)
Me.BJStandLabelHATab.Name = "BJStandLabelHATab"
Me.BJStandLabelHATab.Size = New System.Drawing.Size(96, 18)
Me.BJStandLabelHATab.TabIndex = 83
Me.BJStandLabelHATab.Text = "BJ Stand EV"
'
'UCDetailsBoxHATab
'
Me.UCDetailsBoxHATab.Location = New System.Drawing.Point(701, 28)
Me.UCDetailsBoxHATab.Name = "UCDetailsBoxHATab"
Me.UCDetailsBoxHATab.ReadOnly = true
Me.UCDetailsBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.UCDetailsBoxHATab.TabIndex = 82
Me.UCDetailsBoxHATab.TabStop = false
Me.UCDetailsBoxHATab.Text = ""
Me.UCDetailsBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UCDetailsLabelHATab
'
Me.UCDetailsLabelHATab.Location = New System.Drawing.Point(634, 28)
Me.UCDetailsLabelHATab.Name = "UCDetailsLabelHATab"
Me.UCDetailsLabelHATab.Size = New System.Drawing.Size(57, 18)
Me.UCDetailsLabelHATab.TabIndex = 81
Me.UCDetailsLabelHATab.Text = "Upcard"
'
'NCardsDetailLabelHATab
'
Me.NCardsDetailLabelHATab.Location = New System.Drawing.Point(509, 28)
Me.NCardsDetailLabelHATab.Name = "NCardsDetailLabelHATab"
Me.NCardsDetailLabelHATab.Size = New System.Drawing.Size(57, 18)
Me.NCardsDetailLabelHATab.TabIndex = 75
Me.NCardsDetailLabelHATab.Text = "N Cards"
'
'NCardsDetailsBoxHATab
'
Me.NCardsDetailsBoxHATab.Location = New System.Drawing.Point(576, 28)
Me.NCardsDetailsBoxHATab.Name = "NCardsDetailsBoxHATab"
Me.NCardsDetailsBoxHATab.ReadOnly = true
Me.NCardsDetailsBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.NCardsDetailsBoxHATab.TabIndex = 76
Me.NCardsDetailsBoxHATab.TabStop = false
Me.NCardsDetailsBoxHATab.Text = ""
Me.NCardsDetailsBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SoftDetailsCheckHATab
'
Me.SoftDetailsCheckHATab.Enabled = false
Me.SoftDetailsCheckHATab.Location = New System.Drawing.Point(470, 28)
Me.SoftDetailsCheckHATab.Name = "SoftDetailsCheckHATab"
Me.SoftDetailsCheckHATab.Size = New System.Drawing.Size(20, 18)
Me.SoftDetailsCheckHATab.TabIndex = 74
Me.SoftDetailsCheckHATab.TabStop = false
'
'SoftDetailsLabelHATab
'
Me.SoftDetailsLabelHATab.Location = New System.Drawing.Point(422, 28)
Me.SoftDetailsLabelHATab.Name = "SoftDetailsLabelHATab"
Me.SoftDetailsLabelHATab.Size = New System.Drawing.Size(29, 18)
Me.SoftDetailsLabelHATab.TabIndex = 73
Me.SoftDetailsLabelHATab.Text = "Soft"
'
'UsedLabelHATab
'
Me.UsedLabelHATab.Location = New System.Drawing.Point(38, 92)
Me.UsedLabelHATab.Name = "UsedLabelHATab"
Me.UsedLabelHATab.Size = New System.Drawing.Size(96, 19)
Me.UsedLabelHATab.TabIndex = 44
Me.UsedLabelHATab.Text = "Hand Multiplier"
'
'UsedTCLabelHATab
'
Me.UsedTCLabelHATab.Location = New System.Drawing.Point(326, 92)
Me.UsedTCLabelHATab.Name = "UsedTCLabelHATab"
Me.UsedTCLabelHATab.Size = New System.Drawing.Size(48, 19)
Me.UsedTCLabelHATab.TabIndex = 43
Me.UsedTCLabelHATab.Text = "2-Card"
'
'UsedCDLabelHATab
'
Me.UsedCDLabelHATab.Location = New System.Drawing.Point(490, 92)
Me.UsedCDLabelHATab.Name = "UsedCDLabelHATab"
Me.UsedCDLabelHATab.Size = New System.Drawing.Size(28, 19)
Me.UsedCDLabelHATab.TabIndex = 42
Me.UsedCDLabelHATab.Text = "CD"
'
'UsedForcedLabelHATab
'
Me.UsedForcedLabelHATab.Location = New System.Drawing.Point(634, 92)
Me.UsedForcedLabelHATab.Name = "UsedForcedLabelHATab"
Me.UsedForcedLabelHATab.Size = New System.Drawing.Size(57, 19)
Me.UsedForcedLabelHATab.TabIndex = 41
Me.UsedForcedLabelHATab.Text = "Forced"
'
'UsedTDLabelHATab
'
Me.UsedTDLabelHATab.Location = New System.Drawing.Point(182, 92)
Me.UsedTDLabelHATab.Name = "UsedTDLabelHATab"
Me.UsedTDLabelHATab.Size = New System.Drawing.Size(29, 19)
Me.UsedTDLabelHATab.TabIndex = 40
Me.UsedTDLabelHATab.Text = "TD"
'
'StratTCLabelHATab
'
Me.StratTCLabelHATab.Location = New System.Drawing.Point(326, 65)
Me.StratTCLabelHATab.Name = "StratTCLabelHATab"
Me.StratTCLabelHATab.Size = New System.Drawing.Size(48, 18)
Me.StratTCLabelHATab.TabIndex = 35
Me.StratTCLabelHATab.Text = "2-Card"
'
'StratCDLabelHATab
'
Me.StratCDLabelHATab.Location = New System.Drawing.Point(490, 65)
Me.StratCDLabelHATab.Name = "StratCDLabelHATab"
Me.StratCDLabelHATab.Size = New System.Drawing.Size(28, 18)
Me.StratCDLabelHATab.TabIndex = 34
Me.StratCDLabelHATab.Text = "CD"
'
'StratForcedLabelHATab
'
Me.StratForcedLabelHATab.Location = New System.Drawing.Point(634, 65)
Me.StratForcedLabelHATab.Name = "StratForcedLabelHATab"
Me.StratForcedLabelHATab.Size = New System.Drawing.Size(48, 18)
Me.StratForcedLabelHATab.TabIndex = 33
Me.StratForcedLabelHATab.Text = "Forced"
'
'StratTDLabelHATab
'
Me.StratTDLabelHATab.Location = New System.Drawing.Point(182, 65)
Me.StratTDLabelHATab.Name = "StratTDLabelHATab"
Me.StratTDLabelHATab.Size = New System.Drawing.Size(29, 18)
Me.StratTDLabelHATab.TabIndex = 32
Me.StratTDLabelHATab.Text = "TD"
'
'TCStratBoxHATab
'
Me.TCStratBoxHATab.Location = New System.Drawing.Point(384, 65)
Me.TCStratBoxHATab.Name = "TCStratBoxHATab"
Me.TCStratBoxHATab.ReadOnly = true
Me.TCStratBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.TCStratBoxHATab.TabIndex = 31
Me.TCStratBoxHATab.TabStop = false
Me.TCStratBoxHATab.Text = ""
Me.TCStratBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CDStratBoxHATab
'
Me.CDStratBoxHATab.Location = New System.Drawing.Point(528, 65)
Me.CDStratBoxHATab.Name = "CDStratBoxHATab"
Me.CDStratBoxHATab.ReadOnly = true
Me.CDStratBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.CDStratBoxHATab.TabIndex = 30
Me.CDStratBoxHATab.TabStop = false
Me.CDStratBoxHATab.Text = ""
Me.CDStratBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ForcedStratBoxHATab
'
Me.ForcedStratBoxHATab.Location = New System.Drawing.Point(701, 65)
Me.ForcedStratBoxHATab.Name = "ForcedStratBoxHATab"
Me.ForcedStratBoxHATab.ReadOnly = true
Me.ForcedStratBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.ForcedStratBoxHATab.TabIndex = 29
Me.ForcedStratBoxHATab.TabStop = false
Me.ForcedStratBoxHATab.Text = ""
Me.ForcedStratBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'StandBoxHATab
'
Me.StandBoxHATab.Location = New System.Drawing.Point(144, 157)
Me.StandBoxHATab.Name = "StandBoxHATab"
Me.StandBoxHATab.ReadOnly = true
Me.StandBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.StandBoxHATab.TabIndex = 28
Me.StandBoxHATab.TabStop = false
Me.StandBoxHATab.Text = ""
'
'DoubleBoxHATab
'
Me.DoubleBoxHATab.Location = New System.Drawing.Point(144, 185)
Me.DoubleBoxHATab.Name = "DoubleBoxHATab"
Me.DoubleBoxHATab.ReadOnly = true
Me.DoubleBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.DoubleBoxHATab.TabIndex = 27
Me.DoubleBoxHATab.TabStop = false
Me.DoubleBoxHATab.Text = ""
'
'SurrenderBoxHATab
'
Me.SurrenderBoxHATab.Location = New System.Drawing.Point(144, 212)
Me.SurrenderBoxHATab.Name = "SurrenderBoxHATab"
Me.SurrenderBoxHATab.ReadOnly = true
Me.SurrenderBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.SurrenderBoxHATab.TabIndex = 26
Me.SurrenderBoxHATab.TabStop = false
Me.SurrenderBoxHATab.Text = ""
'
'TDSplitBoxHATab
'
Me.TDSplitBoxHATab.Location = New System.Drawing.Point(624, 168)
Me.TDSplitBoxHATab.Name = "TDSplitBoxHATab"
Me.TDSplitBoxHATab.ReadOnly = true
Me.TDSplitBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.TDSplitBoxHATab.TabIndex = 25
Me.TDSplitBoxHATab.TabStop = false
Me.TDSplitBoxHATab.Text = ""
'
'TCSplitBoxHATab
'
Me.TCSplitBoxHATab.Location = New System.Drawing.Point(624, 200)
Me.TCSplitBoxHATab.Name = "TCSplitBoxHATab"
Me.TCSplitBoxHATab.ReadOnly = true
Me.TCSplitBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.TCSplitBoxHATab.TabIndex = 24
Me.TCSplitBoxHATab.TabStop = false
Me.TCSplitBoxHATab.Text = ""
'
'CDSplitBoxHATab
'
Me.CDSplitBoxHATab.Location = New System.Drawing.Point(624, 232)
Me.CDSplitBoxHATab.Name = "CDSplitBoxHATab"
Me.CDSplitBoxHATab.ReadOnly = true
Me.CDSplitBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.CDSplitBoxHATab.TabIndex = 23
Me.CDSplitBoxHATab.TabStop = false
Me.CDSplitBoxHATab.Text = ""
'
'ForcedSplitBoxHATab
'
Me.ForcedSplitBoxHATab.Location = New System.Drawing.Point(624, 264)
Me.ForcedSplitBoxHATab.Name = "ForcedSplitBoxHATab"
Me.ForcedSplitBoxHATab.ReadOnly = true
Me.ForcedSplitBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.ForcedSplitBoxHATab.TabIndex = 22
Me.ForcedSplitBoxHATab.TabStop = false
Me.ForcedSplitBoxHATab.Text = ""
'
'ForcedHitBoxHATab
'
Me.ForcedHitBoxHATab.Location = New System.Drawing.Point(384, 264)
Me.ForcedHitBoxHATab.Name = "ForcedHitBoxHATab"
Me.ForcedHitBoxHATab.ReadOnly = true
Me.ForcedHitBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.ForcedHitBoxHATab.TabIndex = 21
Me.ForcedHitBoxHATab.TabStop = false
Me.ForcedHitBoxHATab.Text = ""
'
'CDHitBoxHATab
'
Me.CDHitBoxHATab.Location = New System.Drawing.Point(384, 232)
Me.CDHitBoxHATab.Name = "CDHitBoxHATab"
Me.CDHitBoxHATab.ReadOnly = true
Me.CDHitBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.CDHitBoxHATab.TabIndex = 20
Me.CDHitBoxHATab.TabStop = false
Me.CDHitBoxHATab.Text = ""
'
'TCHitBoxHATab
'
Me.TCHitBoxHATab.Location = New System.Drawing.Point(384, 200)
Me.TCHitBoxHATab.Name = "TCHitBoxHATab"
Me.TCHitBoxHATab.ReadOnly = true
Me.TCHitBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.TCHitBoxHATab.TabIndex = 19
Me.TCHitBoxHATab.TabStop = false
Me.TCHitBoxHATab.Text = ""
'
'TDHitBoxHATab
'
Me.TDHitBoxHATab.Location = New System.Drawing.Point(384, 168)
Me.TDHitBoxHATab.Name = "TDHitBoxHATab"
Me.TDHitBoxHATab.ReadOnly = true
Me.TDHitBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.TDHitBoxHATab.TabIndex = 18
Me.TDHitBoxHATab.TabStop = false
Me.TDHitBoxHATab.Text = ""
'
'ProbBoxHATab
'
Me.ProbBoxHATab.Location = New System.Drawing.Point(144, 129)
Me.ProbBoxHATab.Name = "ProbBoxHATab"
Me.ProbBoxHATab.ReadOnly = true
Me.ProbBoxHATab.Size = New System.Drawing.Size(115, 22)
Me.ProbBoxHATab.TabIndex = 17
Me.ProbBoxHATab.TabStop = false
Me.ProbBoxHATab.Text = ""
Me.ProbBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TDStratBoxHATab
'
Me.TDStratBoxHATab.Location = New System.Drawing.Point(221, 65)
Me.TDStratBoxHATab.Name = "TDStratBoxHATab"
Me.TDStratBoxHATab.ReadOnly = true
Me.TDStratBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.TDStratBoxHATab.TabIndex = 16
Me.TDStratBoxHATab.TabStop = false
Me.TDStratBoxHATab.Text = ""
Me.TDStratBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TDSplitLabelHATab
'
Me.TDSplitLabelHATab.Location = New System.Drawing.Point(512, 168)
Me.TDSplitLabelHATab.Name = "TDSplitLabelHATab"
Me.TDSplitLabelHATab.Size = New System.Drawing.Size(106, 19)
Me.TDSplitLabelHATab.TabIndex = 14
Me.TDSplitLabelHATab.Text = "TD Split EV"
'
'ForcedSplitLabelHATab
'
Me.ForcedSplitLabelHATab.Location = New System.Drawing.Point(512, 264)
Me.ForcedSplitLabelHATab.Name = "ForcedSplitLabelHATab"
Me.ForcedSplitLabelHATab.Size = New System.Drawing.Size(106, 18)
Me.ForcedSplitLabelHATab.TabIndex = 13
Me.ForcedSplitLabelHATab.Text = "Forced Split EV"
'
'ForcedHitLabelHATab
'
Me.ForcedHitLabelHATab.Location = New System.Drawing.Point(288, 264)
Me.ForcedHitLabelHATab.Name = "ForcedHitLabelHATab"
Me.ForcedHitLabelHATab.Size = New System.Drawing.Size(96, 18)
Me.ForcedHitLabelHATab.TabIndex = 12
Me.ForcedHitLabelHATab.Text = "Forced Hit EV"
'
'StratLabelHATab
'
Me.StratLabelHATab.Location = New System.Drawing.Point(38, 65)
Me.StratLabelHATab.Name = "StratLabelHATab"
Me.StratLabelHATab.Size = New System.Drawing.Size(68, 18)
Me.StratLabelHATab.TabIndex = 11
Me.StratLabelHATab.Text = "Strategy"
'
'CDHitLabelHATab
'
Me.CDHitLabelHATab.Location = New System.Drawing.Point(288, 232)
Me.CDHitLabelHATab.Name = "CDHitLabelHATab"
Me.CDHitLabelHATab.Size = New System.Drawing.Size(96, 19)
Me.CDHitLabelHATab.TabIndex = 10
Me.CDHitLabelHATab.Text = "CD Hit EV"
'
'TDHitLabelHATab
'
Me.TDHitLabelHATab.Location = New System.Drawing.Point(288, 168)
Me.TDHitLabelHATab.Name = "TDHitLabelHATab"
Me.TDHitLabelHATab.Size = New System.Drawing.Size(96, 19)
Me.TDHitLabelHATab.TabIndex = 9
Me.TDHitLabelHATab.Text = "TD Hit EV"
'
'TCHitLabelHATab
'
Me.TCHitLabelHATab.Location = New System.Drawing.Point(288, 200)
Me.TCHitLabelHATab.Name = "TCHitLabelHATab"
Me.TCHitLabelHATab.Size = New System.Drawing.Size(96, 19)
Me.TCHitLabelHATab.TabIndex = 8
Me.TCHitLabelHATab.Text = "2-Card Hit EV"
'
'SurrLabelHATab
'
Me.SurrLabelHATab.Location = New System.Drawing.Point(38, 212)
Me.SurrLabelHATab.Name = "SurrLabelHATab"
Me.SurrLabelHATab.Size = New System.Drawing.Size(96, 19)
Me.SurrLabelHATab.TabIndex = 7
Me.SurrLabelHATab.Text = "Surrender EV"
'
'TCSplitLabelHATab
'
Me.TCSplitLabelHATab.Location = New System.Drawing.Point(512, 200)
Me.TCSplitLabelHATab.Name = "TCSplitLabelHATab"
Me.TCSplitLabelHATab.Size = New System.Drawing.Size(106, 19)
Me.TCSplitLabelHATab.TabIndex = 6
Me.TCSplitLabelHATab.Text = "2-Card Split EV"
'
'CDSplitLabelHATab
'
Me.CDSplitLabelHATab.Location = New System.Drawing.Point(512, 232)
Me.CDSplitLabelHATab.Name = "CDSplitLabelHATab"
Me.CDSplitLabelHATab.Size = New System.Drawing.Size(106, 19)
Me.CDSplitLabelHATab.TabIndex = 5
Me.CDSplitLabelHATab.Text = "CD Split EV"
'
'DoubleLabelHATab
'
Me.DoubleLabelHATab.Location = New System.Drawing.Point(38, 185)
Me.DoubleLabelHATab.Name = "DoubleLabelHATab"
Me.DoubleLabelHATab.Size = New System.Drawing.Size(96, 18)
Me.DoubleLabelHATab.TabIndex = 4
Me.DoubleLabelHATab.Text = "Double EV"
'
'StandLabelHATab
'
Me.StandLabelHATab.Location = New System.Drawing.Point(38, 157)
Me.StandLabelHATab.Name = "StandLabelHATab"
Me.StandLabelHATab.Size = New System.Drawing.Size(96, 18)
Me.StandLabelHATab.TabIndex = 3
Me.StandLabelHATab.Text = "Stand EV"
'
'ProbLabelHATab
'
Me.ProbLabelHATab.Location = New System.Drawing.Point(38, 129)
Me.ProbLabelHATab.Name = "ProbLabelHATab"
Me.ProbLabelHATab.Size = New System.Drawing.Size(96, 19)
Me.ProbLabelHATab.TabIndex = 2
Me.ProbLabelHATab.Text = "Probability"
'
'HandLabelHATab
'
Me.HandLabelHATab.Location = New System.Drawing.Point(38, 28)
Me.HandLabelHATab.Name = "HandLabelHATab"
Me.HandLabelHATab.Size = New System.Drawing.Size(39, 18)
Me.HandLabelHATab.TabIndex = 1
Me.HandLabelHATab.Text = "Hand"
'
'HandNameBoxHATab
'
Me.HandNameBoxHATab.Location = New System.Drawing.Point(86, 28)
Me.HandNameBoxHATab.Name = "HandNameBoxHATab"
Me.HandNameBoxHATab.ReadOnly = true
Me.HandNameBoxHATab.Size = New System.Drawing.Size(212, 22)
Me.HandNameBoxHATab.TabIndex = 0
Me.HandNameBoxHATab.TabStop = false
Me.HandNameBoxHATab.Text = ""
Me.HandNameBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TotalDetailsLabelHATab
'
Me.TotalDetailsLabelHATab.Location = New System.Drawing.Point(317, 28)
Me.TotalDetailsLabelHATab.Name = "TotalDetailsLabelHATab"
Me.TotalDetailsLabelHATab.Size = New System.Drawing.Size(38, 18)
Me.TotalDetailsLabelHATab.TabIndex = 71
Me.TotalDetailsLabelHATab.Text = "Total"
'
'TotalDetailsBoxHATab
'
Me.TotalDetailsBoxHATab.Location = New System.Drawing.Point(365, 28)
Me.TotalDetailsBoxHATab.Name = "TotalDetailsBoxHATab"
Me.TotalDetailsBoxHATab.ReadOnly = true
Me.TotalDetailsBoxHATab.Size = New System.Drawing.Size(38, 22)
Me.TotalDetailsBoxHATab.TabIndex = 72
Me.TotalDetailsBoxHATab.TabStop = false
Me.TotalDetailsBoxHATab.Text = ""
Me.TotalDetailsBoxHATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'HandSizeAnalysisTab
'
Me.HandSizeAnalysisTab.Controls.Add(Me.TotalComboBoxHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.UCComboBoxHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.Note2LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.OrLessCheckHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.OrMoreCheckHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.SplitEVBoxHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.SplitEVLabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.NoteLabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.HandUsedCheckHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.StrategyGroupHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.NCards2LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.SEV2LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.HEV2LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.DEV2LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.Strat2LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.SurrEV2LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.SEV1LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.HEV1LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.DEV1LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.Strat1LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.SurrEV1LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C18LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C19LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C20LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C11LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C12LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C13LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C14LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C15LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C16LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C17LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C10LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.NCards1LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.AnyLabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C3LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C4LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C5LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C6LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C7LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C8LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C9LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.C2LabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.UCLabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.SoftCheckHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.SoftLabelHSATab)
Me.HandSizeAnalysisTab.Controls.Add(Me.TotalLabelHSATab)
Me.HandSizeAnalysisTab.Location = New System.Drawing.Point(4, 25)
Me.HandSizeAnalysisTab.Name = "HandSizeAnalysisTab"
Me.HandSizeAnalysisTab.Size = New System.Drawing.Size(817, 580)
Me.HandSizeAnalysisTab.TabIndex = 1
Me.HandSizeAnalysisTab.Text = "N Card Hands"
'
'TotalComboBoxHSATab
'
Me.TotalComboBoxHSATab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.TotalComboBoxHSATab.Items.AddRange(New Object() {"4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.TotalComboBoxHSATab.Location = New System.Drawing.Point(288, 37)
Me.TotalComboBoxHSATab.Name = "TotalComboBoxHSATab"
Me.TotalComboBoxHSATab.Size = New System.Drawing.Size(67, 24)
Me.TotalComboBoxHSATab.TabIndex = 0
'
'UCComboBoxHSATab
'
Me.UCComboBoxHSATab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UCComboBoxHSATab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UCComboBoxHSATab.Location = New System.Drawing.Point(466, 37)
Me.UCComboBoxHSATab.Name = "UCComboBoxHSATab"
Me.UCComboBoxHSATab.Size = New System.Drawing.Size(57, 24)
Me.UCComboBoxHSATab.TabIndex = 180
'
'Note2LabelHSATab
'
Me.Note2LabelHSATab.Location = New System.Drawing.Point(288, 554)
Me.Note2LabelHSATab.Name = "Note2LabelHSATab"
Me.Note2LabelHSATab.Size = New System.Drawing.Size(326, 18)
Me.Note2LabelHSATab.TabIndex = 223
Me.Note2LabelHSATab.Text = "*Note: Splits are only included for Hard 4 and Soft 12."
'
'OrLessCheckHSATab
'
Me.OrLessCheckHSATab.Location = New System.Drawing.Point(341, 74)
Me.OrLessCheckHSATab.Name = "OrLessCheckHSATab"
Me.OrLessCheckHSATab.Size = New System.Drawing.Size(125, 18)
Me.OrLessCheckHSATab.TabIndex = 182
Me.OrLessCheckHSATab.Text = "NCards or less"
'
'OrMoreCheckHSATab
'
Me.OrMoreCheckHSATab.Location = New System.Drawing.Point(206, 74)
Me.OrMoreCheckHSATab.Name = "OrMoreCheckHSATab"
Me.OrMoreCheckHSATab.Size = New System.Drawing.Size(125, 18)
Me.OrMoreCheckHSATab.TabIndex = 181
Me.OrMoreCheckHSATab.Text = "NCards or more"
'
'SplitEVBoxHSATab
'
Me.SplitEVBoxHSATab.Location = New System.Drawing.Point(424, 166)
Me.SplitEVBoxHSATab.Name = "SplitEVBoxHSATab"
Me.SplitEVBoxHSATab.ReadOnly = true
Me.SplitEVBoxHSATab.Size = New System.Drawing.Size(67, 22)
Me.SplitEVBoxHSATab.TabIndex = 222
Me.SplitEVBoxHSATab.TabStop = false
Me.SplitEVBoxHSATab.Text = ""
'
'SplitEVLabelHSATab
'
Me.SplitEVLabelHSATab.Location = New System.Drawing.Point(341, 166)
Me.SplitEVLabelHSATab.Name = "SplitEVLabelHSATab"
Me.SplitEVLabelHSATab.Size = New System.Drawing.Size(57, 19)
Me.SplitEVLabelHSATab.TabIndex = 221
Me.SplitEVLabelHSATab.Text = "Split EV"
Me.SplitEVLabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'NoteLabelHSATab
'
Me.NoteLabelHSATab.Location = New System.Drawing.Point(221, 517)
Me.NoteLabelHSATab.Name = "NoteLabelHSATab"
Me.NoteLabelHSATab.Size = New System.Drawing.Size(461, 37)
Me.NoteLabelHSATab.TabIndex = 220
Me.NoteLabelHSATab.Text = "*Note: The above values do not take into account Hands Used changes that would oc"& _ 
"cur with strategy changes due to the number of cards in a hand."
'
'HandUsedCheckHSATab
'
Me.HandUsedCheckHSATab.Location = New System.Drawing.Point(485, 74)
Me.HandUsedCheckHSATab.Name = "HandUsedCheckHSATab"
Me.HandUsedCheckHSATab.Size = New System.Drawing.Size(182, 18)
Me.HandUsedCheckHSATab.TabIndex = 183
Me.HandUsedCheckHSATab.Text = "Only include Hands Used"
'
'StrategyGroupHSATab
'
Me.StrategyGroupHSATab.Controls.Add(Me.CDButtonHSATab)
Me.StrategyGroupHSATab.Controls.Add(Me.TCButtonHSATab)
Me.StrategyGroupHSATab.Controls.Add(Me.ForcedButtonHSATab)
Me.StrategyGroupHSATab.Controls.Add(Me.TDButtonHSATab)
Me.StrategyGroupHSATab.Location = New System.Drawing.Point(149, 102)
Me.StrategyGroupHSATab.Name = "StrategyGroupHSATab"
Me.StrategyGroupHSATab.Size = New System.Drawing.Size(557, 46)
Me.StrategyGroupHSATab.TabIndex = 184
Me.StrategyGroupHSATab.TabStop = false
Me.StrategyGroupHSATab.Text = "Strategy"
'
'CDButtonHSATab
'
Me.CDButtonHSATab.Location = New System.Drawing.Point(278, 18)
Me.CDButtonHSATab.Name = "CDButtonHSATab"
Me.CDButtonHSATab.Size = New System.Drawing.Size(125, 19)
Me.CDButtonHSATab.TabIndex = 2
Me.CDButtonHSATab.Text = "CD Strategy"
'
'TCButtonHSATab
'
Me.TCButtonHSATab.Location = New System.Drawing.Point(144, 18)
Me.TCButtonHSATab.Name = "TCButtonHSATab"
Me.TCButtonHSATab.Size = New System.Drawing.Size(125, 19)
Me.TCButtonHSATab.TabIndex = 1
Me.TCButtonHSATab.Text = "2-Card Strategy"
'
'ForcedButtonHSATab
'
Me.ForcedButtonHSATab.Location = New System.Drawing.Point(413, 18)
Me.ForcedButtonHSATab.Name = "ForcedButtonHSATab"
Me.ForcedButtonHSATab.Size = New System.Drawing.Size(125, 19)
Me.ForcedButtonHSATab.TabIndex = 3
Me.ForcedButtonHSATab.Text = "Forced Strategy"
'
'TDButtonHSATab
'
Me.TDButtonHSATab.Location = New System.Drawing.Point(29, 18)
Me.TDButtonHSATab.Name = "TDButtonHSATab"
Me.TDButtonHSATab.Size = New System.Drawing.Size(105, 19)
Me.TDButtonHSATab.TabIndex = 0
Me.TDButtonHSATab.Text = "TD Strategy"
'
'NCards2LabelHSATab
'
Me.NCards2LabelHSATab.Location = New System.Drawing.Point(424, 203)
Me.NCards2LabelHSATab.Name = "NCards2LabelHSATab"
Me.NCards2LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.NCards2LabelHSATab.TabIndex = 219
Me.NCards2LabelHSATab.Text = "N Cards"
Me.NCards2LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'SEV2LabelHSATab
'
Me.SEV2LabelHSATab.Location = New System.Drawing.Point(542, 203)
Me.SEV2LabelHSATab.Name = "SEV2LabelHSATab"
Me.SEV2LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.SEV2LabelHSATab.TabIndex = 218
Me.SEV2LabelHSATab.Text = "SEV"
Me.SEV2LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'HEV2LabelHSATab
'
Me.HEV2LabelHSATab.Location = New System.Drawing.Point(610, 203)
Me.HEV2LabelHSATab.Name = "HEV2LabelHSATab"
Me.HEV2LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.HEV2LabelHSATab.TabIndex = 217
Me.HEV2LabelHSATab.Text = "HEV"
Me.HEV2LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'DEV2LabelHSATab
'
Me.DEV2LabelHSATab.Location = New System.Drawing.Point(677, 203)
Me.DEV2LabelHSATab.Name = "DEV2LabelHSATab"
Me.DEV2LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.DEV2LabelHSATab.TabIndex = 216
Me.DEV2LabelHSATab.Text = "DEV"
Me.DEV2LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'Strat2LabelHSATab
'
Me.Strat2LabelHSATab.Location = New System.Drawing.Point(488, 203)
Me.Strat2LabelHSATab.Name = "Strat2LabelHSATab"
Me.Strat2LabelHSATab.Size = New System.Drawing.Size(48, 16)
Me.Strat2LabelHSATab.TabIndex = 215
Me.Strat2LabelHSATab.Text = "Strat"
Me.Strat2LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'SurrEV2LabelHSATab
'
Me.SurrEV2LabelHSATab.Location = New System.Drawing.Point(744, 203)
Me.SurrEV2LabelHSATab.Name = "SurrEV2LabelHSATab"
Me.SurrEV2LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.SurrEV2LabelHSATab.TabIndex = 214
Me.SurrEV2LabelHSATab.Text = "SurrEV"
Me.SurrEV2LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'SEV1LabelHSATab
'
Me.SEV1LabelHSATab.Location = New System.Drawing.Point(134, 203)
Me.SEV1LabelHSATab.Name = "SEV1LabelHSATab"
Me.SEV1LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.SEV1LabelHSATab.TabIndex = 213
Me.SEV1LabelHSATab.Text = "SEV"
Me.SEV1LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'HEV1LabelHSATab
'
Me.HEV1LabelHSATab.Location = New System.Drawing.Point(198, 203)
Me.HEV1LabelHSATab.Name = "HEV1LabelHSATab"
Me.HEV1LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.HEV1LabelHSATab.TabIndex = 212
Me.HEV1LabelHSATab.Text = "HEV"
Me.HEV1LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'DEV1LabelHSATab
'
Me.DEV1LabelHSATab.Location = New System.Drawing.Point(262, 203)
Me.DEV1LabelHSATab.Name = "DEV1LabelHSATab"
Me.DEV1LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.DEV1LabelHSATab.TabIndex = 211
Me.DEV1LabelHSATab.Text = "DEV"
Me.DEV1LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'Strat1LabelHSATab
'
Me.Strat1LabelHSATab.Location = New System.Drawing.Point(80, 203)
Me.Strat1LabelHSATab.Name = "Strat1LabelHSATab"
Me.Strat1LabelHSATab.Size = New System.Drawing.Size(48, 16)
Me.Strat1LabelHSATab.TabIndex = 210
Me.Strat1LabelHSATab.Text = "Strat"
Me.Strat1LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'SurrEV1LabelHSATab
'
Me.SurrEV1LabelHSATab.Location = New System.Drawing.Point(326, 203)
Me.SurrEV1LabelHSATab.Name = "SurrEV1LabelHSATab"
Me.SurrEV1LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.SurrEV1LabelHSATab.TabIndex = 209
Me.SurrEV1LabelHSATab.Text = "SurrEV"
Me.SurrEV1LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C18LabelHSATab
'
Me.C18LabelHSATab.Location = New System.Drawing.Point(424, 428)
Me.C18LabelHSATab.Name = "C18LabelHSATab"
Me.C18LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C18LabelHSATab.TabIndex = 208
Me.C18LabelHSATab.Text = "18"
Me.C18LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C19LabelHSATab
'
Me.C19LabelHSATab.Location = New System.Drawing.Point(424, 456)
Me.C19LabelHSATab.Name = "C19LabelHSATab"
Me.C19LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C19LabelHSATab.TabIndex = 207
Me.C19LabelHSATab.Text = "19"
Me.C19LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C20LabelHSATab
'
Me.C20LabelHSATab.Location = New System.Drawing.Point(424, 484)
Me.C20LabelHSATab.Name = "C20LabelHSATab"
Me.C20LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C20LabelHSATab.TabIndex = 206
Me.C20LabelHSATab.Text = "20"
Me.C20LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C11LabelHSATab
'
Me.C11LabelHSATab.Location = New System.Drawing.Point(424, 232)
Me.C11LabelHSATab.Name = "C11LabelHSATab"
Me.C11LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C11LabelHSATab.TabIndex = 205
Me.C11LabelHSATab.Text = "11"
Me.C11LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C12LabelHSATab
'
Me.C12LabelHSATab.Location = New System.Drawing.Point(424, 260)
Me.C12LabelHSATab.Name = "C12LabelHSATab"
Me.C12LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C12LabelHSATab.TabIndex = 204
Me.C12LabelHSATab.Text = "12"
Me.C12LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C13LabelHSATab
'
Me.C13LabelHSATab.Location = New System.Drawing.Point(424, 288)
Me.C13LabelHSATab.Name = "C13LabelHSATab"
Me.C13LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C13LabelHSATab.TabIndex = 203
Me.C13LabelHSATab.Text = "13"
Me.C13LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C14LabelHSATab
'
Me.C14LabelHSATab.Location = New System.Drawing.Point(424, 316)
Me.C14LabelHSATab.Name = "C14LabelHSATab"
Me.C14LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C14LabelHSATab.TabIndex = 202
Me.C14LabelHSATab.Text = "14"
Me.C14LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C15LabelHSATab
'
Me.C15LabelHSATab.Location = New System.Drawing.Point(424, 344)
Me.C15LabelHSATab.Name = "C15LabelHSATab"
Me.C15LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C15LabelHSATab.TabIndex = 201
Me.C15LabelHSATab.Text = "15"
Me.C15LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C16LabelHSATab
'
Me.C16LabelHSATab.Location = New System.Drawing.Point(424, 372)
Me.C16LabelHSATab.Name = "C16LabelHSATab"
Me.C16LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C16LabelHSATab.TabIndex = 200
Me.C16LabelHSATab.Text = "16"
Me.C16LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C17LabelHSATab
'
Me.C17LabelHSATab.Location = New System.Drawing.Point(424, 400)
Me.C17LabelHSATab.Name = "C17LabelHSATab"
Me.C17LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C17LabelHSATab.TabIndex = 199
Me.C17LabelHSATab.Text = "17"
Me.C17LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C10LabelHSATab
'
Me.C10LabelHSATab.Location = New System.Drawing.Point(16, 484)
Me.C10LabelHSATab.Name = "C10LabelHSATab"
Me.C10LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C10LabelHSATab.TabIndex = 198
Me.C10LabelHSATab.Text = "10"
Me.C10LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'NCards1LabelHSATab
'
Me.NCards1LabelHSATab.Location = New System.Drawing.Point(16, 203)
Me.NCards1LabelHSATab.Name = "NCards1LabelHSATab"
Me.NCards1LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.NCards1LabelHSATab.TabIndex = 197
Me.NCards1LabelHSATab.Text = "N Cards"
Me.NCards1LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'AnyLabelHSATab
'
Me.AnyLabelHSATab.Location = New System.Drawing.Point(16, 232)
Me.AnyLabelHSATab.Name = "AnyLabelHSATab"
Me.AnyLabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.AnyLabelHSATab.TabIndex = 196
Me.AnyLabelHSATab.Text = "Any"
Me.AnyLabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C3LabelHSATab
'
Me.C3LabelHSATab.Location = New System.Drawing.Point(16, 288)
Me.C3LabelHSATab.Name = "C3LabelHSATab"
Me.C3LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C3LabelHSATab.TabIndex = 195
Me.C3LabelHSATab.Text = "3"
Me.C3LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C4LabelHSATab
'
Me.C4LabelHSATab.Location = New System.Drawing.Point(16, 316)
Me.C4LabelHSATab.Name = "C4LabelHSATab"
Me.C4LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C4LabelHSATab.TabIndex = 194
Me.C4LabelHSATab.Text = "4"
Me.C4LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C5LabelHSATab
'
Me.C5LabelHSATab.Location = New System.Drawing.Point(16, 344)
Me.C5LabelHSATab.Name = "C5LabelHSATab"
Me.C5LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C5LabelHSATab.TabIndex = 193
Me.C5LabelHSATab.Text = "5"
Me.C5LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C6LabelHSATab
'
Me.C6LabelHSATab.Location = New System.Drawing.Point(16, 372)
Me.C6LabelHSATab.Name = "C6LabelHSATab"
Me.C6LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C6LabelHSATab.TabIndex = 192
Me.C6LabelHSATab.Text = "6"
Me.C6LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C7LabelHSATab
'
Me.C7LabelHSATab.Location = New System.Drawing.Point(16, 400)
Me.C7LabelHSATab.Name = "C7LabelHSATab"
Me.C7LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C7LabelHSATab.TabIndex = 191
Me.C7LabelHSATab.Text = "7"
Me.C7LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C8LabelHSATab
'
Me.C8LabelHSATab.Location = New System.Drawing.Point(16, 428)
Me.C8LabelHSATab.Name = "C8LabelHSATab"
Me.C8LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C8LabelHSATab.TabIndex = 190
Me.C8LabelHSATab.Text = "8"
Me.C8LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C9LabelHSATab
'
Me.C9LabelHSATab.Location = New System.Drawing.Point(16, 456)
Me.C9LabelHSATab.Name = "C9LabelHSATab"
Me.C9LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C9LabelHSATab.TabIndex = 189
Me.C9LabelHSATab.Text = "9"
Me.C9LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C2LabelHSATab
'
Me.C2LabelHSATab.Location = New System.Drawing.Point(16, 260)
Me.C2LabelHSATab.Name = "C2LabelHSATab"
Me.C2LabelHSATab.Size = New System.Drawing.Size(56, 16)
Me.C2LabelHSATab.TabIndex = 188
Me.C2LabelHSATab.Text = "2"
Me.C2LabelHSATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UCLabelHSATab
'
Me.UCLabelHSATab.Location = New System.Drawing.Point(466, 9)
Me.UCLabelHSATab.Name = "UCLabelHSATab"
Me.UCLabelHSATab.Size = New System.Drawing.Size(57, 19)
Me.UCLabelHSATab.TabIndex = 187
Me.UCLabelHSATab.Text = "Upcard"
'
'SoftCheckHSATab
'
Me.SoftCheckHSATab.Location = New System.Drawing.Point(398, 37)
Me.SoftCheckHSATab.Name = "SoftCheckHSATab"
Me.SoftCheckHSATab.Size = New System.Drawing.Size(20, 18)
Me.SoftCheckHSATab.TabIndex = 179
'
'SoftLabelHSATab
'
Me.SoftLabelHSATab.Location = New System.Drawing.Point(379, 9)
Me.SoftLabelHSATab.Name = "SoftLabelHSATab"
Me.SoftLabelHSATab.Size = New System.Drawing.Size(67, 19)
Me.SoftLabelHSATab.TabIndex = 186
Me.SoftLabelHSATab.Text = "Hand Soft"
'
'TotalLabelHSATab
'
Me.TotalLabelHSATab.Location = New System.Drawing.Point(283, 9)
Me.TotalLabelHSATab.Name = "TotalLabelHSATab"
Me.TotalLabelHSATab.Size = New System.Drawing.Size(77, 19)
Me.TotalLabelHSATab.TabIndex = 185
Me.TotalLabelHSATab.Text = "Hand Total"
'
'DoubleAnalysisTab
'
Me.DoubleAnalysisTab.Controls.Add(Me.NCardsComboBoxDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.TotalComboBoxDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.UCComboBoxDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.ExactMatchCheckDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.ListSizeLabelDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.ListSizeBoxDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.HardOnlyCheckDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.SoftOnlyCheckDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.OrLessCheckDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.OrMoreCheckDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.EitherCheckDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.IncludesLabelDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.HandBoxDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.NCardLabelDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.UCLabelDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.SoftLabelDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.TotalLabelDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.HandListBoxDATab)
Me.DoubleAnalysisTab.Controls.Add(Me.HandDetailsGroupDATab)
Me.DoubleAnalysisTab.Location = New System.Drawing.Point(4, 25)
Me.DoubleAnalysisTab.Name = "DoubleAnalysisTab"
Me.DoubleAnalysisTab.Size = New System.Drawing.Size(817, 580)
Me.DoubleAnalysisTab.TabIndex = 2
Me.DoubleAnalysisTab.Text = "Double Analysis"
'
'NCardsComboBoxDATab
'
Me.NCardsComboBoxDATab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.NCardsComboBoxDATab.Items.AddRange(New Object() {"Any", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.NCardsComboBoxDATab.Location = New System.Drawing.Point(442, 37)
Me.NCardsComboBoxDATab.Name = "NCardsComboBoxDATab"
Me.NCardsComboBoxDATab.Size = New System.Drawing.Size(67, 24)
Me.NCardsComboBoxDATab.TabIndex = 93
'
'TotalComboBoxDATab
'
Me.TotalComboBoxDATab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.TotalComboBoxDATab.Items.AddRange(New Object() {"Any", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.TotalComboBoxDATab.Location = New System.Drawing.Point(240, 37)
Me.TotalComboBoxDATab.Name = "TotalComboBoxDATab"
Me.TotalComboBoxDATab.Size = New System.Drawing.Size(67, 24)
Me.TotalComboBoxDATab.TabIndex = 89
'
'UCComboBoxDATab
'
Me.UCComboBoxDATab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UCComboBoxDATab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UCComboBoxDATab.Location = New System.Drawing.Point(538, 37)
Me.UCComboBoxDATab.Name = "UCComboBoxDATab"
Me.UCComboBoxDATab.Size = New System.Drawing.Size(57, 24)
Me.UCComboBoxDATab.TabIndex = 96
'
'ExactMatchCheckDATab
'
Me.ExactMatchCheckDATab.Location = New System.Drawing.Point(499, 111)
Me.ExactMatchCheckDATab.Name = "ExactMatchCheckDATab"
Me.ExactMatchCheckDATab.Size = New System.Drawing.Size(106, 18)
Me.ExactMatchCheckDATab.TabIndex = 98
Me.ExactMatchCheckDATab.Text = "Exact Match"
'
'ListSizeLabelDATab
'
Me.ListSizeLabelDATab.Location = New System.Drawing.Point(298, 258)
Me.ListSizeLabelDATab.Name = "ListSizeLabelDATab"
Me.ListSizeLabelDATab.Size = New System.Drawing.Size(182, 28)
Me.ListSizeLabelDATab.TabIndex = 107
Me.ListSizeLabelDATab.Text = "Hands meeting above criteria"
Me.ListSizeLabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ListSizeBoxDATab
'
Me.ListSizeBoxDATab.Location = New System.Drawing.Point(480, 258)
Me.ListSizeBoxDATab.Name = "ListSizeBoxDATab"
Me.ListSizeBoxDATab.ReadOnly = true
Me.ListSizeBoxDATab.Size = New System.Drawing.Size(48, 22)
Me.ListSizeBoxDATab.TabIndex = 106
Me.ListSizeBoxDATab.TabStop = false
Me.ListSizeBoxDATab.Text = ""
Me.ListSizeBoxDATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'HardOnlyCheckDATab
'
Me.HardOnlyCheckDATab.Location = New System.Drawing.Point(336, 65)
Me.HardOnlyCheckDATab.Name = "HardOnlyCheckDATab"
Me.HardOnlyCheckDATab.Size = New System.Drawing.Size(96, 18)
Me.HardOnlyCheckDATab.TabIndex = 91
Me.HardOnlyCheckDATab.Text = "Hard Only"
'
'SoftOnlyCheckDATab
'
Me.SoftOnlyCheckDATab.Location = New System.Drawing.Point(336, 83)
Me.SoftOnlyCheckDATab.Name = "SoftOnlyCheckDATab"
Me.SoftOnlyCheckDATab.Size = New System.Drawing.Size(86, 19)
Me.SoftOnlyCheckDATab.TabIndex = 92
Me.SoftOnlyCheckDATab.Text = "Soft Only"
'
'OrLessCheckDATab
'
Me.OrLessCheckDATab.Location = New System.Drawing.Point(442, 83)
Me.OrLessCheckDATab.Name = "OrLessCheckDATab"
Me.OrLessCheckDATab.Size = New System.Drawing.Size(86, 19)
Me.OrLessCheckDATab.TabIndex = 95
Me.OrLessCheckDATab.Text = "Or Less"
'
'OrMoreCheckDATab
'
Me.OrMoreCheckDATab.Location = New System.Drawing.Point(442, 65)
Me.OrMoreCheckDATab.Name = "OrMoreCheckDATab"
Me.OrMoreCheckDATab.Size = New System.Drawing.Size(86, 18)
Me.OrMoreCheckDATab.TabIndex = 94
Me.OrMoreCheckDATab.Text = "Or More"
'
'EitherCheckDATab
'
Me.EitherCheckDATab.Location = New System.Drawing.Point(336, 37)
Me.EitherCheckDATab.Name = "EitherCheckDATab"
Me.EitherCheckDATab.Size = New System.Drawing.Size(67, 18)
Me.EitherCheckDATab.TabIndex = 90
Me.EitherCheckDATab.Text = "Either"
'
'IncludesLabelDATab
'
Me.IncludesLabelDATab.Location = New System.Drawing.Point(211, 111)
Me.IncludesLabelDATab.Name = "IncludesLabelDATab"
Me.IncludesLabelDATab.Size = New System.Drawing.Size(96, 18)
Me.IncludesLabelDATab.TabIndex = 105
Me.IncludesLabelDATab.Text = "Hand Includes"
Me.IncludesLabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'
'HandBoxDATab
'
Me.HandBoxDATab.Location = New System.Drawing.Point(317, 111)
Me.HandBoxDATab.Name = "HandBoxDATab"
Me.HandBoxDATab.Size = New System.Drawing.Size(163, 22)
Me.HandBoxDATab.TabIndex = 97
Me.HandBoxDATab.TabStop = false
Me.HandBoxDATab.Text = ""
Me.HandBoxDATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'NCardLabelDATab
'
Me.NCardLabelDATab.Location = New System.Drawing.Point(442, 9)
Me.NCardLabelDATab.Name = "NCardLabelDATab"
Me.NCardLabelDATab.Size = New System.Drawing.Size(57, 19)
Me.NCardLabelDATab.TabIndex = 104
Me.NCardLabelDATab.Text = "N Cards"
'
'UCLabelDATab
'
Me.UCLabelDATab.Location = New System.Drawing.Point(538, 9)
Me.UCLabelDATab.Name = "UCLabelDATab"
Me.UCLabelDATab.Size = New System.Drawing.Size(57, 19)
Me.UCLabelDATab.TabIndex = 103
Me.UCLabelDATab.Text = "Upcard"
'
'SoftLabelDATab
'
Me.SoftLabelDATab.Location = New System.Drawing.Point(336, 9)
Me.SoftLabelDATab.Name = "SoftLabelDATab"
Me.SoftLabelDATab.Size = New System.Drawing.Size(67, 19)
Me.SoftLabelDATab.TabIndex = 102
Me.SoftLabelDATab.Text = "Hand Soft"
'
'TotalLabelDATab
'
Me.TotalLabelDATab.Location = New System.Drawing.Point(240, 9)
Me.TotalLabelDATab.Name = "TotalLabelDATab"
Me.TotalLabelDATab.Size = New System.Drawing.Size(77, 19)
Me.TotalLabelDATab.TabIndex = 101
Me.TotalLabelDATab.Text = "Hand Total"
'
'HandListBoxDATab
'
Me.HandListBoxDATab.ItemHeight = 16
Me.HandListBoxDATab.Location = New System.Drawing.Point(221, 148)
Me.HandListBoxDATab.Name = "HandListBoxDATab"
Me.HandListBoxDATab.Size = New System.Drawing.Size(384, 100)
Me.HandListBoxDATab.TabIndex = 99
'
'HandDetailsGroupDATab
'
Me.HandDetailsGroupDATab.Controls.Add(Me.DAllowedCheckDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.DAllowedLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.DoubleBox2DATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.DoubleLabel2DATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.C3LabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.CTLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.C4LabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.C5LabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.C2LabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.C6LabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.C7LabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.C8LabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.C9LabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.CALabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.CardStratEVLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.CardStratLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.NextCardLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.BreakdownLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.PostDoubleLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.UCDetailsBoxDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.UCDetailsLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.NCardsDetailLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.NCardsDetailsBoxDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.SoftDetailsCheckDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.SoftDetailsLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.StandBoxDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.DoubleBoxDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.SurrenderBoxDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.StratBoxDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.StratLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.SurrLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.DoubleLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.StandLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.CardProbLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.HandLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.HandNameBoxDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.TotalDetailsLabelDATab)
Me.HandDetailsGroupDATab.Controls.Add(Me.TotalDetailsBoxDATab)
Me.HandDetailsGroupDATab.Location = New System.Drawing.Point(10, 286)
Me.HandDetailsGroupDATab.Name = "HandDetailsGroupDATab"
Me.HandDetailsGroupDATab.Size = New System.Drawing.Size(796, 286)
Me.HandDetailsGroupDATab.TabIndex = 100
Me.HandDetailsGroupDATab.TabStop = false
Me.HandDetailsGroupDATab.Text = "Hand Details"
'
'DAllowedCheckDATab
'
Me.DAllowedCheckDATab.Enabled = false
Me.DAllowedCheckDATab.Location = New System.Drawing.Point(144, 203)
Me.DAllowedCheckDATab.Name = "DAllowedCheckDATab"
Me.DAllowedCheckDATab.Size = New System.Drawing.Size(19, 19)
Me.DAllowedCheckDATab.TabIndex = 164
Me.DAllowedCheckDATab.TabStop = false
'
'DAllowedLabelDATab
'
Me.DAllowedLabelDATab.Location = New System.Drawing.Point(38, 203)
Me.DAllowedLabelDATab.Name = "DAllowedLabelDATab"
Me.DAllowedLabelDATab.Size = New System.Drawing.Size(106, 19)
Me.DAllowedLabelDATab.TabIndex = 163
Me.DAllowedLabelDATab.Text = "Double Allowed"
'
'DoubleBox2DATab
'
Me.DoubleBox2DATab.Location = New System.Drawing.Point(115, 166)
Me.DoubleBox2DATab.Name = "DoubleBox2DATab"
Me.DoubleBox2DATab.ReadOnly = true
Me.DoubleBox2DATab.Size = New System.Drawing.Size(77, 22)
Me.DoubleBox2DATab.TabIndex = 135
Me.DoubleBox2DATab.TabStop = false
Me.DoubleBox2DATab.Text = ""
'
'DoubleLabel2DATab
'
Me.DoubleLabel2DATab.Location = New System.Drawing.Point(38, 166)
Me.DoubleLabel2DATab.Name = "DoubleLabel2DATab"
Me.DoubleLabel2DATab.Size = New System.Drawing.Size(77, 19)
Me.DoubleLabel2DATab.TabIndex = 134
Me.DoubleLabel2DATab.Text = "Double EV"
'
'C3LabelDATab
'
Me.C3LabelDATab.Location = New System.Drawing.Point(400, 168)
Me.C3LabelDATab.Name = "C3LabelDATab"
Me.C3LabelDATab.Size = New System.Drawing.Size(32, 16)
Me.C3LabelDATab.TabIndex = 133
Me.C3LabelDATab.Text = "3"
Me.C3LabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'CTLabelDATab
'
Me.CTLabelDATab.Location = New System.Drawing.Point(736, 168)
Me.CTLabelDATab.Name = "CTLabelDATab"
Me.CTLabelDATab.Size = New System.Drawing.Size(32, 16)
Me.CTLabelDATab.TabIndex = 132
Me.CTLabelDATab.Text = "Ten"
Me.CTLabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C4LabelDATab
'
Me.C4LabelDATab.Location = New System.Drawing.Point(448, 168)
Me.C4LabelDATab.Name = "C4LabelDATab"
Me.C4LabelDATab.Size = New System.Drawing.Size(32, 16)
Me.C4LabelDATab.TabIndex = 131
Me.C4LabelDATab.Text = "4"
Me.C4LabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C5LabelDATab
'
Me.C5LabelDATab.Location = New System.Drawing.Point(496, 168)
Me.C5LabelDATab.Name = "C5LabelDATab"
Me.C5LabelDATab.Size = New System.Drawing.Size(32, 16)
Me.C5LabelDATab.TabIndex = 130
Me.C5LabelDATab.Text = "5"
Me.C5LabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C2LabelDATab
'
Me.C2LabelDATab.Location = New System.Drawing.Point(352, 168)
Me.C2LabelDATab.Name = "C2LabelDATab"
Me.C2LabelDATab.Size = New System.Drawing.Size(32, 16)
Me.C2LabelDATab.TabIndex = 129
Me.C2LabelDATab.Text = "2"
Me.C2LabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C6LabelDATab
'
Me.C6LabelDATab.Location = New System.Drawing.Point(544, 168)
Me.C6LabelDATab.Name = "C6LabelDATab"
Me.C6LabelDATab.Size = New System.Drawing.Size(32, 16)
Me.C6LabelDATab.TabIndex = 128
Me.C6LabelDATab.Text = "6"
Me.C6LabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C7LabelDATab
'
Me.C7LabelDATab.Location = New System.Drawing.Point(592, 168)
Me.C7LabelDATab.Name = "C7LabelDATab"
Me.C7LabelDATab.Size = New System.Drawing.Size(32, 16)
Me.C7LabelDATab.TabIndex = 127
Me.C7LabelDATab.Text = "7"
Me.C7LabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C8LabelDATab
'
Me.C8LabelDATab.Location = New System.Drawing.Point(640, 168)
Me.C8LabelDATab.Name = "C8LabelDATab"
Me.C8LabelDATab.Size = New System.Drawing.Size(32, 16)
Me.C8LabelDATab.TabIndex = 126
Me.C8LabelDATab.Text = "8"
Me.C8LabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C9LabelDATab
'
Me.C9LabelDATab.Location = New System.Drawing.Point(688, 168)
Me.C9LabelDATab.Name = "C9LabelDATab"
Me.C9LabelDATab.Size = New System.Drawing.Size(32, 16)
Me.C9LabelDATab.TabIndex = 125
Me.C9LabelDATab.Text = "9"
Me.C9LabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'CALabelDATab
'
Me.CALabelDATab.Location = New System.Drawing.Point(304, 168)
Me.CALabelDATab.Name = "CALabelDATab"
Me.CALabelDATab.Size = New System.Drawing.Size(32, 16)
Me.CALabelDATab.TabIndex = 124
Me.CALabelDATab.Text = "Ace"
Me.CALabelDATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'CardStratEVLabelDATab
'
Me.CardStratEVLabelDATab.Location = New System.Drawing.Point(208, 250)
Me.CardStratEVLabelDATab.Name = "CardStratEVLabelDATab"
Me.CardStratEVLabelDATab.Size = New System.Drawing.Size(80, 20)
Me.CardStratEVLabelDATab.TabIndex = 89
Me.CardStratEVLabelDATab.Text = "Strategy EV"
'
'CardStratLabelDATab
'
Me.CardStratLabelDATab.Location = New System.Drawing.Point(208, 194)
Me.CardStratLabelDATab.Name = "CardStratLabelDATab"
Me.CardStratLabelDATab.Size = New System.Drawing.Size(80, 20)
Me.CardStratLabelDATab.TabIndex = 86
Me.CardStratLabelDATab.Text = "Strategy"
'
'NextCardLabelDATab
'
Me.NextCardLabelDATab.Location = New System.Drawing.Point(208, 168)
Me.NextCardLabelDATab.Name = "NextCardLabelDATab"
Me.NextCardLabelDATab.Size = New System.Drawing.Size(80, 16)
Me.NextCardLabelDATab.TabIndex = 85
Me.NextCardLabelDATab.Text = "Next Card"
'
'BreakdownLabelDATab
'
Me.BreakdownLabelDATab.Location = New System.Drawing.Point(19, 138)
Me.BreakdownLabelDATab.Name = "BreakdownLabelDATab"
Me.BreakdownLabelDATab.Size = New System.Drawing.Size(173, 19)
Me.BreakdownLabelDATab.TabIndex = 84
Me.BreakdownLabelDATab.Text = "Breakdown of Double EV:"
'
'PostDoubleLabelDATab
'
Me.PostDoubleLabelDATab.Location = New System.Drawing.Point(19, 65)
Me.PostDoubleLabelDATab.Name = "PostDoubleLabelDATab"
Me.PostDoubleLabelDATab.Size = New System.Drawing.Size(173, 18)
Me.PostDoubleLabelDATab.TabIndex = 83
Me.PostDoubleLabelDATab.Text = "When Hand is Post-Double:"
'
'UCDetailsBoxDATab
'
Me.UCDetailsBoxDATab.Location = New System.Drawing.Point(701, 28)
Me.UCDetailsBoxDATab.Name = "UCDetailsBoxDATab"
Me.UCDetailsBoxDATab.ReadOnly = true
Me.UCDetailsBoxDATab.Size = New System.Drawing.Size(38, 22)
Me.UCDetailsBoxDATab.TabIndex = 82
Me.UCDetailsBoxDATab.TabStop = false
Me.UCDetailsBoxDATab.Text = ""
Me.UCDetailsBoxDATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UCDetailsLabelDATab
'
Me.UCDetailsLabelDATab.Location = New System.Drawing.Point(634, 28)
Me.UCDetailsLabelDATab.Name = "UCDetailsLabelDATab"
Me.UCDetailsLabelDATab.Size = New System.Drawing.Size(57, 18)
Me.UCDetailsLabelDATab.TabIndex = 81
Me.UCDetailsLabelDATab.Text = "Upcard"
'
'NCardsDetailLabelDATab
'
Me.NCardsDetailLabelDATab.Location = New System.Drawing.Point(509, 28)
Me.NCardsDetailLabelDATab.Name = "NCardsDetailLabelDATab"
Me.NCardsDetailLabelDATab.Size = New System.Drawing.Size(57, 18)
Me.NCardsDetailLabelDATab.TabIndex = 75
Me.NCardsDetailLabelDATab.Text = "N Cards"
'
'NCardsDetailsBoxDATab
'
Me.NCardsDetailsBoxDATab.Location = New System.Drawing.Point(576, 28)
Me.NCardsDetailsBoxDATab.Name = "NCardsDetailsBoxDATab"
Me.NCardsDetailsBoxDATab.ReadOnly = true
Me.NCardsDetailsBoxDATab.Size = New System.Drawing.Size(38, 22)
Me.NCardsDetailsBoxDATab.TabIndex = 76
Me.NCardsDetailsBoxDATab.TabStop = false
Me.NCardsDetailsBoxDATab.Text = ""
Me.NCardsDetailsBoxDATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SoftDetailsCheckDATab
'
Me.SoftDetailsCheckDATab.Enabled = false
Me.SoftDetailsCheckDATab.Location = New System.Drawing.Point(470, 28)
Me.SoftDetailsCheckDATab.Name = "SoftDetailsCheckDATab"
Me.SoftDetailsCheckDATab.Size = New System.Drawing.Size(20, 18)
Me.SoftDetailsCheckDATab.TabIndex = 74
Me.SoftDetailsCheckDATab.TabStop = false
'
'SoftDetailsLabelDATab
'
Me.SoftDetailsLabelDATab.Location = New System.Drawing.Point(422, 28)
Me.SoftDetailsLabelDATab.Name = "SoftDetailsLabelDATab"
Me.SoftDetailsLabelDATab.Size = New System.Drawing.Size(29, 18)
Me.SoftDetailsLabelDATab.TabIndex = 73
Me.SoftDetailsLabelDATab.Text = "Soft"
'
'StandBoxDATab
'
Me.StandBoxDATab.Location = New System.Drawing.Point(288, 92)
Me.StandBoxDATab.Name = "StandBoxDATab"
Me.StandBoxDATab.ReadOnly = true
Me.StandBoxDATab.Size = New System.Drawing.Size(77, 22)
Me.StandBoxDATab.TabIndex = 28
Me.StandBoxDATab.TabStop = false
Me.StandBoxDATab.Text = ""
'
'DoubleBoxDATab
'
Me.DoubleBoxDATab.Location = New System.Drawing.Point(480, 92)
Me.DoubleBoxDATab.Name = "DoubleBoxDATab"
Me.DoubleBoxDATab.ReadOnly = true
Me.DoubleBoxDATab.Size = New System.Drawing.Size(77, 22)
Me.DoubleBoxDATab.TabIndex = 27
Me.DoubleBoxDATab.TabStop = false
Me.DoubleBoxDATab.Text = ""
'
'SurrenderBoxDATab
'
Me.SurrenderBoxDATab.Location = New System.Drawing.Point(691, 92)
Me.SurrenderBoxDATab.Name = "SurrenderBoxDATab"
Me.SurrenderBoxDATab.ReadOnly = true
Me.SurrenderBoxDATab.Size = New System.Drawing.Size(77, 22)
Me.SurrenderBoxDATab.TabIndex = 26
Me.SurrenderBoxDATab.TabStop = false
Me.SurrenderBoxDATab.Text = ""
'
'StratBoxDATab
'
Me.StratBoxDATab.Location = New System.Drawing.Point(134, 92)
Me.StratBoxDATab.Name = "StratBoxDATab"
Me.StratBoxDATab.ReadOnly = true
Me.StratBoxDATab.Size = New System.Drawing.Size(39, 22)
Me.StratBoxDATab.TabIndex = 16
Me.StratBoxDATab.TabStop = false
Me.StratBoxDATab.Text = ""
Me.StratBoxDATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'StratLabelDATab
'
Me.StratLabelDATab.Location = New System.Drawing.Point(38, 92)
Me.StratLabelDATab.Name = "StratLabelDATab"
Me.StratLabelDATab.Size = New System.Drawing.Size(77, 19)
Me.StratLabelDATab.TabIndex = 11
Me.StratLabelDATab.Text = "Strategy"
'
'SurrLabelDATab
'
Me.SurrLabelDATab.Location = New System.Drawing.Point(595, 92)
Me.SurrLabelDATab.Name = "SurrLabelDATab"
Me.SurrLabelDATab.Size = New System.Drawing.Size(96, 19)
Me.SurrLabelDATab.TabIndex = 7
Me.SurrLabelDATab.Text = "Surrender EV"
'
'DoubleLabelDATab
'
Me.DoubleLabelDATab.Location = New System.Drawing.Point(403, 92)
Me.DoubleLabelDATab.Name = "DoubleLabelDATab"
Me.DoubleLabelDATab.Size = New System.Drawing.Size(77, 19)
Me.DoubleLabelDATab.TabIndex = 4
Me.DoubleLabelDATab.Text = "Double EV"
'
'StandLabelDATab
'
Me.StandLabelDATab.Location = New System.Drawing.Point(211, 92)
Me.StandLabelDATab.Name = "StandLabelDATab"
Me.StandLabelDATab.Size = New System.Drawing.Size(77, 19)
Me.StandLabelDATab.TabIndex = 3
Me.StandLabelDATab.Text = "Stand EV"
'
'CardProbLabelDATab
'
Me.CardProbLabelDATab.Location = New System.Drawing.Point(208, 222)
Me.CardProbLabelDATab.Name = "CardProbLabelDATab"
Me.CardProbLabelDATab.Size = New System.Drawing.Size(80, 20)
Me.CardProbLabelDATab.TabIndex = 2
Me.CardProbLabelDATab.Text = "Probability"
'
'HandLabelDATab
'
Me.HandLabelDATab.Location = New System.Drawing.Point(38, 28)
Me.HandLabelDATab.Name = "HandLabelDATab"
Me.HandLabelDATab.Size = New System.Drawing.Size(39, 18)
Me.HandLabelDATab.TabIndex = 1
Me.HandLabelDATab.Text = "Hand"
'
'HandNameBoxDATab
'
Me.HandNameBoxDATab.Location = New System.Drawing.Point(86, 28)
Me.HandNameBoxDATab.Name = "HandNameBoxDATab"
Me.HandNameBoxDATab.ReadOnly = true
Me.HandNameBoxDATab.Size = New System.Drawing.Size(212, 22)
Me.HandNameBoxDATab.TabIndex = 0
Me.HandNameBoxDATab.TabStop = false
Me.HandNameBoxDATab.Text = ""
Me.HandNameBoxDATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TotalDetailsLabelDATab
'
Me.TotalDetailsLabelDATab.Location = New System.Drawing.Point(317, 28)
Me.TotalDetailsLabelDATab.Name = "TotalDetailsLabelDATab"
Me.TotalDetailsLabelDATab.Size = New System.Drawing.Size(38, 18)
Me.TotalDetailsLabelDATab.TabIndex = 71
Me.TotalDetailsLabelDATab.Text = "Total"
'
'TotalDetailsBoxDATab
'
Me.TotalDetailsBoxDATab.Location = New System.Drawing.Point(365, 28)
Me.TotalDetailsBoxDATab.Name = "TotalDetailsBoxDATab"
Me.TotalDetailsBoxDATab.ReadOnly = true
Me.TotalDetailsBoxDATab.Size = New System.Drawing.Size(38, 22)
Me.TotalDetailsBoxDATab.TabIndex = 72
Me.TotalDetailsBoxDATab.TabStop = false
Me.TotalDetailsBoxDATab.Text = ""
Me.TotalDetailsBoxDATab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'AllExceptionsTab
'
Me.AllExceptionsTab.Controls.Add(Me.ExceptionsTabControl)
Me.AllExceptionsTab.Location = New System.Drawing.Point(4, 25)
Me.AllExceptionsTab.Name = "AllExceptionsTab"
Me.AllExceptionsTab.Size = New System.Drawing.Size(846, 626)
Me.AllExceptionsTab.TabIndex = 13
Me.AllExceptionsTab.Text = "Exceptions"
'
'ExceptionsTabControl
'
Me.ExceptionsTabControl.Controls.Add(Me.ExceptionsTab)
Me.ExceptionsTabControl.Controls.Add(Me.NCardExceptionsTab)
Me.ExceptionsTabControl.Location = New System.Drawing.Point(10, 9)
Me.ExceptionsTabControl.Name = "ExceptionsTabControl"
Me.ExceptionsTabControl.SelectedIndex = 0
Me.ExceptionsTabControl.Size = New System.Drawing.Size(825, 609)
Me.ExceptionsTabControl.TabIndex = 0
'
'ExceptionsTab
'
Me.ExceptionsTab.Controls.Add(Me.TotalComboBoxETab)
Me.ExceptionsTab.Controls.Add(Me.NCardsComboBoxETab)
Me.ExceptionsTab.Controls.Add(Me.UCComboBoxETab)
Me.ExceptionsTab.Controls.Add(Me.PostSplitCheckETab)
Me.ExceptionsTab.Controls.Add(Me.PreSplitCheckETab)
Me.ExceptionsTab.Controls.Add(Me.ETypeLabelETab)
Me.ExceptionsTab.Controls.Add(Me.ExTypeComboBoxETab)
Me.ExceptionsTab.Controls.Add(Me.ExactMatchCheckETab)
Me.ExceptionsTab.Controls.Add(Me.ListSizeLabelETab)
Me.ExceptionsTab.Controls.Add(Me.ListSizeBoxETab)
Me.ExceptionsTab.Controls.Add(Me.HardOnlyCheckETab)
Me.ExceptionsTab.Controls.Add(Me.SoftOnlyCheckETab)
Me.ExceptionsTab.Controls.Add(Me.OrLessCheckETab)
Me.ExceptionsTab.Controls.Add(Me.OrMoreCheckETab)
Me.ExceptionsTab.Controls.Add(Me.EitherCheckETab)
Me.ExceptionsTab.Controls.Add(Me.IncludesLabelETab)
Me.ExceptionsTab.Controls.Add(Me.HandBoxETab)
Me.ExceptionsTab.Controls.Add(Me.NCardLabelETab)
Me.ExceptionsTab.Controls.Add(Me.UCLabelETab)
Me.ExceptionsTab.Controls.Add(Me.SoftLabelETab)
Me.ExceptionsTab.Controls.Add(Me.TotalLabelETab)
Me.ExceptionsTab.Controls.Add(Me.HandListBoxETab)
Me.ExceptionsTab.Controls.Add(Me.ExceptionDetailsGroupETab)
Me.ExceptionsTab.Location = New System.Drawing.Point(4, 25)
Me.ExceptionsTab.Name = "ExceptionsTab"
Me.ExceptionsTab.Size = New System.Drawing.Size(817, 580)
Me.ExceptionsTab.TabIndex = 0
Me.ExceptionsTab.Text = "Individual Hands"
'
'TotalComboBoxETab
'
Me.TotalComboBoxETab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.TotalComboBoxETab.Items.AddRange(New Object() {"Any", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.TotalComboBoxETab.Location = New System.Drawing.Point(125, 42)
Me.TotalComboBoxETab.Name = "TotalComboBoxETab"
Me.TotalComboBoxETab.Size = New System.Drawing.Size(67, 24)
Me.TotalComboBoxETab.TabIndex = 92
'
'NCardsComboBoxETab
'
Me.NCardsComboBoxETab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.NCardsComboBoxETab.Items.AddRange(New Object() {"Any", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.NCardsComboBoxETab.Location = New System.Drawing.Point(326, 42)
Me.NCardsComboBoxETab.Name = "NCardsComboBoxETab"
Me.NCardsComboBoxETab.Size = New System.Drawing.Size(68, 24)
Me.NCardsComboBoxETab.TabIndex = 96
'
'UCComboBoxETab
'
Me.UCComboBoxETab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UCComboBoxETab.Items.AddRange(New Object() {"All", "A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UCComboBoxETab.Location = New System.Drawing.Point(422, 42)
Me.UCComboBoxETab.Name = "UCComboBoxETab"
Me.UCComboBoxETab.Size = New System.Drawing.Size(58, 24)
Me.UCComboBoxETab.TabIndex = 99
'
'PostSplitCheckETab
'
Me.PostSplitCheckETab.Location = New System.Drawing.Point(509, 60)
Me.PostSplitCheckETab.Name = "PostSplitCheckETab"
Me.PostSplitCheckETab.Size = New System.Drawing.Size(86, 18)
Me.PostSplitCheckETab.TabIndex = 101
Me.PostSplitCheckETab.Text = "Post-Split"
'
'PreSplitCheckETab
'
Me.PreSplitCheckETab.Location = New System.Drawing.Point(509, 42)
Me.PreSplitCheckETab.Name = "PreSplitCheckETab"
Me.PreSplitCheckETab.Size = New System.Drawing.Size(86, 18)
Me.PreSplitCheckETab.TabIndex = 100
Me.PreSplitCheckETab.Text = "Pre-Split"
'
'ETypeLabelETab
'
Me.ETypeLabelETab.Location = New System.Drawing.Point(605, 14)
Me.ETypeLabelETab.Name = "ETypeLabelETab"
Me.ETypeLabelETab.Size = New System.Drawing.Size(105, 18)
Me.ETypeLabelETab.TabIndex = 114
Me.ETypeLabelETab.Text = "Exception Type"
'
'ExTypeComboBoxETab
'
Me.ExTypeComboBoxETab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.ExTypeComboBoxETab.Location = New System.Drawing.Point(605, 42)
Me.ExTypeComboBoxETab.Name = "ExTypeComboBoxETab"
Me.ExTypeComboBoxETab.Size = New System.Drawing.Size(96, 24)
Me.ExTypeComboBoxETab.TabIndex = 102
'
'ExactMatchCheckETab
'
Me.ExactMatchCheckETab.Location = New System.Drawing.Point(499, 115)
Me.ExactMatchCheckETab.Name = "ExactMatchCheckETab"
Me.ExactMatchCheckETab.Size = New System.Drawing.Size(106, 19)
Me.ExactMatchCheckETab.TabIndex = 104
Me.ExactMatchCheckETab.Text = "Exact Match"
'
'ListSizeLabelETab
'
Me.ListSizeLabelETab.Location = New System.Drawing.Point(298, 254)
Me.ListSizeLabelETab.Name = "ListSizeLabelETab"
Me.ListSizeLabelETab.Size = New System.Drawing.Size(182, 28)
Me.ListSizeLabelETab.TabIndex = 113
Me.ListSizeLabelETab.Text = "Hands meeting above criteria"
Me.ListSizeLabelETab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ListSizeBoxETab
'
Me.ListSizeBoxETab.Location = New System.Drawing.Point(480, 254)
Me.ListSizeBoxETab.Name = "ListSizeBoxETab"
Me.ListSizeBoxETab.ReadOnly = true
Me.ListSizeBoxETab.Size = New System.Drawing.Size(48, 22)
Me.ListSizeBoxETab.TabIndex = 112
Me.ListSizeBoxETab.TabStop = false
Me.ListSizeBoxETab.Text = ""
Me.ListSizeBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'HardOnlyCheckETab
'
Me.HardOnlyCheckETab.Location = New System.Drawing.Point(221, 69)
Me.HardOnlyCheckETab.Name = "HardOnlyCheckETab"
Me.HardOnlyCheckETab.Size = New System.Drawing.Size(96, 19)
Me.HardOnlyCheckETab.TabIndex = 94
Me.HardOnlyCheckETab.Text = "Hard Only"
'
'SoftOnlyCheckETab
'
Me.SoftOnlyCheckETab.Location = New System.Drawing.Point(221, 88)
Me.SoftOnlyCheckETab.Name = "SoftOnlyCheckETab"
Me.SoftOnlyCheckETab.Size = New System.Drawing.Size(86, 18)
Me.SoftOnlyCheckETab.TabIndex = 95
Me.SoftOnlyCheckETab.Text = "Soft Only"
'
'OrLessCheckETab
'
Me.OrLessCheckETab.Location = New System.Drawing.Point(326, 88)
Me.OrLessCheckETab.Name = "OrLessCheckETab"
Me.OrLessCheckETab.Size = New System.Drawing.Size(87, 18)
Me.OrLessCheckETab.TabIndex = 98
Me.OrLessCheckETab.Text = "Or Less"
'
'OrMoreCheckETab
'
Me.OrMoreCheckETab.Location = New System.Drawing.Point(326, 69)
Me.OrMoreCheckETab.Name = "OrMoreCheckETab"
Me.OrMoreCheckETab.Size = New System.Drawing.Size(87, 19)
Me.OrMoreCheckETab.TabIndex = 97
Me.OrMoreCheckETab.Text = "Or More"
'
'EitherCheckETab
'
Me.EitherCheckETab.Location = New System.Drawing.Point(221, 42)
Me.EitherCheckETab.Name = "EitherCheckETab"
Me.EitherCheckETab.Size = New System.Drawing.Size(67, 18)
Me.EitherCheckETab.TabIndex = 93
Me.EitherCheckETab.Text = "Either"
'
'IncludesLabelETab
'
Me.IncludesLabelETab.Location = New System.Drawing.Point(211, 115)
Me.IncludesLabelETab.Name = "IncludesLabelETab"
Me.IncludesLabelETab.Size = New System.Drawing.Size(96, 19)
Me.IncludesLabelETab.TabIndex = 111
Me.IncludesLabelETab.Text = "Hand Includes"
Me.IncludesLabelETab.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'
'HandBoxETab
'
Me.HandBoxETab.Location = New System.Drawing.Point(317, 115)
Me.HandBoxETab.Name = "HandBoxETab"
Me.HandBoxETab.Size = New System.Drawing.Size(163, 22)
Me.HandBoxETab.TabIndex = 103
Me.HandBoxETab.TabStop = false
Me.HandBoxETab.Text = ""
Me.HandBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'NCardLabelETab
'
Me.NCardLabelETab.Location = New System.Drawing.Point(326, 14)
Me.NCardLabelETab.Name = "NCardLabelETab"
Me.NCardLabelETab.Size = New System.Drawing.Size(58, 18)
Me.NCardLabelETab.TabIndex = 110
Me.NCardLabelETab.Text = "N Cards"
'
'UCLabelETab
'
Me.UCLabelETab.Location = New System.Drawing.Point(422, 14)
Me.UCLabelETab.Name = "UCLabelETab"
Me.UCLabelETab.Size = New System.Drawing.Size(58, 18)
Me.UCLabelETab.TabIndex = 109
Me.UCLabelETab.Text = "Upcard"
'
'SoftLabelETab
'
Me.SoftLabelETab.Location = New System.Drawing.Point(221, 14)
Me.SoftLabelETab.Name = "SoftLabelETab"
Me.SoftLabelETab.Size = New System.Drawing.Size(67, 18)
Me.SoftLabelETab.TabIndex = 108
Me.SoftLabelETab.Text = "Hand Soft"
'
'TotalLabelETab
'
Me.TotalLabelETab.Location = New System.Drawing.Point(125, 14)
Me.TotalLabelETab.Name = "TotalLabelETab"
Me.TotalLabelETab.Size = New System.Drawing.Size(77, 18)
Me.TotalLabelETab.TabIndex = 107
Me.TotalLabelETab.Text = "Hand Total"
'
'HandListBoxETab
'
Me.HandListBoxETab.ItemHeight = 16
Me.HandListBoxETab.Location = New System.Drawing.Point(221, 143)
Me.HandListBoxETab.Name = "HandListBoxETab"
Me.HandListBoxETab.Size = New System.Drawing.Size(384, 100)
Me.HandListBoxETab.TabIndex = 105
'
'ExceptionDetailsGroupETab
'
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExRuleNameBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExRuleNameLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExForcedButtonETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.PaircardBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.PaircardLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExSurrenderBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExStandBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExDoubleBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.StateBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExTypeBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExNameBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseNameBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExStratDetailsLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseHandUsedCheckETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExHandUsedCheckETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.NCardsDetailLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.NCardsDetailsBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.SoftDetailsCheckETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.SoftDetailsLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.UsedLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.StratNameDetailsLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.StratLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExStratETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseStandBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseDoubleBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseSurrenderBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseSplitBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExSplitBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExHitBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseHitBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ProbBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseStratETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.SplitLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.BaseStratDetailsLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.HitLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.StateLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.SurrLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ExTypeLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.DoubleLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.StandLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.ProbLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.HandLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.HandNameBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.TotalDetailsLabelETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.TotalDetailsBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.UCDetailsBoxETab)
Me.ExceptionDetailsGroupETab.Controls.Add(Me.UCDetailsLabelETab)
Me.ExceptionDetailsGroupETab.Location = New System.Drawing.Point(10, 277)
Me.ExceptionDetailsGroupETab.Name = "ExceptionDetailsGroupETab"
Me.ExceptionDetailsGroupETab.Size = New System.Drawing.Size(796, 295)
Me.ExceptionDetailsGroupETab.TabIndex = 106
Me.ExceptionDetailsGroupETab.TabStop = false
Me.ExceptionDetailsGroupETab.Text = "Exception Details"
'
'ExRuleNameBoxETab
'
Me.ExRuleNameBoxETab.Location = New System.Drawing.Point(605, 106)
Me.ExRuleNameBoxETab.Name = "ExRuleNameBoxETab"
Me.ExRuleNameBoxETab.ReadOnly = true
Me.ExRuleNameBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ExRuleNameBoxETab.TabIndex = 109
Me.ExRuleNameBoxETab.TabStop = false
Me.ExRuleNameBoxETab.Text = ""
Me.ExRuleNameBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ExRuleNameLabelETab
'
Me.ExRuleNameLabelETab.Location = New System.Drawing.Point(480, 106)
Me.ExRuleNameLabelETab.Name = "ExRuleNameLabelETab"
Me.ExRuleNameLabelETab.Size = New System.Drawing.Size(106, 19)
Me.ExRuleNameLabelETab.TabIndex = 108
Me.ExRuleNameLabelETab.Text = "Exception Name"
'
'ExForcedButtonETab
'
Me.ExForcedButtonETab.Location = New System.Drawing.Point(538, 226)
Me.ExForcedButtonETab.Name = "ExForcedButtonETab"
Me.ExForcedButtonETab.Size = New System.Drawing.Size(153, 37)
Me.ExForcedButtonETab.TabIndex = 107
Me.ExForcedButtonETab.Text = "Send to Forced Rules"
'
'PaircardBoxETab
'
Me.PaircardBoxETab.Location = New System.Drawing.Point(605, 189)
Me.PaircardBoxETab.Name = "PaircardBoxETab"
Me.PaircardBoxETab.ReadOnly = true
Me.PaircardBoxETab.Size = New System.Drawing.Size(115, 22)
Me.PaircardBoxETab.TabIndex = 105
Me.PaircardBoxETab.TabStop = false
Me.PaircardBoxETab.Text = ""
'
'PaircardLabelETab
'
Me.PaircardLabelETab.Location = New System.Drawing.Point(480, 189)
Me.PaircardLabelETab.Name = "PaircardLabelETab"
Me.PaircardLabelETab.Size = New System.Drawing.Size(96, 19)
Me.PaircardLabelETab.TabIndex = 104
Me.PaircardLabelETab.Text = "Paircard"
'
'ExSurrenderBoxETab
'
Me.ExSurrenderBoxETab.Location = New System.Drawing.Point(317, 217)
Me.ExSurrenderBoxETab.Name = "ExSurrenderBoxETab"
Me.ExSurrenderBoxETab.ReadOnly = true
Me.ExSurrenderBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ExSurrenderBoxETab.TabIndex = 103
Me.ExSurrenderBoxETab.TabStop = false
Me.ExSurrenderBoxETab.Text = ""
'
'ExStandBoxETab
'
Me.ExStandBoxETab.Location = New System.Drawing.Point(317, 134)
Me.ExStandBoxETab.Name = "ExStandBoxETab"
Me.ExStandBoxETab.ReadOnly = true
Me.ExStandBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ExStandBoxETab.TabIndex = 102
Me.ExStandBoxETab.TabStop = false
Me.ExStandBoxETab.Text = ""
'
'ExDoubleBoxETab
'
Me.ExDoubleBoxETab.Location = New System.Drawing.Point(317, 162)
Me.ExDoubleBoxETab.Name = "ExDoubleBoxETab"
Me.ExDoubleBoxETab.ReadOnly = true
Me.ExDoubleBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ExDoubleBoxETab.TabIndex = 101
Me.ExDoubleBoxETab.TabStop = false
Me.ExDoubleBoxETab.Text = ""
'
'StateBoxETab
'
Me.StateBoxETab.Location = New System.Drawing.Point(605, 162)
Me.StateBoxETab.Name = "StateBoxETab"
Me.StateBoxETab.ReadOnly = true
Me.StateBoxETab.Size = New System.Drawing.Size(115, 22)
Me.StateBoxETab.TabIndex = 100
Me.StateBoxETab.TabStop = false
Me.StateBoxETab.Text = ""
'
'ExTypeBoxETab
'
Me.ExTypeBoxETab.Location = New System.Drawing.Point(605, 78)
Me.ExTypeBoxETab.Name = "ExTypeBoxETab"
Me.ExTypeBoxETab.ReadOnly = true
Me.ExTypeBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ExTypeBoxETab.TabIndex = 99
Me.ExTypeBoxETab.TabStop = false
Me.ExTypeBoxETab.Text = ""
Me.ExTypeBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ExNameBoxETab
'
Me.ExNameBoxETab.Location = New System.Drawing.Point(317, 78)
Me.ExNameBoxETab.Name = "ExNameBoxETab"
Me.ExNameBoxETab.ReadOnly = true
Me.ExNameBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ExNameBoxETab.TabIndex = 98
Me.ExNameBoxETab.TabStop = false
Me.ExNameBoxETab.Text = ""
Me.ExNameBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BaseNameBoxETab
'
Me.BaseNameBoxETab.Location = New System.Drawing.Point(182, 78)
Me.BaseNameBoxETab.Name = "BaseNameBoxETab"
Me.BaseNameBoxETab.ReadOnly = true
Me.BaseNameBoxETab.Size = New System.Drawing.Size(116, 22)
Me.BaseNameBoxETab.TabIndex = 97
Me.BaseNameBoxETab.TabStop = false
Me.BaseNameBoxETab.Text = ""
Me.BaseNameBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ExStratDetailsLabelETab
'
Me.ExStratDetailsLabelETab.Location = New System.Drawing.Point(307, 51)
Me.ExStratDetailsLabelETab.Name = "ExStratDetailsLabelETab"
Me.ExStratDetailsLabelETab.Size = New System.Drawing.Size(135, 18)
Me.ExStratDetailsLabelETab.TabIndex = 96
Me.ExStratDetailsLabelETab.Text = "Exception Strategy"
Me.ExStratDetailsLabelETab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'BaseHandUsedCheckETab
'
Me.BaseHandUsedCheckETab.Enabled = false
Me.BaseHandUsedCheckETab.Location = New System.Drawing.Point(230, 272)
Me.BaseHandUsedCheckETab.Name = "BaseHandUsedCheckETab"
Me.BaseHandUsedCheckETab.Size = New System.Drawing.Size(20, 19)
Me.BaseHandUsedCheckETab.TabIndex = 80
Me.BaseHandUsedCheckETab.TabStop = false
'
'ExHandUsedCheckETab
'
Me.ExHandUsedCheckETab.Enabled = false
Me.ExHandUsedCheckETab.Location = New System.Drawing.Point(365, 272)
Me.ExHandUsedCheckETab.Name = "ExHandUsedCheckETab"
Me.ExHandUsedCheckETab.Size = New System.Drawing.Size(19, 19)
Me.ExHandUsedCheckETab.TabIndex = 79
Me.ExHandUsedCheckETab.TabStop = false
'
'NCardsDetailLabelETab
'
Me.NCardsDetailLabelETab.Location = New System.Drawing.Point(509, 23)
Me.NCardsDetailLabelETab.Name = "NCardsDetailLabelETab"
Me.NCardsDetailLabelETab.Size = New System.Drawing.Size(57, 19)
Me.NCardsDetailLabelETab.TabIndex = 75
Me.NCardsDetailLabelETab.Text = "N Cards"
'
'NCardsDetailsBoxETab
'
Me.NCardsDetailsBoxETab.Location = New System.Drawing.Point(576, 23)
Me.NCardsDetailsBoxETab.Name = "NCardsDetailsBoxETab"
Me.NCardsDetailsBoxETab.ReadOnly = true
Me.NCardsDetailsBoxETab.Size = New System.Drawing.Size(38, 22)
Me.NCardsDetailsBoxETab.TabIndex = 76
Me.NCardsDetailsBoxETab.TabStop = false
Me.NCardsDetailsBoxETab.Text = ""
Me.NCardsDetailsBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SoftDetailsCheckETab
'
Me.SoftDetailsCheckETab.Enabled = false
Me.SoftDetailsCheckETab.Location = New System.Drawing.Point(470, 23)
Me.SoftDetailsCheckETab.Name = "SoftDetailsCheckETab"
Me.SoftDetailsCheckETab.Size = New System.Drawing.Size(20, 19)
Me.SoftDetailsCheckETab.TabIndex = 74
Me.SoftDetailsCheckETab.TabStop = false
'
'SoftDetailsLabelETab
'
Me.SoftDetailsLabelETab.Location = New System.Drawing.Point(422, 23)
Me.SoftDetailsLabelETab.Name = "SoftDetailsLabelETab"
Me.SoftDetailsLabelETab.Size = New System.Drawing.Size(29, 19)
Me.SoftDetailsLabelETab.TabIndex = 73
Me.SoftDetailsLabelETab.Text = "Soft"
'
'UsedLabelETab
'
Me.UsedLabelETab.Location = New System.Drawing.Point(67, 272)
Me.UsedLabelETab.Name = "UsedLabelETab"
Me.UsedLabelETab.Size = New System.Drawing.Size(77, 19)
Me.UsedLabelETab.TabIndex = 44
Me.UsedLabelETab.Text = "Hand Used"
'
'StratNameDetailsLabelETab
'
Me.StratNameDetailsLabelETab.Location = New System.Drawing.Point(67, 78)
Me.StratNameDetailsLabelETab.Name = "StratNameDetailsLabelETab"
Me.StratNameDetailsLabelETab.Size = New System.Drawing.Size(96, 19)
Me.StratNameDetailsLabelETab.TabIndex = 43
Me.StratNameDetailsLabelETab.Text = "Strategy Name"
'
'StratLabelETab
'
Me.StratLabelETab.Location = New System.Drawing.Point(67, 106)
Me.StratLabelETab.Name = "StratLabelETab"
Me.StratLabelETab.Size = New System.Drawing.Size(58, 19)
Me.StratLabelETab.TabIndex = 35
Me.StratLabelETab.Text = "Strategy"
'
'ExStratETab
'
Me.ExStratETab.Location = New System.Drawing.Point(355, 106)
Me.ExStratETab.Name = "ExStratETab"
Me.ExStratETab.ReadOnly = true
Me.ExStratETab.Size = New System.Drawing.Size(39, 22)
Me.ExStratETab.TabIndex = 31
Me.ExStratETab.TabStop = false
Me.ExStratETab.Text = ""
Me.ExStratETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BaseStandBoxETab
'
Me.BaseStandBoxETab.Location = New System.Drawing.Point(182, 134)
Me.BaseStandBoxETab.Name = "BaseStandBoxETab"
Me.BaseStandBoxETab.ReadOnly = true
Me.BaseStandBoxETab.Size = New System.Drawing.Size(116, 22)
Me.BaseStandBoxETab.TabIndex = 28
Me.BaseStandBoxETab.TabStop = false
Me.BaseStandBoxETab.Text = ""
'
'BaseDoubleBoxETab
'
Me.BaseDoubleBoxETab.Location = New System.Drawing.Point(182, 162)
Me.BaseDoubleBoxETab.Name = "BaseDoubleBoxETab"
Me.BaseDoubleBoxETab.ReadOnly = true
Me.BaseDoubleBoxETab.Size = New System.Drawing.Size(116, 22)
Me.BaseDoubleBoxETab.TabIndex = 27
Me.BaseDoubleBoxETab.TabStop = false
Me.BaseDoubleBoxETab.Text = ""
'
'BaseSurrenderBoxETab
'
Me.BaseSurrenderBoxETab.Location = New System.Drawing.Point(182, 217)
Me.BaseSurrenderBoxETab.Name = "BaseSurrenderBoxETab"
Me.BaseSurrenderBoxETab.ReadOnly = true
Me.BaseSurrenderBoxETab.Size = New System.Drawing.Size(116, 22)
Me.BaseSurrenderBoxETab.TabIndex = 26
Me.BaseSurrenderBoxETab.TabStop = false
Me.BaseSurrenderBoxETab.Text = ""
'
'BaseSplitBoxETab
'
Me.BaseSplitBoxETab.Location = New System.Drawing.Point(182, 245)
Me.BaseSplitBoxETab.Name = "BaseSplitBoxETab"
Me.BaseSplitBoxETab.ReadOnly = true
Me.BaseSplitBoxETab.Size = New System.Drawing.Size(116, 22)
Me.BaseSplitBoxETab.TabIndex = 25
Me.BaseSplitBoxETab.TabStop = false
Me.BaseSplitBoxETab.Text = ""
'
'ExSplitBoxETab
'
Me.ExSplitBoxETab.Location = New System.Drawing.Point(317, 245)
Me.ExSplitBoxETab.Name = "ExSplitBoxETab"
Me.ExSplitBoxETab.ReadOnly = true
Me.ExSplitBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ExSplitBoxETab.TabIndex = 24
Me.ExSplitBoxETab.TabStop = false
Me.ExSplitBoxETab.Text = ""
'
'ExHitBoxETab
'
Me.ExHitBoxETab.Location = New System.Drawing.Point(317, 189)
Me.ExHitBoxETab.Name = "ExHitBoxETab"
Me.ExHitBoxETab.ReadOnly = true
Me.ExHitBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ExHitBoxETab.TabIndex = 19
Me.ExHitBoxETab.TabStop = false
Me.ExHitBoxETab.Text = ""
'
'BaseHitBoxETab
'
Me.BaseHitBoxETab.Location = New System.Drawing.Point(182, 189)
Me.BaseHitBoxETab.Name = "BaseHitBoxETab"
Me.BaseHitBoxETab.ReadOnly = true
Me.BaseHitBoxETab.Size = New System.Drawing.Size(116, 22)
Me.BaseHitBoxETab.TabIndex = 18
Me.BaseHitBoxETab.TabStop = false
Me.BaseHitBoxETab.Text = ""
'
'ProbBoxETab
'
Me.ProbBoxETab.Location = New System.Drawing.Point(605, 134)
Me.ProbBoxETab.Name = "ProbBoxETab"
Me.ProbBoxETab.ReadOnly = true
Me.ProbBoxETab.Size = New System.Drawing.Size(115, 22)
Me.ProbBoxETab.TabIndex = 17
Me.ProbBoxETab.TabStop = false
Me.ProbBoxETab.Text = ""
Me.ProbBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BaseStratETab
'
Me.BaseStratETab.Location = New System.Drawing.Point(221, 106)
Me.BaseStratETab.Name = "BaseStratETab"
Me.BaseStratETab.ReadOnly = true
Me.BaseStratETab.Size = New System.Drawing.Size(38, 22)
Me.BaseStratETab.TabIndex = 16
Me.BaseStratETab.TabStop = false
Me.BaseStratETab.Text = ""
Me.BaseStratETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SplitLabelETab
'
Me.SplitLabelETab.Location = New System.Drawing.Point(67, 245)
Me.SplitLabelETab.Name = "SplitLabelETab"
Me.SplitLabelETab.Size = New System.Drawing.Size(58, 18)
Me.SplitLabelETab.TabIndex = 14
Me.SplitLabelETab.Text = "Split EV"
'
'BaseStratDetailsLabelETab
'
Me.BaseStratDetailsLabelETab.Location = New System.Drawing.Point(192, 51)
Me.BaseStratDetailsLabelETab.Name = "BaseStratDetailsLabelETab"
Me.BaseStratDetailsLabelETab.Size = New System.Drawing.Size(96, 18)
Me.BaseStratDetailsLabelETab.TabIndex = 11
Me.BaseStratDetailsLabelETab.Text = "Base Strategy"
Me.BaseStratDetailsLabelETab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'HitLabelETab
'
Me.HitLabelETab.Location = New System.Drawing.Point(67, 189)
Me.HitLabelETab.Name = "HitLabelETab"
Me.HitLabelETab.Size = New System.Drawing.Size(58, 19)
Me.HitLabelETab.TabIndex = 9
Me.HitLabelETab.Text = "Hit EV"
'
'StateLabelETab
'
Me.StateLabelETab.Location = New System.Drawing.Point(480, 162)
Me.StateLabelETab.Name = "StateLabelETab"
Me.StateLabelETab.Size = New System.Drawing.Size(96, 18)
Me.StateLabelETab.TabIndex = 8
Me.StateLabelETab.Text = "Shoe State"
'
'SurrLabelETab
'
Me.SurrLabelETab.Location = New System.Drawing.Point(67, 217)
Me.SurrLabelETab.Name = "SurrLabelETab"
Me.SurrLabelETab.Size = New System.Drawing.Size(96, 18)
Me.SurrLabelETab.TabIndex = 7
Me.SurrLabelETab.Text = "Surrender EV"
'
'ExTypeLabelETab
'
Me.ExTypeLabelETab.Location = New System.Drawing.Point(480, 78)
Me.ExTypeLabelETab.Name = "ExTypeLabelETab"
Me.ExTypeLabelETab.Size = New System.Drawing.Size(106, 19)
Me.ExTypeLabelETab.TabIndex = 6
Me.ExTypeLabelETab.Text = "Exception Type"
'
'DoubleLabelETab
'
Me.DoubleLabelETab.Location = New System.Drawing.Point(67, 162)
Me.DoubleLabelETab.Name = "DoubleLabelETab"
Me.DoubleLabelETab.Size = New System.Drawing.Size(96, 18)
Me.DoubleLabelETab.TabIndex = 4
Me.DoubleLabelETab.Text = "Double EV"
'
'StandLabelETab
'
Me.StandLabelETab.Location = New System.Drawing.Point(67, 134)
Me.StandLabelETab.Name = "StandLabelETab"
Me.StandLabelETab.Size = New System.Drawing.Size(96, 18)
Me.StandLabelETab.TabIndex = 3
Me.StandLabelETab.Text = "Stand EV"
'
'ProbLabelETab
'
Me.ProbLabelETab.Location = New System.Drawing.Point(480, 134)
Me.ProbLabelETab.Name = "ProbLabelETab"
Me.ProbLabelETab.Size = New System.Drawing.Size(125, 18)
Me.ProbLabelETab.TabIndex = 2
Me.ProbLabelETab.Text = "Pre-Split Probability"
'
'HandLabelETab
'
Me.HandLabelETab.Location = New System.Drawing.Point(38, 23)
Me.HandLabelETab.Name = "HandLabelETab"
Me.HandLabelETab.Size = New System.Drawing.Size(39, 19)
Me.HandLabelETab.TabIndex = 1
Me.HandLabelETab.Text = "Hand"
'
'HandNameBoxETab
'
Me.HandNameBoxETab.Location = New System.Drawing.Point(86, 23)
Me.HandNameBoxETab.Name = "HandNameBoxETab"
Me.HandNameBoxETab.ReadOnly = true
Me.HandNameBoxETab.Size = New System.Drawing.Size(212, 22)
Me.HandNameBoxETab.TabIndex = 0
Me.HandNameBoxETab.TabStop = false
Me.HandNameBoxETab.Text = ""
Me.HandNameBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TotalDetailsLabelETab
'
Me.TotalDetailsLabelETab.Location = New System.Drawing.Point(317, 23)
Me.TotalDetailsLabelETab.Name = "TotalDetailsLabelETab"
Me.TotalDetailsLabelETab.Size = New System.Drawing.Size(38, 19)
Me.TotalDetailsLabelETab.TabIndex = 71
Me.TotalDetailsLabelETab.Text = "Total"
'
'TotalDetailsBoxETab
'
Me.TotalDetailsBoxETab.Location = New System.Drawing.Point(365, 23)
Me.TotalDetailsBoxETab.Name = "TotalDetailsBoxETab"
Me.TotalDetailsBoxETab.ReadOnly = true
Me.TotalDetailsBoxETab.Size = New System.Drawing.Size(38, 22)
Me.TotalDetailsBoxETab.TabIndex = 72
Me.TotalDetailsBoxETab.TabStop = false
Me.TotalDetailsBoxETab.Text = ""
Me.TotalDetailsBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UCDetailsBoxETab
'
Me.UCDetailsBoxETab.Location = New System.Drawing.Point(701, 23)
Me.UCDetailsBoxETab.Name = "UCDetailsBoxETab"
Me.UCDetailsBoxETab.ReadOnly = true
Me.UCDetailsBoxETab.Size = New System.Drawing.Size(38, 22)
Me.UCDetailsBoxETab.TabIndex = 95
Me.UCDetailsBoxETab.TabStop = false
Me.UCDetailsBoxETab.Text = ""
Me.UCDetailsBoxETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UCDetailsLabelETab
'
Me.UCDetailsLabelETab.Location = New System.Drawing.Point(634, 23)
Me.UCDetailsLabelETab.Name = "UCDetailsLabelETab"
Me.UCDetailsLabelETab.Size = New System.Drawing.Size(57, 19)
Me.UCDetailsLabelETab.TabIndex = 94
Me.UCDetailsLabelETab.Text = "Upcard"
'
'NCardExceptionsTab
'
Me.NCardExceptionsTab.Controls.Add(Me.NCardsComboBoxNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.TotalComboBoxNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.ExAllForcedButtonNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.NoteLabelNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.UCComboBoxNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.ETypeLabelNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.ExTypeComboBoxNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.ListSizeLabelNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.ListSizeBoxNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.HardOnlyCheckNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.SoftOnlyCheckNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.EitherCheckNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.NCardLabelNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.UCLabelNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.SoftLabelNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.TotalLabelNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.HandListBoxNCETab)
Me.NCardExceptionsTab.Controls.Add(Me.ExceptionDetailsGroupNCETab)
Me.NCardExceptionsTab.Location = New System.Drawing.Point(4, 25)
Me.NCardExceptionsTab.Name = "NCardExceptionsTab"
Me.NCardExceptionsTab.Size = New System.Drawing.Size(817, 580)
Me.NCardExceptionsTab.TabIndex = 1
Me.NCardExceptionsTab.Text = "N Card Hands"
'
'NCardsComboBoxNCETab
'
Me.NCardsComboBoxNCETab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.NCardsComboBoxNCETab.Items.AddRange(New Object() {"Any", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.NCardsComboBoxNCETab.Location = New System.Drawing.Point(384, 37)
Me.NCardsComboBoxNCETab.Name = "NCardsComboBoxNCETab"
Me.NCardsComboBoxNCETab.Size = New System.Drawing.Size(67, 24)
Me.NCardsComboBoxNCETab.TabIndex = 120
'
'TotalComboBoxNCETab
'
Me.TotalComboBoxNCETab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.TotalComboBoxNCETab.Items.AddRange(New Object() {"Any", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.TotalComboBoxNCETab.Location = New System.Drawing.Point(182, 37)
Me.TotalComboBoxNCETab.Name = "TotalComboBoxNCETab"
Me.TotalComboBoxNCETab.Size = New System.Drawing.Size(68, 24)
Me.TotalComboBoxNCETab.TabIndex = 116
'
'ExAllForcedButtonNCETab
'
Me.ExAllForcedButtonNCETab.Location = New System.Drawing.Point(614, 111)
Me.ExAllForcedButtonNCETab.Name = "ExAllForcedButtonNCETab"
Me.ExAllForcedButtonNCETab.Size = New System.Drawing.Size(154, 37)
Me.ExAllForcedButtonNCETab.TabIndex = 133
Me.ExAllForcedButtonNCETab.Text = "Send all to Forced Rules"
'
'NoteLabelNCETab
'
Me.NoteLabelNCETab.Location = New System.Drawing.Point(269, 286)
Me.NoteLabelNCETab.Name = "NoteLabelNCETab"
Me.NoteLabelNCETab.Size = New System.Drawing.Size(297, 19)
Me.NoteLabelNCETab.TabIndex = 132
Me.NoteLabelNCETab.Text = "*Note: Only hands that are used are included."
'
'UCComboBoxNCETab
'
Me.UCComboBoxNCETab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UCComboBoxNCETab.Items.AddRange(New Object() {"All", "A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UCComboBoxNCETab.Location = New System.Drawing.Point(480, 37)
Me.UCComboBoxNCETab.Name = "UCComboBoxNCETab"
Me.UCComboBoxNCETab.Size = New System.Drawing.Size(58, 24)
Me.UCComboBoxNCETab.TabIndex = 121
'
'ETypeLabelNCETab
'
Me.ETypeLabelNCETab.Location = New System.Drawing.Point(566, 9)
Me.ETypeLabelNCETab.Name = "ETypeLabelNCETab"
Me.ETypeLabelNCETab.Size = New System.Drawing.Size(106, 19)
Me.ETypeLabelNCETab.TabIndex = 131
Me.ETypeLabelNCETab.Text = "Exception Type"
'
'ExTypeComboBoxNCETab
'
Me.ExTypeComboBoxNCETab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.ExTypeComboBoxNCETab.Location = New System.Drawing.Point(566, 37)
Me.ExTypeComboBoxNCETab.Name = "ExTypeComboBoxNCETab"
Me.ExTypeComboBoxNCETab.Size = New System.Drawing.Size(96, 24)
Me.ExTypeComboBoxNCETab.TabIndex = 122
'
'ListSizeLabelNCETab
'
Me.ListSizeLabelNCETab.Location = New System.Drawing.Point(259, 258)
Me.ListSizeLabelNCETab.Name = "ListSizeLabelNCETab"
Me.ListSizeLabelNCETab.Size = New System.Drawing.Size(221, 28)
Me.ListSizeLabelNCETab.TabIndex = 130
Me.ListSizeLabelNCETab.Text = "Exceptions meeting above criteria"
Me.ListSizeLabelNCETab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ListSizeBoxNCETab
'
Me.ListSizeBoxNCETab.Location = New System.Drawing.Point(480, 258)
Me.ListSizeBoxNCETab.Name = "ListSizeBoxNCETab"
Me.ListSizeBoxNCETab.ReadOnly = true
Me.ListSizeBoxNCETab.Size = New System.Drawing.Size(48, 22)
Me.ListSizeBoxNCETab.TabIndex = 129
Me.ListSizeBoxNCETab.TabStop = false
Me.ListSizeBoxNCETab.Text = ""
Me.ListSizeBoxNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'HardOnlyCheckNCETab
'
Me.HardOnlyCheckNCETab.Location = New System.Drawing.Point(288, 65)
Me.HardOnlyCheckNCETab.Name = "HardOnlyCheckNCETab"
Me.HardOnlyCheckNCETab.Size = New System.Drawing.Size(96, 18)
Me.HardOnlyCheckNCETab.TabIndex = 118
Me.HardOnlyCheckNCETab.Text = "Hard Only"
'
'SoftOnlyCheckNCETab
'
Me.SoftOnlyCheckNCETab.Location = New System.Drawing.Point(288, 83)
Me.SoftOnlyCheckNCETab.Name = "SoftOnlyCheckNCETab"
Me.SoftOnlyCheckNCETab.Size = New System.Drawing.Size(86, 19)
Me.SoftOnlyCheckNCETab.TabIndex = 119
Me.SoftOnlyCheckNCETab.Text = "Soft Only"
'
'EitherCheckNCETab
'
Me.EitherCheckNCETab.Location = New System.Drawing.Point(288, 37)
Me.EitherCheckNCETab.Name = "EitherCheckNCETab"
Me.EitherCheckNCETab.Size = New System.Drawing.Size(67, 18)
Me.EitherCheckNCETab.TabIndex = 117
Me.EitherCheckNCETab.Text = "Either"
'
'NCardLabelNCETab
'
Me.NCardLabelNCETab.Location = New System.Drawing.Point(384, 9)
Me.NCardLabelNCETab.Name = "NCardLabelNCETab"
Me.NCardLabelNCETab.Size = New System.Drawing.Size(58, 19)
Me.NCardLabelNCETab.TabIndex = 128
Me.NCardLabelNCETab.Text = "N Cards"
'
'UCLabelNCETab
'
Me.UCLabelNCETab.Location = New System.Drawing.Point(480, 9)
Me.UCLabelNCETab.Name = "UCLabelNCETab"
Me.UCLabelNCETab.Size = New System.Drawing.Size(58, 19)
Me.UCLabelNCETab.TabIndex = 127
Me.UCLabelNCETab.Text = "Upcard"
'
'SoftLabelNCETab
'
Me.SoftLabelNCETab.Location = New System.Drawing.Point(288, 9)
Me.SoftLabelNCETab.Name = "SoftLabelNCETab"
Me.SoftLabelNCETab.Size = New System.Drawing.Size(67, 19)
Me.SoftLabelNCETab.TabIndex = 126
Me.SoftLabelNCETab.Text = "Hand Soft"
'
'TotalLabelNCETab
'
Me.TotalLabelNCETab.Location = New System.Drawing.Point(182, 9)
Me.TotalLabelNCETab.Name = "TotalLabelNCETab"
Me.TotalLabelNCETab.Size = New System.Drawing.Size(77, 19)
Me.TotalLabelNCETab.TabIndex = 125
Me.TotalLabelNCETab.Text = "Hand Total"
'
'HandListBoxNCETab
'
Me.HandListBoxNCETab.ItemHeight = 16
Me.HandListBoxNCETab.Location = New System.Drawing.Point(221, 111)
Me.HandListBoxNCETab.Name = "HandListBoxNCETab"
Me.HandListBoxNCETab.Size = New System.Drawing.Size(384, 132)
Me.HandListBoxNCETab.TabIndex = 123
'
'ExceptionDetailsGroupNCETab
'
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExRuleNameBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExRuleNameLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExForcedButtonNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExSurrenderBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExStandBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExDoubleBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExTypeBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExNameBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.BaseNameBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExStratDetailsLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.NCardsDetailLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.NCardsDetailsBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.SoftDetailsCheckNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.SoftDetailsLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.StratNameDetailsLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.StratLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExStratNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.BaseStandBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.BaseDoubleBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.BaseSurrenderBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExHitBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.BaseHitBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.BaseStratNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.BaseStratDetailsLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.HitLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.SurrLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.ExTypeLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.DoubleLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.StandLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.TotalDetailsLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.TotalDetailsBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.UCDetailsBoxNCETab)
Me.ExceptionDetailsGroupNCETab.Controls.Add(Me.UCDetailsLabelNCETab)
Me.ExceptionDetailsGroupNCETab.Location = New System.Drawing.Point(10, 305)
Me.ExceptionDetailsGroupNCETab.Name = "ExceptionDetailsGroupNCETab"
Me.ExceptionDetailsGroupNCETab.Size = New System.Drawing.Size(796, 267)
Me.ExceptionDetailsGroupNCETab.TabIndex = 124
Me.ExceptionDetailsGroupNCETab.TabStop = false
Me.ExceptionDetailsGroupNCETab.Text = "Exception Details"
'
'ExRuleNameBoxNCETab
'
Me.ExRuleNameBoxNCETab.Location = New System.Drawing.Point(605, 122)
Me.ExRuleNameBoxNCETab.Name = "ExRuleNameBoxNCETab"
Me.ExRuleNameBoxNCETab.ReadOnly = true
Me.ExRuleNameBoxNCETab.Size = New System.Drawing.Size(115, 22)
Me.ExRuleNameBoxNCETab.TabIndex = 106
Me.ExRuleNameBoxNCETab.TabStop = false
Me.ExRuleNameBoxNCETab.Text = ""
Me.ExRuleNameBoxNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ExRuleNameLabelNCETab
'
Me.ExRuleNameLabelNCETab.Location = New System.Drawing.Point(480, 122)
Me.ExRuleNameLabelNCETab.Name = "ExRuleNameLabelNCETab"
Me.ExRuleNameLabelNCETab.Size = New System.Drawing.Size(106, 19)
Me.ExRuleNameLabelNCETab.TabIndex = 105
Me.ExRuleNameLabelNCETab.Text = "Exception Name"
'
'ExForcedButtonNCETab
'
Me.ExForcedButtonNCETab.Location = New System.Drawing.Point(538, 166)
Me.ExForcedButtonNCETab.Name = "ExForcedButtonNCETab"
Me.ExForcedButtonNCETab.Size = New System.Drawing.Size(153, 37)
Me.ExForcedButtonNCETab.TabIndex = 104
Me.ExForcedButtonNCETab.Text = "Send to Forced Rules"
'
'ExSurrenderBoxNCETab
'
Me.ExSurrenderBoxNCETab.Location = New System.Drawing.Point(317, 231)
Me.ExSurrenderBoxNCETab.Name = "ExSurrenderBoxNCETab"
Me.ExSurrenderBoxNCETab.ReadOnly = true
Me.ExSurrenderBoxNCETab.Size = New System.Drawing.Size(115, 22)
Me.ExSurrenderBoxNCETab.TabIndex = 103
Me.ExSurrenderBoxNCETab.TabStop = false
Me.ExSurrenderBoxNCETab.Text = ""
'
'ExStandBoxNCETab
'
Me.ExStandBoxNCETab.Location = New System.Drawing.Point(317, 148)
Me.ExStandBoxNCETab.Name = "ExStandBoxNCETab"
Me.ExStandBoxNCETab.ReadOnly = true
Me.ExStandBoxNCETab.Size = New System.Drawing.Size(115, 22)
Me.ExStandBoxNCETab.TabIndex = 102
Me.ExStandBoxNCETab.TabStop = false
Me.ExStandBoxNCETab.Text = ""
'
'ExDoubleBoxNCETab
'
Me.ExDoubleBoxNCETab.Location = New System.Drawing.Point(317, 175)
Me.ExDoubleBoxNCETab.Name = "ExDoubleBoxNCETab"
Me.ExDoubleBoxNCETab.ReadOnly = true
Me.ExDoubleBoxNCETab.Size = New System.Drawing.Size(115, 22)
Me.ExDoubleBoxNCETab.TabIndex = 101
Me.ExDoubleBoxNCETab.TabStop = false
Me.ExDoubleBoxNCETab.Text = ""
'
'ExTypeBoxNCETab
'
Me.ExTypeBoxNCETab.Location = New System.Drawing.Point(605, 92)
Me.ExTypeBoxNCETab.Name = "ExTypeBoxNCETab"
Me.ExTypeBoxNCETab.ReadOnly = true
Me.ExTypeBoxNCETab.Size = New System.Drawing.Size(115, 22)
Me.ExTypeBoxNCETab.TabIndex = 99
Me.ExTypeBoxNCETab.TabStop = false
Me.ExTypeBoxNCETab.Text = ""
Me.ExTypeBoxNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ExNameBoxNCETab
'
Me.ExNameBoxNCETab.Location = New System.Drawing.Point(317, 92)
Me.ExNameBoxNCETab.Name = "ExNameBoxNCETab"
Me.ExNameBoxNCETab.ReadOnly = true
Me.ExNameBoxNCETab.Size = New System.Drawing.Size(115, 22)
Me.ExNameBoxNCETab.TabIndex = 98
Me.ExNameBoxNCETab.TabStop = false
Me.ExNameBoxNCETab.Text = ""
Me.ExNameBoxNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BaseNameBoxNCETab
'
Me.BaseNameBoxNCETab.Location = New System.Drawing.Point(182, 92)
Me.BaseNameBoxNCETab.Name = "BaseNameBoxNCETab"
Me.BaseNameBoxNCETab.ReadOnly = true
Me.BaseNameBoxNCETab.Size = New System.Drawing.Size(116, 22)
Me.BaseNameBoxNCETab.TabIndex = 97
Me.BaseNameBoxNCETab.TabStop = false
Me.BaseNameBoxNCETab.Text = ""
Me.BaseNameBoxNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ExStratDetailsLabelNCETab
'
Me.ExStratDetailsLabelNCETab.Location = New System.Drawing.Point(307, 65)
Me.ExStratDetailsLabelNCETab.Name = "ExStratDetailsLabelNCETab"
Me.ExStratDetailsLabelNCETab.Size = New System.Drawing.Size(135, 18)
Me.ExStratDetailsLabelNCETab.TabIndex = 96
Me.ExStratDetailsLabelNCETab.Text = "Exception Strategy"
Me.ExStratDetailsLabelNCETab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'NCardsDetailLabelNCETab
'
Me.NCardsDetailLabelNCETab.Location = New System.Drawing.Point(403, 28)
Me.NCardsDetailLabelNCETab.Name = "NCardsDetailLabelNCETab"
Me.NCardsDetailLabelNCETab.Size = New System.Drawing.Size(58, 18)
Me.NCardsDetailLabelNCETab.TabIndex = 75
Me.NCardsDetailLabelNCETab.Text = "N Cards"
'
'NCardsDetailsBoxNCETab
'
Me.NCardsDetailsBoxNCETab.Location = New System.Drawing.Point(470, 28)
Me.NCardsDetailsBoxNCETab.Name = "NCardsDetailsBoxNCETab"
Me.NCardsDetailsBoxNCETab.ReadOnly = true
Me.NCardsDetailsBoxNCETab.Size = New System.Drawing.Size(39, 22)
Me.NCardsDetailsBoxNCETab.TabIndex = 76
Me.NCardsDetailsBoxNCETab.TabStop = false
Me.NCardsDetailsBoxNCETab.Text = ""
Me.NCardsDetailsBoxNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SoftDetailsCheckNCETab
'
Me.SoftDetailsCheckNCETab.Enabled = false
Me.SoftDetailsCheckNCETab.Location = New System.Drawing.Point(365, 28)
Me.SoftDetailsCheckNCETab.Name = "SoftDetailsCheckNCETab"
Me.SoftDetailsCheckNCETab.Size = New System.Drawing.Size(19, 18)
Me.SoftDetailsCheckNCETab.TabIndex = 74
Me.SoftDetailsCheckNCETab.TabStop = false
'
'SoftDetailsLabelNCETab
'
Me.SoftDetailsLabelNCETab.Location = New System.Drawing.Point(317, 28)
Me.SoftDetailsLabelNCETab.Name = "SoftDetailsLabelNCETab"
Me.SoftDetailsLabelNCETab.Size = New System.Drawing.Size(29, 18)
Me.SoftDetailsLabelNCETab.TabIndex = 73
Me.SoftDetailsLabelNCETab.Text = "Soft"
'
'StratNameDetailsLabelNCETab
'
Me.StratNameDetailsLabelNCETab.Location = New System.Drawing.Point(67, 92)
Me.StratNameDetailsLabelNCETab.Name = "StratNameDetailsLabelNCETab"
Me.StratNameDetailsLabelNCETab.Size = New System.Drawing.Size(96, 19)
Me.StratNameDetailsLabelNCETab.TabIndex = 43
Me.StratNameDetailsLabelNCETab.Text = "Strategy Name"
'
'StratLabelNCETab
'
Me.StratLabelNCETab.Location = New System.Drawing.Point(67, 120)
Me.StratLabelNCETab.Name = "StratLabelNCETab"
Me.StratLabelNCETab.Size = New System.Drawing.Size(58, 18)
Me.StratLabelNCETab.TabIndex = 35
Me.StratLabelNCETab.Text = "Strategy"
'
'ExStratNCETab
'
Me.ExStratNCETab.Location = New System.Drawing.Point(355, 120)
Me.ExStratNCETab.Name = "ExStratNCETab"
Me.ExStratNCETab.ReadOnly = true
Me.ExStratNCETab.Size = New System.Drawing.Size(39, 22)
Me.ExStratNCETab.TabIndex = 31
Me.ExStratNCETab.TabStop = false
Me.ExStratNCETab.Text = ""
Me.ExStratNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BaseStandBoxNCETab
'
Me.BaseStandBoxNCETab.Location = New System.Drawing.Point(182, 148)
Me.BaseStandBoxNCETab.Name = "BaseStandBoxNCETab"
Me.BaseStandBoxNCETab.ReadOnly = true
Me.BaseStandBoxNCETab.Size = New System.Drawing.Size(116, 22)
Me.BaseStandBoxNCETab.TabIndex = 28
Me.BaseStandBoxNCETab.TabStop = false
Me.BaseStandBoxNCETab.Text = ""
'
'BaseDoubleBoxNCETab
'
Me.BaseDoubleBoxNCETab.Location = New System.Drawing.Point(182, 175)
Me.BaseDoubleBoxNCETab.Name = "BaseDoubleBoxNCETab"
Me.BaseDoubleBoxNCETab.ReadOnly = true
Me.BaseDoubleBoxNCETab.Size = New System.Drawing.Size(116, 22)
Me.BaseDoubleBoxNCETab.TabIndex = 27
Me.BaseDoubleBoxNCETab.TabStop = false
Me.BaseDoubleBoxNCETab.Text = ""
'
'BaseSurrenderBoxNCETab
'
Me.BaseSurrenderBoxNCETab.Location = New System.Drawing.Point(182, 231)
Me.BaseSurrenderBoxNCETab.Name = "BaseSurrenderBoxNCETab"
Me.BaseSurrenderBoxNCETab.ReadOnly = true
Me.BaseSurrenderBoxNCETab.Size = New System.Drawing.Size(116, 22)
Me.BaseSurrenderBoxNCETab.TabIndex = 26
Me.BaseSurrenderBoxNCETab.TabStop = false
Me.BaseSurrenderBoxNCETab.Text = ""
'
'ExHitBoxNCETab
'
Me.ExHitBoxNCETab.Location = New System.Drawing.Point(317, 203)
Me.ExHitBoxNCETab.Name = "ExHitBoxNCETab"
Me.ExHitBoxNCETab.ReadOnly = true
Me.ExHitBoxNCETab.Size = New System.Drawing.Size(115, 22)
Me.ExHitBoxNCETab.TabIndex = 19
Me.ExHitBoxNCETab.TabStop = false
Me.ExHitBoxNCETab.Text = ""
'
'BaseHitBoxNCETab
'
Me.BaseHitBoxNCETab.Location = New System.Drawing.Point(182, 203)
Me.BaseHitBoxNCETab.Name = "BaseHitBoxNCETab"
Me.BaseHitBoxNCETab.ReadOnly = true
Me.BaseHitBoxNCETab.Size = New System.Drawing.Size(116, 22)
Me.BaseHitBoxNCETab.TabIndex = 18
Me.BaseHitBoxNCETab.TabStop = false
Me.BaseHitBoxNCETab.Text = ""
'
'BaseStratNCETab
'
Me.BaseStratNCETab.Location = New System.Drawing.Point(221, 120)
Me.BaseStratNCETab.Name = "BaseStratNCETab"
Me.BaseStratNCETab.ReadOnly = true
Me.BaseStratNCETab.Size = New System.Drawing.Size(38, 22)
Me.BaseStratNCETab.TabIndex = 16
Me.BaseStratNCETab.TabStop = false
Me.BaseStratNCETab.Text = ""
Me.BaseStratNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'BaseStratDetailsLabelNCETab
'
Me.BaseStratDetailsLabelNCETab.Location = New System.Drawing.Point(192, 65)
Me.BaseStratDetailsLabelNCETab.Name = "BaseStratDetailsLabelNCETab"
Me.BaseStratDetailsLabelNCETab.Size = New System.Drawing.Size(96, 18)
Me.BaseStratDetailsLabelNCETab.TabIndex = 11
Me.BaseStratDetailsLabelNCETab.Text = "Base Strategy"
Me.BaseStratDetailsLabelNCETab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'HitLabelNCETab
'
Me.HitLabelNCETab.Location = New System.Drawing.Point(67, 203)
Me.HitLabelNCETab.Name = "HitLabelNCETab"
Me.HitLabelNCETab.Size = New System.Drawing.Size(58, 19)
Me.HitLabelNCETab.TabIndex = 9
Me.HitLabelNCETab.Text = "Hit EV"
'
'SurrLabelNCETab
'
Me.SurrLabelNCETab.Location = New System.Drawing.Point(67, 231)
Me.SurrLabelNCETab.Name = "SurrLabelNCETab"
Me.SurrLabelNCETab.Size = New System.Drawing.Size(96, 18)
Me.SurrLabelNCETab.TabIndex = 7
Me.SurrLabelNCETab.Text = "Surrender EV"
'
'ExTypeLabelNCETab
'
Me.ExTypeLabelNCETab.Location = New System.Drawing.Point(480, 92)
Me.ExTypeLabelNCETab.Name = "ExTypeLabelNCETab"
Me.ExTypeLabelNCETab.Size = New System.Drawing.Size(106, 19)
Me.ExTypeLabelNCETab.TabIndex = 6
Me.ExTypeLabelNCETab.Text = "Exception Type"
'
'DoubleLabelNCETab
'
Me.DoubleLabelNCETab.Location = New System.Drawing.Point(67, 175)
Me.DoubleLabelNCETab.Name = "DoubleLabelNCETab"
Me.DoubleLabelNCETab.Size = New System.Drawing.Size(96, 19)
Me.DoubleLabelNCETab.TabIndex = 4
Me.DoubleLabelNCETab.Text = "Double EV"
'
'StandLabelNCETab
'
Me.StandLabelNCETab.Location = New System.Drawing.Point(67, 148)
Me.StandLabelNCETab.Name = "StandLabelNCETab"
Me.StandLabelNCETab.Size = New System.Drawing.Size(96, 18)
Me.StandLabelNCETab.TabIndex = 3
Me.StandLabelNCETab.Text = "Stand EV"
'
'TotalDetailsLabelNCETab
'
Me.TotalDetailsLabelNCETab.Location = New System.Drawing.Point(211, 28)
Me.TotalDetailsLabelNCETab.Name = "TotalDetailsLabelNCETab"
Me.TotalDetailsLabelNCETab.Size = New System.Drawing.Size(39, 18)
Me.TotalDetailsLabelNCETab.TabIndex = 71
Me.TotalDetailsLabelNCETab.Text = "Total"
'
'TotalDetailsBoxNCETab
'
Me.TotalDetailsBoxNCETab.Location = New System.Drawing.Point(259, 28)
Me.TotalDetailsBoxNCETab.Name = "TotalDetailsBoxNCETab"
Me.TotalDetailsBoxNCETab.ReadOnly = true
Me.TotalDetailsBoxNCETab.Size = New System.Drawing.Size(39, 22)
Me.TotalDetailsBoxNCETab.TabIndex = 72
Me.TotalDetailsBoxNCETab.TabStop = false
Me.TotalDetailsBoxNCETab.Text = ""
Me.TotalDetailsBoxNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UCDetailsBoxNCETab
'
Me.UCDetailsBoxNCETab.Location = New System.Drawing.Point(595, 28)
Me.UCDetailsBoxNCETab.Name = "UCDetailsBoxNCETab"
Me.UCDetailsBoxNCETab.ReadOnly = true
Me.UCDetailsBoxNCETab.Size = New System.Drawing.Size(39, 22)
Me.UCDetailsBoxNCETab.TabIndex = 95
Me.UCDetailsBoxNCETab.TabStop = false
Me.UCDetailsBoxNCETab.Text = ""
Me.UCDetailsBoxNCETab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UCDetailsLabelNCETab
'
Me.UCDetailsLabelNCETab.Location = New System.Drawing.Point(528, 28)
Me.UCDetailsLabelNCETab.Name = "UCDetailsLabelNCETab"
Me.UCDetailsLabelNCETab.Size = New System.Drawing.Size(58, 18)
Me.UCDetailsLabelNCETab.TabIndex = 94
Me.UCDetailsLabelNCETab.Text = "Upcard"
'
'ForcedTab
'
Me.ForcedTab.Controls.Add(Me.CopyPairsButtonFSTab)
Me.ForcedTab.Controls.Add(Me.CopyTDButtonFSTab)
Me.ForcedTab.Controls.Add(Me.ForcedStratTabControlFSTab)
Me.ForcedTab.Controls.Add(Me.LoadForcedTablesButtonFSTab)
Me.ForcedTab.Controls.Add(Me.SaveForcedTablesButtonFSTab)
Me.ForcedTab.Controls.Add(Me.ClearForcedTablesButtonFSTab)
Me.ForcedTab.Controls.Add(Me.CopyCDButtonFSTab)
Me.ForcedTab.Controls.Add(Me.RecalcForcedStratButtonFSTab)
Me.ForcedTab.Location = New System.Drawing.Point(4, 25)
Me.ForcedTab.Name = "ForcedTab"
Me.ForcedTab.Size = New System.Drawing.Size(846, 626)
Me.ForcedTab.TabIndex = 8
Me.ForcedTab.Text = "Forced Strategy"
'
'CopyPairsButtonFSTab
'
Me.CopyPairsButtonFSTab.Location = New System.Drawing.Point(211, 582)
Me.CopyPairsButtonFSTab.Name = "CopyPairsButtonFSTab"
Me.CopyPairsButtonFSTab.Size = New System.Drawing.Size(87, 27)
Me.CopyPairsButtonFSTab.TabIndex = 902
Me.CopyPairsButtonFSTab.Text = "Copy Pairs"
'
'CopyTDButtonFSTab
'
Me.CopyTDButtonFSTab.Location = New System.Drawing.Point(19, 582)
Me.CopyTDButtonFSTab.Name = "CopyTDButtonFSTab"
Me.CopyTDButtonFSTab.Size = New System.Drawing.Size(87, 27)
Me.CopyTDButtonFSTab.TabIndex = 900
Me.CopyTDButtonFSTab.Text = "Copy TD"
'
'ForcedStratTabControlFSTab
'
Me.ForcedStratTabControlFSTab.Controls.Add(Me.OptionsTabFSTab)
Me.ForcedStratTabControlFSTab.Controls.Add(Me.HardSoftTDTabFSTab)
Me.ForcedStratTabControlFSTab.Controls.Add(Me.SoftPairsCDTabFSTab)
Me.ForcedStratTabControlFSTab.Controls.Add(Me.HardCDTabFSTab)
Me.ForcedStratTabControlFSTab.Controls.Add(Me.OtherTabFSTab)
Me.ForcedStratTabControlFSTab.Location = New System.Drawing.Point(10, 9)
Me.ForcedStratTabControlFSTab.Name = "ForcedStratTabControlFSTab"
Me.ForcedStratTabControlFSTab.SelectedIndex = 0
Me.ForcedStratTabControlFSTab.Size = New System.Drawing.Size(825, 554)
Me.ForcedStratTabControlFSTab.TabIndex = 903
'
'OptionsTabFSTab
'
Me.OptionsTabFSTab.Controls.Add(Me.CalcNCardStratButtonFSTab)
Me.OptionsTabFSTab.Controls.Add(Me.PairsRuleApplyLabelFSTab)
Me.OptionsTabFSTab.Controls.Add(Me.ForcedTablePostCheckFSTab)
Me.OptionsTabFSTab.Controls.Add(Me.ForcedTablePreCheckFSTab)
Me.OptionsTabFSTab.Controls.Add(Me.ForcednCDLabelFSTab)
Me.OptionsTabFSTab.Controls.Add(Me.ForcednCDBoxFSTab)
Me.OptionsTabFSTab.Controls.Add(Me.ForcedWarningLabelFSTab)
Me.OptionsTabFSTab.Location = New System.Drawing.Point(4, 25)
Me.OptionsTabFSTab.Name = "OptionsTabFSTab"
Me.OptionsTabFSTab.Size = New System.Drawing.Size(817, 525)
Me.OptionsTabFSTab.TabIndex = 3
Me.OptionsTabFSTab.Text = "Forced Strategy Options"
'
'CalcNCardStratButtonFSTab
'
Me.CalcNCardStratButtonFSTab.Location = New System.Drawing.Point(24, 144)
Me.CalcNCardStratButtonFSTab.Name = "CalcNCardStratButtonFSTab"
Me.CalcNCardStratButtonFSTab.Size = New System.Drawing.Size(168, 40)
Me.CalcNCardStratButtonFSTab.TabIndex = 8
Me.CalcNCardStratButtonFSTab.Text = "Calculate NCard Strategy"
'
'PairsRuleApplyLabelFSTab
'
Me.PairsRuleApplyLabelFSTab.Location = New System.Drawing.Point(250, 392)
Me.PairsRuleApplyLabelFSTab.Name = "PairsRuleApplyLabelFSTab"
Me.PairsRuleApplyLabelFSTab.Size = New System.Drawing.Size(307, 64)
Me.PairsRuleApplyLabelFSTab.TabIndex = 7
Me.PairsRuleApplyLabelFSTab.Text = "*Note: Table Total based rules will not apply to pairs and only exact match ""Othe"& _ 
"r"" rules will.  AA/22 pairs will assume to have a secondary Hit strategy if assi"& _ 
"gned to P."
Me.PairsRuleApplyLabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ForcedTablePostCheckFSTab
'
Me.ForcedTablePostCheckFSTab.Location = New System.Drawing.Point(24, 83)
Me.ForcedTablePostCheckFSTab.Name = "ForcedTablePostCheckFSTab"
Me.ForcedTablePostCheckFSTab.Size = New System.Drawing.Size(221, 28)
Me.ForcedTablePostCheckFSTab.TabIndex = 2
Me.ForcedTablePostCheckFSTab.Text = "Apply Forced Tables Post-Split"
'
'ForcedTablePreCheckFSTab
'
Me.ForcedTablePreCheckFSTab.Location = New System.Drawing.Point(24, 65)
Me.ForcedTablePreCheckFSTab.Name = "ForcedTablePreCheckFSTab"
Me.ForcedTablePreCheckFSTab.Size = New System.Drawing.Size(211, 18)
Me.ForcedTablePreCheckFSTab.TabIndex = 1
Me.ForcedTablePreCheckFSTab.Text = "Apply Forced Tables Pre-Split"
'
'ForcednCDLabelFSTab
'
Me.ForcednCDLabelFSTab.Location = New System.Drawing.Point(24, 32)
Me.ForcednCDLabelFSTab.Name = "ForcednCDLabelFSTab"
Me.ForcednCDLabelFSTab.Size = New System.Drawing.Size(192, 16)
Me.ForcednCDLabelFSTab.TabIndex = 6
Me.ForcednCDLabelFSTab.Text = "Forced Strategy is n-Card CD"
'
'ForcednCDBoxFSTab
'
Me.ForcednCDBoxFSTab.Location = New System.Drawing.Point(221, 28)
Me.ForcednCDBoxFSTab.Name = "ForcednCDBoxFSTab"
Me.ForcednCDBoxFSTab.Size = New System.Drawing.Size(38, 22)
Me.ForcednCDBoxFSTab.TabIndex = 0
Me.ForcednCDBoxFSTab.Text = ""
'
'ForcedWarningLabelFSTab
'
Me.ForcedWarningLabelFSTab.Location = New System.Drawing.Point(250, 471)
Me.ForcedWarningLabelFSTab.Name = "ForcedWarningLabelFSTab"
Me.ForcedWarningLabelFSTab.Size = New System.Drawing.Size(307, 33)
Me.ForcedWarningLabelFSTab.TabIndex = 4
Me.ForcedWarningLabelFSTab.Text = "*Note: Forced strategies not compatible with game rules will be ignored"
Me.ForcedWarningLabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'HardSoftTDTabFSTab
'
Me.HardSoftTDTabFSTab.Controls.Add(Me.RowClickLabelFSTab)
Me.HardSoftTDTabFSTab.Controls.Add(Me.PairCDLabelComboboxFSTab)
Me.HardSoftTDTabFSTab.Controls.Add(Me.SoftCDLabelComboboxFSTab)
Me.HardSoftTDTabFSTab.Controls.Add(Me.HardCDLabelComboboxFSTab)
Me.HardSoftTDTabFSTab.Controls.Add(Me.SoftTDLabelComboboxFSTab)
Me.HardSoftTDTabFSTab.Controls.Add(Me.HardTDLabelComboboxFSTab)
Me.HardSoftTDTabFSTab.Controls.Add(Me.SoftTDGroupFSTab)
Me.HardSoftTDTabFSTab.Controls.Add(Me.ForcedTableComboboxFSTab)
Me.HardSoftTDTabFSTab.Controls.Add(Me.HardTDGroupFSTab)
Me.HardSoftTDTabFSTab.Location = New System.Drawing.Point(4, 25)
Me.HardSoftTDTabFSTab.Name = "HardSoftTDTabFSTab"
Me.HardSoftTDTabFSTab.Size = New System.Drawing.Size(817, 525)
Me.HardSoftTDTabFSTab.TabIndex = 5
Me.HardSoftTDTabFSTab.Text = "TD Hard/Soft"
Me.HardSoftTDTabFSTab.Visible = false
'
'RowClickLabelFSTab
'
Me.RowClickLabelFSTab.Location = New System.Drawing.Point(432, 304)
Me.RowClickLabelFSTab.Name = "RowClickLabelFSTab"
Me.RowClickLabelFSTab.Size = New System.Drawing.Size(355, 18)
Me.RowClickLabelFSTab.TabIndex = 230
Me.RowClickLabelFSTab.Text = "*Note: Click on a row heading to change the entire row."
'
'PairCDLabelComboboxFSTab
'
Me.PairCDLabelComboboxFSTab.Location = New System.Drawing.Point(720, 471)
Me.PairCDLabelComboboxFSTab.Name = "PairCDLabelComboboxFSTab"
Me.PairCDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
Me.PairCDLabelComboboxFSTab.TabIndex = 229
Me.PairCDLabelComboboxFSTab.TabStop = false
Me.PairCDLabelComboboxFSTab.Visible = false
'
'SoftCDLabelComboboxFSTab
'
Me.SoftCDLabelComboboxFSTab.Location = New System.Drawing.Point(624, 471)
Me.SoftCDLabelComboboxFSTab.Name = "SoftCDLabelComboboxFSTab"
Me.SoftCDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
Me.SoftCDLabelComboboxFSTab.TabIndex = 228
Me.SoftCDLabelComboboxFSTab.TabStop = false
Me.SoftCDLabelComboboxFSTab.Visible = false
'
'HardCDLabelComboboxFSTab
'
Me.HardCDLabelComboboxFSTab.Location = New System.Drawing.Point(624, 443)
Me.HardCDLabelComboboxFSTab.Name = "HardCDLabelComboboxFSTab"
Me.HardCDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
Me.HardCDLabelComboboxFSTab.TabIndex = 227
Me.HardCDLabelComboboxFSTab.TabStop = false
Me.HardCDLabelComboboxFSTab.Visible = false
'
'SoftTDLabelComboboxFSTab
'
Me.SoftTDLabelComboboxFSTab.Location = New System.Drawing.Point(720, 415)
Me.SoftTDLabelComboboxFSTab.Name = "SoftTDLabelComboboxFSTab"
Me.SoftTDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
Me.SoftTDLabelComboboxFSTab.TabIndex = 226
Me.SoftTDLabelComboboxFSTab.TabStop = false
Me.SoftTDLabelComboboxFSTab.Visible = false
'
'HardTDLabelComboboxFSTab
'
Me.HardTDLabelComboboxFSTab.Location = New System.Drawing.Point(624, 415)
Me.HardTDLabelComboboxFSTab.Name = "HardTDLabelComboboxFSTab"
Me.HardTDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
Me.HardTDLabelComboboxFSTab.TabIndex = 225
Me.HardTDLabelComboboxFSTab.TabStop = false
Me.HardTDLabelComboboxFSTab.Visible = false
'
'SoftTDGroupFSTab
'
Me.SoftTDGroupFSTab.Controls.Add(Me.SoftTotalTDLabelFSTab)
Me.SoftTDGroupFSTab.Location = New System.Drawing.Point(432, 9)
Me.SoftTDGroupFSTab.Name = "SoftTDGroupFSTab"
Me.SoftTDGroupFSTab.Size = New System.Drawing.Size(355, 279)
Me.SoftTDGroupFSTab.TabIndex = 223
Me.SoftTDGroupFSTab.TabStop = false
Me.SoftTDGroupFSTab.Text = "Soft Total Forced TD Strategy"
'
'SoftTotalTDLabelFSTab
'
Me.SoftTotalTDLabelFSTab.Location = New System.Drawing.Point(10, 18)
Me.SoftTotalTDLabelFSTab.Name = "SoftTotalTDLabelFSTab"
Me.SoftTotalTDLabelFSTab.Size = New System.Drawing.Size(38, 19)
Me.SoftTotalTDLabelFSTab.TabIndex = 50
Me.SoftTotalTDLabelFSTab.Text = "Total"
'
'ForcedTableComboboxFSTab
'
Me.ForcedTableComboboxFSTab.Location = New System.Drawing.Point(624, 498)
Me.ForcedTableComboboxFSTab.Name = "ForcedTableComboboxFSTab"
Me.ForcedTableComboboxFSTab.Size = New System.Drawing.Size(96, 24)
Me.ForcedTableComboboxFSTab.TabIndex = 222
Me.ForcedTableComboboxFSTab.TabStop = false
Me.ForcedTableComboboxFSTab.Visible = false
'
'HardTDGroupFSTab
'
Me.HardTDGroupFSTab.Controls.Add(Me.HardTotalTDLabelFSTab)
Me.HardTDGroupFSTab.Location = New System.Drawing.Point(29, 9)
Me.HardTDGroupFSTab.Name = "HardTDGroupFSTab"
Me.HardTDGroupFSTab.Size = New System.Drawing.Size(355, 471)
Me.HardTDGroupFSTab.TabIndex = 224
Me.HardTDGroupFSTab.TabStop = false
Me.HardTDGroupFSTab.Text = "Hard Total Forced TD Strategy"
'
'HardTotalTDLabelFSTab
'
Me.HardTotalTDLabelFSTab.Location = New System.Drawing.Point(10, 18)
Me.HardTotalTDLabelFSTab.Name = "HardTotalTDLabelFSTab"
Me.HardTotalTDLabelFSTab.Size = New System.Drawing.Size(38, 19)
Me.HardTotalTDLabelFSTab.TabIndex = 222
Me.HardTotalTDLabelFSTab.Text = "Total"
Me.HardTotalTDLabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'SoftPairsCDTabFSTab
'
Me.SoftPairsCDTabFSTab.Controls.Add(Me.PairCDGroupFSTab)
Me.SoftPairsCDTabFSTab.Controls.Add(Me.SoftCDGroupFSTab)
Me.SoftPairsCDTabFSTab.Location = New System.Drawing.Point(4, 25)
Me.SoftPairsCDTabFSTab.Name = "SoftPairsCDTabFSTab"
Me.SoftPairsCDTabFSTab.Size = New System.Drawing.Size(817, 525)
Me.SoftPairsCDTabFSTab.TabIndex = 1
Me.SoftPairsCDTabFSTab.Text = "CD Soft/Pairs"
Me.SoftPairsCDTabFSTab.Visible = false
'
'PairCDGroupFSTab
'
Me.PairCDGroupFSTab.Controls.Add(Me.PairForcedCDLabelFSTab)
Me.PairCDGroupFSTab.Location = New System.Drawing.Point(432, 9)
Me.PairCDGroupFSTab.Name = "PairCDGroupFSTab"
Me.PairCDGroupFSTab.Size = New System.Drawing.Size(355, 303)
Me.PairCDGroupFSTab.TabIndex = 11
Me.PairCDGroupFSTab.TabStop = false
Me.PairCDGroupFSTab.Text = "Pairs Forced Strategy"
'
'PairForcedCDLabelFSTab
'
Me.PairForcedCDLabelFSTab.Location = New System.Drawing.Point(10, 18)
Me.PairForcedCDLabelFSTab.Name = "PairForcedCDLabelFSTab"
Me.PairForcedCDLabelFSTab.Size = New System.Drawing.Size(38, 19)
Me.PairForcedCDLabelFSTab.TabIndex = 50
Me.PairForcedCDLabelFSTab.Text = "Pair"
'
'SoftCDGroupFSTab
'
Me.SoftCDGroupFSTab.Controls.Add(Me.TotalForcedCDLabelFSTab)
Me.SoftCDGroupFSTab.Location = New System.Drawing.Point(29, 9)
Me.SoftCDGroupFSTab.Name = "SoftCDGroupFSTab"
Me.SoftCDGroupFSTab.Size = New System.Drawing.Size(355, 279)
Me.SoftCDGroupFSTab.TabIndex = 10
Me.SoftCDGroupFSTab.TabStop = false
Me.SoftCDGroupFSTab.Text = "Soft Total Forced Strategy"
'
'TotalForcedCDLabelFSTab
'
Me.TotalForcedCDLabelFSTab.Location = New System.Drawing.Point(10, 18)
Me.TotalForcedCDLabelFSTab.Name = "TotalForcedCDLabelFSTab"
Me.TotalForcedCDLabelFSTab.Size = New System.Drawing.Size(38, 19)
Me.TotalForcedCDLabelFSTab.TabIndex = 50
Me.TotalForcedCDLabelFSTab.Text = "Total"
'
'HardCDTabFSTab
'
Me.HardCDTabFSTab.Controls.Add(Me.HardCDHand2LabelFSTab)
Me.HardCDTabFSTab.Controls.Add(Me.HardCDHand1LabelFSTab)
Me.HardCDTabFSTab.Location = New System.Drawing.Point(4, 25)
Me.HardCDTabFSTab.Name = "HardCDTabFSTab"
Me.HardCDTabFSTab.Size = New System.Drawing.Size(817, 525)
Me.HardCDTabFSTab.TabIndex = 0
Me.HardCDTabFSTab.Text = "CD Hard"
Me.HardCDTabFSTab.Visible = false
'
'HardCDHand2LabelFSTab
'
Me.HardCDHand2LabelFSTab.Location = New System.Drawing.Point(440, 16)
Me.HardCDHand2LabelFSTab.Name = "HardCDHand2LabelFSTab"
Me.HardCDHand2LabelFSTab.Size = New System.Drawing.Size(38, 19)
Me.HardCDHand2LabelFSTab.TabIndex = 168
Me.HardCDHand2LabelFSTab.Text = "Hand"
Me.HardCDHand2LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'HardCDHand1LabelFSTab
'
Me.HardCDHand1LabelFSTab.Location = New System.Drawing.Point(24, 16)
Me.HardCDHand1LabelFSTab.Name = "HardCDHand1LabelFSTab"
Me.HardCDHand1LabelFSTab.Size = New System.Drawing.Size(38, 19)
Me.HardCDHand1LabelFSTab.TabIndex = 113
Me.HardCDHand1LabelFSTab.Text = "Hand"
Me.HardCDHand1LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'OtherTabFSTab
'
Me.OtherTabFSTab.Controls.Add(Me.DeleteAllForcedRulesButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.RenameRuleButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.LoadForcedRulesButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.UncheckAllForcedRulesButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.SavedForcedRulesButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.RestoreDefaultForcedRulesButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.MoveForcedRulesDownButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.MoveForcedRulesUpButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.UpdateForcedRuleButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.DeleteForcedRuleButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.ForcedRuleLabelFSTab)
Me.OtherTabFSTab.Controls.Add(Me.ForcedRuleDetailsGroupFSTab)
Me.OtherTabFSTab.Controls.Add(Me.AddForcedRuleButtonFSTab)
Me.OtherTabFSTab.Controls.Add(Me.ForcedRulesCheckListBoxFSTab)
Me.OtherTabFSTab.Location = New System.Drawing.Point(4, 25)
Me.OtherTabFSTab.Name = "OtherTabFSTab"
Me.OtherTabFSTab.Size = New System.Drawing.Size(817, 525)
Me.OtherTabFSTab.TabIndex = 4
Me.OtherTabFSTab.Text = "Other Forced Rules"
Me.OtherTabFSTab.Visible = false
'
'DeleteAllForcedRulesButtonFSTab
'
Me.DeleteAllForcedRulesButtonFSTab.Location = New System.Drawing.Point(653, 120)
Me.DeleteAllForcedRulesButtonFSTab.Name = "DeleteAllForcedRulesButtonFSTab"
Me.DeleteAllForcedRulesButtonFSTab.Size = New System.Drawing.Size(125, 28)
Me.DeleteAllForcedRulesButtonFSTab.TabIndex = 11
Me.DeleteAllForcedRulesButtonFSTab.Text = "Delete All Rules"
'
'RenameRuleButtonFSTab
'
Me.RenameRuleButtonFSTab.Location = New System.Drawing.Point(653, 92)
Me.RenameRuleButtonFSTab.Name = "RenameRuleButtonFSTab"
Me.RenameRuleButtonFSTab.Size = New System.Drawing.Size(125, 28)
Me.RenameRuleButtonFSTab.TabIndex = 10
Me.RenameRuleButtonFSTab.Text = "Rename Rule"
'
'LoadForcedRulesButtonFSTab
'
Me.LoadForcedRulesButtonFSTab.Location = New System.Drawing.Point(470, 166)
Me.LoadForcedRulesButtonFSTab.Name = "LoadForcedRulesButtonFSTab"
Me.LoadForcedRulesButtonFSTab.Size = New System.Drawing.Size(173, 28)
Me.LoadForcedRulesButtonFSTab.TabIndex = 3
Me.LoadForcedRulesButtonFSTab.Text = "Load Forced Rules File"
'
'UncheckAllForcedRulesButtonFSTab
'
Me.UncheckAllForcedRulesButtonFSTab.Location = New System.Drawing.Point(653, 65)
Me.UncheckAllForcedRulesButtonFSTab.Name = "UncheckAllForcedRulesButtonFSTab"
Me.UncheckAllForcedRulesButtonFSTab.Size = New System.Drawing.Size(125, 27)
Me.UncheckAllForcedRulesButtonFSTab.TabIndex = 9
Me.UncheckAllForcedRulesButtonFSTab.Text = "Uncheck All Rules"
'
'SavedForcedRulesButtonFSTab
'
Me.SavedForcedRulesButtonFSTab.Location = New System.Drawing.Point(470, 194)
Me.SavedForcedRulesButtonFSTab.Name = "SavedForcedRulesButtonFSTab"
Me.SavedForcedRulesButtonFSTab.Size = New System.Drawing.Size(173, 28)
Me.SavedForcedRulesButtonFSTab.TabIndex = 6
Me.SavedForcedRulesButtonFSTab.Text = "Save Forced Rules File"
'
'RestoreDefaultForcedRulesButtonFSTab
'
Me.RestoreDefaultForcedRulesButtonFSTab.Location = New System.Drawing.Point(288, 194)
Me.RestoreDefaultForcedRulesButtonFSTab.Name = "RestoreDefaultForcedRulesButtonFSTab"
Me.RestoreDefaultForcedRulesButtonFSTab.Size = New System.Drawing.Size(182, 28)
Me.RestoreDefaultForcedRulesButtonFSTab.TabIndex = 5
Me.RestoreDefaultForcedRulesButtonFSTab.Text = "Restore Default Rules"
'
'MoveForcedRulesDownButtonFSTab
'
Me.MoveForcedRulesDownButtonFSTab.Location = New System.Drawing.Point(653, 37)
Me.MoveForcedRulesDownButtonFSTab.Name = "MoveForcedRulesDownButtonFSTab"
Me.MoveForcedRulesDownButtonFSTab.Size = New System.Drawing.Size(125, 28)
Me.MoveForcedRulesDownButtonFSTab.TabIndex = 8
Me.MoveForcedRulesDownButtonFSTab.Text = "Move Rule Down"
'
'MoveForcedRulesUpButtonFSTab
'
Me.MoveForcedRulesUpButtonFSTab.Location = New System.Drawing.Point(653, 9)
Me.MoveForcedRulesUpButtonFSTab.Name = "MoveForcedRulesUpButtonFSTab"
Me.MoveForcedRulesUpButtonFSTab.Size = New System.Drawing.Size(125, 28)
Me.MoveForcedRulesUpButtonFSTab.TabIndex = 7
Me.MoveForcedRulesUpButtonFSTab.Text = "Move Rule Up"
'
'UpdateForcedRuleButtonFSTab
'
Me.UpdateForcedRuleButtonFSTab.Location = New System.Drawing.Point(288, 166)
Me.UpdateForcedRuleButtonFSTab.Name = "UpdateForcedRuleButtonFSTab"
Me.UpdateForcedRuleButtonFSTab.Size = New System.Drawing.Size(182, 28)
Me.UpdateForcedRuleButtonFSTab.TabIndex = 2
Me.UpdateForcedRuleButtonFSTab.Text = "Update Forced Rule"
'
'DeleteForcedRuleButtonFSTab
'
Me.DeleteForcedRuleButtonFSTab.Location = New System.Drawing.Point(115, 194)
Me.DeleteForcedRuleButtonFSTab.Name = "DeleteForcedRuleButtonFSTab"
Me.DeleteForcedRuleButtonFSTab.Size = New System.Drawing.Size(173, 28)
Me.DeleteForcedRuleButtonFSTab.TabIndex = 4
Me.DeleteForcedRuleButtonFSTab.Text = "Delete Forced Rule"
'
'ForcedRuleLabelFSTab
'
Me.ForcedRuleLabelFSTab.Location = New System.Drawing.Point(307, -9)
Me.ForcedRuleLabelFSTab.Name = "ForcedRuleLabelFSTab"
Me.ForcedRuleLabelFSTab.Size = New System.Drawing.Size(163, 9)
Me.ForcedRuleLabelFSTab.TabIndex = 28
Me.ForcedRuleLabelFSTab.Text = "Forced Strategies Applied"
'
'ForcedRuleDetailsGroupFSTab
'
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandNCardsComboBoxFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandTotalComboBoxFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.UpcardComboBoxFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.ForcedRuleStratBoxFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.StrategyComboBoxFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.DealerUpcardLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.StrategyLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.ExactMatchCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.SoftCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.OrLessCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.ClearForcedRuleButtonFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.PostSplitCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.PreSplitCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.ForcedRuleNameBoxFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.ForcedRuleNameLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandCompCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandTotalSizeCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.TotalBoxFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.NCardsBoxFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.OrMoreCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandNCardsLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.TotalLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.SoftLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.NCardsLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C3LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C10LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C4LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C5LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C2LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C6LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C7LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C8LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.C9LabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.CAceLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandSoftCheckFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandSoftLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandTotalLabelFSTab)
Me.ForcedRuleDetailsGroupFSTab.Location = New System.Drawing.Point(19, 240)
Me.ForcedRuleDetailsGroupFSTab.Name = "ForcedRuleDetailsGroupFSTab"
Me.ForcedRuleDetailsGroupFSTab.Size = New System.Drawing.Size(787, 268)
Me.ForcedRuleDetailsGroupFSTab.TabIndex = 12
Me.ForcedRuleDetailsGroupFSTab.TabStop = false
Me.ForcedRuleDetailsGroupFSTab.Text = "Forced Rule Details"
'
'HandNCardsComboBoxFSTab
'
Me.HandNCardsComboBoxFSTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.HandNCardsComboBoxFSTab.Items.AddRange(New Object() {"Any", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.HandNCardsComboBoxFSTab.Location = New System.Drawing.Point(211, 120)
Me.HandNCardsComboBoxFSTab.Name = "HandNCardsComboBoxFSTab"
Me.HandNCardsComboBoxFSTab.Size = New System.Drawing.Size(67, 24)
Me.HandNCardsComboBoxFSTab.TabIndex = 5
'
'HandTotalComboBoxFSTab
'
Me.HandTotalComboBoxFSTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.HandTotalComboBoxFSTab.Items.AddRange(New Object() {"Any", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.HandTotalComboBoxFSTab.Location = New System.Drawing.Point(29, 120)
Me.HandTotalComboBoxFSTab.Name = "HandTotalComboBoxFSTab"
Me.HandTotalComboBoxFSTab.Size = New System.Drawing.Size(67, 24)
Me.HandTotalComboBoxFSTab.TabIndex = 3
'
'UpcardComboBoxFSTab
'
Me.UpcardComboBoxFSTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UpcardComboBoxFSTab.Items.AddRange(New Object() {"All", "A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UpcardComboBoxFSTab.Location = New System.Drawing.Point(211, 222)
Me.UpcardComboBoxFSTab.Name = "UpcardComboBoxFSTab"
Me.UpcardComboBoxFSTab.Size = New System.Drawing.Size(58, 24)
Me.UpcardComboBoxFSTab.TabIndex = 11
'
'ForcedRuleStratBoxFSTab
'
Me.ForcedRuleStratBoxFSTab.Location = New System.Drawing.Point(634, 203)
Me.ForcedRuleStratBoxFSTab.Name = "ForcedRuleStratBoxFSTab"
Me.ForcedRuleStratBoxFSTab.ReadOnly = true
Me.ForcedRuleStratBoxFSTab.Size = New System.Drawing.Size(28, 22)
Me.ForcedRuleStratBoxFSTab.TabIndex = 12
Me.ForcedRuleStratBoxFSTab.Text = ""
Me.ForcedRuleStratBoxFSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'StrategyComboBoxFSTab
'
Me.StrategyComboBoxFSTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.StrategyComboBoxFSTab.Location = New System.Drawing.Point(662, 179)
Me.StrategyComboBoxFSTab.Name = "StrategyComboBoxFSTab"
Me.StrategyComboBoxFSTab.Size = New System.Drawing.Size(96, 24)
Me.StrategyComboBoxFSTab.TabIndex = 223
Me.StrategyComboBoxFSTab.TabStop = false
Me.StrategyComboBoxFSTab.Visible = false
'
'DealerUpcardLabelFSTab
'
Me.DealerUpcardLabelFSTab.Location = New System.Drawing.Point(192, 203)
Me.DealerUpcardLabelFSTab.Name = "DealerUpcardLabelFSTab"
Me.DealerUpcardLabelFSTab.Size = New System.Drawing.Size(96, 19)
Me.DealerUpcardLabelFSTab.TabIndex = 104
Me.DealerUpcardLabelFSTab.Text = "Dealer Upcard"
'
'StrategyLabelFSTab
'
Me.StrategyLabelFSTab.Location = New System.Drawing.Point(566, 203)
Me.StrategyLabelFSTab.Name = "StrategyLabelFSTab"
Me.StrategyLabelFSTab.Size = New System.Drawing.Size(58, 28)
Me.StrategyLabelFSTab.TabIndex = 102
Me.StrategyLabelFSTab.Text = "Strategy"
'
'ExactMatchCheckFSTab
'
Me.ExactMatchCheckFSTab.Location = New System.Drawing.Point(298, 166)
Me.ExactMatchCheckFSTab.Name = "ExactMatchCheckFSTab"
Me.ExactMatchCheckFSTab.Size = New System.Drawing.Size(163, 19)
Me.ExactMatchCheckFSTab.TabIndex = 8
Me.ExactMatchCheckFSTab.Text = "Exact Match Required"
'
'SoftCheckFSTab
'
Me.SoftCheckFSTab.Enabled = false
Me.SoftCheckFSTab.Location = New System.Drawing.Point(691, 120)
Me.SoftCheckFSTab.Name = "SoftCheckFSTab"
Me.SoftCheckFSTab.Size = New System.Drawing.Size(19, 18)
Me.SoftCheckFSTab.TabIndex = 31
Me.SoftCheckFSTab.TabStop = false
'
'OrLessCheckFSTab
'
Me.OrLessCheckFSTab.Location = New System.Drawing.Point(211, 166)
Me.OrLessCheckFSTab.Name = "OrLessCheckFSTab"
Me.OrLessCheckFSTab.Size = New System.Drawing.Size(87, 19)
Me.OrLessCheckFSTab.TabIndex = 7
Me.OrLessCheckFSTab.Text = "Or Less"
'
'ClearForcedRuleButtonFSTab
'
Me.ClearForcedRuleButtonFSTab.Location = New System.Drawing.Point(653, 28)
Me.ClearForcedRuleButtonFSTab.Name = "ClearForcedRuleButtonFSTab"
Me.ClearForcedRuleButtonFSTab.Size = New System.Drawing.Size(105, 27)
Me.ClearForcedRuleButtonFSTab.TabIndex = 99
Me.ClearForcedRuleButtonFSTab.Text = "Clear Rule"
'
'PostSplitCheckFSTab
'
Me.PostSplitCheckFSTab.Location = New System.Drawing.Point(29, 222)
Me.PostSplitCheckFSTab.Name = "PostSplitCheckFSTab"
Me.PostSplitCheckFSTab.Size = New System.Drawing.Size(125, 18)
Me.PostSplitCheckFSTab.TabIndex = 10
Me.PostSplitCheckFSTab.Text = "Apply Post-Split"
'
'PreSplitCheckFSTab
'
Me.PreSplitCheckFSTab.Location = New System.Drawing.Point(29, 203)
Me.PreSplitCheckFSTab.Name = "PreSplitCheckFSTab"
Me.PreSplitCheckFSTab.Size = New System.Drawing.Size(125, 19)
Me.PreSplitCheckFSTab.TabIndex = 9
Me.PreSplitCheckFSTab.Text = "Apply Pre-Split"
'
'ForcedRuleNameBoxFSTab
'
Me.ForcedRuleNameBoxFSTab.Location = New System.Drawing.Point(182, 28)
Me.ForcedRuleNameBoxFSTab.Name = "ForcedRuleNameBoxFSTab"
Me.ForcedRuleNameBoxFSTab.Size = New System.Drawing.Size(404, 22)
Me.ForcedRuleNameBoxFSTab.TabIndex = 0
Me.ForcedRuleNameBoxFSTab.Text = ""
'
'ForcedRuleNameLabelFSTab
'
Me.ForcedRuleNameLabelFSTab.Location = New System.Drawing.Point(29, 28)
Me.ForcedRuleNameLabelFSTab.Name = "ForcedRuleNameLabelFSTab"
Me.ForcedRuleNameLabelFSTab.Size = New System.Drawing.Size(144, 18)
Me.ForcedRuleNameLabelFSTab.TabIndex = 77
Me.ForcedRuleNameLabelFSTab.Text = "Forced Rule Name"
'
'HandCompCheckFSTab
'
Me.HandCompCheckFSTab.Location = New System.Drawing.Point(298, 65)
Me.HandCompCheckFSTab.Name = "HandCompCheckFSTab"
Me.HandCompCheckFSTab.Size = New System.Drawing.Size(240, 27)
Me.HandCompCheckFSTab.TabIndex = 2
Me.HandCompCheckFSTab.Text = "Rule Based on Hand Composition"
'
'HandTotalSizeCheckFSTab
'
Me.HandTotalSizeCheckFSTab.Location = New System.Drawing.Point(29, 65)
Me.HandTotalSizeCheckFSTab.Name = "HandTotalSizeCheckFSTab"
Me.HandTotalSizeCheckFSTab.Size = New System.Drawing.Size(269, 27)
Me.HandTotalSizeCheckFSTab.TabIndex = 1
Me.HandTotalSizeCheckFSTab.Text = "Strategy Based on Hand Total and Size"
'
'TotalBoxFSTab
'
Me.TotalBoxFSTab.Enabled = false
Me.TotalBoxFSTab.Location = New System.Drawing.Point(634, 120)
Me.TotalBoxFSTab.Name = "TotalBoxFSTab"
Me.TotalBoxFSTab.Size = New System.Drawing.Size(28, 22)
Me.TotalBoxFSTab.TabIndex = 30
Me.TotalBoxFSTab.TabStop = false
Me.TotalBoxFSTab.Text = ""
Me.TotalBoxFSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'NCardsBoxFSTab
'
Me.NCardsBoxFSTab.Enabled = false
Me.NCardsBoxFSTab.Location = New System.Drawing.Point(730, 120)
Me.NCardsBoxFSTab.Name = "NCardsBoxFSTab"
Me.NCardsBoxFSTab.Size = New System.Drawing.Size(28, 22)
Me.NCardsBoxFSTab.TabIndex = 32
Me.NCardsBoxFSTab.TabStop = false
Me.NCardsBoxFSTab.Text = ""
Me.NCardsBoxFSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'OrMoreCheckFSTab
'
Me.OrMoreCheckFSTab.Location = New System.Drawing.Point(211, 148)
Me.OrMoreCheckFSTab.Name = "OrMoreCheckFSTab"
Me.OrMoreCheckFSTab.Size = New System.Drawing.Size(87, 18)
Me.OrMoreCheckFSTab.TabIndex = 6
Me.OrMoreCheckFSTab.Text = "Or More"
'
'HandNCardsLabelFSTab
'
Me.HandNCardsLabelFSTab.Location = New System.Drawing.Point(211, 102)
Me.HandNCardsLabelFSTab.Name = "HandNCardsLabelFSTab"
Me.HandNCardsLabelFSTab.Size = New System.Drawing.Size(58, 18)
Me.HandNCardsLabelFSTab.TabIndex = 58
Me.HandNCardsLabelFSTab.Text = "N Cards"
Me.HandNCardsLabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'TotalLabelFSTab
'
Me.TotalLabelFSTab.Location = New System.Drawing.Point(634, 102)
Me.TotalLabelFSTab.Name = "TotalLabelFSTab"
Me.TotalLabelFSTab.Size = New System.Drawing.Size(38, 18)
Me.TotalLabelFSTab.TabIndex = 57
Me.TotalLabelFSTab.Text = "Total"
'
'SoftLabelFSTab
'
Me.SoftLabelFSTab.Location = New System.Drawing.Point(682, 102)
Me.SoftLabelFSTab.Name = "SoftLabelFSTab"
Me.SoftLabelFSTab.Size = New System.Drawing.Size(38, 18)
Me.SoftLabelFSTab.TabIndex = 56
Me.SoftLabelFSTab.Text = "Soft"
'
'NCardsLabelFSTab
'
Me.NCardsLabelFSTab.Location = New System.Drawing.Point(720, 83)
Me.NCardsLabelFSTab.Name = "NCardsLabelFSTab"
Me.NCardsLabelFSTab.Size = New System.Drawing.Size(48, 37)
Me.NCardsLabelFSTab.TabIndex = 55
Me.NCardsLabelFSTab.Text = "n Cards"
Me.NCardsLabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C3LabelFSTab
'
Me.C3LabelFSTab.Location = New System.Drawing.Point(360, 104)
Me.C3LabelFSTab.Name = "C3LabelFSTab"
Me.C3LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C3LabelFSTab.TabIndex = 54
Me.C3LabelFSTab.Text = "3"
Me.C3LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C10LabelFSTab
'
Me.C10LabelFSTab.Location = New System.Drawing.Point(584, 104)
Me.C10LabelFSTab.Name = "C10LabelFSTab"
Me.C10LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C10LabelFSTab.TabIndex = 53
Me.C10LabelFSTab.Text = "T"
Me.C10LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C4LabelFSTab
'
Me.C4LabelFSTab.Location = New System.Drawing.Point(392, 104)
Me.C4LabelFSTab.Name = "C4LabelFSTab"
Me.C4LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C4LabelFSTab.TabIndex = 52
Me.C4LabelFSTab.Text = "4"
Me.C4LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C5LabelFSTab
'
Me.C5LabelFSTab.Location = New System.Drawing.Point(424, 104)
Me.C5LabelFSTab.Name = "C5LabelFSTab"
Me.C5LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C5LabelFSTab.TabIndex = 51
Me.C5LabelFSTab.Text = "5"
Me.C5LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C2LabelFSTab
'
Me.C2LabelFSTab.Location = New System.Drawing.Point(328, 104)
Me.C2LabelFSTab.Name = "C2LabelFSTab"
Me.C2LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C2LabelFSTab.TabIndex = 50
Me.C2LabelFSTab.Text = "2"
Me.C2LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C6LabelFSTab
'
Me.C6LabelFSTab.Location = New System.Drawing.Point(456, 104)
Me.C6LabelFSTab.Name = "C6LabelFSTab"
Me.C6LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C6LabelFSTab.TabIndex = 49
Me.C6LabelFSTab.Text = "6"
Me.C6LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C7LabelFSTab
'
Me.C7LabelFSTab.Location = New System.Drawing.Point(488, 104)
Me.C7LabelFSTab.Name = "C7LabelFSTab"
Me.C7LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C7LabelFSTab.TabIndex = 48
Me.C7LabelFSTab.Text = "7"
Me.C7LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C8LabelFSTab
'
Me.C8LabelFSTab.Location = New System.Drawing.Point(520, 104)
Me.C8LabelFSTab.Name = "C8LabelFSTab"
Me.C8LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C8LabelFSTab.TabIndex = 47
Me.C8LabelFSTab.Text = "8"
Me.C8LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'C9LabelFSTab
'
Me.C9LabelFSTab.Location = New System.Drawing.Point(552, 104)
Me.C9LabelFSTab.Name = "C9LabelFSTab"
Me.C9LabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.C9LabelFSTab.TabIndex = 46
Me.C9LabelFSTab.Text = "9"
Me.C9LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'CAceLabelFSTab
'
Me.CAceLabelFSTab.Location = New System.Drawing.Point(296, 104)
Me.CAceLabelFSTab.Name = "CAceLabelFSTab"
Me.CAceLabelFSTab.Size = New System.Drawing.Size(20, 16)
Me.CAceLabelFSTab.TabIndex = 45
Me.CAceLabelFSTab.Text = "A"
Me.CAceLabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'HandSoftCheckFSTab
'
Me.HandSoftCheckFSTab.Location = New System.Drawing.Point(144, 120)
Me.HandSoftCheckFSTab.Name = "HandSoftCheckFSTab"
Me.HandSoftCheckFSTab.Size = New System.Drawing.Size(19, 18)
Me.HandSoftCheckFSTab.TabIndex = 4
'
'HandSoftLabelFSTab
'
Me.HandSoftLabelFSTab.Location = New System.Drawing.Point(125, 102)
Me.HandSoftLabelFSTab.Name = "HandSoftLabelFSTab"
Me.HandSoftLabelFSTab.Size = New System.Drawing.Size(67, 18)
Me.HandSoftLabelFSTab.TabIndex = 5
Me.HandSoftLabelFSTab.Text = "Hand Soft"
'
'HandTotalLabelFSTab
'
Me.HandTotalLabelFSTab.Location = New System.Drawing.Point(29, 102)
Me.HandTotalLabelFSTab.Name = "HandTotalLabelFSTab"
Me.HandTotalLabelFSTab.Size = New System.Drawing.Size(77, 18)
Me.HandTotalLabelFSTab.TabIndex = 3
Me.HandTotalLabelFSTab.Text = "Hand Total"
'
'AddForcedRuleButtonFSTab
'
Me.AddForcedRuleButtonFSTab.Location = New System.Drawing.Point(115, 166)
Me.AddForcedRuleButtonFSTab.Name = "AddForcedRuleButtonFSTab"
Me.AddForcedRuleButtonFSTab.Size = New System.Drawing.Size(173, 28)
Me.AddForcedRuleButtonFSTab.TabIndex = 1
Me.AddForcedRuleButtonFSTab.Text = "Add Forced Rule"
'
'ForcedRulesCheckListBoxFSTab
'
Me.ForcedRulesCheckListBoxFSTab.Location = New System.Drawing.Point(115, 9)
Me.ForcedRulesCheckListBoxFSTab.Name = "ForcedRulesCheckListBoxFSTab"
Me.ForcedRulesCheckListBoxFSTab.Size = New System.Drawing.Size(528, 140)
Me.ForcedRulesCheckListBoxFSTab.TabIndex = 0
'
'LoadForcedTablesButtonFSTab
'
Me.LoadForcedTablesButtonFSTab.Location = New System.Drawing.Point(739, 582)
Me.LoadForcedTablesButtonFSTab.Name = "LoadForcedTablesButtonFSTab"
Me.LoadForcedTablesButtonFSTab.Size = New System.Drawing.Size(87, 27)
Me.LoadForcedTablesButtonFSTab.TabIndex = 906
Me.LoadForcedTablesButtonFSTab.Text = "Load Table"
'
'SaveForcedTablesButtonFSTab
'
Me.SaveForcedTablesButtonFSTab.Location = New System.Drawing.Point(643, 582)
Me.SaveForcedTablesButtonFSTab.Name = "SaveForcedTablesButtonFSTab"
Me.SaveForcedTablesButtonFSTab.Size = New System.Drawing.Size(87, 27)
Me.SaveForcedTablesButtonFSTab.TabIndex = 905
Me.SaveForcedTablesButtonFSTab.Text = "Save Table"
'
'ClearForcedTablesButtonFSTab
'
Me.ClearForcedTablesButtonFSTab.Location = New System.Drawing.Point(547, 582)
Me.ClearForcedTablesButtonFSTab.Name = "ClearForcedTablesButtonFSTab"
Me.ClearForcedTablesButtonFSTab.Size = New System.Drawing.Size(87, 27)
Me.ClearForcedTablesButtonFSTab.TabIndex = 904
Me.ClearForcedTablesButtonFSTab.Text = "Clear Table"
'
'CopyCDButtonFSTab
'
Me.CopyCDButtonFSTab.Location = New System.Drawing.Point(115, 582)
Me.CopyCDButtonFSTab.Name = "CopyCDButtonFSTab"
Me.CopyCDButtonFSTab.Size = New System.Drawing.Size(87, 27)
Me.CopyCDButtonFSTab.TabIndex = 901
Me.CopyCDButtonFSTab.Text = "Copy CD"
'
'RecalcForcedStratButtonFSTab
'
Me.RecalcForcedStratButtonFSTab.Location = New System.Drawing.Point(317, 572)
Me.RecalcForcedStratButtonFSTab.Name = "RecalcForcedStratButtonFSTab"
Me.RecalcForcedStratButtonFSTab.Size = New System.Drawing.Size(211, 46)
Me.RecalcForcedStratButtonFSTab.TabIndex = 903
Me.RecalcForcedStratButtonFSTab.Text = "Recalculate Forced Strategy EVs"
'
'EORTab
'
Me.EORTab.Controls.Add(Me.EORTabControlEORTab)
Me.EORTab.Location = New System.Drawing.Point(4, 25)
Me.EORTab.Name = "EORTab"
Me.EORTab.Size = New System.Drawing.Size(846, 626)
Me.EORTab.TabIndex = 9
Me.EORTab.Text = "EORs"
'
'EORTabControlEORTab
'
Me.EORTabControlEORTab.Controls.Add(Me.SummaryEORTab)
Me.EORTabControlEORTab.Controls.Add(Me.HATabEORTab)
Me.EORTabControlEORTab.Controls.Add(Me.TotalTabEORTab)
Me.EORTabControlEORTab.Location = New System.Drawing.Point(10, 9)
Me.EORTabControlEORTab.Name = "EORTabControlEORTab"
Me.EORTabControlEORTab.SelectedIndex = 0
Me.EORTabControlEORTab.Size = New System.Drawing.Size(825, 609)
Me.EORTabControlEORTab.TabIndex = 128
'
'SummaryEORTab
'
Me.SummaryEORTab.Controls.Add(Me.TextButtonEORTab)
Me.SummaryEORTab.Controls.Add(Me.NCardEORLabelEORTab)
Me.SummaryEORTab.Controls.Add(Me.NCardEORBoxEORTab)
Me.SummaryEORTab.Controls.Add(Me.NetLabelEORTab)
Me.SummaryEORTab.Controls.Add(Me.ClearEORsButtonEORTab)
Me.SummaryEORTab.Controls.Add(Me.TDLabel1SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.TDEVBoxEORTab)
Me.SummaryEORTab.Controls.Add(Me.EORCardComboboxEORTab)
Me.SummaryEORTab.Controls.Add(Me.CDLabel3SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.TDLabel3SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.ForcedLabel3SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.NetLabelSummEORTab)
Me.SummaryEORTab.Controls.Add(Me.CDLabel1SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.ForcedLabel1SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.CDEVBoxEORTab)
Me.SummaryEORTab.Controls.Add(Me.ForcedEVBoxEORTab)
Me.SummaryEORTab.Controls.Add(Me.CardLabel2SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.CardLabel1SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.CalcEORsButtonEORTab)
Me.SummaryEORTab.Controls.Add(Me.NoteLabelEORTab)
Me.SummaryEORTab.Controls.Add(Me.ProbLabel2SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.ProbLabel1SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.EORLabelSummEORTab)
Me.SummaryEORTab.Controls.Add(Me.NetEORLabelSummEORTab)
Me.SummaryEORTab.Controls.Add(Me.CDLabel2SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.TDLabel2SummEORTab)
Me.SummaryEORTab.Controls.Add(Me.ForcedLabel2SummEORTab)
Me.SummaryEORTab.Location = New System.Drawing.Point(4, 25)
Me.SummaryEORTab.Name = "SummaryEORTab"
Me.SummaryEORTab.Size = New System.Drawing.Size(817, 580)
Me.SummaryEORTab.TabIndex = 6
Me.SummaryEORTab.Text = "Summary"
'
'TextButtonEORTab
'
Me.TextButtonEORTab.Location = New System.Drawing.Point(632, 512)
Me.TextButtonEORTab.Name = "TextButtonEORTab"
Me.TextButtonEORTab.Size = New System.Drawing.Size(72, 24)
Me.TextButtonEORTab.TabIndex = 239
Me.TextButtonEORTab.Text = "Text"
'
'NCardEORLabelEORTab
'
Me.NCardEORLabelEORTab.Location = New System.Drawing.Point(64, 504)
Me.NCardEORLabelEORTab.Name = "NCardEORLabelEORTab"
Me.NCardEORLabelEORTab.Size = New System.Drawing.Size(104, 32)
Me.NCardEORLabelEORTab.TabIndex = 238
Me.NCardEORLabelEORTab.Text = "Number of cards to remove"
'
'NCardEORBoxEORTab
'
Me.NCardEORBoxEORTab.Location = New System.Drawing.Point(184, 512)
Me.NCardEORBoxEORTab.Name = "NCardEORBoxEORTab"
Me.NCardEORBoxEORTab.Size = New System.Drawing.Size(72, 22)
Me.NCardEORBoxEORTab.TabIndex = 237
Me.NCardEORBoxEORTab.Text = ""
'
'NetLabelEORTab
'
Me.NetLabelEORTab.Location = New System.Drawing.Point(735, 160)
Me.NetLabelEORTab.Name = "NetLabelEORTab"
Me.NetLabelEORTab.Size = New System.Drawing.Size(56, 16)
Me.NetLabelEORTab.TabIndex = 236
Me.NetLabelEORTab.Text = "Net"
Me.NetLabelEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ClearEORsButtonEORTab
'
Me.ClearEORsButtonEORTab.Location = New System.Drawing.Point(712, 512)
Me.ClearEORsButtonEORTab.Name = "ClearEORsButtonEORTab"
Me.ClearEORsButtonEORTab.Size = New System.Drawing.Size(80, 37)
Me.ClearEORsButtonEORTab.TabIndex = 235
Me.ClearEORsButtonEORTab.Text = "Clear EOR Tables"
'
'TDLabel1SummEORTab
'
Me.TDLabel1SummEORTab.Location = New System.Drawing.Point(184, 96)
Me.TDLabel1SummEORTab.Name = "TDLabel1SummEORTab"
Me.TDLabel1SummEORTab.Size = New System.Drawing.Size(173, 18)
Me.TDLabel1SummEORTab.TabIndex = 234
Me.TDLabel1SummEORTab.Text = "Original Total Dependent"
'
'TDEVBoxEORTab
'
Me.TDEVBoxEORTab.Location = New System.Drawing.Point(368, 88)
Me.TDEVBoxEORTab.Name = "TDEVBoxEORTab"
Me.TDEVBoxEORTab.ReadOnly = true
Me.TDEVBoxEORTab.Size = New System.Drawing.Size(172, 22)
Me.TDEVBoxEORTab.TabIndex = 233
Me.TDEVBoxEORTab.TabStop = false
Me.TDEVBoxEORTab.Text = ""
Me.TDEVBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'EORCardComboboxEORTab
'
Me.EORCardComboboxEORTab.Location = New System.Drawing.Point(488, 512)
Me.EORCardComboboxEORTab.Name = "EORCardComboboxEORTab"
Me.EORCardComboboxEORTab.Size = New System.Drawing.Size(106, 24)
Me.EORCardComboboxEORTab.TabIndex = 232
'
'CDLabel3SummEORTab
'
Me.CDLabel3SummEORTab.Location = New System.Drawing.Point(20, 428)
Me.CDLabel3SummEORTab.Name = "CDLabel3SummEORTab"
Me.CDLabel3SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.CDLabel3SummEORTab.TabIndex = 231
Me.CDLabel3SummEORTab.Text = "Composition Dependent"
Me.CDLabel3SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'TDLabel3SummEORTab
'
Me.TDLabel3SummEORTab.Location = New System.Drawing.Point(20, 456)
Me.TDLabel3SummEORTab.Name = "TDLabel3SummEORTab"
Me.TDLabel3SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.TDLabel3SummEORTab.TabIndex = 230
Me.TDLabel3SummEORTab.Text = "Specific TD Strategy"
Me.TDLabel3SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'ForcedLabel3SummEORTab
'
Me.ForcedLabel3SummEORTab.Location = New System.Drawing.Point(20, 400)
Me.ForcedLabel3SummEORTab.Name = "ForcedLabel3SummEORTab"
Me.ForcedLabel3SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.ForcedLabel3SummEORTab.TabIndex = 229
Me.ForcedLabel3SummEORTab.Text = "Fixed Forced Strategy"
Me.ForcedLabel3SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'NetLabelSummEORTab
'
Me.NetLabelSummEORTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.NetLabelSummEORTab.Location = New System.Drawing.Point(368, 8)
Me.NetLabelSummEORTab.Name = "NetLabelSummEORTab"
Me.NetLabelSummEORTab.Size = New System.Drawing.Size(176, 19)
Me.NetLabelSummEORTab.TabIndex = 228
Me.NetLabelSummEORTab.Text = "Original Net EV"
Me.NetLabelSummEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'CDLabel1SummEORTab
'
Me.CDLabel1SummEORTab.Location = New System.Drawing.Point(184, 68)
Me.CDLabel1SummEORTab.Name = "CDLabel1SummEORTab"
Me.CDLabel1SummEORTab.Size = New System.Drawing.Size(173, 19)
Me.CDLabel1SummEORTab.TabIndex = 227
Me.CDLabel1SummEORTab.Text = "Composition Dependent"
'
'ForcedLabel1SummEORTab
'
Me.ForcedLabel1SummEORTab.Location = New System.Drawing.Point(184, 40)
Me.ForcedLabel1SummEORTab.Name = "ForcedLabel1SummEORTab"
Me.ForcedLabel1SummEORTab.Size = New System.Drawing.Size(173, 18)
Me.ForcedLabel1SummEORTab.TabIndex = 226
Me.ForcedLabel1SummEORTab.Text = "Forced Strategy"
'
'CDEVBoxEORTab
'
Me.CDEVBoxEORTab.Location = New System.Drawing.Point(368, 64)
Me.CDEVBoxEORTab.Name = "CDEVBoxEORTab"
Me.CDEVBoxEORTab.ReadOnly = true
Me.CDEVBoxEORTab.Size = New System.Drawing.Size(172, 22)
Me.CDEVBoxEORTab.TabIndex = 225
Me.CDEVBoxEORTab.TabStop = false
Me.CDEVBoxEORTab.Text = ""
Me.CDEVBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'ForcedEVBoxEORTab
'
Me.ForcedEVBoxEORTab.Location = New System.Drawing.Point(368, 40)
Me.ForcedEVBoxEORTab.Name = "ForcedEVBoxEORTab"
Me.ForcedEVBoxEORTab.ReadOnly = true
Me.ForcedEVBoxEORTab.Size = New System.Drawing.Size(172, 22)
Me.ForcedEVBoxEORTab.TabIndex = 224
Me.ForcedEVBoxEORTab.TabStop = false
Me.ForcedEVBoxEORTab.Text = ""
Me.ForcedEVBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CardLabel2SummEORTab
'
Me.CardLabel2SummEORTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.CardLabel2SummEORTab.Location = New System.Drawing.Point(20, 344)
Me.CardLabel2SummEORTab.Name = "CardLabel2SummEORTab"
Me.CardLabel2SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.CardLabel2SummEORTab.TabIndex = 223
Me.CardLabel2SummEORTab.Text = "Card Removed"
Me.CardLabel2SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'CardLabel1SummEORTab
'
Me.CardLabel1SummEORTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.CardLabel1SummEORTab.Location = New System.Drawing.Point(20, 160)
Me.CardLabel1SummEORTab.Name = "CardLabel1SummEORTab"
Me.CardLabel1SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.CardLabel1SummEORTab.TabIndex = 222
Me.CardLabel1SummEORTab.Text = "Card Removed"
Me.CardLabel1SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'CalcEORsButtonEORTab
'
Me.CalcEORsButtonEORTab.Location = New System.Drawing.Point(296, 504)
Me.CalcEORsButtonEORTab.Name = "CalcEORsButtonEORTab"
Me.CalcEORsButtonEORTab.Size = New System.Drawing.Size(164, 37)
Me.CalcEORsButtonEORTab.TabIndex = 221
Me.CalcEORsButtonEORTab.Text = "Calculate EOR(s)"
'
'NoteLabelEORTab
'
Me.NoteLabelEORTab.Location = New System.Drawing.Point(224, 552)
Me.NoteLabelEORTab.Name = "NoteLabelEORTab"
Me.NoteLabelEORTab.Size = New System.Drawing.Size(480, 18)
Me.NoteLabelEORTab.TabIndex = 220
Me.NoteLabelEORTab.Text = "*Note: The Forced strategy does not change but the CD and TD strategies do."
Me.NoteLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'ProbLabel2SummEORTab
'
Me.ProbLabel2SummEORTab.Location = New System.Drawing.Point(20, 372)
Me.ProbLabel2SummEORTab.Name = "ProbLabel2SummEORTab"
Me.ProbLabel2SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.ProbLabel2SummEORTab.TabIndex = 194
Me.ProbLabel2SummEORTab.Text = "Probability"
Me.ProbLabel2SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'ProbLabel1SummEORTab
'
Me.ProbLabel1SummEORTab.Location = New System.Drawing.Point(20, 188)
Me.ProbLabel1SummEORTab.Name = "ProbLabel1SummEORTab"
Me.ProbLabel1SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.ProbLabel1SummEORTab.TabIndex = 189
Me.ProbLabel1SummEORTab.Text = "Probability"
Me.ProbLabel1SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'EORLabelSummEORTab
'
Me.EORLabelSummEORTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.EORLabelSummEORTab.Location = New System.Drawing.Point(368, 136)
Me.EORLabelSummEORTab.Name = "EORLabelSummEORTab"
Me.EORLabelSummEORTab.Size = New System.Drawing.Size(176, 19)
Me.EORLabelSummEORTab.TabIndex = 188
Me.EORLabelSummEORTab.Text = "Effect of Removal (EOR)"
Me.EORLabelSummEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'NetEORLabelSummEORTab
'
Me.NetEORLabelSummEORTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.NetEORLabelSummEORTab.Location = New System.Drawing.Point(368, 320)
Me.NetEORLabelSummEORTab.Name = "NetEORLabelSummEORTab"
Me.NetEORLabelSummEORTab.Size = New System.Drawing.Size(182, 19)
Me.NetEORLabelSummEORTab.TabIndex = 187
Me.NetEORLabelSummEORTab.Text = "Net EV With Card Removed"
Me.NetEORLabelSummEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'CDLabel2SummEORTab
'
Me.CDLabel2SummEORTab.Location = New System.Drawing.Point(20, 244)
Me.CDLabel2SummEORTab.Name = "CDLabel2SummEORTab"
Me.CDLabel2SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.CDLabel2SummEORTab.TabIndex = 185
Me.CDLabel2SummEORTab.Text = "Composition Dependent"
Me.CDLabel2SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'TDLabel2SummEORTab
'
Me.TDLabel2SummEORTab.Location = New System.Drawing.Point(20, 272)
Me.TDLabel2SummEORTab.Name = "TDLabel2SummEORTab"
Me.TDLabel2SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.TDLabel2SummEORTab.TabIndex = 184
Me.TDLabel2SummEORTab.Text = "Specific TD Strategy"
Me.TDLabel2SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'ForcedLabel2SummEORTab
'
Me.ForcedLabel2SummEORTab.Location = New System.Drawing.Point(20, 216)
Me.ForcedLabel2SummEORTab.Name = "ForcedLabel2SummEORTab"
Me.ForcedLabel2SummEORTab.Size = New System.Drawing.Size(155, 16)
Me.ForcedLabel2SummEORTab.TabIndex = 183
Me.ForcedLabel2SummEORTab.Text = "Fixed Forced Strategy"
Me.ForcedLabel2SummEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'HATabEORTab
'
Me.HATabEORTab.Controls.Add(Me.ChangedOnlyCheckBoxEORTab)
Me.HATabEORTab.Controls.Add(Me.NCardsComboBoxEORTab)
Me.HATabEORTab.Controls.Add(Me.TotalComboBoxEORTab)
Me.HATabEORTab.Controls.Add(Me.CardRemovedComboBoxEORTab)
Me.HATabEORTab.Controls.Add(Me.UCComboBoxEORTab)
Me.HATabEORTab.Controls.Add(Me.CardRemovedLabelEORTab)
Me.HATabEORTab.Controls.Add(Me.ExactMatchCheckEORTab)
Me.HATabEORTab.Controls.Add(Me.ListSizeLabelEORTab)
Me.HATabEORTab.Controls.Add(Me.ListSizeBoxEORTab)
Me.HATabEORTab.Controls.Add(Me.HardOnlyCheckEORTab)
Me.HATabEORTab.Controls.Add(Me.SoftOnlyCheckEORTab)
Me.HATabEORTab.Controls.Add(Me.OrLessCheckEORTab)
Me.HATabEORTab.Controls.Add(Me.OrMoreCheckEORTab)
Me.HATabEORTab.Controls.Add(Me.EitherCheckEORTab)
Me.HATabEORTab.Controls.Add(Me.IncludesLabelEORTab)
Me.HATabEORTab.Controls.Add(Me.HandBoxEORTab)
Me.HATabEORTab.Controls.Add(Me.NCardLabelEORTab)
Me.HATabEORTab.Controls.Add(Me.UCLabelEORTab)
Me.HATabEORTab.Controls.Add(Me.SoftLabelEORTab)
Me.HATabEORTab.Controls.Add(Me.TotalLabelEORTab)
Me.HATabEORTab.Controls.Add(Me.HandListBoxEORTab)
Me.HATabEORTab.Controls.Add(Me.HandDetailsGroupEORTab)
Me.HATabEORTab.Location = New System.Drawing.Point(4, 25)
Me.HATabEORTab.Name = "HATabEORTab"
Me.HATabEORTab.Size = New System.Drawing.Size(817, 580)
Me.HATabEORTab.TabIndex = 7
Me.HATabEORTab.Text = "EOR Hand Analysis"
'
'ChangedOnlyCheckBoxEORTab
'
Me.ChangedOnlyCheckBoxEORTab.Location = New System.Drawing.Point(640, 32)
Me.ChangedOnlyCheckBoxEORTab.Name = "ChangedOnlyCheckBoxEORTab"
Me.ChangedOnlyCheckBoxEORTab.Size = New System.Drawing.Size(112, 24)
Me.ChangedOnlyCheckBoxEORTab.TabIndex = 226
Me.ChangedOnlyCheckBoxEORTab.Text = "Changed Only"
'
'NCardsComboBoxEORTab
'
Me.NCardsComboBoxEORTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.NCardsComboBoxEORTab.Items.AddRange(New Object() {"Any", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.NCardsComboBoxEORTab.Location = New System.Drawing.Point(352, 32)
Me.NCardsComboBoxEORTab.Name = "NCardsComboBoxEORTab"
Me.NCardsComboBoxEORTab.Size = New System.Drawing.Size(67, 24)
Me.NCardsComboBoxEORTab.TabIndex = 4
'
'TotalComboBoxEORTab
'
Me.TotalComboBoxEORTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.TotalComboBoxEORTab.Items.AddRange(New Object() {"Any", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
Me.TotalComboBoxEORTab.Location = New System.Drawing.Point(152, 32)
Me.TotalComboBoxEORTab.Name = "TotalComboBoxEORTab"
Me.TotalComboBoxEORTab.Size = New System.Drawing.Size(67, 24)
Me.TotalComboBoxEORTab.TabIndex = 0
'
'CardRemovedComboBoxEORTab
'
Me.CardRemovedComboBoxEORTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.CardRemovedComboBoxEORTab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.CardRemovedComboBoxEORTab.Location = New System.Drawing.Point(544, 32)
Me.CardRemovedComboBoxEORTab.Name = "CardRemovedComboBoxEORTab"
Me.CardRemovedComboBoxEORTab.Size = New System.Drawing.Size(57, 24)
Me.CardRemovedComboBoxEORTab.TabIndex = 225
'
'UCComboBoxEORTab
'
Me.UCComboBoxEORTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UCComboBoxEORTab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UCComboBoxEORTab.Location = New System.Drawing.Point(448, 32)
Me.UCComboBoxEORTab.Name = "UCComboBoxEORTab"
Me.UCComboBoxEORTab.Size = New System.Drawing.Size(57, 24)
Me.UCComboBoxEORTab.TabIndex = 7
'
'CardRemovedLabelEORTab
'
Me.CardRemovedLabelEORTab.Location = New System.Drawing.Point(528, 16)
Me.CardRemovedLabelEORTab.Name = "CardRemovedLabelEORTab"
Me.CardRemovedLabelEORTab.Size = New System.Drawing.Size(96, 15)
Me.CardRemovedLabelEORTab.TabIndex = 90
Me.CardRemovedLabelEORTab.Text = "Card Removed"
'
'ExactMatchCheckEORTab
'
Me.ExactMatchCheckEORTab.Location = New System.Drawing.Point(509, 111)
Me.ExactMatchCheckEORTab.Name = "ExactMatchCheckEORTab"
Me.ExactMatchCheckEORTab.Size = New System.Drawing.Size(105, 18)
Me.ExactMatchCheckEORTab.TabIndex = 10
Me.ExactMatchCheckEORTab.Text = "Exact Match"
'
'ListSizeLabelEORTab
'
Me.ListSizeLabelEORTab.Location = New System.Drawing.Point(298, 268)
Me.ListSizeLabelEORTab.Name = "ListSizeLabelEORTab"
Me.ListSizeLabelEORTab.Size = New System.Drawing.Size(182, 18)
Me.ListSizeLabelEORTab.TabIndex = 88
Me.ListSizeLabelEORTab.Text = "Hands meeting above criteria"
Me.ListSizeLabelEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'ListSizeBoxEORTab
'
Me.ListSizeBoxEORTab.Location = New System.Drawing.Point(480, 268)
Me.ListSizeBoxEORTab.Name = "ListSizeBoxEORTab"
Me.ListSizeBoxEORTab.ReadOnly = true
Me.ListSizeBoxEORTab.Size = New System.Drawing.Size(48, 22)
Me.ListSizeBoxEORTab.TabIndex = 87
Me.ListSizeBoxEORTab.TabStop = false
Me.ListSizeBoxEORTab.Text = ""
Me.ListSizeBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'HardOnlyCheckEORTab
'
Me.HardOnlyCheckEORTab.Location = New System.Drawing.Point(248, 64)
Me.HardOnlyCheckEORTab.Name = "HardOnlyCheckEORTab"
Me.HardOnlyCheckEORTab.Size = New System.Drawing.Size(96, 18)
Me.HardOnlyCheckEORTab.TabIndex = 2
Me.HardOnlyCheckEORTab.Text = "Hard Only"
'
'SoftOnlyCheckEORTab
'
Me.SoftOnlyCheckEORTab.Location = New System.Drawing.Point(248, 80)
Me.SoftOnlyCheckEORTab.Name = "SoftOnlyCheckEORTab"
Me.SoftOnlyCheckEORTab.Size = New System.Drawing.Size(86, 19)
Me.SoftOnlyCheckEORTab.TabIndex = 3
Me.SoftOnlyCheckEORTab.Text = "Soft Only"
'
'OrLessCheckEORTab
'
Me.OrLessCheckEORTab.Location = New System.Drawing.Point(352, 80)
Me.OrLessCheckEORTab.Name = "OrLessCheckEORTab"
Me.OrLessCheckEORTab.Size = New System.Drawing.Size(86, 19)
Me.OrLessCheckEORTab.TabIndex = 6
Me.OrLessCheckEORTab.Text = "Or Less"
'
'OrMoreCheckEORTab
'
Me.OrMoreCheckEORTab.Location = New System.Drawing.Point(352, 64)
Me.OrMoreCheckEORTab.Name = "OrMoreCheckEORTab"
Me.OrMoreCheckEORTab.Size = New System.Drawing.Size(86, 18)
Me.OrMoreCheckEORTab.TabIndex = 5
Me.OrMoreCheckEORTab.Text = "Or More"
'
'EitherCheckEORTab
'
Me.EitherCheckEORTab.Location = New System.Drawing.Point(248, 32)
Me.EitherCheckEORTab.Name = "EitherCheckEORTab"
Me.EitherCheckEORTab.Size = New System.Drawing.Size(67, 18)
Me.EitherCheckEORTab.TabIndex = 1
Me.EitherCheckEORTab.Text = "Either"
'
'IncludesLabelEORTab
'
Me.IncludesLabelEORTab.Location = New System.Drawing.Point(221, 111)
Me.IncludesLabelEORTab.Name = "IncludesLabelEORTab"
Me.IncludesLabelEORTab.Size = New System.Drawing.Size(96, 18)
Me.IncludesLabelEORTab.TabIndex = 86
Me.IncludesLabelEORTab.Text = "Hand Includes"
Me.IncludesLabelEORTab.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'
'HandBoxEORTab
'
Me.HandBoxEORTab.Location = New System.Drawing.Point(326, 111)
Me.HandBoxEORTab.Name = "HandBoxEORTab"
Me.HandBoxEORTab.Size = New System.Drawing.Size(164, 22)
Me.HandBoxEORTab.TabIndex = 9
Me.HandBoxEORTab.TabStop = false
Me.HandBoxEORTab.Text = ""
Me.HandBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'NCardLabelEORTab
'
Me.NCardLabelEORTab.Location = New System.Drawing.Point(352, 16)
Me.NCardLabelEORTab.Name = "NCardLabelEORTab"
Me.NCardLabelEORTab.Size = New System.Drawing.Size(57, 15)
Me.NCardLabelEORTab.TabIndex = 85
Me.NCardLabelEORTab.Text = "N Cards"
Me.NCardLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'UCLabelEORTab
'
Me.UCLabelEORTab.Location = New System.Drawing.Point(440, 16)
Me.UCLabelEORTab.Name = "UCLabelEORTab"
Me.UCLabelEORTab.Size = New System.Drawing.Size(67, 15)
Me.UCLabelEORTab.TabIndex = 84
Me.UCLabelEORTab.Text = "Upcard"
Me.UCLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'SoftLabelEORTab
'
Me.SoftLabelEORTab.Location = New System.Drawing.Point(248, 16)
Me.SoftLabelEORTab.Name = "SoftLabelEORTab"
Me.SoftLabelEORTab.Size = New System.Drawing.Size(67, 15)
Me.SoftLabelEORTab.TabIndex = 83
Me.SoftLabelEORTab.Text = "Hand Soft"
'
'TotalLabelEORTab
'
Me.TotalLabelEORTab.Location = New System.Drawing.Point(144, 16)
Me.TotalLabelEORTab.Name = "TotalLabelEORTab"
Me.TotalLabelEORTab.Size = New System.Drawing.Size(77, 15)
Me.TotalLabelEORTab.TabIndex = 82
Me.TotalLabelEORTab.Text = "Hand Total"
Me.TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'HandListBoxEORTab
'
Me.HandListBoxEORTab.ItemHeight = 16
Me.HandListBoxEORTab.Location = New System.Drawing.Point(221, 148)
Me.HandListBoxEORTab.Name = "HandListBoxEORTab"
Me.HandListBoxEORTab.Size = New System.Drawing.Size(384, 100)
Me.HandListBoxEORTab.TabIndex = 1
'
'HandDetailsGroupEORTab
'
Me.HandDetailsGroupEORTab.Controls.Add(Me.CardRemovedDetailsBoxEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.CardRemovedDetailsLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.Diff3DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.New3DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.Orig3DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.Diff2DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.New2DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.Orig2DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.Diff1DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.New1DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.Orig1DetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.UCDetailsBoxEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.UCDetailsLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.NCardsDetailLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.NCardsDetailsBoxEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.SoftDetailsCheckEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.SoftDetailsLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.CDDetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.ForcedDetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.TDDetailsLabelHATab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.SplitLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.StratLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.HitLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.SurrLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.DoubleLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.StandLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.HandLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.HandNameBoxEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.TotalDetailsLabelEORTab)
Me.HandDetailsGroupEORTab.Controls.Add(Me.TotalDetailsBoxEORTab)
Me.HandDetailsGroupEORTab.Location = New System.Drawing.Point(10, 295)
Me.HandDetailsGroupEORTab.Name = "HandDetailsGroupEORTab"
Me.HandDetailsGroupEORTab.Size = New System.Drawing.Size(796, 277)
Me.HandDetailsGroupEORTab.TabIndex = 12
Me.HandDetailsGroupEORTab.TabStop = false
Me.HandDetailsGroupEORTab.Text = "Hand Details"
'
'CardRemovedDetailsBoxEORTab
'
Me.CardRemovedDetailsBoxEORTab.Location = New System.Drawing.Point(749, 28)
Me.CardRemovedDetailsBoxEORTab.Name = "CardRemovedDetailsBoxEORTab"
Me.CardRemovedDetailsBoxEORTab.ReadOnly = true
Me.CardRemovedDetailsBoxEORTab.Size = New System.Drawing.Size(38, 22)
Me.CardRemovedDetailsBoxEORTab.TabIndex = 162
Me.CardRemovedDetailsBoxEORTab.TabStop = false
Me.CardRemovedDetailsBoxEORTab.Text = ""
Me.CardRemovedDetailsBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'CardRemovedDetailsLabelEORTab
'
Me.CardRemovedDetailsLabelEORTab.Location = New System.Drawing.Point(653, 28)
Me.CardRemovedDetailsLabelEORTab.Name = "CardRemovedDetailsLabelEORTab"
Me.CardRemovedDetailsLabelEORTab.Size = New System.Drawing.Size(96, 18)
Me.CardRemovedDetailsLabelEORTab.TabIndex = 161
Me.CardRemovedDetailsLabelEORTab.Text = "Card Removed"
'
'Diff3DetailsLabelHATab
'
Me.Diff3DetailsLabelHATab.Location = New System.Drawing.Point(696, 88)
Me.Diff3DetailsLabelHATab.Name = "Diff3DetailsLabelHATab"
Me.Diff3DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.Diff3DetailsLabelHATab.TabIndex = 160
Me.Diff3DetailsLabelHATab.Text = "EOR/Diff"
Me.Diff3DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'New3DetailsLabelHATab
'
Me.New3DetailsLabelHATab.Location = New System.Drawing.Point(636, 88)
Me.New3DetailsLabelHATab.Name = "New3DetailsLabelHATab"
Me.New3DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.New3DetailsLabelHATab.TabIndex = 158
Me.New3DetailsLabelHATab.Text = "New"
Me.New3DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Orig3DetailsLabelHATab
'
Me.Orig3DetailsLabelHATab.Location = New System.Drawing.Point(576, 88)
Me.Orig3DetailsLabelHATab.Name = "Orig3DetailsLabelHATab"
Me.Orig3DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.Orig3DetailsLabelHATab.TabIndex = 154
Me.Orig3DetailsLabelHATab.Text = "Orig"
Me.Orig3DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Diff2DetailsLabelHATab
'
Me.Diff2DetailsLabelHATab.Location = New System.Drawing.Point(480, 88)
Me.Diff2DetailsLabelHATab.Name = "Diff2DetailsLabelHATab"
Me.Diff2DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.Diff2DetailsLabelHATab.TabIndex = 139
Me.Diff2DetailsLabelHATab.Text = "EOR/Diff"
Me.Diff2DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'New2DetailsLabelHATab
'
Me.New2DetailsLabelHATab.Location = New System.Drawing.Point(420, 88)
Me.New2DetailsLabelHATab.Name = "New2DetailsLabelHATab"
Me.New2DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.New2DetailsLabelHATab.TabIndex = 137
Me.New2DetailsLabelHATab.Text = "New"
Me.New2DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Orig2DetailsLabelHATab
'
Me.Orig2DetailsLabelHATab.Location = New System.Drawing.Point(360, 88)
Me.Orig2DetailsLabelHATab.Name = "Orig2DetailsLabelHATab"
Me.Orig2DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.Orig2DetailsLabelHATab.TabIndex = 133
Me.Orig2DetailsLabelHATab.Text = "Orig"
Me.Orig2DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Diff1DetailsLabelHATab
'
Me.Diff1DetailsLabelHATab.Location = New System.Drawing.Point(264, 88)
Me.Diff1DetailsLabelHATab.Name = "Diff1DetailsLabelHATab"
Me.Diff1DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.Diff1DetailsLabelHATab.TabIndex = 118
Me.Diff1DetailsLabelHATab.Text = "EOR/Diff"
Me.Diff1DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'New1DetailsLabelHATab
'
Me.New1DetailsLabelHATab.Location = New System.Drawing.Point(204, 88)
Me.New1DetailsLabelHATab.Name = "New1DetailsLabelHATab"
Me.New1DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.New1DetailsLabelHATab.TabIndex = 116
Me.New1DetailsLabelHATab.Text = "New"
Me.New1DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Orig1DetailsLabelHATab
'
Me.Orig1DetailsLabelHATab.Location = New System.Drawing.Point(144, 88)
Me.Orig1DetailsLabelHATab.Name = "Orig1DetailsLabelHATab"
Me.Orig1DetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.Orig1DetailsLabelHATab.TabIndex = 111
Me.Orig1DetailsLabelHATab.Text = "Orig"
Me.Orig1DetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'UCDetailsBoxEORTab
'
Me.UCDetailsBoxEORTab.Location = New System.Drawing.Point(605, 28)
Me.UCDetailsBoxEORTab.Name = "UCDetailsBoxEORTab"
Me.UCDetailsBoxEORTab.ReadOnly = true
Me.UCDetailsBoxEORTab.Size = New System.Drawing.Size(38, 22)
Me.UCDetailsBoxEORTab.TabIndex = 82
Me.UCDetailsBoxEORTab.TabStop = false
Me.UCDetailsBoxEORTab.Text = ""
Me.UCDetailsBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'UCDetailsLabelEORTab
'
Me.UCDetailsLabelEORTab.Location = New System.Drawing.Point(547, 28)
Me.UCDetailsLabelEORTab.Name = "UCDetailsLabelEORTab"
Me.UCDetailsLabelEORTab.Size = New System.Drawing.Size(58, 18)
Me.UCDetailsLabelEORTab.TabIndex = 81
Me.UCDetailsLabelEORTab.Text = "Upcard"
'
'NCardsDetailLabelEORTab
'
Me.NCardsDetailLabelEORTab.Location = New System.Drawing.Point(442, 28)
Me.NCardsDetailLabelEORTab.Name = "NCardsDetailLabelEORTab"
Me.NCardsDetailLabelEORTab.Size = New System.Drawing.Size(57, 18)
Me.NCardsDetailLabelEORTab.TabIndex = 75
Me.NCardsDetailLabelEORTab.Text = "N Cards"
'
'NCardsDetailsBoxEORTab
'
Me.NCardsDetailsBoxEORTab.Location = New System.Drawing.Point(499, 28)
Me.NCardsDetailsBoxEORTab.Name = "NCardsDetailsBoxEORTab"
Me.NCardsDetailsBoxEORTab.ReadOnly = true
Me.NCardsDetailsBoxEORTab.Size = New System.Drawing.Size(39, 22)
Me.NCardsDetailsBoxEORTab.TabIndex = 76
Me.NCardsDetailsBoxEORTab.TabStop = false
Me.NCardsDetailsBoxEORTab.Text = ""
Me.NCardsDetailsBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'SoftDetailsCheckEORTab
'
Me.SoftDetailsCheckEORTab.Enabled = false
Me.SoftDetailsCheckEORTab.Location = New System.Drawing.Point(413, 28)
Me.SoftDetailsCheckEORTab.Name = "SoftDetailsCheckEORTab"
Me.SoftDetailsCheckEORTab.Size = New System.Drawing.Size(19, 18)
Me.SoftDetailsCheckEORTab.TabIndex = 74
Me.SoftDetailsCheckEORTab.TabStop = false
'
'SoftDetailsLabelEORTab
'
Me.SoftDetailsLabelEORTab.Location = New System.Drawing.Point(374, 28)
Me.SoftDetailsLabelEORTab.Name = "SoftDetailsLabelEORTab"
Me.SoftDetailsLabelEORTab.Size = New System.Drawing.Size(29, 18)
Me.SoftDetailsLabelEORTab.TabIndex = 73
Me.SoftDetailsLabelEORTab.Text = "Soft"
'
'CDDetailsLabelHATab
'
Me.CDDetailsLabelHATab.Location = New System.Drawing.Point(424, 64)
Me.CDDetailsLabelHATab.Name = "CDDetailsLabelHATab"
Me.CDDetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.CDDetailsLabelHATab.TabIndex = 34
Me.CDDetailsLabelHATab.Text = "CD"
Me.CDDetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'ForcedDetailsLabelHATab
'
Me.ForcedDetailsLabelHATab.Location = New System.Drawing.Point(208, 64)
Me.ForcedDetailsLabelHATab.Name = "ForcedDetailsLabelHATab"
Me.ForcedDetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.ForcedDetailsLabelHATab.TabIndex = 33
Me.ForcedDetailsLabelHATab.Text = "Forced"
Me.ForcedDetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'TDDetailsLabelHATab
'
Me.TDDetailsLabelHATab.Location = New System.Drawing.Point(643, 64)
Me.TDDetailsLabelHATab.Name = "TDDetailsLabelHATab"
Me.TDDetailsLabelHATab.Size = New System.Drawing.Size(60, 16)
Me.TDDetailsLabelHATab.TabIndex = 32
Me.TDDetailsLabelHATab.Text = "TD"
Me.TDDetailsLabelHATab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'SplitLabelEORTab
'
Me.SplitLabelEORTab.Location = New System.Drawing.Point(38, 246)
Me.SplitLabelEORTab.Name = "SplitLabelEORTab"
Me.SplitLabelEORTab.Size = New System.Drawing.Size(96, 16)
Me.SplitLabelEORTab.TabIndex = 14
Me.SplitLabelEORTab.Text = "Split EV"
'
'StratLabelEORTab
'
Me.StratLabelEORTab.Location = New System.Drawing.Point(38, 106)
Me.StratLabelEORTab.Name = "StratLabelEORTab"
Me.StratLabelEORTab.Size = New System.Drawing.Size(96, 16)
Me.StratLabelEORTab.TabIndex = 11
Me.StratLabelEORTab.Text = "Strategy"
'
'HitLabelEORTab
'
Me.HitLabelEORTab.Location = New System.Drawing.Point(38, 162)
Me.HitLabelEORTab.Name = "HitLabelEORTab"
Me.HitLabelEORTab.Size = New System.Drawing.Size(96, 16)
Me.HitLabelEORTab.TabIndex = 9
Me.HitLabelEORTab.Text = "Hit EV"
'
'SurrLabelEORTab
'
Me.SurrLabelEORTab.Location = New System.Drawing.Point(38, 218)
Me.SurrLabelEORTab.Name = "SurrLabelEORTab"
Me.SurrLabelEORTab.Size = New System.Drawing.Size(96, 16)
Me.SurrLabelEORTab.TabIndex = 7
Me.SurrLabelEORTab.Text = "Surrender EV"
'
'DoubleLabelEORTab
'
Me.DoubleLabelEORTab.Location = New System.Drawing.Point(38, 190)
Me.DoubleLabelEORTab.Name = "DoubleLabelEORTab"
Me.DoubleLabelEORTab.Size = New System.Drawing.Size(96, 16)
Me.DoubleLabelEORTab.TabIndex = 4
Me.DoubleLabelEORTab.Text = "Double EV"
'
'StandLabelEORTab
'
Me.StandLabelEORTab.Location = New System.Drawing.Point(38, 134)
Me.StandLabelEORTab.Name = "StandLabelEORTab"
Me.StandLabelEORTab.Size = New System.Drawing.Size(96, 16)
Me.StandLabelEORTab.TabIndex = 3
Me.StandLabelEORTab.Text = "Stand EV"
'
'HandLabelEORTab
'
Me.HandLabelEORTab.Location = New System.Drawing.Point(19, 28)
Me.HandLabelEORTab.Name = "HandLabelEORTab"
Me.HandLabelEORTab.Size = New System.Drawing.Size(39, 18)
Me.HandLabelEORTab.TabIndex = 1
Me.HandLabelEORTab.Text = "Hand"
'
'HandNameBoxEORTab
'
Me.HandNameBoxEORTab.Location = New System.Drawing.Point(67, 28)
Me.HandNameBoxEORTab.Name = "HandNameBoxEORTab"
Me.HandNameBoxEORTab.ReadOnly = true
Me.HandNameBoxEORTab.Size = New System.Drawing.Size(211, 22)
Me.HandNameBoxEORTab.TabIndex = 0
Me.HandNameBoxEORTab.TabStop = false
Me.HandNameBoxEORTab.Text = ""
Me.HandNameBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TotalDetailsLabelEORTab
'
Me.TotalDetailsLabelEORTab.Location = New System.Drawing.Point(288, 28)
Me.TotalDetailsLabelEORTab.Name = "TotalDetailsLabelEORTab"
Me.TotalDetailsLabelEORTab.Size = New System.Drawing.Size(38, 18)
Me.TotalDetailsLabelEORTab.TabIndex = 71
Me.TotalDetailsLabelEORTab.Text = "Total"
'
'TotalDetailsBoxEORTab
'
Me.TotalDetailsBoxEORTab.Location = New System.Drawing.Point(326, 28)
Me.TotalDetailsBoxEORTab.Name = "TotalDetailsBoxEORTab"
Me.TotalDetailsBoxEORTab.ReadOnly = true
Me.TotalDetailsBoxEORTab.Size = New System.Drawing.Size(39, 22)
Me.TotalDetailsBoxEORTab.TabIndex = 72
Me.TotalDetailsBoxEORTab.TabStop = false
Me.TotalDetailsBoxEORTab.Text = ""
Me.TotalDetailsBoxEORTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'
'TotalTabEORTab
'
Me.TotalTabEORTab.Controls.Add(Me.CardRemovedTotalComboBoxEORTab)
Me.TotalTabEORTab.Controls.Add(Me.UCTotalComboBoxEORTab)
Me.TotalTabEORTab.Controls.Add(Me.CardRemovedTotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.UCTotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.HandTypeGroupEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Diff2TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.New2TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Orig2TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Diff4TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.New4TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Orig4TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Diff5TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.New5TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Orig5TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Diff3TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.New3TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Orig3TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Diff1TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.New1TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.Orig1TotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.StratTotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.HitTotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.SurrenderTotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.DoubleTotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.StandTotalLabelEORTab)
Me.TotalTabEORTab.Controls.Add(Me.StrategyGroupEORTab)
Me.TotalTabEORTab.Location = New System.Drawing.Point(4, 25)
Me.TotalTabEORTab.Name = "TotalTabEORTab"
Me.TotalTabEORTab.Size = New System.Drawing.Size(817, 580)
Me.TotalTabEORTab.TabIndex = 8
Me.TotalTabEORTab.Text = "EOR Hand Total Analysis"
'
'CardRemovedTotalComboBoxEORTab
'
Me.CardRemovedTotalComboBoxEORTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.CardRemovedTotalComboBoxEORTab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.CardRemovedTotalComboBoxEORTab.Location = New System.Drawing.Point(691, 18)
Me.CardRemovedTotalComboBoxEORTab.Name = "CardRemovedTotalComboBoxEORTab"
Me.CardRemovedTotalComboBoxEORTab.Size = New System.Drawing.Size(58, 24)
Me.CardRemovedTotalComboBoxEORTab.TabIndex = 224
'
'UCTotalComboBoxEORTab
'
Me.UCTotalComboBoxEORTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.UCTotalComboBoxEORTab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
Me.UCTotalComboBoxEORTab.Location = New System.Drawing.Point(595, 18)
Me.UCTotalComboBoxEORTab.Name = "UCTotalComboBoxEORTab"
Me.UCTotalComboBoxEORTab.Size = New System.Drawing.Size(58, 24)
Me.UCTotalComboBoxEORTab.TabIndex = 223
'
'CardRemovedTotalLabelEORTab
'
Me.CardRemovedTotalLabelEORTab.Location = New System.Drawing.Point(672, 0)
Me.CardRemovedTotalLabelEORTab.Name = "CardRemovedTotalLabelEORTab"
Me.CardRemovedTotalLabelEORTab.Size = New System.Drawing.Size(96, 15)
Me.CardRemovedTotalLabelEORTab.TabIndex = 222
Me.CardRemovedTotalLabelEORTab.Text = "Card Removed"
'
'UCTotalLabelEORTab
'
Me.UCTotalLabelEORTab.Location = New System.Drawing.Point(595, 0)
Me.UCTotalLabelEORTab.Name = "UCTotalLabelEORTab"
Me.UCTotalLabelEORTab.Size = New System.Drawing.Size(58, 15)
Me.UCTotalLabelEORTab.TabIndex = 220
Me.UCTotalLabelEORTab.Text = "Upcard"
Me.UCTotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'HandTypeGroupEORTab
'
Me.HandTypeGroupEORTab.Controls.Add(Me.SoftButtonEORTab)
Me.HandTypeGroupEORTab.Controls.Add(Me.HardButtonEORTab)
Me.HandTypeGroupEORTab.Location = New System.Drawing.Point(384, 0)
Me.HandTypeGroupEORTab.Name = "HandTypeGroupEORTab"
Me.HandTypeGroupEORTab.Size = New System.Drawing.Size(182, 46)
Me.HandTypeGroupEORTab.TabIndex = 218
Me.HandTypeGroupEORTab.TabStop = false
Me.HandTypeGroupEORTab.Text = "Hand Type"
'
'SoftButtonEORTab
'
Me.SoftButtonEORTab.Location = New System.Drawing.Point(106, 18)
Me.SoftButtonEORTab.Name = "SoftButtonEORTab"
Me.SoftButtonEORTab.Size = New System.Drawing.Size(57, 19)
Me.SoftButtonEORTab.TabIndex = 4
Me.SoftButtonEORTab.Text = "Soft"
'
'HardButtonEORTab
'
Me.HardButtonEORTab.Location = New System.Drawing.Point(29, 18)
Me.HardButtonEORTab.Name = "HardButtonEORTab"
Me.HardButtonEORTab.Size = New System.Drawing.Size(57, 19)
Me.HardButtonEORTab.TabIndex = 3
Me.HardButtonEORTab.Text = "Hard"
'
'Diff2TotalLabelEORTab
'
Me.Diff2TotalLabelEORTab.Location = New System.Drawing.Point(288, 80)
Me.Diff2TotalLabelEORTab.Name = "Diff2TotalLabelEORTab"
Me.Diff2TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Diff2TotalLabelEORTab.TabIndex = 217
Me.Diff2TotalLabelEORTab.Text = "EOR"
Me.Diff2TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'New2TotalLabelEORTab
'
Me.New2TotalLabelEORTab.Location = New System.Drawing.Point(238, 80)
Me.New2TotalLabelEORTab.Name = "New2TotalLabelEORTab"
Me.New2TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.New2TotalLabelEORTab.TabIndex = 216
Me.New2TotalLabelEORTab.Text = "New"
Me.New2TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Orig2TotalLabelEORTab
'
Me.Orig2TotalLabelEORTab.Location = New System.Drawing.Point(188, 80)
Me.Orig2TotalLabelEORTab.Name = "Orig2TotalLabelEORTab"
Me.Orig2TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Orig2TotalLabelEORTab.TabIndex = 215
Me.Orig2TotalLabelEORTab.Text = "Orig"
Me.Orig2TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Diff4TotalLabelEORTab
'
Me.Diff4TotalLabelEORTab.Location = New System.Drawing.Point(600, 80)
Me.Diff4TotalLabelEORTab.Name = "Diff4TotalLabelEORTab"
Me.Diff4TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Diff4TotalLabelEORTab.TabIndex = 214
Me.Diff4TotalLabelEORTab.Text = "EOR"
Me.Diff4TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'New4TotalLabelEORTab
'
Me.New4TotalLabelEORTab.Location = New System.Drawing.Point(550, 80)
Me.New4TotalLabelEORTab.Name = "New4TotalLabelEORTab"
Me.New4TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.New4TotalLabelEORTab.TabIndex = 213
Me.New4TotalLabelEORTab.Text = "New"
Me.New4TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Orig4TotalLabelEORTab
'
Me.Orig4TotalLabelEORTab.Location = New System.Drawing.Point(500, 80)
Me.Orig4TotalLabelEORTab.Name = "Orig4TotalLabelEORTab"
Me.Orig4TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Orig4TotalLabelEORTab.TabIndex = 212
Me.Orig4TotalLabelEORTab.Text = "Orig"
Me.Orig4TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Diff5TotalLabelEORTab
'
Me.Diff5TotalLabelEORTab.Location = New System.Drawing.Point(756, 80)
Me.Diff5TotalLabelEORTab.Name = "Diff5TotalLabelEORTab"
Me.Diff5TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Diff5TotalLabelEORTab.TabIndex = 211
Me.Diff5TotalLabelEORTab.Text = "EOR"
Me.Diff5TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'New5TotalLabelEORTab
'
Me.New5TotalLabelEORTab.Location = New System.Drawing.Point(706, 80)
Me.New5TotalLabelEORTab.Name = "New5TotalLabelEORTab"
Me.New5TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.New5TotalLabelEORTab.TabIndex = 210
Me.New5TotalLabelEORTab.Text = "New"
Me.New5TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Orig5TotalLabelEORTab
'
Me.Orig5TotalLabelEORTab.Location = New System.Drawing.Point(656, 80)
Me.Orig5TotalLabelEORTab.Name = "Orig5TotalLabelEORTab"
Me.Orig5TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Orig5TotalLabelEORTab.TabIndex = 209
Me.Orig5TotalLabelEORTab.Text = "Orig"
Me.Orig5TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Diff3TotalLabelEORTab
'
Me.Diff3TotalLabelEORTab.Location = New System.Drawing.Point(444, 80)
Me.Diff3TotalLabelEORTab.Name = "Diff3TotalLabelEORTab"
Me.Diff3TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Diff3TotalLabelEORTab.TabIndex = 208
Me.Diff3TotalLabelEORTab.Text = "EOR"
Me.Diff3TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'New3TotalLabelEORTab
'
Me.New3TotalLabelEORTab.Location = New System.Drawing.Point(394, 80)
Me.New3TotalLabelEORTab.Name = "New3TotalLabelEORTab"
Me.New3TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.New3TotalLabelEORTab.TabIndex = 207
Me.New3TotalLabelEORTab.Text = "New"
Me.New3TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Orig3TotalLabelEORTab
'
Me.Orig3TotalLabelEORTab.Location = New System.Drawing.Point(344, 80)
Me.Orig3TotalLabelEORTab.Name = "Orig3TotalLabelEORTab"
Me.Orig3TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Orig3TotalLabelEORTab.TabIndex = 206
Me.Orig3TotalLabelEORTab.Text = "Orig"
Me.Orig3TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Diff1TotalLabelEORTab
'
Me.Diff1TotalLabelEORTab.Location = New System.Drawing.Point(132, 80)
Me.Diff1TotalLabelEORTab.Name = "Diff1TotalLabelEORTab"
Me.Diff1TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Diff1TotalLabelEORTab.TabIndex = 169
Me.Diff1TotalLabelEORTab.Text = "EOR"
Me.Diff1TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'New1TotalLabelEORTab
'
Me.New1TotalLabelEORTab.Location = New System.Drawing.Point(82, 80)
Me.New1TotalLabelEORTab.Name = "New1TotalLabelEORTab"
Me.New1TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.New1TotalLabelEORTab.TabIndex = 168
Me.New1TotalLabelEORTab.Text = "New"
Me.New1TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Orig1TotalLabelEORTab
'
Me.Orig1TotalLabelEORTab.Location = New System.Drawing.Point(32, 80)
Me.Orig1TotalLabelEORTab.Name = "Orig1TotalLabelEORTab"
Me.Orig1TotalLabelEORTab.Size = New System.Drawing.Size(50, 16)
Me.Orig1TotalLabelEORTab.TabIndex = 167
Me.Orig1TotalLabelEORTab.Text = "Orig"
Me.Orig1TotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'StratTotalLabelEORTab
'
Me.StratTotalLabelEORTab.Location = New System.Drawing.Point(32, 56)
Me.StratTotalLabelEORTab.Name = "StratTotalLabelEORTab"
Me.StratTotalLabelEORTab.Size = New System.Drawing.Size(150, 16)
Me.StratTotalLabelEORTab.TabIndex = 165
Me.StratTotalLabelEORTab.Text = "Strategy"
Me.StratTotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'HitTotalLabelEORTab
'
Me.HitTotalLabelEORTab.Location = New System.Drawing.Point(344, 56)
Me.HitTotalLabelEORTab.Name = "HitTotalLabelEORTab"
Me.HitTotalLabelEORTab.Size = New System.Drawing.Size(150, 16)
Me.HitTotalLabelEORTab.TabIndex = 164
Me.HitTotalLabelEORTab.Text = "Hit EV"
Me.HitTotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'SurrenderTotalLabelEORTab
'
Me.SurrenderTotalLabelEORTab.Location = New System.Drawing.Point(656, 56)
Me.SurrenderTotalLabelEORTab.Name = "SurrenderTotalLabelEORTab"
Me.SurrenderTotalLabelEORTab.Size = New System.Drawing.Size(150, 16)
Me.SurrenderTotalLabelEORTab.TabIndex = 163
Me.SurrenderTotalLabelEORTab.Text = "Surrender EV"
Me.SurrenderTotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'DoubleTotalLabelEORTab
'
Me.DoubleTotalLabelEORTab.Location = New System.Drawing.Point(500, 56)
Me.DoubleTotalLabelEORTab.Name = "DoubleTotalLabelEORTab"
Me.DoubleTotalLabelEORTab.Size = New System.Drawing.Size(150, 16)
Me.DoubleTotalLabelEORTab.TabIndex = 162
Me.DoubleTotalLabelEORTab.Text = "Double EV"
Me.DoubleTotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'StandTotalLabelEORTab
'
Me.StandTotalLabelEORTab.Location = New System.Drawing.Point(188, 56)
Me.StandTotalLabelEORTab.Name = "StandTotalLabelEORTab"
Me.StandTotalLabelEORTab.Size = New System.Drawing.Size(150, 16)
Me.StandTotalLabelEORTab.TabIndex = 161
Me.StandTotalLabelEORTab.Text = "Stand EV"
Me.StandTotalLabelEORTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'StrategyGroupEORTab
'
Me.StrategyGroupEORTab.Controls.Add(Me.ForcedButtonEORTab)
Me.StrategyGroupEORTab.Controls.Add(Me.TDButtonEORTab)
Me.StrategyGroupEORTab.Location = New System.Drawing.Point(67, 0)
Me.StrategyGroupEORTab.Name = "StrategyGroupEORTab"
Me.StrategyGroupEORTab.Size = New System.Drawing.Size(298, 46)
Me.StrategyGroupEORTab.TabIndex = 96
Me.StrategyGroupEORTab.TabStop = false
Me.StrategyGroupEORTab.Text = "Strategy"
'
'ForcedButtonEORTab
'
Me.ForcedButtonEORTab.Location = New System.Drawing.Point(29, 18)
Me.ForcedButtonEORTab.Name = "ForcedButtonEORTab"
Me.ForcedButtonEORTab.Size = New System.Drawing.Size(125, 19)
Me.ForcedButtonEORTab.TabIndex = 3
Me.ForcedButtonEORTab.Text = "Forced Strategy"
'
'TDButtonEORTab
'
Me.TDButtonEORTab.Location = New System.Drawing.Point(173, 18)
Me.TDButtonEORTab.Name = "TDButtonEORTab"
Me.TDButtonEORTab.Size = New System.Drawing.Size(105, 19)
Me.TDButtonEORTab.TabIndex = 0
Me.TDButtonEORTab.Text = "TD Strategy"
'
'SplitsTab
'
Me.SplitsTab.Controls.Add(Me.UC10LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UC9LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UC8LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UC7LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UC6LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UC5LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UC4LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UC3LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UC2LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.UCALabelSplitTab)
Me.SplitsTab.Controls.Add(Me.Card5LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL25LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL35LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL15LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.Card3LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL23LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL33LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL13LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.Card4LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL24LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL34LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL14LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.Card2LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL22LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL32LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL12LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.Card1LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL21LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL31LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.SPL11LabelSplitTab)
Me.SplitsTab.Controls.Add(Me.PaircardsGroupSplitTab)
Me.SplitsTab.Controls.Add(Me.StrategyGroupSplitTab)
Me.SplitsTab.Location = New System.Drawing.Point(4, 25)
Me.SplitsTab.Name = "SplitsTab"
Me.SplitsTab.Size = New System.Drawing.Size(846, 626)
Me.SplitsTab.TabIndex = 5
Me.SplitsTab.Text = "Split EVs"
'
'UC10LabelSplitTab
'
Me.UC10LabelSplitTab.Location = New System.Drawing.Point(688, 74)
Me.UC10LabelSplitTab.Name = "UC10LabelSplitTab"
Me.UC10LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC10LabelSplitTab.TabIndex = 207
Me.UC10LabelSplitTab.Text = "T"
Me.UC10LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UC9LabelSplitTab
'
Me.UC9LabelSplitTab.Location = New System.Drawing.Point(616, 74)
Me.UC9LabelSplitTab.Name = "UC9LabelSplitTab"
Me.UC9LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC9LabelSplitTab.TabIndex = 206
Me.UC9LabelSplitTab.Text = "9"
Me.UC9LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UC8LabelSplitTab
'
Me.UC8LabelSplitTab.Location = New System.Drawing.Point(544, 74)
Me.UC8LabelSplitTab.Name = "UC8LabelSplitTab"
Me.UC8LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC8LabelSplitTab.TabIndex = 205
Me.UC8LabelSplitTab.Text = "8"
Me.UC8LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UC7LabelSplitTab
'
Me.UC7LabelSplitTab.Location = New System.Drawing.Point(472, 74)
Me.UC7LabelSplitTab.Name = "UC7LabelSplitTab"
Me.UC7LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC7LabelSplitTab.TabIndex = 204
Me.UC7LabelSplitTab.Text = "7"
Me.UC7LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UC6LabelSplitTab
'
Me.UC6LabelSplitTab.Location = New System.Drawing.Point(400, 74)
Me.UC6LabelSplitTab.Name = "UC6LabelSplitTab"
Me.UC6LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC6LabelSplitTab.TabIndex = 203
Me.UC6LabelSplitTab.Text = "6"
Me.UC6LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UC5LabelSplitTab
'
Me.UC5LabelSplitTab.Location = New System.Drawing.Point(328, 74)
Me.UC5LabelSplitTab.Name = "UC5LabelSplitTab"
Me.UC5LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC5LabelSplitTab.TabIndex = 202
Me.UC5LabelSplitTab.Text = "5"
Me.UC5LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UC4LabelSplitTab
'
Me.UC4LabelSplitTab.Location = New System.Drawing.Point(256, 74)
Me.UC4LabelSplitTab.Name = "UC4LabelSplitTab"
Me.UC4LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC4LabelSplitTab.TabIndex = 201
Me.UC4LabelSplitTab.Text = "4"
Me.UC4LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UC3LabelSplitTab
'
Me.UC3LabelSplitTab.Location = New System.Drawing.Point(184, 74)
Me.UC3LabelSplitTab.Name = "UC3LabelSplitTab"
Me.UC3LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC3LabelSplitTab.TabIndex = 200
Me.UC3LabelSplitTab.Text = "3"
Me.UC3LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UC2LabelSplitTab
'
Me.UC2LabelSplitTab.Location = New System.Drawing.Point(112, 74)
Me.UC2LabelSplitTab.Name = "UC2LabelSplitTab"
Me.UC2LabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UC2LabelSplitTab.TabIndex = 199
Me.UC2LabelSplitTab.Text = "2"
Me.UC2LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'UCALabelSplitTab
'
Me.UCALabelSplitTab.Location = New System.Drawing.Point(760, 74)
Me.UCALabelSplitTab.Name = "UCALabelSplitTab"
Me.UCALabelSplitTab.Size = New System.Drawing.Size(20, 16)
Me.UCALabelSplitTab.TabIndex = 198
Me.UCALabelSplitTab.Text = "A"
Me.UCALabelSplitTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'Card5LabelSplitTab
'
Me.Card5LabelSplitTab.Location = New System.Drawing.Point(0, 470)
Me.Card5LabelSplitTab.Name = "Card5LabelSplitTab"
Me.Card5LabelSplitTab.Size = New System.Drawing.Size(34, 18)
Me.Card5LabelSplitTab.TabIndex = 197
Me.Card5LabelSplitTab.Text = "5, 5"
Me.Card5LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'SPL25LabelSplitTab
'
Me.SPL25LabelSplitTab.Location = New System.Drawing.Point(38, 498)
Me.SPL25LabelSplitTab.Name = "SPL25LabelSplitTab"
Me.SPL25LabelSplitTab.Size = New System.Drawing.Size(39, 19)
Me.SPL25LabelSplitTab.TabIndex = 196
Me.SPL25LabelSplitTab.Text = "SPL2"
'
'SPL35LabelSplitTab
'
Me.SPL35LabelSplitTab.Location = New System.Drawing.Point(38, 526)
Me.SPL35LabelSplitTab.Name = "SPL35LabelSplitTab"
Me.SPL35LabelSplitTab.Size = New System.Drawing.Size(39, 19)
Me.SPL35LabelSplitTab.TabIndex = 195
Me.SPL35LabelSplitTab.Text = "SPL3"
'
'SPL15LabelSplitTab
'
Me.SPL15LabelSplitTab.Location = New System.Drawing.Point(38, 470)
Me.SPL15LabelSplitTab.Name = "SPL15LabelSplitTab"
Me.SPL15LabelSplitTab.Size = New System.Drawing.Size(39, 18)
Me.SPL15LabelSplitTab.TabIndex = 194
Me.SPL15LabelSplitTab.Text = "SPL1"
'
'Card3LabelSplitTab
'
Me.Card3LabelSplitTab.Location = New System.Drawing.Point(0, 286)
Me.Card3LabelSplitTab.Name = "Card3LabelSplitTab"
Me.Card3LabelSplitTab.Size = New System.Drawing.Size(34, 19)
Me.Card3LabelSplitTab.TabIndex = 193
Me.Card3LabelSplitTab.Text = "3, 3"
Me.Card3LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'SPL23LabelSplitTab
'
Me.SPL23LabelSplitTab.Location = New System.Drawing.Point(38, 314)
Me.SPL23LabelSplitTab.Name = "SPL23LabelSplitTab"
Me.SPL23LabelSplitTab.Size = New System.Drawing.Size(39, 18)
Me.SPL23LabelSplitTab.TabIndex = 192
Me.SPL23LabelSplitTab.Text = "SPL2"
'
'SPL33LabelSplitTab
'
Me.SPL33LabelSplitTab.Location = New System.Drawing.Point(38, 342)
Me.SPL33LabelSplitTab.Name = "SPL33LabelSplitTab"
Me.SPL33LabelSplitTab.Size = New System.Drawing.Size(39, 18)
Me.SPL33LabelSplitTab.TabIndex = 191
Me.SPL33LabelSplitTab.Text = "SPL3"
'
'SPL13LabelSplitTab
'
Me.SPL13LabelSplitTab.Location = New System.Drawing.Point(38, 286)
Me.SPL13LabelSplitTab.Name = "SPL13LabelSplitTab"
Me.SPL13LabelSplitTab.Size = New System.Drawing.Size(39, 19)
Me.SPL13LabelSplitTab.TabIndex = 190
Me.SPL13LabelSplitTab.Text = "SPL1"
'
'Card4LabelSplitTab
'
Me.Card4LabelSplitTab.Location = New System.Drawing.Point(0, 378)
Me.Card4LabelSplitTab.Name = "Card4LabelSplitTab"
Me.Card4LabelSplitTab.Size = New System.Drawing.Size(34, 19)
Me.Card4LabelSplitTab.TabIndex = 189
Me.Card4LabelSplitTab.Text = "4, 4"
Me.Card4LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'SPL24LabelSplitTab
'
Me.SPL24LabelSplitTab.Location = New System.Drawing.Point(38, 406)
Me.SPL24LabelSplitTab.Name = "SPL24LabelSplitTab"
Me.SPL24LabelSplitTab.Size = New System.Drawing.Size(39, 19)
Me.SPL24LabelSplitTab.TabIndex = 188
Me.SPL24LabelSplitTab.Text = "SPL2"
'
'SPL34LabelSplitTab
'
Me.SPL34LabelSplitTab.Location = New System.Drawing.Point(38, 434)
Me.SPL34LabelSplitTab.Name = "SPL34LabelSplitTab"
Me.SPL34LabelSplitTab.Size = New System.Drawing.Size(39, 18)
Me.SPL34LabelSplitTab.TabIndex = 187
Me.SPL34LabelSplitTab.Text = "SPL3"
'
'SPL14LabelSplitTab
'
Me.SPL14LabelSplitTab.Location = New System.Drawing.Point(38, 378)
Me.SPL14LabelSplitTab.Name = "SPL14LabelSplitTab"
Me.SPL14LabelSplitTab.Size = New System.Drawing.Size(39, 19)
Me.SPL14LabelSplitTab.TabIndex = 186
Me.SPL14LabelSplitTab.Text = "SPL1"
'
'Card2LabelSplitTab
'
Me.Card2LabelSplitTab.Location = New System.Drawing.Point(0, 194)
Me.Card2LabelSplitTab.Name = "Card2LabelSplitTab"
Me.Card2LabelSplitTab.Size = New System.Drawing.Size(34, 18)
Me.Card2LabelSplitTab.TabIndex = 185
Me.Card2LabelSplitTab.Text = "2, 2"
Me.Card2LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'SPL22LabelSplitTab
'
Me.SPL22LabelSplitTab.Location = New System.Drawing.Point(38, 222)
Me.SPL22LabelSplitTab.Name = "SPL22LabelSplitTab"
Me.SPL22LabelSplitTab.Size = New System.Drawing.Size(39, 18)
Me.SPL22LabelSplitTab.TabIndex = 184
Me.SPL22LabelSplitTab.Text = "SPL2"
'
'SPL32LabelSplitTab
'
Me.SPL32LabelSplitTab.Location = New System.Drawing.Point(38, 250)
Me.SPL32LabelSplitTab.Name = "SPL32LabelSplitTab"
Me.SPL32LabelSplitTab.Size = New System.Drawing.Size(39, 19)
Me.SPL32LabelSplitTab.TabIndex = 183
Me.SPL32LabelSplitTab.Text = "SPL3"
'
'SPL12LabelSplitTab
'
Me.SPL12LabelSplitTab.Location = New System.Drawing.Point(38, 194)
Me.SPL12LabelSplitTab.Name = "SPL12LabelSplitTab"
Me.SPL12LabelSplitTab.Size = New System.Drawing.Size(39, 18)
Me.SPL12LabelSplitTab.TabIndex = 182
Me.SPL12LabelSplitTab.Text = "SPL1"
'
'Card1LabelSplitTab
'
Me.Card1LabelSplitTab.Location = New System.Drawing.Point(0, 102)
Me.Card1LabelSplitTab.Name = "Card1LabelSplitTab"
Me.Card1LabelSplitTab.Size = New System.Drawing.Size(34, 18)
Me.Card1LabelSplitTab.TabIndex = 179
Me.Card1LabelSplitTab.Text = "A, A"
Me.Card1LabelSplitTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'SPL21LabelSplitTab
'
Me.SPL21LabelSplitTab.Location = New System.Drawing.Point(38, 130)
Me.SPL21LabelSplitTab.Name = "SPL21LabelSplitTab"
Me.SPL21LabelSplitTab.Size = New System.Drawing.Size(39, 19)
Me.SPL21LabelSplitTab.TabIndex = 178
Me.SPL21LabelSplitTab.Text = "SPL2"
'
'SPL31LabelSplitTab
'
Me.SPL31LabelSplitTab.Location = New System.Drawing.Point(38, 158)
Me.SPL31LabelSplitTab.Name = "SPL31LabelSplitTab"
Me.SPL31LabelSplitTab.Size = New System.Drawing.Size(39, 18)
Me.SPL31LabelSplitTab.TabIndex = 177
Me.SPL31LabelSplitTab.Text = "SPL3"
'
'SPL11LabelSplitTab
'
Me.SPL11LabelSplitTab.Location = New System.Drawing.Point(38, 102)
Me.SPL11LabelSplitTab.Name = "SPL11LabelSplitTab"
Me.SPL11LabelSplitTab.Size = New System.Drawing.Size(39, 18)
Me.SPL11LabelSplitTab.TabIndex = 176
Me.SPL11LabelSplitTab.Text = "SPL1"
'
'PaircardsGroupSplitTab
'
Me.PaircardsGroupSplitTab.Controls.Add(Me.SixtoTButtonSplitTab)
Me.PaircardsGroupSplitTab.Controls.Add(Me.Ato5ButtonSplitTab)
Me.PaircardsGroupSplitTab.Location = New System.Drawing.Point(269, 563)
Me.PaircardsGroupSplitTab.Name = "PaircardsGroupSplitTab"
Me.PaircardsGroupSplitTab.Size = New System.Drawing.Size(355, 46)
Me.PaircardsGroupSplitTab.TabIndex = 1
Me.PaircardsGroupSplitTab.TabStop = false
Me.PaircardsGroupSplitTab.Text = "Paircards"
'
'SixtoTButtonSplitTab
'
Me.SixtoTButtonSplitTab.Location = New System.Drawing.Point(192, 18)
Me.SixtoTButtonSplitTab.Name = "SixtoTButtonSplitTab"
Me.SixtoTButtonSplitTab.Size = New System.Drawing.Size(125, 19)
Me.SixtoTButtonSplitTab.TabIndex = 1
Me.SixtoTButtonSplitTab.Text = "Paircards 6-T"
'
'Ato5ButtonSplitTab
'
Me.Ato5ButtonSplitTab.Location = New System.Drawing.Point(58, 18)
Me.Ato5ButtonSplitTab.Name = "Ato5ButtonSplitTab"
Me.Ato5ButtonSplitTab.Size = New System.Drawing.Size(124, 19)
Me.Ato5ButtonSplitTab.TabIndex = 0
Me.Ato5ButtonSplitTab.Text = "Paircards A-5"
'
'StrategyGroupSplitTab
'
Me.StrategyGroupSplitTab.Controls.Add(Me.CDButtonSplitTab)
Me.StrategyGroupSplitTab.Controls.Add(Me.TCButtonSplitTab)
Me.StrategyGroupSplitTab.Controls.Add(Me.ForcedButtonSplitTab)
Me.StrategyGroupSplitTab.Controls.Add(Me.TDButtonSplitTab)
Me.StrategyGroupSplitTab.Location = New System.Drawing.Point(154, 18)
Me.StrategyGroupSplitTab.Name = "StrategyGroupSplitTab"
Me.StrategyGroupSplitTab.Size = New System.Drawing.Size(576, 47)
Me.StrategyGroupSplitTab.TabIndex = 0
Me.StrategyGroupSplitTab.TabStop = false
Me.StrategyGroupSplitTab.Text = "Strategy"
'
'CDButtonSplitTab
'
Me.CDButtonSplitTab.Location = New System.Drawing.Point(298, 18)
Me.CDButtonSplitTab.Name = "CDButtonSplitTab"
Me.CDButtonSplitTab.Size = New System.Drawing.Size(105, 19)
Me.CDButtonSplitTab.TabIndex = 2
Me.CDButtonSplitTab.Text = "CD Strategy"
'
'TCButtonSplitTab
'
Me.TCButtonSplitTab.Location = New System.Drawing.Point(163, 18)
Me.TCButtonSplitTab.Name = "TCButtonSplitTab"
Me.TCButtonSplitTab.Size = New System.Drawing.Size(125, 19)
Me.TCButtonSplitTab.TabIndex = 1
Me.TCButtonSplitTab.Text = "2-Card Strategy"
'
'ForcedButtonSplitTab
'
Me.ForcedButtonSplitTab.Location = New System.Drawing.Point(432, 18)
Me.ForcedButtonSplitTab.Name = "ForcedButtonSplitTab"
Me.ForcedButtonSplitTab.Size = New System.Drawing.Size(125, 19)
Me.ForcedButtonSplitTab.TabIndex = 3
Me.ForcedButtonSplitTab.Text = "Forced Strategy"
'
'TDButtonSplitTab
'
Me.TDButtonSplitTab.Location = New System.Drawing.Point(29, 18)
Me.TDButtonSplitTab.Name = "TDButtonSplitTab"
Me.TDButtonSplitTab.Size = New System.Drawing.Size(105, 19)
Me.TDButtonSplitTab.TabIndex = 0
Me.TDButtonSplitTab.Text = "TD Strategy"
'
'OtherTab
'
Me.OtherTab.Controls.Add(Me.ColorTableGroupOTab)
Me.OtherTab.Location = New System.Drawing.Point(4, 25)
Me.OtherTab.Name = "OtherTab"
Me.OtherTab.Size = New System.Drawing.Size(846, 626)
Me.OtherTab.TabIndex = 10
Me.OtherTab.Text = "Other"
'
'ColorTableGroupOTab
'
Me.ColorTableGroupOTab.Controls.Add(Me.RestoreDefaultColorTableButtonOTab)
Me.ColorTableGroupOTab.Controls.Add(Me.SaveColorTableFileButtonOTab)
Me.ColorTableGroupOTab.Controls.Add(Me.LoadColorTableFileButtonOTab)
Me.ColorTableGroupOTab.Location = New System.Drawing.Point(605, 18)
Me.ColorTableGroupOTab.Name = "ColorTableGroupOTab"
Me.ColorTableGroupOTab.Size = New System.Drawing.Size(221, 499)
Me.ColorTableGroupOTab.TabIndex = 0
Me.ColorTableGroupOTab.TabStop = false
Me.ColorTableGroupOTab.Text = "Strategy Colors"
'
'RestoreDefaultColorTableButtonOTab
'
Me.RestoreDefaultColorTableButtonOTab.Location = New System.Drawing.Point(48, 376)
Me.RestoreDefaultColorTableButtonOTab.Name = "RestoreDefaultColorTableButtonOTab"
Me.RestoreDefaultColorTableButtonOTab.Size = New System.Drawing.Size(128, 28)
Me.RestoreDefaultColorTableButtonOTab.TabIndex = 100
Me.RestoreDefaultColorTableButtonOTab.Text = "Restore Defaults"
'
'SaveColorTableFileButtonOTab
'
Me.SaveColorTableFileButtonOTab.Location = New System.Drawing.Point(48, 416)
Me.SaveColorTableFileButtonOTab.Name = "SaveColorTableFileButtonOTab"
Me.SaveColorTableFileButtonOTab.Size = New System.Drawing.Size(128, 28)
Me.SaveColorTableFileButtonOTab.TabIndex = 101
Me.SaveColorTableFileButtonOTab.Text = "Save Colors Table"
'
'LoadColorTableFileButtonOTab
'
Me.LoadColorTableFileButtonOTab.Location = New System.Drawing.Point(48, 456)
Me.LoadColorTableFileButtonOTab.Name = "LoadColorTableFileButtonOTab"
Me.LoadColorTableFileButtonOTab.Size = New System.Drawing.Size(128, 28)
Me.LoadColorTableFileButtonOTab.TabIndex = 102
Me.LoadColorTableFileButtonOTab.Text = "Load Colors Table"
'
'BJCAResultsForm
'
Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
Me.ClientSize = New System.Drawing.Size(854, 653)
Me.Controls.Add(Me.ResultsFormTabControl)
Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
Me.Name = "BJCAResultsForm"
Me.Text = "MGP's BJ CA Results Analysis"
Me.ResultsFormTabControl.ResumeLayout(false)
Me.SummaryTab.ResumeLayout(false)
Me.StratTab.ResumeLayout(false)
Me.StratTabControlSTab.ResumeLayout(false)
Me.HardSoftTDTabSTab.ResumeLayout(false)
Me.StratGroupTDHardSoftTab.ResumeLayout(false)
Me.SoftTDGroupSTab.ResumeLayout(false)
Me.HardTDGroupSTab.ResumeLayout(false)
Me.SoftPairsCDTabSTab.ResumeLayout(false)
Me.EVsBoxSTab.ResumeLayout(false)
Me.StratGroupTDSoftPairsTab.ResumeLayout(false)
Me.PairCDGroupSTab.ResumeLayout(false)
Me.SoftCDGroupSTab.ResumeLayout(false)
Me.HardCDTabSTab.ResumeLayout(false)
Me.StratGroupCDHardTab.ResumeLayout(false)
Me.SuitedTabSTab.ResumeLayout(false)
Me.HandDetailsGroupSTab.ResumeLayout(false)
Me.RulesTab.ResumeLayout(false)
Me.ShoeGroupRTab.ResumeLayout(false)
Me.AnalysisTab.ResumeLayout(false)
Me.AnalysisTabControl.ResumeLayout(false)
Me.HandAnalysisTab.ResumeLayout(false)
Me.HandDetailsGroupHATab.ResumeLayout(false)
Me.HandSizeAnalysisTab.ResumeLayout(false)
Me.StrategyGroupHSATab.ResumeLayout(false)
Me.DoubleAnalysisTab.ResumeLayout(false)
Me.HandDetailsGroupDATab.ResumeLayout(false)
Me.AllExceptionsTab.ResumeLayout(false)
Me.ExceptionsTabControl.ResumeLayout(false)
Me.ExceptionsTab.ResumeLayout(false)
Me.ExceptionDetailsGroupETab.ResumeLayout(false)
Me.NCardExceptionsTab.ResumeLayout(false)
Me.ExceptionDetailsGroupNCETab.ResumeLayout(false)
Me.ForcedTab.ResumeLayout(false)
Me.ForcedStratTabControlFSTab.ResumeLayout(false)
Me.OptionsTabFSTab.ResumeLayout(false)
Me.HardSoftTDTabFSTab.ResumeLayout(false)
Me.SoftTDGroupFSTab.ResumeLayout(false)
Me.HardTDGroupFSTab.ResumeLayout(false)
Me.SoftPairsCDTabFSTab.ResumeLayout(false)
Me.PairCDGroupFSTab.ResumeLayout(false)
Me.SoftCDGroupFSTab.ResumeLayout(false)
Me.HardCDTabFSTab.ResumeLayout(false)
Me.OtherTabFSTab.ResumeLayout(false)
Me.ForcedRuleDetailsGroupFSTab.ResumeLayout(false)
Me.EORTab.ResumeLayout(false)
Me.EORTabControlEORTab.ResumeLayout(false)
Me.SummaryEORTab.ResumeLayout(false)
Me.HATabEORTab.ResumeLayout(false)
Me.HandDetailsGroupEORTab.ResumeLayout(false)
Me.TotalTabEORTab.ResumeLayout(false)
Me.HandTypeGroupEORTab.ResumeLayout(false)
Me.StrategyGroupEORTab.ResumeLayout(false)
Me.SplitsTab.ResumeLayout(false)
Me.PaircardsGroupSplitTab.ResumeLayout(false)
Me.StrategyGroupSplitTab.ResumeLayout(false)
Me.OtherTab.ResumeLayout(false)
Me.ColorTableGroupOTab.ResumeLayout(false)
Me.ResumeLayout(false)

    End Sub

#End Region

#Region " General Methods "

    Private Sub InitializeForm()
        ProbLabelBoxSTab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        ProbBoxSTab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        ProbBoxHATab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))

        ExTypeBoxETab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        ExRuleNameBoxETab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        ProbBoxETab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))

        ExTypeBoxNCETab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        ExRuleNameBoxNCETab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))

        PopulateSummaryUpcardLabels()
        PopulateSummaryTables()
        PopulatePDTiesTable()
        PopulateSplitsAllowedTable()
        PopulateShoeTable()
        PopulateHandSizeAnalysisTable()
        PopulateSplitsTable()

        PopulateStratTableUpcardLabels()
        PopulateStratHardTDLabels()
        PopulateStratSoftTDLabels()
        PopulateStratHardCDLabels()
        PopulateStratSoftCDLabels()
        PopulateStratPairCDLabels()
        PopulateStratHardTDTable()
        PopulateStratSoftPairTDTables()
        PopulateStratHardCDTable()
        PopulateStratSoftPairCDTables()

        PopulateForcedRulesHandRulesTable()
        PopulateForcedTableUpcardLabels()

        PopulateHardTDLabels()
        PopulateSoftTDLabels()
        PopulateHardCDLabels()
        PopulateSoftCDLabels()
        PopulatePairCDLabels()

        PopulateHardTDTable()
        PopulateSoftTDTable()
        PopulateHardCDTable()
        PopulateSoftPairCDTables()
        LoadStrategyComboBoxFSTab()

        PopulateDoubleAnalysisTable()

        EORNcards = 1
        NCardEORBoxEORTab.Text = EORNcards
        EORs(11) = New BJCAEORsClass
        PopulateSummaryEORUpcardLabels()
        PopulateSummaryEORTables()
        LoadEORCardComboBoxEORTab()
        PopulateHandEORTable()

        PopulateHardEORTable()
        PopulateHardEORLabels()
        PopulateSoftEORTable()
        PopulateSoftEORLabels()

        ReDim StratColorBoxArray(Constants.NumStrats)
        PopulateStratColorTable()

        PopulateSuitedHandTable()

    End Sub

    Public Sub LoadFormResults()
        ResultsLoaded = True
        LoadDefaultOptions()
        LoadRulesForm()
        LoadFormForcedStrat()
        LoadFormSummaryPage()
        LoadFormSplitTable()
        LoadHandListHAForm()
        LoadExTypeComboBoxETab()
        LoadExTypeComboBoxNCETab()
        LoadFormColorTable()
        ClearFormSummaryEORPage()

        LoadSuitedHandListSForm()
    End Sub

    Private Sub LoadDefaultOptions()
        'Strategy Table Defaults
        TDButtonTDHardSoftTab.Checked = True

        'Hand Analysis Defaults
        TotalComboBoxHATab.SelectedIndex = 0
        UCComboBoxHATab.SelectedIndex = 0
        NCardsComboBoxHATab.SelectedIndex = 0
        EitherCheckHATab.Checked = True
        HardOnlyCheckHATab.Enabled = False
        SoftOnlyCheckHATab.Enabled = False
        HardOnlyCheckHATab.Checked = False
        SoftOnlyCheckHATab.Checked = False
        OrMoreCheckHATab.Enabled = False
        OrLessCheckHATab.Enabled = False
        OrMoreCheckHATab.Checked = False
        OrLessCheckHATab.Checked = False

        'Double Analysis Defaults 
        TotalComboBoxDATab.SelectedIndex = 0
        UCComboBoxDATab.SelectedIndex = 0
        NCardsComboBoxDATab.SelectedIndex = 0
        EitherCheckDATab.Checked = True
        HardOnlyCheckDATab.Enabled = False
        SoftOnlyCheckDATab.Enabled = False
        HardOnlyCheckDATab.Checked = False
        SoftOnlyCheckDATab.Checked = False
        OrMoreCheckDATab.Enabled = False
        OrLessCheckDATab.Enabled = False
        OrMoreCheckDATab.Checked = False
        OrLessCheckDATab.Checked = False

        'Hand Total Analysis Defaults
        TotalComboBoxHSATab.SelectedIndex = 0
        UCComboBoxHSATab.SelectedIndex = 0
        HandUsedCheckHSATab.Checked = True
        TDButtonHSATab.Checked = True

        'Split EV Defaults
        Ato5ButtonSplitTab.Checked = True
        TDButtonSplitTab.Checked = True

        'Exceptions Defaults
        TotalComboBoxETab.SelectedIndex = 0
        NCardsComboBoxETab.SelectedIndex = 0
        EitherCheckETab.Checked = True
        HardOnlyCheckETab.Enabled = False
        SoftOnlyCheckETab.Enabled = False
        HardOnlyCheckETab.Checked = False
        SoftOnlyCheckETab.Checked = False
        OrMoreCheckETab.Enabled = False
        OrLessCheckETab.Enabled = False
        OrMoreCheckETab.Checked = False
        OrLessCheckETab.Checked = False
        PreSplitCheckETab.Checked = True
        PostSplitCheckETab.Checked = True
        UCComboBoxETab.SelectedIndex = 0

        'N Card Exceptions Defaults
        TotalComboBoxNCETab.SelectedIndex = 0
        NCardsComboBoxNCETab.SelectedIndex = 0
        EitherCheckNCETab.Checked = True
        HardOnlyCheckNCETab.Enabled = False
        SoftOnlyCheckNCETab.Enabled = False
        HardOnlyCheckNCETab.Checked = False
        SoftOnlyCheckNCETab.Checked = False
        UCComboBoxNCETab.SelectedIndex = 0

        'EOR Defaults
        TotalComboBoxEORTab.SelectedIndex = 0
        NCardsComboBoxEORTab.SelectedIndex = 0
        EitherCheckEORTab.Checked = True
        HardOnlyCheckEORTab.Enabled = False
        SoftOnlyCheckEORTab.Enabled = False
        HardOnlyCheckEORTab.Checked = False
        SoftOnlyCheckEORTab.Checked = False
        OrMoreCheckEORTab.Enabled = False
        OrLessCheckEORTab.Enabled = False
        OrMoreCheckEORTab.Checked = False
        OrLessCheckEORTab.Checked = False
        UCComboBoxEORTab.SelectedIndex = 0
        CardRemovedComboBoxEORTab.SelectedIndex = 0

        UCComboBoxSTab.SelectedIndex = 0
        UCTotalComboBoxEORTab.SelectedIndex = 0
        CardRemovedTotalComboBoxEORTab.SelectedIndex = 0
        ForcedButtonEORTab.Checked = True
        HardButtonEORTab.Checked = True

    End Sub

    Private Sub LoadFormForcedStrat()
        ForcednCDBoxFSTab.Text = FormRules.ForcedStrat.ForcednCD
        ForcedTablePreCheckFSTab.Checked = FormRules.ForcedStrat.ForcedTablePreSplit
        ForcedTablePostCheckFSTab.Checked = FormRules.ForcedStrat.ForcedTablePostSplit

        LoadFormForcedTables()
        LoadFormForcedRulesList(FormRules.ForcedStrat.ForcedRulesList)
    End Sub

    Private Sub PrintToExelButtonSummTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToExelButtonSummTab.Click
        If MsgBox("Output details to Excel?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Results.PrintToExcel()
        End If
    End Sub

#End Region

#Region " Summary Tab "

    Public Sub LoadFormSummaryPage()
        Dim upcard As Integer

        FillNumberTextBox(TDEVBoxSummTab, Results.TD.GameEVs.NetGameEV, 15, True, Results.TD.ComputeStrat)
        FillNumberTextBox(TCEVBoxSummTab, Results.TC.GameEVs.NetGameEV, 15, True, Results.TC.ComputeStrat)
        FillNumberTextBox(CDEVBoxSummTab, Results.Opt.GameEVs.NetGameEV, 15, True)
        FillNumberTextBox(ForcedEVBoxSummTab, Results.Forced.GameEVs.NetGameEV, 15, True, Results.Forced.ComputeStrat)

        For upcard = 0 To 9
            If Results.Opt.GameEVs.CardProbs(upcard + 1) > 0 Then

                'Upcard EVs
                UCEVsArray(0, upcard).Text = CStr(Math.Round(Results.Opt.GameEVs.CardProbs(upcard + 1) * 100, 2)) + "%"
                FillNumberTextBox(UCEVsArray(1, upcard), Results.TD.GameEVs.UpcardEVs(upcard + 1), 2, True, Results.TD.ComputeStrat)
                FillNumberTextBox(UCEVsArray(2, upcard), Results.TC.GameEVs.UpcardEVs(upcard + 1), 2, True, Results.TC.ComputeStrat)
                FillNumberTextBox(UCEVsArray(3, upcard), Results.Opt.GameEVs.UpcardEVs(upcard + 1), 2, True)
                FillNumberTextBox(UCEVsArray(4, upcard), Results.Forced.GameEVs.UpcardEVs(upcard + 1), 2, True, Results.Forced.ComputeStrat)

                'Player's First Card EVs
                PCardEVsArray(0, upcard).Text = CStr(Math.Round(Results.Opt.GameEVs.CardProbs(upcard + 1) * 100, 2)) + "%"
                FillNumberTextBox(PCardEVsArray(1, upcard), Results.TD.GameEVs.FirstCardEVs(upcard + 1), 2, True, Results.TD.ComputeStrat)
                FillNumberTextBox(PCardEVsArray(2, upcard), Results.TC.GameEVs.FirstCardEVs(upcard + 1), 2, True, Results.TC.ComputeStrat)
                FillNumberTextBox(PCardEVsArray(3, upcard), Results.Opt.GameEVs.FirstCardEVs(upcard + 1), 2, True)
                FillNumberTextBox(PCardEVsArray(4, upcard), Results.Forced.GameEVs.FirstCardEVs(upcard + 1), 2, True, Results.Forced.ComputeStrat)

            End If
        Next upcard
    End Sub

    Private Sub TextButtonSummTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextButtonSummTab.Click
        Dim TextForm As New BJCATextForm
        Dim textString As String
        Dim upcard As Integer
        Dim column As Integer

        textString = "Net Game EVs"
        textString += Chr(13)
        textString += SetStringSize("TD EV:", 11, 11, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.TD.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
        textString += Chr(13)
        textString += SetStringSize("2C EV:", 11, 11, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.TC.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
        textString += Chr(13)
        textString += SetStringSize("CD EV:", 11, 11, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
        textString += Chr(13)
        textString += SetStringSize("Forced EV:", 11, 11, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.Forced.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
        textString += Chr(13)
        textString += Chr(13)

        'Upcard EVs
        textString += "Dealer Upcard EVs"
        textString += Chr(13)
        textString += SetStringSize("", 9, 9, StringAlignment.Far)
        textString += GetUpcardLabelString(9, False)
        textString += Chr(13)

        textString += SetStringSize("Prob:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.CardProbs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("TD:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.TD.GameEVs.UpcardEVs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("2C:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.TC.GameEVs.UpcardEVs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("CD:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.UpcardEVs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("Forced:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.Forced.GameEVs.UpcardEVs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += Chr(13)

        'Player's First Card EVs
        textString += "Player First Card EVs"
        textString += Chr(13)
        textString += SetStringSize("", 9, 9, StringAlignment.Far)
        textString += GetUpcardLabelString(9, False)
        textString += Chr(13)
        textString += SetStringSize("Prob:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.CardProbs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("TD:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.TD.GameEVs.FirstCardEVs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("2C:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.TC.GameEVs.FirstCardEVs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("CD:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.FirstCardEVs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("Forced:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.Forced.GameEVs.FirstCardEVs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += Chr(13)

        TextForm.RichTextBoxTextForm.Text = textString
        TextForm.Show()
    End Sub

    Private Sub PopulateSummaryUpcardLabels()
        PopulateUpcardLabels(SummaryTab, 248, 158, 56)
        PopulateUpcardLabels(SummaryTab, 248, 341, 56)
    End Sub

    Private Sub PopulateSummaryTables()
        Dim row As Integer
        Dim upcard As Integer

        For row = 0 To 4
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                'Populate the Upcard Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(56, 20)
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(232 + 9 * 56, 186 + row * 28)
                Else
                    box.Location = New System.Drawing.Point(232 + (upcard - 1) * 56, 186 + row * 28)
                End If
                If row = 0 Then
                    box.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
                End If

                SummaryTab.Controls.Add(box)
                UCEVsArray(row, upcard) = box
            Next upcard
        Next row

        For row = 0 To 4
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                'Populate the Player's Card Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(56, 20)
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(232 + 9 * 56, 369 + row * 28)
                Else
                    box.Location = New System.Drawing.Point(232 + (upcard - 1) * 56, 369 + row * 28)
                End If
                If row = 0 Then
                    box.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
                End If

                SummaryTab.Controls.Add(box)
                PCardEVsArray(row, upcard) = box
            Next upcard
        Next row

    End Sub

#End Region

#Region " Rules Tab "

    Private Sub PopulatePDTiesTable()
        Dim total As Integer

        For total = 17 To 22
            Dim box As New IndexedTextBox

            'Populate the Split EVs Table
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(56, 20)
            box.ReadOnly = True
            box.TabStop = False
            box.Index = 0
            box.Location = New System.Drawing.Point(162 + 56 * (total - 17), 312)

            RulesTab.Controls.Add(box)
            PDTiesArray(total - 17) = box
        Next total

    End Sub

    Private Sub PopulateSplitsAllowedTable()
        Dim card As Integer

        For card = 0 To 9
            Dim box As New IndexedTextBox

            'Populate the Split EVs Table
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(48, 20)
            box.ReadOnly = True
            box.TabStop = False
            box.Index = 0
            box.Location = New System.Drawing.Point(101 + 48 * card, 378)

            RulesTab.Controls.Add(box)
            SplitsAllowedArray(card) = box
        Next card

    End Sub

    Private Sub PopulateShoeTable()
        Dim card As Integer
        Dim row As Integer

        For card = 0 To 10
            For row = 0 To 4
                Dim box As New IndexedTextBox

                'Populate the Split EVs Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(56, 20)
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                If card = 10 Then
                    box.Location = New System.Drawing.Point(736, 46 + 28 * row)
                Else
                    box.Location = New System.Drawing.Point(140 + 56 * card, 46 + 28 * row)
                End If
                If row = 0 Then
                    box.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
                End If

                ShoeGroupRTab.Controls.Add(box)
                ShoeArray(row, card) = box
            Next row
        Next card

    End Sub

    Private Sub LoadRulesForm()
        Dim card As Integer
        Dim row As Integer
        Dim net(5) As Integer
        Dim suitName As String

        NDecksBoxRTab.Text = Results.DeckType

        For card = 1 To 10
            ShoeArray(0, card - 1).Text = Results.OriginalShoe.Cards(card)
            ShoeArray(1, card - 1).Text = Results.OriginalShoe.Suits(card, 0)
            ShoeArray(2, card - 1).Text = Results.OriginalShoe.Suits(card, 1)
            ShoeArray(3, card - 1).Text = Results.OriginalShoe.Suits(card, 2)
            ShoeArray(4, card - 1).Text = Results.OriginalShoe.Suits(card, 3)
            net(0) += Results.OriginalShoe.Cards(card)
            net(1) += Results.OriginalShoe.Suits(card, 0)
            net(2) += Results.OriginalShoe.Suits(card, 1)
            net(3) += Results.OriginalShoe.Suits(card, 2)
            net(4) += Results.OriginalShoe.Suits(card, 3)
        Next card
        ShoeArray(0, 10).Text = net(0)
        ShoeArray(1, 10).Text = net(1)
        ShoeArray(2, 10).Text = net(2)
        ShoeArray(3, 10).Text = net(3)
        ShoeArray(4, 10).Text = net(4)

        If Results.StandOnSoft = 17 Then
            StandsOnSoftBoxRTab.Text = "S17"
        Else
            StandsOnSoftBoxRTab.Text = "H17"
        End If

        If Results.ENHC Then
            BJRuleBoxRTab.Text = "ENHC"
        ElseIf Results.BBO Then
            BJRuleBoxRTab.Text = "BBO"
        ElseIf Results.OBBO Then
            BJRuleBoxRTab.Text = "OBBO"
        ElseIf Results.AOBBO Then
            BJRuleBoxRTab.Text = "AOBBO"
        Else
            BJRuleBoxRTab.Text = "OBO"
        End If

        DTypeBoxRTab.Text = Results.DoubleType
        DANBoxRTab.Text = Results.DAN
        DASBoxRTab.Text = Results.DAS

        SurrTypeBoxRTab.Text = Results.SurrType
        SANBoxRTab.Text = Results.SAN
        SASBoxRTab.Text = Results.SAS

        SPLBoxRTab.Text = "SPL" + CStr(Results.SPL)
        BJPaysBoxRTab.Text = CStr(Results.BJPays)

        SMABoxRTab.Text = Results.SMA
        HSABoxRTab.Text = Results.HSA
        DSABoxRTab.Text = Results.DSA
        SSABoxRTab.Text = Results.SSA

        If Results.RDA Then
            If Results.RDAPS Then
                RDABoxRTab.Text = CStr(Results.RDDepth) + "x AS"
            Else
                RDABoxRTab.Text = CStr(Results.RDDepth) + "x"
            End If
        Else
            RDABoxRTab.Text = Results.RDA
        End If
        If Results.DDR Then
            DDRTypeBoxRTab.Visible = True
            If Results.DDRType = Constants.Surr.LS Then
                DDRTypeBoxRTab.Text = "Late"
            Else
                DDRTypeBoxRTab.Text = "Early"
            End If
            If Results.DDRPS Then
                DDRBoxRTab.Text = "Always"
            Else
                DDRBoxRTab.Text = "Presplit"
            End If
        Else
            DDRBoxRTab.Text = Results.DDR
            DDRTypeBoxRTab.Visible = False
        End If

        If Results.DSoftAllHard Then
            DSoftHardBoxRTab.Text = "All"
        ElseIf Results.DSoft19Hard Then
            DSoftHardBoxRTab.Text = "19 Only"
        Else
            DSoftHardBoxRTab.Text = "False"
        End If

        P21AutowinBoxRTab.Text = Results.P21AutoWin

        SurrPaysBoxRTab.Text = CStr(Results.SurrPays)
        SurrPaysDBJBoxRTab.Text = CStr(Results.SurrDBJPays)
        MacauBoxRTab.Text = Results.MacauType

        If Results.CDP Then
            CDTypeBoxRTab.Text = "CDP"
        ElseIf Results.CDPN Then
            CDTypeBoxRTab.Text = "CDPN"
        Else
            CDTypeBoxRTab.Text = "CDZ-"
        End If

        BJSplitAcesBoxRTab.Text = Results.BJSplitAces
        BJSplitTensBoxRTab.Text = Results.BJSplitTens

        CheckTenBoxRTab.Text = Results.CheckTen
        CheckAceBoxRTab.Text = Results.CheckAce

        For card = 1 To 10
            SplitsAllowedArray(card - 1).Text = Results.SplitAllowed(card)
        Next card

        For card = 17 To 22
            PDTiesArray(card - 17).Text = CStr(Results.PDTies(card))
        Next card

        'Fill the Bonus Rules Box
        row = 0
        If Results.BJBonuses.PayoffSuited > 0 Then
            BonusRulesBoxRTab.Items.Add("Suited BJ Bonus " + CStr(Results.BJBonuses.PayoffSuited))
            row += 1
        End If
        If Results.BJBonuses.PayoffSpecificSuit > 0 Then
            If Results.BJBonuses.SuitToWin = 0 Then
                suitName = "Spades BJ Bonus "
            ElseIf Results.BJBonuses.SuitToWin = 1 Then
                suitName = "Hearts BJ Bonus "
            ElseIf Results.BJBonuses.SuitToWin = 2 Then
                suitName = "Diamonds BJ Bonus "
            Else
                suitName = "Clubs BJ Bonus "
            End If
            BonusRulesBoxRTab.Items.Add(suitName + CStr(Results.BJBonuses.PayoffSpecificSuit))
            row += 1
        End If
        If Results.BJBonuses.PayoffGeneralBJ > 0 Then
            BonusRulesBoxRTab.Items.Add("Specific Ten BJ Bonus " + CStr(Results.BJBonuses.PayoffGeneralBJ))
            row += 1
        End If
        If Results.BJBonuses.PayoffGeneral > 0 Then
            BonusRulesBoxRTab.Items.Add("Specific Ten Suited BJ Bonus " + CStr(Results.BJBonuses.PayoffGeneral))
            row += 1
        End If
        If Results.BJBonuses.PayoffUCGeneral > 0 Then
            If Results.BJBonuses.Upcard = 0 Then
                suitName = "Specific Ten Spades Bonus "
            ElseIf Results.BJBonuses.Upcard = 1 Then
                suitName = "Specific Ten Hearts Bonus "
            ElseIf Results.BJBonuses.Upcard = 2 Then
                suitName = "Specific Ten Diamonds Bonus "
            Else
                suitName = "Specific Ten Clubs Bonus "
            End If
            BonusRulesBoxRTab.Items.Add(suitName + CStr(Results.BJBonuses.PayoffUCGeneral))
            row += 1
        End If
        If (Results.BJBonuses.PayoffGeneralBJ > 0 Or Results.BJBonuses.PayoffGeneral > 0 Or Results.BJBonuses.PayoffUCGeneral > 0) And Results.BJBonuses.SpecificTenFraction > 0 Then
            BonusRulesBoxRTab.Items.Add("BJ Bonus Specific Ten Fraction " + CStr(Results.BJBonuses.SpecificTenFraction))
            row += 1
        End If
        For card = 0 To Results.BonusRulesList.NumRules - 1
            If Results.BonusRulesList.L(card).RuleOn Then
                BonusRulesBoxRTab.Items.Add(Results.BonusRulesList.L(card).Name)
                row += 1
            End If
        Next card
        If row = 0 Then
            BonusRulesBoxRTab.Items.Add("None")
        End If

        'Fill the Forced Rules Box
        row = 0
        For card = 0 To Results.ForcedRulesList.NumRules - 1
            If Results.ForcedRulesList.L(card).RuleOn Then
                ForcedRulesBoxRTab.Items.Add(Results.ForcedRulesList.L(card).Name)
                row += 1
            End If
        Next card
        For card = 0 To Results.ForcedTableRulesList.NumRules - 1
            If Results.ForcedTableRulesList.L(card).RuleOn Then
                ForcedRulesBoxRTab.Items.Add(Results.ForcedTableRulesList.L(card).Name)
                row += 1
            End If
        Next card
        If row = 0 Then
            ForcedRulesBoxRTab.Items.Add("None")
        End If

    End Sub

#End Region

#Region " Strategy Tab "

#Region " Strategy Tables "

    Private Sub LoadFormStrategyTables()
        Dim cStrat As BJCAStrategyClass
        Dim total As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim card2 As Integer
        Dim row As Integer
        Dim index As Integer
        Dim hand As New BJCAHandClass

        ClearFormStrategyTables()

        If TDButtonCDHardTab.Checked And Results.TD.ComputeStrat Then
            cStrat = Results.TD
        ElseIf TCButtonCDHardTab.Checked And Results.TC.ComputeStrat Then
            cStrat = Results.TC
        ElseIf ForcedButtonCDHardTab.Checked And Results.Forced.ComputeStrat Then
            cStrat = Results.Forced
        Else
            cStrat = Results.Opt
            CDButtonCDHardTab.Checked = True
        End If


        '2 Card Dependent Hard Strategies
        row = 0
        For total = 5 To 19
            If total < 13 Then
                card = 2
            Else
                card = total - 10
            End If
            card2 = total - card
            Do
                If Results.CardProb(card, 0) > 0 Then
                    hand.Deal(card)
                    If Results.CardProb(card2, 0) > 0 Then
                        hand.Deal(card2)
                        index = Results.FindPlayerHand(hand)
                        For upcard = 1 To 10
                            If Results.UCAllowed(upcard) Then
                                HardCDStratTableArray(row, upcard - 1).Text = Results.C.StratShortText(cStrat.HandEVs(index).EVs.Strat(upcard))
                                HardCDStratTableArray(row, upcard - 1).BackColor = FormRules.ColorTable.C(cStrat.HandEVs(index).EVs.Strat(upcard))
                                HardCDStratTableArray(row, upcard - 1).Index = cStrat.HandEVs(index).EVs.Strat(upcard)
                            End If
                        Next upcard
                        hand.Undeal(card2)
                    End If
                    hand.Undeal(card)
                End If
                row = row + 1
                card = card + 1
                card2 = card2 - 1
            Loop Until (card >= card2)
        Next total

        '2 Card Dependent Soft Hands
        If Results.CardProb(1, 0) > 0 Then
            hand.Deal(1)
            index = Results.FindPlayerHand(hand)
            For card = 2 To 10
                If Results.CardProb(card, 0) > 0 Then
                    hand.Deal(card)
                    For upcard = 1 To 10
                        If Results.UCAllowed(upcard) Then
                            SoftCDStratTableArray(card - 2, upcard - 1).Text = Results.C.StratShortText(cStrat.HandEVs(Results.PlayerHands(index).HitHand(card)).EVs.Strat(upcard))
                            SoftCDStratTableArray(card - 2, upcard - 1).BackColor = FormRules.ColorTable.C(cStrat.HandEVs(Results.PlayerHands(index).HitHand(card)).EVs.Strat(upcard))
                            SoftCDStratTableArray(card - 2, upcard - 1).Index = cStrat.HandEVs(Results.PlayerHands(index).HitHand(card)).EVs.Strat(upcard)
                        End If
                    Next upcard
                    hand.Undeal(card)
                End If
            Next card
            hand.Undeal(1)
        End If

        '2 Card Dependent Splits
        For card = 1 To 10
            For upcard = 1 To 10
                If Results.CurrentShoe.Cards(card) >= 2 And Results.UCAllowed(upcard) Then
                    hand.Deal(card)
                    hand.Deal(card)
                    index = Results.FindPlayerHand(hand)
                    hand.Undeal(card)
                    hand.Undeal(card)
                    PairCDStratTableArray(card - 1, upcard - 1).Text = Results.C.StratShortText(cStrat.HandEVs(index).EVs.Strat(upcard))
                    PairCDStratTableArray(card - 1, upcard - 1).BackColor = FormRules.ColorTable.C(cStrat.HandEVs(index).EVs.Strat(upcard))
                    PairCDStratTableArray(card - 1, upcard - 1).Index = cStrat.HandEVs(index).EVs.Strat(upcard)
                End If
            Next upcard
        Next card

        If cStrat.Name <> "CD" Then
            'Total Dependent Hard Strategies
            For total = 4 To 21
                For upcard = 1 To 10
                    If Results.UCAllowed(upcard) Then
                        HardTDStratTableArray(total - 4, upcard - 1).Text = Results.C.StratShortText(cStrat.StratTD(total, False + 1).Strat(upcard))
                        If cStrat.NCardOn Then
                            If cStrat.StratTD(total, False + 1).NCardStrat(upcard) <> Constants.Strat.None Then
                                HardTDStratTableArray(total - 4, upcard - 1).Text += CStr(cStrat.StratTD(total, False + 1).NCardDeviation(upcard))
                                HardTDStratTableArray(total - 4, upcard - 1).Text += Constants.StratShortText(cStrat.StratTD(total, False + 1).NCardStrat(upcard))
                            End If
                        End If
                        HardTDStratTableArray(total - 4, upcard - 1).BackColor = FormRules.ColorTable.C(cStrat.StratTD(total, False + 1).Strat(upcard))
                        HardTDStratTableArray(total - 4, upcard - 1).Index = cStrat.StratTD(total, False + 1).Strat(upcard)
                    End If
                Next upcard
            Next total

            'Total Dependent Soft Hands
            For total = 12 To 21
                For upcard = 1 To 10
                    If Results.UCAllowed(upcard) Then
                        SoftTDStratTableArray(total - 12, upcard - 1).Text = Results.C.StratShortText(cStrat.StratTD(total, True + 1).Strat(upcard))
                        If cStrat.NCardOn Then
                            If cStrat.StratTD(total, True + 1).NCardStrat(upcard) <> Constants.Strat.None Then
                                SoftTDStratTableArray(total - 12, upcard - 1).Text += CStr(cStrat.StratTD(total, True + 1).NCardDeviation(upcard))
                                SoftTDStratTableArray(total - 12, upcard - 1).Text += Constants.StratShortText(cStrat.StratTD(total, True + 1).NCardStrat(upcard))
                            End If
                        End If
                        SoftTDStratTableArray(total - 12, upcard - 1).BackColor = FormRules.ColorTable.C(cStrat.StratTD(total, True + 1).Strat(upcard))
                        SoftTDStratTableArray(total - 12, upcard - 1).Index = cStrat.StratTD(total, True + 1).Strat(upcard)
                    End If
                Next upcard
            Next total
        End If
    End Sub

    Private Sub LoadEVsBox(ByRef cStrat As BJCAStrategyClass, ByVal hand As BJCAHandClass, ByVal upcard As Integer)
        Dim index As Integer

        If hand.NumCards > 0 Then
            index = Results.FindPlayerHand(hand)
            ProbLabelBoxSTab.Text = "Probability:"
            ProbBoxSTab.Text = CStr(Math.Round(Results.PlayerHands(index).HandEVs.Prob(upcard), 15))

            If cStrat.HandEVs(index).SPreallowed(upcard) Then
                StandLabelBoxSTab.Text = "Stand EV:"
                FillNumberTextBox(StandEVBoxSTab, Results.PlayerHands(index).HandEVs.StandEV(upcard), 15, False)
                StandLabelBoxSTab.BackColor = StandEVBoxSTab.BackColor
                StandLabelBoxSTab.Visible = True
                StandEVBoxSTab.Visible = True
            Else
                StandLabelBoxSTab.Visible = False
                StandEVBoxSTab.Visible = False
            End If
            If (upcard = 1 And Results.CheckAce) Or (upcard = 10 And Results.CheckTen) Then
                BJStandLabelBoxSTab.Text = "BJ Stand EV:"
                FillNumberTextBox(BJStandEVBoxSTab, Results.PlayerHands(index).HandEVs.BJStandEV(upcard), 15, False)
                If Results.PlayerHands(index).HandEVs.BJStandEV(upcard) = 0 Then
                    BJStandLabelBoxSTab.BackColor = Nothing
                    BJStandEVBoxSTab.Text = "0"
                Else
                    BJStandLabelBoxSTab.BackColor = BJStandEVBoxSTab.BackColor
                End If
                BJStandLabelBoxSTab.Visible = True
                BJStandEVBoxSTab.Visible = True
            Else
                BJStandLabelBoxSTab.Visible = False
                BJStandEVBoxSTab.Visible = False
            End If

            If cStrat.HandEVs(index).HPreallowed(upcard) Then
                HitLabelBoxSTab.Text = "Hit EV:"
                FillNumberTextBox(HitEVBoxSTab, cStrat.HandEVs(index).EVs.HitEV(upcard), 15, False)
                HitLabelBoxSTab.BackColor = HitEVBoxSTab.BackColor
                HitLabelBoxSTab.Visible = True
                HitEVBoxSTab.Visible = True
            Else
                HitLabelBoxSTab.Visible = False
                HitEVBoxSTab.Visible = False
            End If

            If cStrat.HandEVs(index).DPreallowed(upcard) Then
                DoubleLabelBoxSTab.Text = "Double EV:"
                FillNumberTextBox(DoubleEVBoxSTab, Results.PlayerHands(index).HandEVs.DEV(upcard), 15, False)
                DoubleLabelBoxSTab.BackColor = DoubleEVBoxSTab.BackColor
                DoubleLabelBoxSTab.Visible = True
                DoubleEVBoxSTab.Visible = True
            Else
                DoubleLabelBoxSTab.Visible = False
                DoubleEVBoxSTab.Visible = False
            End If

            If cStrat.HandEVs(index).RPreallowed(upcard) Then
                SurrLabelBoxSTab.Text = "Surrender EV:"
                FillNumberTextBox(SurrEVBoxSTab, Results.PlayerHands(index).HandEVs.SurrEV(upcard), 15, False)
                SurrLabelBoxSTab.BackColor = SurrEVBoxSTab.BackColor
                SurrLabelBoxSTab.Visible = True
                SurrEVBoxSTab.Visible = True
            Else
                SurrLabelBoxSTab.Visible = False
                SurrEVBoxSTab.Visible = False
            End If

            If cStrat.HandEVs(index).PAllowed(upcard) Then
                SplitLabelBoxSTab.Text = "Split EV:"
                FillNumberTextBox(SplitEVBoxSTab, cStrat.HandEVs(index).SplitEV(upcard), 15, False)
                SplitLabelBoxSTab.BackColor = SplitEVBoxSTab.BackColor
                SplitLabelBoxSTab.Visible = True
                SplitEVBoxSTab.Visible = True
            Else
                SplitLabelBoxSTab.Visible = False
                SplitEVBoxSTab.Visible = False
            End If
        Else
            ProbLabelBoxSTab.Text = "Probability:"
            ProbBoxSTab.Text = CStr(Math.Round(cStrat.StratTD(hand.Total, hand.Soft + 1).NetProb(upcard), 15))
            ProbLabelBoxSTab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
            ProbBoxSTab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))

            If cStrat.StratTD(hand.Total, hand.Soft + 1).NetSProb(upcard) > 0 Then
                StandLabelBoxSTab.Text = "Stand EV:"
                FillNumberTextBox(StandEVBoxSTab, cStrat.StratTD(hand.Total, hand.Soft + 1).StratStandEV(upcard), 15, False)
                StandLabelBoxSTab.BackColor = StandEVBoxSTab.BackColor
                StandLabelBoxSTab.Visible = True
                StandEVBoxSTab.Visible = True
            Else
                StandLabelBoxSTab.Visible = False
                StandEVBoxSTab.Visible = False
            End If

            If cStrat.StratTD(hand.Total, hand.Soft + 1).NetHProb(upcard) > 0 Then
                HitLabelBoxSTab.Text = "Hit EV:"
                FillNumberTextBox(HitEVBoxSTab, cStrat.StratTD(hand.Total, hand.Soft + 1).StratHitEV(upcard), 15, False)
                HitLabelBoxSTab.BackColor = HitEVBoxSTab.BackColor
                HitLabelBoxSTab.Visible = True
                HitEVBoxSTab.Visible = True
            Else
                HitLabelBoxSTab.Visible = False
                HitEVBoxSTab.Visible = False
            End If

            If cStrat.StratTD(hand.Total, hand.Soft + 1).NetDProb(upcard) > 0 Then
                DoubleLabelBoxSTab.Text = "Double EV:"
                FillNumberTextBox(DoubleEVBoxSTab, cStrat.StratTD(hand.Total, hand.Soft + 1).StratDEV(upcard), 15, False)
                DoubleLabelBoxSTab.BackColor = DoubleEVBoxSTab.BackColor
                DoubleLabelBoxSTab.Visible = True
                DoubleEVBoxSTab.Visible = True
            Else
                DoubleLabelBoxSTab.Visible = False
                DoubleEVBoxSTab.Visible = False
            End If

            If cStrat.StratTD(hand.Total, hand.Soft + 1).NetSurrProb(upcard) > 0 Then
                SurrLabelBoxSTab.Text = "Surrender EV:"
                FillNumberTextBox(SurrEVBoxSTab, cStrat.StratTD(hand.Total, hand.Soft + 1).StratSurrEV(upcard), 15, False)
                SurrLabelBoxSTab.BackColor = SurrEVBoxSTab.BackColor
                SurrLabelBoxSTab.Visible = True
                SurrEVBoxSTab.Visible = True
            Else
                SurrLabelBoxSTab.Visible = False
                SurrEVBoxSTab.Visible = False
            End If
        End If

    End Sub

    Private Sub PrintStrategyTextBox()
        Dim TextForm As New BJCATextForm
        Dim textString As String
        Dim upcard As Integer
        Dim total As Integer
        Dim card As Integer
        Dim card2 As Integer
        Dim row As Integer
        Dim column As Integer

        If TDButtonCDHardTab.Checked And Results.TD.ComputeStrat Then
            textString = "Total Dependent Strategy"
            textString += Chr(13)
            textString += SetStringSize("Net Game EV:", 14, 14, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.TD.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
            textString += Chr(13)
        ElseIf TCButtonCDHardTab.Checked And Results.TC.ComputeStrat Then
            textString = "2-Card Strategy"
            textString += Chr(13)
            textString += SetStringSize("Net Game EV:", 14, 14, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.TC.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
            textString += Chr(13)
        ElseIf ForcedButtonCDHardTab.Checked And Results.Forced.ComputeStrat Then
            textString = "Forced Strategy"
            textString += Chr(13)
            textString += SetStringSize("Net Game EV:", 14, 14, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.Forced.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
            textString += Chr(13)
        Else
            textString = "Composition Dependent Strategy"
            textString += Chr(13)
            textString += SetStringSize("Net Game EV:", 14, 14, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
            textString += Chr(13)
        End If
        textString += Chr(13)
        textString += Chr(13)

        If Not CDButtonCDHardTab.Checked Then
            'Total Dependent Hard Strategies
            textString += "Total Dependent Hard Hands"
            textString += Chr(13)
            textString += SetStringSize("", 9, 9, StringAlignment.Far)
            textString += GetUpcardLabelString(9, False)
            textString += Chr(13)
            For total = 4 To 21
                textString += SetStringSize(CStr(total), 9, 9, StringAlignment.Near)
                For upcard = 1 To 10
                    If upcard = 10 Then
                        column = 0
                    Else
                        column = upcard
                    End If
                    textString += SetStringSize(HardTDStratTableArray(total - 4, column).Text, 9, 9, StringAlignment.Center)
                Next upcard
                textString += Chr(13)
            Next total
            textString += Chr(13)
            textString += Chr(13)

            'Total Dependent Soft Hands
            textString += "Total Dependent Soft Hands"
            textString += Chr(13)
            textString += SetStringSize("", 9, 9, StringAlignment.Far)
            textString += GetUpcardLabelString(9, False)
            textString += Chr(13)
            For total = 12 To 21
                textString += SetStringSize(CStr(total), 9, 9, StringAlignment.Near)
                For upcard = 1 To 10
                    If upcard = 10 Then
                        column = 0
                    Else
                        column = upcard
                    End If
                    textString += SetStringSize(SoftTDStratTableArray(total - 12, column).Text, 9, 9, StringAlignment.Center)
                Next upcard
                textString += Chr(13)
            Next total
            textString += Chr(13)
            textString += Chr(13)
        End If

        '2 Card Dependent Splits
        textString += "Split Hands"
        textString += Chr(13)
        textString += SetStringSize("", 9, 9, StringAlignment.Far)
        textString += GetUpcardLabelString(9, False)
        textString += Chr(13)
        For card = 1 To 10
            textString += SetStringSize(CStr(card) + ", " + CStr(card), 9, 9, StringAlignment.Near)
            For upcard = 1 To 10
                If upcard = 10 Then
                    column = 0
                Else
                    column = upcard
                End If
                textString += SetStringSize(PairCDStratTableArray(card - 1, column).Text, 9, 9, StringAlignment.Center)
            Next upcard
            textString += Chr(13)
        Next card
        textString += Chr(13)
        textString += Chr(13)

        '2 Card Dependent Hard Strategies
        textString += "2-Card Hard Hands"
        textString += Chr(13)
        textString += SetStringSize("", 9, 9, StringAlignment.Far)
        textString += GetUpcardLabelString(9, False)
        textString += Chr(13)
        row = 0
        For total = 5 To 19
            If total < 13 Then
                card = 2
            Else
                card = total - 10
            End If
            card2 = total - card
            Do
                textString += SetStringSize(CStr(card) + ", " + CStr(card2), 9, 9, StringAlignment.Near)
                For upcard = 1 To 10
                    If upcard = 10 Then
                        column = 0
                    Else
                        column = upcard
                    End If
                    textString += SetStringSize(HardCDStratTableArray(row, column).Text, 9, 9, StringAlignment.Center)
                Next upcard
                textString += Chr(13)
                row = row + 1
                card = card + 1
                card2 = card2 - 1
            Loop Until (card >= card2)
        Next total
        textString += Chr(13)
        textString += Chr(13)

        '2 Card Dependent Soft Hands
        textString += "2-Card Soft Hands"
        textString += Chr(13)
        textString += SetStringSize("", 9, 9, StringAlignment.Far)
        textString += GetUpcardLabelString(9, False)
        textString += Chr(13)
        For card = 2 To 10
            textString += SetStringSize("A, " + CStr(card), 9, 9, StringAlignment.Near)
            For upcard = 1 To 10
                If upcard = 10 Then
                    column = 0
                Else
                    column = upcard
                End If
                textString += SetStringSize(SoftCDStratTableArray(card - 2, column).Text, 9, 9, StringAlignment.Center)
            Next upcard
            textString += Chr(13)
        Next card
        textString += Chr(13)
        textString += Chr(13)

        TextForm.RichTextBoxTextForm.Text = textString
        TextForm.Show()
    End Sub

    Private Sub PopulateStratTableUpcardLabels()
        PopulateUpcardLabels(HardTDGroupSTab, 56, 24, 28)
        PopulateUpcardLabels(SoftTDGroupSTab, 56, 24, 28)
        PopulateUpcardLabels(SoftCDGroupSTab, 56, 24, 28)
        PopulateUpcardLabels(PairCDGroupSTab, 56, 24, 28)
        PopulateUpcardLabels(HardCDTabSTab, 88, 16, 28)
        PopulateUpcardLabels(HardCDTabSTab, 504, 16, 28)
    End Sub

    Private Sub PopulateStratHardTDLabels()
        Dim total As Integer

        For total = 4 To 21
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(40, 22)
            label.Location = New System.Drawing.Point(8, 48 + (total - 4) * 24)
            label.Text = total
            label.Index = total - 4
            HardTDGroupSTab.Controls.Add(label)
        Next
    End Sub

    Private Sub PopulateStratSoftTDLabels()
        Dim total As Integer

        For total = 12 To 21
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(40, 22)
            label.Location = New System.Drawing.Point(8, 48 + (total - 12) * 24)
            label.Text = total
            label.Index = total - 12
            SoftTDGroupSTab.Controls.Add(label)
        Next
    End Sub

    Private Sub PopulateStratHardCDLabels()
        Dim row As Integer
        Dim total As Integer
        Dim card As Integer
        Dim card2 As Integer

        row = 0
        For total = 5 To 19
            If total < 13 Then
                card = 2
            Else
                card = total - 10
            End If
            card2 = total - card
            Do
                Dim label As New IndexedLabel
                label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                label.Size = New System.Drawing.Size(64, 20)
                If card2 = 10 Then
                    label.Text = CStr(card) + ", T  (" + CStr(card + card2) + ")"
                ElseIf (card + card2) < 10 Then
                    label.Text = CStr(card) + ", " + CStr(card2) + "   (" + CStr(card + card2) + ")"
                Else
                    label.Text = CStr(card) + ", " + CStr(card2) + "  (" + CStr(card + card2) + ")"
                End If

                If row < 18 Then
                    label.Location = New System.Drawing.Point(8, 40 + row * 24)
                Else
                    label.Location = New System.Drawing.Point(424, 40 + (row - 18) * 24)
                End If

                label.Index = row
                HardCDTabSTab.Controls.Add(label)

                row = row + 1
                card = card + 1
                card2 = card2 - 1
            Loop Until (card >= card2)
        Next total
    End Sub

    Private Sub PopulateStratSoftCDLabels()
        Dim card As Integer

        For card = 2 To 10
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(32, 20)
            If card = 10 Then
                label.Text = "A, T"
            Else
                label.Text = "A, " + CStr(card)
            End If

            label.Location = New System.Drawing.Point(8, 48 + (card - 2) * 24)
            label.Index = card - 2
            SoftCDGroupSTab.Controls.Add(label)
        Next card
    End Sub

    Private Sub PopulateStratPairCDLabels()
        Dim card As Integer

        For card = 1 To 10
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(32, 20)
            If card = 1 Then
                label.Text = "A, A"
            ElseIf card = 10 Then
                label.Text = "T, T"
            Else
                label.Text = CStr(card) + ", " + CStr(card)
            End If

            label.Location = New System.Drawing.Point(8, 48 + (card - 1) * 24)
            label.Index = card - 1
            PairCDGroupSTab.Controls.Add(label)
        Next card
    End Sub

    Private Sub PopulateStratHardTDTable()
        Dim total As Integer
        Dim upcard As Integer

        For total = 4 To 21
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(28, 20)
                box.ReadOnly = True

                box.Hand.Total = total
                box.Hand.Soft = False
                box.Hand.NumCards = 0
                box.Index = 0
                box.Text = Constants.StratShortText(box.Index)
                box.BackColor = Results.ColorTable.C(box.Index)
                box.Index2 = upcard + 1

                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(56 + 9 * 28, 48 + (total - 4) * 24)
                Else
                    box.Location = New System.Drawing.Point(56 + (upcard - 1) * 28, 48 + (total - 4) * 24)
                End If

                HardTDGroupSTab.Controls.Add(box)
                HardTDStratTableArray(total - 4, upcard) = box

                'Add Handler to the general handler
                AddHandler box.Click, AddressOf HardTDStratTableArrayHandler_Click
            Next upcard
        Next total
    End Sub

    Private Sub PopulateStratSoftPairTDTables()
        Dim total As Integer
        Dim upcard As Integer

        'Populate Soft TD Group
        For total = 12 To 21
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(28, 20)
                box.ReadOnly = True

                box.Hand.Total = total
                box.Hand.Soft = True
                box.Hand.NumCards = 0
                box.Index = 0
                box.Text = Constants.StratShortText(box.Index)
                box.BackColor = Results.ColorTable.C(box.Index)
                box.Index2 = upcard + 1

                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(56 + 9 * 28, 48 + (total - 12) * 24)
                Else
                    box.Location = New System.Drawing.Point(56 + (upcard - 1) * 28, 48 + (total - 12) * 24)
                End If

                SoftTDGroupSTab.Controls.Add(box)
                SoftTDStratTableArray(total - 12, upcard) = box

                'Add Handler to the general handler
                AddHandler box.Click, AddressOf SoftTDStratTableArrayHandler_Click
            Next upcard
        Next total
    End Sub

    Private Sub PopulateStratHardCDTable()
        Dim row As Integer
        Dim column As Integer
        Dim total As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim card2 As Integer

        row = 0
        column = 88
        For total = 5 To 19
            If total < 13 Then
                card = 2
            Else
                card = total - 10
            End If
            card2 = total - card
            Do
                For upcard = 0 To 9
                    Dim box As New IndexedTextBox

                    If row = 18 Then
                        row -= 18
                        column = 504
                    End If
                    box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                    box.Size = New System.Drawing.Size(28, 20)
                    box.ReadOnly = True

                    box.Hand.Cards(card) = 1
                    box.Hand.Cards(card2) = 1
                    box.Hand.UpdateTotal()
                    box.Index = 0
                    box.Text = Constants.StratShortText(box.Index)
                    box.BackColor = Results.ColorTable.C(box.Index)
                    box.Index2 = upcard + 1

                    If upcard = 0 Then
                        box.Location = New System.Drawing.Point(column + 9 * 28, 40 + row * 24)
                    Else
                        box.Location = New System.Drawing.Point(column + (upcard - 1) * 28, 40 + row * 24)
                    End If

                    HardCDTabSTab.Controls.Add(box)
                    If column = 88 Then
                        HardCDStratTableArray(row, upcard) = box
                    Else
                        HardCDStratTableArray(row + 18, upcard) = box
                    End If

                    'Add Handler to the general handler
                    AddHandler box.Click, AddressOf HardCDStratTableArrayHandler_Click
                Next upcard
                row = row + 1
                card = card + 1
                card2 = card2 - 1
            Loop Until (card >= card2)
        Next
    End Sub

    Private Sub PopulateStratSoftPairCDTables()
        Dim total As Integer
        Dim upcard As Integer

        'Populate Soft CD Group
        For total = 13 To 21
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(28, 20)
                box.ReadOnly = True
                box.Text = Constants.StratShortText(box.Index)
                box.BackColor = Results.ColorTable.C(box.Index)

                box.Hand.Cards(1) = 1
                box.Hand.Cards(total - 11) = 1
                box.Hand.UpdateTotal()
                box.Index = 0
                box.Index2 = upcard + 1

                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(56 + 9 * 28, 48 + (total - 13) * 24)
                Else
                    box.Location = New System.Drawing.Point(56 + (upcard - 1) * 28, 48 + (total - 13) * 24)
                End If

                SoftCDGroupSTab.Controls.Add(box)
                SoftCDStratTableArray(total - 13, upcard) = box

                'Add Handler to the general handler
                AddHandler box.Click, AddressOf SoftCDStratTableArrayHandler_Click
            Next upcard
        Next total

        'Populate Pair CD Group
        For total = 1 To 10
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(28, 20)
                box.ReadOnly = True

                box.Hand.Cards(total) = 2
                box.Hand.UpdateTotal()
                box.Index = 0
                box.Text = Constants.StratShortText(box.Index)
                box.BackColor = Results.ColorTable.C(box.Index)
                box.Index2 = upcard + 1

                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(56 + 9 * 28, 48 + (total - 1) * 24)
                Else
                    box.Location = New System.Drawing.Point(56 + (upcard - 1) * 28, 48 + (total - 1) * 24)
                End If

                PairCDGroupSTab.Controls.Add(box)
                PairCDStratTableArray(total - 1, upcard) = box

                'Add Handler to the general handler
                AddHandler box.Click, AddressOf PairCDStratTableArrayHandler_Click
            Next upcard
        Next total
    End Sub

    Private Sub TDButtonTDHardSoftTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDButtonTDHardSoftTab.CheckedChanged
        If TDButtonTDHardSoftTab.Checked Then
            TDButtonCDHardTab.Checked = True
            TDButtonCDSoftPairsTab.Checked = True

            If Results.TD.ComputeStrat Then
                LoadFormStrategyTables()
            Else
                ClearFormStrategyTables()
            End If
        End If
    End Sub

    Private Sub TDButtonCDHardTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDButtonCDHardTab.CheckedChanged
        If TDButtonCDHardTab.Checked Then
            TDButtonTDHardSoftTab.Checked = True
            TDButtonCDSoftPairsTab.Checked = True

            If Results.TD.ComputeStrat Then
                LoadFormStrategyTables()
            Else
                ClearFormStrategyTables()
            End If
        End If
    End Sub

    Private Sub TDButtonCDSoftPairsTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDButtonCDSoftPairsTab.CheckedChanged
        If TDButtonCDSoftPairsTab.Checked Then
            TDButtonTDHardSoftTab.Checked = True
            TDButtonCDHardTab.Checked = True

            If Results.TD.ComputeStrat Then
                LoadFormStrategyTables()
            Else
                ClearFormStrategyTables()
            End If
        End If
    End Sub

    Private Sub TCButtonTDHardSoftTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TCButtonTDHardSoftTab.CheckedChanged
        If TCButtonTDHardSoftTab.Checked Then
            TCButtonCDHardTab.Checked = True
            TCButtonCDSoftPairsTab.Checked = True

            LoadFormStrategyTables()
        End If
    End Sub

    Private Sub TCButtonCDHardTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TCButtonCDHardTab.CheckedChanged
        If TCButtonCDHardTab.Checked Then
            TCButtonTDHardSoftTab.Checked = True
            TCButtonCDSoftPairsTab.Checked = True

            If Results.TC.ComputeStrat Then
                LoadFormStrategyTables()
            Else
                ClearFormStrategyTables()
            End If
        End If
    End Sub

    Private Sub TCButtonCDSoftPairsTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TCButtonCDSoftPairsTab.CheckedChanged
        If TCButtonCDSoftPairsTab.Checked Then
            TCButtonTDHardSoftTab.Checked = True
            TCButtonCDHardTab.Checked = True

            If Results.TC.ComputeStrat Then
                LoadFormStrategyTables()
            Else
                ClearFormStrategyTables()
            End If
        End If
    End Sub

    Private Sub CDButtonTDHardSoftTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDButtonTDHardSoftTab.CheckedChanged
        If CDButtonTDHardSoftTab.Checked Then
            CDButtonCDHardTab.Checked = True
            CDButtonCDSoftPairsTab.Checked = True

            LoadFormStrategyTables()
        End If
    End Sub

    Private Sub CDButtonCDHardTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDButtonCDHardTab.CheckedChanged
        If CDButtonCDHardTab.Checked Then
            CDButtonTDHardSoftTab.Checked = True
            CDButtonCDSoftPairsTab.Checked = True

            LoadFormStrategyTables()
        End If
    End Sub

    Private Sub CDButtonCDSoftPairsTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDButtonCDSoftPairsTab.CheckedChanged
        If CDButtonCDSoftPairsTab.Checked Then
            CDButtonTDHardSoftTab.Checked = True
            CDButtonCDHardTab.Checked = True

            LoadFormStrategyTables()
        End If
    End Sub

    Private Sub ForcedButtonTDHardSoftTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedButtonTDHardSoftTab.CheckedChanged
        If ForcedButtonTDHardSoftTab.Checked Then
            ForcedButtonCDHardTab.Checked = True
            ForcedButtonCDSoftPairsTab.Checked = True

            If Results.Forced.ComputeStrat Then
                LoadFormStrategyTables()
            Else
                ClearFormStrategyTables()
            End If
        End If
    End Sub

    Private Sub ForcedButtonCDHardTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedButtonCDHardTab.CheckedChanged
        If ForcedButtonCDHardTab.Checked Then
            ForcedButtonTDHardSoftTab.Checked = True
            ForcedButtonCDSoftPairsTab.Checked = True

            If Results.Forced.ComputeStrat Then
                LoadFormStrategyTables()
            Else
                ClearFormStrategyTables()
            End If
        End If
    End Sub

    Private Sub ForcedButtonCDSoftPairsTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedButtonCDSoftPairsTab.CheckedChanged
        If ForcedButtonCDSoftPairsTab.Checked Then
            ForcedButtonTDHardSoftTab.Checked = True
            ForcedButtonCDHardTab.Checked = True

            If Results.Forced.ComputeStrat Then
                LoadFormStrategyTables()
            Else
                ClearFormStrategyTables()
            End If
        End If
    End Sub

    Private Sub TextButton1SummTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextButton1SummTab.Click
        PrintStrategyTextBox()
    End Sub

    Private Sub TextButton2SummTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextButton2SummTab.Click
        PrintStrategyTextBox()
    End Sub

    Private Sub TextButton3STab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextButton3STab.Click
        PrintStrategyTextBox()
    End Sub

    Private Sub ClearFormStrategyTables()
        Dim total As Integer
        Dim row As Integer
        Dim upcard As Integer

        'Clear Hard TD Group
        For total = 4 To 21
            For upcard = 0 To 9
                HardTDStratTableArray(total - 4, upcard).Text = Constants.StratShortText(0)
                HardTDStratTableArray(total - 4, upcard).BackColor = Results.ColorTable.C(0)
                HardTDStratTableArray(total - 4, upcard).Index = 0
            Next upcard
        Next total

        'Clear Soft TD Group
        For total = 12 To 21
            For upcard = 0 To 9
                SoftTDStratTableArray(total - 12, upcard).Text = Constants.StratShortText(0)
                SoftTDStratTableArray(total - 12, upcard).BackColor = Results.ColorTable.C(0)
                SoftTDStratTableArray(total - 12, upcard).Index = 0
            Next upcard
        Next total

        'Clear Hard CD Group
        For row = 0 To 35
            For upcard = 0 To 9
                HardCDStratTableArray(row, upcard).Text = Constants.StratShortText(0)
                HardCDStratTableArray(row, upcard).BackColor = Results.ColorTable.C(0)
                HardCDStratTableArray(row, upcard).Index = 0
            Next upcard
        Next row

        'Clear Soft CD Group
        For total = 13 To 21
            For upcard = 0 To 9
                SoftCDStratTableArray(total - 13, upcard).Text = Constants.StratShortText(0)
                SoftCDStratTableArray(total - 13, upcard).BackColor = Results.ColorTable.C(0)
                SoftCDStratTableArray(total - 13, upcard).Index = 0
            Next upcard
        Next total

        'Clear Pairs Tables
        For total = 1 To 10
            For upcard = 0 To 9
                PairCDStratTableArray(total - 1, upcard).Text = Constants.StratShortText(0)
                PairCDStratTableArray(total - 1, upcard).BackColor = Results.ColorTable.C(0)
                PairCDStratTableArray(total - 1, upcard).Index = 0
            Next upcard
        Next total
    End Sub

    Private Sub HardTDStratTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardTDStratTableArrayHandler.Click
        Dim x As Integer
        Dim y As Integer

        PreviousObject = sender
        PreviousGroup = HardTDGroupSTab
        HardSoftTDTabSTab.Controls.Add(EVsBoxSTab)

        If TDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TD, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf TCButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TC, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf CDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.Opt, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        Else
            LoadEVsBox(Results.Forced, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        End If

        x = HardTDGroupSTab.Location.X + DirectCast(sender, IndexedTextBox).Location.X + 24
        y = HardTDGroupSTab.Location.Y + DirectCast(sender, IndexedTextBox).Location.Y - 4
        If x + EVsBoxSTab.Size.Width > StratTabControlSTab.Size.Width Then
            x -= (EVsBoxSTab.Size.Width + DirectCast(sender, IndexedTextBox).Size.Width)
        End If
        If y + EVsBoxSTab.Size.Height > StratTabControlSTab.Size.Height - 24 Then
            y -= (EVsBoxSTab.Size.Height - DirectCast(sender, IndexedTextBox).Size.Height - 4)
        End If
        EVsBoxSTab.Location = New System.Drawing.Point(x, y)

        EVsBoxSTab.Visible = True
        EVsBoxSTab.BringToFront()
        EVsBoxSTab.Focus()
    End Sub

    Private Sub SoftTDStratTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftTDStratTableArrayHandler.Click
        Dim x As Integer
        Dim y As Integer

        PreviousObject = sender
        PreviousGroup = SoftTDGroupSTab
        HardSoftTDTabSTab.Controls.Add(EVsBoxSTab)

        If TDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TD, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf TCButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TC, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf CDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.Opt, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        Else
            LoadEVsBox(Results.Forced, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        End If

        x = SoftTDGroupSTab.Location.X + DirectCast(sender, IndexedTextBox).Location.X + 24
        y = SoftTDGroupSTab.Location.Y + DirectCast(sender, IndexedTextBox).Location.Y - 4
        If x + EVsBoxSTab.Size.Width > StratTabControlSTab.Size.Width Then
            x -= (EVsBoxSTab.Size.Width + DirectCast(sender, IndexedTextBox).Size.Width)
        End If
        If y + EVsBoxSTab.Size.Height > StratTabControlSTab.Size.Height - 16 Then
            y -= (EVsBoxSTab.Size.Height - DirectCast(sender, IndexedTextBox).Size.Height - 4)
        End If
        EVsBoxSTab.Location = New System.Drawing.Point(x, y)

        EVsBoxSTab.Visible = True
        EVsBoxSTab.BringToFront()
        EVsBoxSTab.Focus()
    End Sub

    Private Sub HardCDStratTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardCDStratTableArrayHandler.Click
        Dim x As Integer
        Dim y As Integer

        PreviousObject = sender
        PreviousGroup = HardCDTabSTab
        HardCDTabSTab.Controls.Add(EVsBoxSTab)

        If TDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TD, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf TCButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TC, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf CDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.Opt, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        Else
            LoadEVsBox(Results.Forced, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        End If

        x = DirectCast(sender, IndexedTextBox).Location.X + 24
        y = DirectCast(sender, IndexedTextBox).Location.Y - 4
        If x + EVsBoxSTab.Size.Width > StratTabControlSTab.Size.Width Then
            x -= (EVsBoxSTab.Size.Width + DirectCast(sender, IndexedTextBox).Size.Width)
        End If
        If y + EVsBoxSTab.Size.Height > StratTabControlSTab.Size.Height - 16 Then
            y -= (EVsBoxSTab.Size.Height - DirectCast(sender, IndexedTextBox).Size.Height - 4)
        End If
        EVsBoxSTab.Location = New System.Drawing.Point(x, y)

        EVsBoxSTab.Visible = True
        EVsBoxSTab.BringToFront()
        EVsBoxSTab.Focus()
    End Sub

    Private Sub SoftCDStratTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftCDStratTableArrayHandler.Click
        Dim x As Integer
        Dim y As Integer

        PreviousObject = sender
        PreviousGroup = SoftCDGroupSTab
        SoftPairsCDTabSTab.Controls.Add(EVsBoxSTab)

        If TDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TD, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf TCButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TC, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf CDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.Opt, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        Else
            LoadEVsBox(Results.Forced, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        End If

        x = SoftCDGroupSTab.Location.X + DirectCast(sender, IndexedTextBox).Location.X + 24
        y = SoftCDGroupSTab.Location.Y + DirectCast(sender, IndexedTextBox).Location.Y - 4
        If x + EVsBoxSTab.Size.Width > StratTabControlSTab.Size.Width Then
            x -= (EVsBoxSTab.Size.Width + DirectCast(sender, IndexedTextBox).Size.Width)
        End If
        If y + EVsBoxSTab.Size.Height > StratTabControlSTab.Size.Height - 16 Then
            y -= (EVsBoxSTab.Size.Height - DirectCast(sender, IndexedTextBox).Size.Height - 4)
        End If
        EVsBoxSTab.Location = New System.Drawing.Point(x, y)

        EVsBoxSTab.Visible = True
        EVsBoxSTab.BringToFront()
        EVsBoxSTab.Focus()
    End Sub

    Private Sub PairCDStratTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PairCDStratTableArrayHandler.Click
        Dim x As Integer
        Dim y As Integer

        PreviousObject = sender
        PreviousGroup = PairCDGroupSTab
        SoftPairsCDTabSTab.Controls.Add(EVsBoxSTab)

        If TDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TD, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf TCButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.TC, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        ElseIf CDButtonTDHardSoftTab.Checked Then
            LoadEVsBox(Results.Opt, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        Else
            LoadEVsBox(Results.Forced, DirectCast(PreviousObject, IndexedTextBox).Hand, DirectCast(PreviousObject, IndexedTextBox).Index2)
        End If

        x = PairCDGroupSTab.Location.X + DirectCast(sender, IndexedTextBox).Location.X + 24
        y = PairCDGroupSTab.Location.Y + DirectCast(sender, IndexedTextBox).Location.Y - 4
        If x + EVsBoxSTab.Size.Width > StratTabControlSTab.Size.Width Then
            x -= (EVsBoxSTab.Size.Width + DirectCast(sender, IndexedTextBox).Size.Width)
        End If
        If y + EVsBoxSTab.Size.Height > StratTabControlSTab.Size.Height - 16 Then
            y -= (EVsBoxSTab.Size.Height - DirectCast(sender, IndexedTextBox).Size.Height - 4)
        End If
        EVsBoxSTab.Location = New System.Drawing.Point(x, y)

        EVsBoxSTab.Visible = True
        EVsBoxSTab.BringToFront()
        EVsBoxSTab.Focus()
    End Sub

    Private Sub StratTabControlSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StratTabControlSTab.SelectedIndexChanged
        EVsBoxSTab.Visible = False
        If Not PreviousGroup Is Nothing Then
            PreviousGroup.Focus()
        End If
    End Sub

    Private Sub ResultsFormTabControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResultsFormTabControl.SelectedIndexChanged
        EVsBoxSTab.Visible = False
        If Not PreviousGroup Is Nothing Then
            PreviousGroup.Focus()
        End If
    End Sub

    Private Sub StratGroupTDHardSoftTab_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StratGroupTDHardSoftTab.Enter
        EVsBoxSTab.Visible = False
        If Not PreviousGroup Is Nothing Then
            PreviousGroup.Focus()
        End If
    End Sub


    Private Sub StratGroupTDSoftPairsTab_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StratGroupTDSoftPairsTab.Enter
        EVsBoxSTab.Visible = False
        If Not PreviousGroup Is Nothing Then
            PreviousGroup.Focus()
        End If
    End Sub

    Private Sub StratGroupCDHardTab_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StratGroupCDHardTab.Enter
        EVsBoxSTab.Visible = False
        If Not PreviousGroup Is Nothing Then
            PreviousGroup.Focus()
        End If
    End Sub

    Private Sub HardSoftTDTabSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardSoftTDTabSTab.Click
        EVsBoxSTab.Visible = False
        If Not PreviousGroup Is Nothing Then
            PreviousGroup.Focus()
        End If
    End Sub

    Private Sub SoftPairsCDTabSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftPairsCDTabSTab.Click
        EVsBoxSTab.Visible = False
        If Not PreviousGroup Is Nothing Then
            PreviousGroup.Focus()
        End If
    End Sub

    Private Sub HardCDTabSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardCDTabSTab.Click
        EVsBoxSTab.Visible = False
        If Not PreviousGroup Is Nothing Then
            PreviousGroup.Focus()
        End If
    End Sub

#End Region

#Region " Suited Bonuses "

    Private Sub PopulateSuitedHandTable()
        Dim row As Integer
        Dim suit As Integer

        ProbDetailsBoxSTab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        NonSuitedProbDetailsBoxSTab.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))

        For row = 0 To 5
            For suit = 0 To 3
                Dim box As New IndexedTextBox

                'Populate the Upcard Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                If row = 0 Then
                    box.Size = New System.Drawing.Size(40, 20)
                    box.Location = New System.Drawing.Point(300 + suit * 128, 120 + row * 28)
                Else
                    box.Size = New System.Drawing.Size(96, 20)
                    box.Location = New System.Drawing.Point(272 + suit * 128, 120 + row * 28)
                End If
                If row = 1 Then
                    box.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
                End If

                HandDetailsGroupSTab.Controls.Add(box)
                SuitedHandEVsArray(row, suit) = box
            Next suit
        Next row

    End Sub

    Private Sub LoadHandSForm()
        Dim index As Integer
        Dim upcard As Integer
        Dim suit As Integer
        Dim hand As New BJCAHandClass

        upcard = UCComboBoxSTab.SelectedIndex + 1

        If HandListBoxSTab.SelectedIndex >= 0 Then
            hand = GetStringHand(HandListBoxSTab.Items(HandListBoxSTab.SelectedIndex))
            For index = 1 To Results.NumPHands
                If hand.SameAs(Results.PlayerHands(index).Hand) Then
                    Exit For
                End If
            Next index

            HandNameBoxSTab.Text = GetHandString(Results.PlayerHands(index).Hand)
            TotalDetailsBoxSTab.Text = CStr(Results.PlayerHands(index).Hand.Total)
            SoftDetailsCheckSTab.Checked = Results.PlayerHands(index).Hand.Soft
            NCardsDetailsBoxSTab.Text = CStr(Results.PlayerHands(index).Hand.NumCards)
            UCDetailsBoxSTab.Text = CStr(upcard)

            If Results.PlayerHands(index).HandEVs.Prob(upcard) = 0 Or Not Results.UCAllowed(upcard) Then
                '                ClearHandSForm(True)
                FillNumberTextBox(NetEVBoxSTab, 0, 13, False)
                FillNumberTextBox(NetBJEVBoxSTab, 0, 13, False)
                NetEVBoxSTab.Text = "N/A"
                FillNumberTextBox(ProbDetailsBoxSTab, Results.PlayerHands(index).HandEVs.Prob(upcard), 13, False, , True)
            Else
                FillNumberTextBox(ProbDetailsBoxSTab, Results.PlayerHands(index).HandEVs.Prob(upcard), 13, False, , True)
                FillNumberTextBox(NetEVBoxSTab, Results.PlayerHands(index).SuitedBonusEVs.SuitedNetEV(upcard), 13, False)
                FillNumberTextBox(NetBJEVBoxSTab, Results.PlayerHands(index).SuitedBonusEVs.SuitedNetBJEV(upcard), 13, False)
                FillStratTextBox(CDStratBoxSTab, Results.Opt.HandEVs(index).EVs.Strat(upcard), False, FormRules.ColorTable)
                FillNumberTextBox(NonSuitedProbDetailsBoxSTab, (Results.PlayerHands(index).HandEVs.Prob(upcard) - Results.PlayerHands(index).HandEVs.SumSuited(upcard)), 13, False, , True)
                FillNumberTextBox(StandBoxSTab, Results.PlayerHands(index).HandEVs.StandEV(upcard), 13, False)
                FillNumberTextBox(CDHitBoxSTab, Results.Opt.HandEVs(index).EVs.HitEV(upcard), 13, False)
                FillNumberTextBox(DoubleBoxSTab, Results.PlayerHands(index).HandEVs.DEV(upcard), 13, False)
                FillNumberTextBox(SurrenderBoxSTab, Results.PlayerHands(index).HandEVs.SurrEV(upcard), 13, False)
                FillNumberTextBox(CDSplitBoxSTab, Results.Opt.HandEVs(index).SplitEV(upcard), 13, False)
                FillNumberTextBox(BJStandBoxSTab, Results.PlayerHands(index).HandEVs.BJStandEV(upcard), 13, False)
                FillNumberTextBox(BonusBoxSTab, Results.PlayerHands(index).HandEVs.BonusEV(upcard), 13, False)
                If ((upcard = 1 And Results.CheckAce) Or (upcard = 10 And Results.CheckTen)) And Results.PlayerHands(index).HandEVs.BJStandEV(upcard) = 0 Then
                    BJStandBoxSTab.Text = "0"
                End If

                For suit = 0 To 3
                    FillStratTextBox(SuitedHandEVsArray(0, suit), Results.PlayerHands(index).SuitedBonusEVs.SuitedStrat(upcard, suit), False, FormRules.ColorTable)
                    FillNumberTextBox(SuitedHandEVsArray(1, suit), Results.PlayerHands(index).HandEVs.ProbSuited(upcard, suit), 13, False, , True)
                    FillNumberTextBox(SuitedHandEVsArray(2, suit), Results.PlayerHands(index).SuitedBonusEVs.SuitedStandEV(upcard, suit), 13, False)
                    FillNumberTextBox(SuitedHandEVsArray(3, suit), Results.PlayerHands(index).SuitedBonusEVs.SuitedHitEV(upcard, suit), 13, False)
                    FillNumberTextBox(SuitedHandEVsArray(4, suit), Results.PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, suit), 13, False)
                    If (upcard = 1 Or upcard = 10) And Results.PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, suit) = 0 Then
                        SuitedHandEVsArray(4, suit).Text = "0"
                    End If
                    FillNumberTextBox(SuitedHandEVsArray(5, suit), Results.PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, suit), 13, False)
                Next suit
            End If
        End If

    End Sub

    Private Sub LoadSuitedHandListSForm()
        Dim index As Integer
        Dim upcard As Integer

        upcard = UCComboBoxSTab.SelectedIndex + 1

        'First empty the box
        For index = HandListBoxSTab.Items.Count - 1 To 0 Step -1
            HandListBoxSTab.Items.RemoveAt(index)
        Next index

        'Then load the box
        For index = 1 To Results.NSuitedHands
            HandListBoxSTab.Items.Add(GetHandString(Results.PlayerHands(Results.SuitedHandsList(index)).Hand))
        Next index
        ListSizeBoxSTab.Text = HandListBoxSTab.Items.Count
        '        ClearHandSForm(False)
    End Sub

    Private Sub HandListBoxSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandListBoxSTab.SelectedIndexChanged
        LoadHandSForm()
    End Sub

    Private Sub UCComboBoxSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UCComboBoxSTab.SelectedIndexChanged
        LoadHandSForm()
    End Sub

#End Region

#End Region

#Region " Hand Analysis Tab "

    Private Sub ClearHandHAForm(ByVal leaveName As Boolean)
        If Not leaveName Then
            HandNameBoxHATab.Text = ""
            TotalDetailsBoxHATab.Text = ""
            SoftDetailsCheckHATab.Checked = False
            NCardsDetailsBoxHATab.Text = ""
            UCDetailsBoxHATab.Text = ""
        End If

        FillStratTextBox(TDStratBoxHATab, BJCAGlobalsClass.Strat.None, False, FormRules.ColorTable)
        FillNumberTextBox(TDMultiplierBoxHATab, 0, 0, False, Results.TD.ComputeStrat, True)

        FillStratTextBox(TCStratBoxHATab, BJCAGlobalsClass.Strat.None, False, FormRules.ColorTable)
        FillNumberTextBox(TCMultiplierBoxHATab, 0, 0, False, Results.TC.ComputeStrat, True)

        FillStratTextBox(CDStratBoxHATab, BJCAGlobalsClass.Strat.None, False, FormRules.ColorTable)
        FillNumberTextBox(CDMultiplierBoxHATab, 0, 0, False, , True)

        FillStratTextBox(ForcedStratBoxHATab, BJCAGlobalsClass.Strat.None, False, FormRules.ColorTable)
        FillNumberTextBox(ForcedMultiplierBoxHATab, 0, 0, False, Results.Forced.ComputeStrat, True)
        ForcedPreCheckBoxHATab.Checked = False
        ForcedPostCheckBoxHATab.Checked = False

        ProbBoxHATab.Text = ""
        FillNumberTextBox(StandBoxHATab, 0, 13, False)
        FillNumberTextBox(DoubleBoxHATab, 0, 13, False)
        FillNumberTextBox(SurrenderBoxHATab, 0, 13, False)
        FillNumberTextBox(BJStandBoxHATab, 0, 13, False)
        FillNumberTextBox(BonusBoxHATab, 0, 13, False)

        FillNumberTextBox(TDHitBoxHATab, 0, 13, False, Results.TD.ComputeStrat)
        FillNumberTextBox(TDSplitBoxHATab, 0, 13, False, Results.TD.ComputeStrat)
        FillNumberTextBox(TCHitBoxHATab, 0, 13, False, Results.TC.ComputeStrat)
        FillNumberTextBox(TCSplitBoxHATab, 0, 13, False, Results.TC.ComputeStrat)
        FillNumberTextBox(CDHitBoxHATab, 0, 13, False)
        FillNumberTextBox(CDSplitBoxHATab, 0, 13, False)
        FillNumberTextBox(ForcedHitBoxHATab, 0, 13, False, Results.Forced.ComputeStrat)
        FillNumberTextBox(ForcedSplitBoxHATab, 0, 13, False, Results.Forced.ComputeStrat)

    End Sub

    Private Sub LoadHandHAForm()
        Dim index As Integer
        Dim upcard As Integer
        Dim hand As New BJCAHandClass

        upcard = UCComboBoxHATab.SelectedIndex + 1

        If HandListBoxHATab.SelectedIndex >= 0 Then
            hand = GetStringHand(HandListBoxHATab.Items(HandListBoxHATab.SelectedIndex))
            For index = 1 To Results.NumPHands
                If hand.SameAs(Results.PlayerHands(index).Hand) Then
                    Exit For
                End If
            Next index

            HandNameBoxHATab.Text = GetHandString(Results.PlayerHands(index).Hand)
            TotalDetailsBoxHATab.Text = CStr(Results.PlayerHands(index).Hand.Total)
            SoftDetailsCheckHATab.Checked = Results.PlayerHands(index).Hand.Soft
            NCardsDetailsBoxHATab.Text = CStr(Results.PlayerHands(index).Hand.NumCards)
            UCDetailsBoxHATab.Text = CStr(upcard)

            If Results.PlayerHands(index).HandEVs.Prob(upcard) = 0 Or Not Results.UCAllowed(upcard) Then
                ClearHandHAForm(True)
                ProbBoxHATab.Text = CStr(Results.PlayerHands(index).HandEVs.Prob(upcard))
            Else
                If Results.TD.ComputeStrat Then
                    FillStratTextBox(TDStratBoxHATab, Results.TD.HandEVs(index).EVs.Strat(upcard), False, FormRules.ColorTable)
                    FillNumberTextBox(TDMultiplierBoxHATab, Results.TD.HandEVs(index).Multiplier(upcard), 0, False, Results.TD.ComputeStrat, True)
                Else
                    FillStratTextBox(TDStratBoxHATab, 0, False, FormRules.ColorTable)
                    FillNumberTextBox(TDMultiplierBoxHATab, 0, 0, False, Results.TD.ComputeStrat, True)
                End If

                If Results.TC.ComputeStrat Then
                    FillStratTextBox(TCStratBoxHATab, Results.TC.HandEVs(index).EVs.Strat(upcard), False, FormRules.ColorTable)
                    FillNumberTextBox(TCMultiplierBoxHATab, Results.TC.HandEVs(index).Multiplier(upcard), 0, False, Results.TC.ComputeStrat, True)
                Else
                    FillStratTextBox(TCStratBoxHATab, 0, False, FormRules.ColorTable)
                    FillNumberTextBox(TCMultiplierBoxHATab, 0, 0, False, Results.TC.ComputeStrat, True)
                End If

                FillStratTextBox(CDStratBoxHATab, Results.Opt.HandEVs(index).EVs.Strat(upcard), False, FormRules.ColorTable)
                FillNumberTextBox(CDMultiplierBoxHATab, Results.Opt.HandEVs(index).Multiplier(upcard), 0, False, , True)

                If Results.Forced.ComputeStrat Then
                    FillStratTextBox(ForcedStratBoxHATab, Results.Forced.HandEVs(index).EVs.Strat(upcard), False, FormRules.ColorTable)
                    FillNumberTextBox(ForcedMultiplierBoxHATab, Results.Forced.HandEVs(index).Multiplier(upcard), 0, False, Results.Forced.ComputeStrat, True)
                Else
                    FillStratTextBox(ForcedStratBoxHATab, 0, False, FormRules.ColorTable)
                    FillNumberTextBox(ForcedMultiplierBoxHATab, 0, 0, False, Results.Forced.ComputeStrat, True)
                End If

                ProbBoxHATab.Text = CStr(Results.PlayerHands(index).HandEVs.Prob(upcard))
                FillNumberTextBox(StandBoxHATab, Results.PlayerHands(index).HandEVs.StandEV(upcard), 13, False)
                FillNumberTextBox(DoubleBoxHATab, Results.PlayerHands(index).HandEVs.DEV(upcard), 13, False)
                FillNumberTextBox(SurrenderBoxHATab, Results.PlayerHands(index).HandEVs.SurrEV(upcard), 13, False)
                FillNumberTextBox(BJStandBoxHATab, Results.PlayerHands(index).HandEVs.BJStandEV(upcard), 13, False)
                FillNumberTextBox(BonusBoxHATab, Results.PlayerHands(index).HandEVs.BonusEV(upcard), 13, False)
                If ((upcard = 1 And Results.CheckAce) Or (upcard = 10 And Results.CheckTen)) And Results.PlayerHands(index).HandEVs.BJStandEV(upcard) = 0 Then
                    BJStandBoxHATab.Text = "0"
                End If

                If Results.TD.ComputeStrat Then
                    FillNumberTextBox(TDHitBoxHATab, Results.TD.HandEVs(index).EVs.HitEV(upcard), 13, False)
                    FillNumberTextBox(TDSplitBoxHATab, Results.TD.HandEVs(index).SplitEV(upcard), 13, False)
                Else
                    FillNumberTextBox(TDHitBoxHATab, 0, 13, False)
                    FillNumberTextBox(TDSplitBoxHATab, 0, 13, False)
                End If
                If Results.TC.ComputeStrat Then
                    FillNumberTextBox(TCHitBoxHATab, Results.TC.HandEVs(index).EVs.HitEV(upcard), 13, False)
                    FillNumberTextBox(TCSplitBoxHATab, Results.TC.HandEVs(index).SplitEV(upcard), 13, False)
                Else
                    FillNumberTextBox(TCHitBoxHATab, 0, 13, False)
                    FillNumberTextBox(TCSplitBoxHATab, 0, 13, False)
                End If
                FillNumberTextBox(CDHitBoxHATab, Results.Opt.HandEVs(index).EVs.HitEV(upcard), 13, False)
                FillNumberTextBox(CDSplitBoxHATab, Results.Opt.HandEVs(index).SplitEV(upcard), 13, False)
                If Results.Forced.ComputeStrat Then
                    ForcedPreCheckBoxHATab.Checked = Results.Forced.HandEVs(index).PreForced(upcard)
                    ForcedPostCheckBoxHATab.Checked = Results.Forced.HandEVs(index).PostForced(upcard)
                    FillNumberTextBox(ForcedHitBoxHATab, Results.Forced.HandEVs(index).EVs.HitEV(upcard), 13, False)
                    FillNumberTextBox(ForcedSplitBoxHATab, Results.Forced.HandEVs(index).SplitEV(upcard), 13, False)
                Else
                    ForcedPreCheckBoxHATab.Checked = False
                    ForcedPostCheckBoxHATab.Checked = False
                    FillNumberTextBox(ForcedHitBoxHATab, 0, 13, False)
                    FillNumberTextBox(ForcedSplitBoxHATab, 0, 13, False)
                End If
            End If
        End If

    End Sub

    Private Sub LoadHandListHAForm()
        Dim index As Integer
        Dim includedHand As New BJCAHandClass
        Dim total As Integer
        Dim either As Boolean
        Dim soft As Integer
        Dim nCards As Integer
        Dim orMore As Boolean
        Dim orLess As Boolean
        Dim listHand As Boolean
        Dim exactMatch As Boolean
        Dim upcard As Integer

        If TotalComboBoxHATab.SelectedIndex = 0 Then
            total = 0
        Else
            total = TotalComboBoxHATab.SelectedIndex + 3
        End If
        either = EitherCheckHATab.Checked
        soft = SoftOnlyCheckHATab.Checked
        If NCardsComboBoxHATab.SelectedIndex = 0 Then
            nCards = 0
        Else
            nCards = NCardsComboBoxHATab.SelectedIndex + 1
        End If
        upcard = UCComboBoxHATab.SelectedIndex + 1
        orMore = OrMoreCheckHATab.Checked
        orLess = OrLessCheckHATab.Checked
        includedHand = HAHand
        exactMatch = ExactMatchCheckHATab.Checked

        'First empty the box
        For index = HandListBoxHATab.Items.Count - 1 To 0 Step -1
            HandListBoxHATab.Items.RemoveAt(index)
        Next index

        'Then load the box
        For index = 1 To Results.NumPHands
            listHand = False
            If Results.PlayerHands(index).Hand.NumCards > 1 And Results.PlayerHands(index).HandEVs.Prob(upcard) > 0 Then
                If Not exactMatch Then
                    If total = 0 Or Results.PlayerHands(index).Hand.Total = total Then
                        If either Or Results.PlayerHands(index).Hand.Soft = soft Then
                            If nCards = 0 Or (Results.PlayerHands(index).Hand.NumCards = nCards) Or (orMore And Results.PlayerHands(index).Hand.NumCards >= nCards) Or (orLess And Results.PlayerHands(index).Hand.NumCards <= nCards) Then
                                If Results.PlayerHands(index).Hand.Includes(includedHand) Then
                                    listHand = True
                                End If
                            End If
                        End If
                    End If
                Else
                    If HAHand.SameAs(Results.PlayerHands(index).Hand) Then
                        listHand = True
                    End If
                End If
            End If
            If listHand Then
                HandListBoxHATab.Items.Add(GetHandString(Results.PlayerHands(index).Hand))
            End If
        Next index
        ListSizeBoxHATab.Text = HandListBoxHATab.Items.Count
        ClearHandHAForm(False)
    End Sub

    Private Sub TotalComboBoxHATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TotalComboBoxHATab.SelectedIndexChanged
        LoadHandListHAForm()
    End Sub

    Private Sub EitherCheckHATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EitherCheckHATab.CheckedChanged
        If EitherCheckHATab.Checked Then
            HardOnlyCheckHATab.Enabled = False
            SoftOnlyCheckHATab.Enabled = False
            HardOnlyCheckHATab.Checked = True
            SoftOnlyCheckHATab.Checked = False
        Else
            HardOnlyCheckHATab.Enabled = True
            SoftOnlyCheckHATab.Enabled = True
        End If
        LoadHandListHAForm()
    End Sub

    Private Sub HardOnlyCheckHATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardOnlyCheckHATab.CheckedChanged
        If HardOnlyCheckHATab.Checked Then
            SoftOnlyCheckHATab.Checked = False
        Else
            SoftOnlyCheckHATab.Checked = True
        End If
        LoadHandListHAForm()
    End Sub

    Private Sub SoftOnlyCheckHATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftOnlyCheckHATab.CheckedChanged
        If SoftOnlyCheckHATab.Checked Then
            HardOnlyCheckHATab.Checked = False
        Else
            HardOnlyCheckHATab.Checked = True
        End If
        LoadHandListHAForm()
    End Sub

    Private Sub NCardsComboBoxHATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NCardsComboBoxHATab.SelectedIndexChanged
        If NCardsComboBoxHATab.SelectedIndex = 0 Then
            OrMoreCheckHATab.Enabled = False
            OrLessCheckHATab.Enabled = False
            OrMoreCheckHATab.Checked = False
            OrLessCheckHATab.Checked = False
        ElseIf NCardsComboBoxHATab.SelectedIndex = 1 Then
            OrMoreCheckHATab.Enabled = True
            OrLessCheckHATab.Enabled = False
            OrLessCheckHATab.Checked = False
        ElseIf NCardsComboBoxHATab.SelectedIndex = 20 Then
            OrMoreCheckHATab.Enabled = False
            OrLessCheckHATab.Enabled = True
            OrMoreCheckHATab.Checked = False
        Else
            OrMoreCheckHATab.Enabled = True
            OrLessCheckHATab.Enabled = True
        End If
        LoadHandListHAForm()
    End Sub

    Private Sub OrMoreCheckHATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrMoreCheckHATab.CheckedChanged
        If OrMoreCheckHATab.Checked Then
            OrLessCheckHATab.Checked = False
        End If
        LoadHandListHAForm()
    End Sub

    Private Sub OrLessCheckHATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrLessCheckHATab.CheckedChanged
        If OrLessCheckHATab.Checked Then
            OrMoreCheckHATab.Checked = False
        End If
        LoadHandListHAForm()
    End Sub

    Private Sub HandBoxHATab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles HandBoxHATab.Validating
        Dim handValid As Boolean

        handValid = ValidCardString(HandBoxHATab.Text)

        If handValid Then
            HAHand = GetStringHand(HandBoxHATab.Text)
            If HAHand.Total > 21 Then
                handValid = False
            End If
        End If

        If Not handValid Then
            MsgBox("This is not a valid non-busted player hand.", MsgBoxStyle.OKOnly)
            HandBoxHATab.Text = ""
            HAHand.Empty()
            e.Cancel = True
        End If

        If ExactMatchCheckHATab.Checked And (HAHand.NumCards < 2 Or HAHand.Total = 0) Then
            ExactMatchCheckHATab.Checked = False
        End If

        LoadHandListHAForm()
    End Sub

    Private Sub ExactMatchCheckHATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExactMatchCheckHATab.CheckedChanged
        If HAHand.Total = 0 Then
            ExactMatchCheckHATab.Checked = False
        ElseIf HAHand.NumCards < 2 Then
            MsgBox("The hand must have at least two card for an exact match.", MsgBoxStyle.OKOnly)
            ExactMatchCheckHATab.Checked = False
        End If
        LoadHandListHAForm()
        If ExactMatchCheckHATab.Checked Then
            TotalLabelHATab.Enabled = False
            TotalComboBoxHATab.Enabled = False
            SoftLabelHATab.Enabled = False
            EitherCheckHATab.Enabled = False
            HardOnlyCheckHATab.Enabled = False
            SoftOnlyCheckHATab.Enabled = False
            NCardLabelHATab.Enabled = False
            NCardsComboBoxHATab.Enabled = False
            OrMoreCheckHATab.Enabled = False
            OrLessCheckHATab.Enabled = False
        Else
            TotalLabelHATab.Enabled = True
            TotalComboBoxHATab.Enabled = True
            SoftLabelHATab.Enabled = True
            EitherCheckHATab.Enabled = True
            If EitherCheckHATab.Checked Then
                HardOnlyCheckHATab.Enabled = False
                SoftOnlyCheckHATab.Enabled = False
            Else
                HardOnlyCheckHATab.Enabled = True
                SoftOnlyCheckHATab.Enabled = True
            End If
            NCardLabelHATab.Enabled = True
            NCardsComboBoxHATab.Enabled = True
            If NCardsComboBoxHATab.SelectedIndex = 0 Then
                OrMoreCheckHATab.Enabled = False
                OrLessCheckHATab.Enabled = False
                OrMoreCheckHATab.Checked = False
                OrLessCheckHATab.Checked = False
            ElseIf NCardsComboBoxHATab.SelectedIndex = 1 Then
                OrMoreCheckHATab.Enabled = True
                OrLessCheckHATab.Enabled = False
                OrLessCheckHATab.Checked = False
            ElseIf NCardsComboBoxHATab.SelectedIndex = 20 Then
                OrMoreCheckHATab.Enabled = False
                OrLessCheckHATab.Enabled = True
                OrMoreCheckHATab.Checked = False
            Else
                OrMoreCheckHATab.Enabled = True
                OrLessCheckHATab.Enabled = True
            End If
        End If
    End Sub

    Private Sub HandListBoxHATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandListBoxHATab.SelectedIndexChanged
        LoadHandHAForm()
    End Sub

    Private Sub UCComboBoxHATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UCComboBoxHATab.SelectedIndexChanged
        LoadHandHAForm()
    End Sub

#End Region

#Region " Hand Size Analysis Tab "

    Private Sub TotalComboBoxHSATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TotalComboBoxHSATab.SelectedIndexChanged
        If (TotalComboBoxHSATab.SelectedIndex + 4) < 11 Then
            SoftCheckHSATab.Checked = False
        End If
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub SoftCheckHSATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftCheckHSATab.CheckedChanged
        If (TotalComboBoxHSATab.SelectedIndex + 4) < 11 Then
            SoftCheckHSATab.Checked = False
        End If
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub OrMoreCheckHSATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrMoreCheckHSATab.CheckedChanged
        If OrMoreCheckHSATab.Checked Then
            OrLessCheckHSATab.Checked = False
        End If
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub OrLessCheckHSATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrLessCheckHSATab.CheckedChanged
        If OrLessCheckHSATab.Checked Then
            OrMoreCheckHSATab.Checked = False
        End If
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub HandUsedCheckHSATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandUsedCheckHSATab.CheckedChanged
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub TDButtonHSATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDButtonHSATab.CheckedChanged
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub TCButtonHSATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TCButtonHSATab.CheckedChanged
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub CDButtonHSATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDButtonHSATab.CheckedChanged
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub ForcedButtonHSATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedButtonHSATab.CheckedChanged
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub UCComboBoxHSATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UCComboBoxHSATab.SelectedIndexChanged
        LoadHandSizeAnalysisForm()
    End Sub

    Private Sub PopulateHandSizeAnalysisTable()
        Dim ncards As Integer
        Dim column As Integer

        For ncards = 0 To 19
            For column = 0 To 4
                Dim box As New IndexedTextBox

                'Populate the Hand Total Analysis Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                If ncards < 10 Then
                    If column = 0 Then
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Location = New System.Drawing.Point(80, 232 + ncards * 28)
                    Else
                        box.Size = New System.Drawing.Size(64, 20)
                        box.Location = New System.Drawing.Point(134 + (column - 1) * 64, 232 + ncards * 28)
                    End If
                Else
                    If column = 0 Then
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Location = New System.Drawing.Point(488, 232 + (ncards - 10) * 28)
                    Else
                        box.Size = New System.Drawing.Size(64, 20)
                        box.Location = New System.Drawing.Point(544 + (column - 1) * 64, 232 + (ncards - 10) * 28)
                    End If
                End If

                HandSizeAnalysisTab.Controls.Add(box)
                HandSizeAnalysisArray(ncards, column) = box
            Next column
        Next ncards
    End Sub

    Private Sub LoadHandSizeAnalysisForm()
        Dim total As Integer
        Dim soft As Integer
        Dim upcard As Integer
        Dim orMore As Boolean
        Dim orLess As Boolean
        Dim handsUsed As Boolean
        Dim strat As New BJCAStrategyClass
        Dim nCards As Integer

        total = TotalComboBoxHSATab.SelectedIndex + 4
        soft = SoftCheckHSATab.Checked + 1
        upcard = UCComboBoxHSATab.SelectedIndex + 1
        orMore = OrMoreCheckHSATab.Checked
        orLess = OrLessCheckHSATab.Checked
        handsUsed = HandUsedCheckHSATab.Checked
        If TDButtonHSATab.Checked Then
            strat = Results.TD
        ElseIf TCButtonHSATab.Checked Then
            strat = Results.TC
        ElseIf CDButtonHSATab.Checked Then
            strat = Results.Opt
        Else
            strat = Results.Forced
        End If

        If Results.PlayerHandTotal(total, soft) > 0 And strat.ComputeStrat Then
            For nCards = 1 To 20
                Dim newEvs As New BJCATDStratClass

                If nCards = 1 Then
                    newEvs = Results.ComputeNCardStratEVs(strat, total, soft, upcard, 2, True, False, handsUsed)
                    FillNumberTextBox(SplitEVBoxHSATab, newEvs.StratEV(upcard), 6, False)
                Else
                    newEvs = Results.ComputeNCardStratEVs(strat, total, soft, upcard, nCards, orMore, orLess, handsUsed)
                End If

                FillStratTextBox(HandSizeAnalysisArray(nCards - 1, 0), newEvs.Strat(upcard), False, FormRules.ColorTable)
                FillNumberTextBox(HandSizeAnalysisArray(nCards - 1, 1), newEvs.StratStandEV(upcard), 6, False)
                FillNumberTextBox(HandSizeAnalysisArray(nCards - 1, 2), newEvs.StratHitEV(upcard), 6, False)
                FillNumberTextBox(HandSizeAnalysisArray(nCards - 1, 3), newEvs.StratDEV(upcard), 6, False)
                FillNumberTextBox(HandSizeAnalysisArray(nCards - 1, 4), newEvs.StratSurrEV(upcard), 6, False)
                newEvs = Nothing
            Next nCards
        End If

        strat = Nothing
    End Sub

#End Region

#Region " Double Analysis Tab "

    Private Sub PopulateDoubleAnalysisTable()
        Dim card As Integer
        Dim row As Integer

        For row = 0 To 2
            For card = 0 To 9
                Dim box As New IndexedTextBox

                'Populate the Split EVs Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(48, 20)
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                box.Location = New System.Drawing.Point(296 + 48 * card, 194 + 28 * row)

                HandDetailsGroupDATab.Controls.Add(box)
                DoubleAnalysisArray(row, card) = box
            Next card
        Next row

    End Sub

    Private Sub ClearHandDAForm(ByVal leaveName As Boolean)
        Dim card As Integer

        If Not leaveName Then
            HandNameBoxDATab.Text = ""
            TotalDetailsBoxDATab.Text = ""
            SoftDetailsCheckDATab.Checked = False
            NCardsDetailsBoxDATab.Text = ""
            UCDetailsBoxDATab.Text = ""
        End If

        FillNumberTextBox(StandBoxDATab, 0, 13, False)
        FillNumberTextBox(DoubleBoxDATab, 0, 13, False)
        FillNumberTextBox(DoubleBox2DATab, 0, 13, False)
        FillNumberTextBox(SurrenderBoxDATab, 0, 13, False)
        For card = 1 To 10
            FillNumberTextBox(DoubleAnalysisArray(0, card - 1), 0, 13, False)
            FillNumberTextBox(DoubleAnalysisArray(1, card - 1), 0, 13, False)
            FillNumberTextBox(DoubleAnalysisArray(2, card - 1), 0, 13, False)
        Next card

    End Sub

    Private Sub LoadHandDAForm()
        Dim index As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim prob As Double
        Dim surrEV As Double
        Dim hand As New BJCAHandClass

        upcard = UCComboBoxDATab.SelectedIndex + 1

        If HandListBoxDATab.SelectedIndex >= 0 Then
            hand = GetStringHand(HandListBoxDATab.Items(HandListBoxDATab.SelectedIndex))
            For index = 1 To Results.NumPHands
                If hand.SameAs(Results.PlayerHands(index).Hand) Then
                    Exit For
                End If
            Next index

            HandNameBoxDATab.Text = GetHandString(Results.PlayerHands(index).Hand)
            TotalDetailsBoxDATab.Text = CStr(Results.PlayerHands(index).Hand.Total)
            SoftDetailsCheckDATab.Checked = Results.PlayerHands(index).Hand.Soft
            NCardsDetailsBoxDATab.Text = CStr(Results.PlayerHands(index).Hand.NumCards)
            UCDetailsBoxDATab.Text = CStr(upcard)

            If Results.PlayerHands(index).HandEVs.Prob(upcard) = 0 Then
                ClearHandDAForm(True)
            Else
                FillNumberTextBox(DoubleBox2DATab, Results.PlayerHands(index).HandEVs.DEV(upcard), 13, False)
                DAllowedCheckDATab.Checked = Results.PlayerHands(index).DPreallowed(upcard)

                If Results.PlayerHands(index).Hand.NumCards > 2 And Results.DoubleNeeded(index, upcard) Then
                    FillStratTextBox(StratBoxDATab, Results.PlayerHands(index).HandEVs.DStrat(upcard), False, FormRules.ColorTable)
                    FillNumberTextBox(StandBoxDATab, Results.PlayerHands(index).HandEVs.StandEV(upcard), 13, False)
                    If Results.RDA Then
                        FillNumberTextBox(DoubleBoxDATab, Results.PlayerHands(index).HandEVs.DEV(upcard), 13, False)
                        If Results.PlayerHands(index).HandEVs.DEV(upcard) = 0 Then
                            DoubleBoxDATab.Text = "0"
                        End If
                    Else
                        FillNumberTextBox(DoubleBoxDATab, 0, 13, False)
                        DoubleBoxDATab.Text = "N/A"
                    End If
                    If Results.DDR Then
                        If Results.DDRType = Constants.Surr.ES Then
                            surrEV = Results.SurrPays
                        Else
                            surrEV = Results.PlayerHands(index).HandEVs.SurrEV(upcard)
                        End If
                        FillNumberTextBox(SurrenderBoxDATab, surrEV, 13, False)
                        If surrEV = 0 Then
                            SurrenderBoxDATab.Text = "0"
                        End If
                    Else
                        FillNumberTextBox(SurrenderBoxDATab, 0, 13, False)
                        SurrenderBoxDATab.Text = "N/A"
                    End If
                Else
                    FillStratTextBox(StratBoxDATab, 0, False, FormRules.ColorTable)
                    FillNumberTextBox(StandBoxDATab, 0, 13, False)
                    FillNumberTextBox(DoubleBoxDATab, 0, 13, False)
                    FillNumberTextBox(SurrenderBoxDATab, 0, 13, False)
                End If

                If Results.PlayerHands(index).DPreallowed(upcard) Then
                    Results.CurrentShoe.Reset(Results.OriginalShoe)
                    Results.CurrentShoe.Deal(upcard)
                    Results.CurrentShoe.Deal(Results.PlayerHands(index).Hand)
                    For card = 1 To 10
                        prob = Results.CardProb(card, upcard)
                        If prob = 0 Then
                            FillNumberTextBox(DoubleAnalysisArray(0, card - 1), 0, 13, False)
                            FillNumberTextBox(DoubleAnalysisArray(1, card - 1), 0, 13, False)
                            FillNumberTextBox(DoubleAnalysisArray(2, card - 1), 0, 13, False)
                        Else
                            If Results.PlayerHands(index).HitHand(card) > 0 Then
                                If Not (Results.DDR Or Results.RDA) Then
                                    FillStratTextBox(DoubleAnalysisArray(0, card - 1), BJCAGlobalsClass.Strat.S, False, FormRules.ColorTable)
                                    FillNumberTextBox(DoubleAnalysisArray(1, card - 1), prob, 13, False)
                                    FillNumberTextBox(DoubleAnalysisArray(2, card - 1), Results.PlayerHands(Results.PlayerHands(index).HitHand(card)).HandEVs.StandEV(upcard), 13, False)
                                Else
                                    FillStratTextBox(DoubleAnalysisArray(0, card - 1), Results.PlayerHands(Results.PlayerHands(index).HitHand(card)).HandEVs.DStrat(upcard), False, FormRules.ColorTable)
                                    FillNumberTextBox(DoubleAnalysisArray(1, card - 1), prob, 13, False)
                                    FillNumberTextBox(DoubleAnalysisArray(2, card - 1), Results.PlayerHands(Results.PlayerHands(index).HitHand(card)).HandEVs.DStratEV(upcard), 13, False)
                                End If
                            Else
                                FillStratTextBox(DoubleAnalysisArray(0, card - 1), 0, False, FormRules.ColorTable)
                                FillNumberTextBox(DoubleAnalysisArray(1, card - 1), prob, 13, False)
                                FillNumberTextBox(DoubleAnalysisArray(2, card - 1), -1, 13, False)
                            End If
                        End If
                    Next card
                Else
                    For card = 1 To 10
                        FillNumberTextBox(DoubleAnalysisArray(0, card - 1), 0, 13, False)
                        FillNumberTextBox(DoubleAnalysisArray(1, card - 1), 0, 13, False)
                        FillNumberTextBox(DoubleAnalysisArray(2, card - 1), 0, 13, False)
                    Next card
                End If
            End If
        End If
    End Sub

    Private Sub LoadHandListDAForm()
        Dim index As Integer
        Dim includedHand As New BJCAHandClass
        Dim total As Integer
        Dim either As Boolean
        Dim soft As Integer
        Dim nCards As Integer
        Dim orMore As Boolean
        Dim orLess As Boolean
        Dim listHand As Boolean
        Dim exactMatch As Boolean
        Dim upcard As Integer

        If TotalComboBoxDATab.SelectedIndex = 0 Then
            total = 0
        Else
            total = TotalComboBoxDATab.SelectedIndex + 3
        End If
        either = EitherCheckDATab.Checked
        soft = SoftOnlyCheckDATab.Checked
        If NCardsComboBoxDATab.SelectedIndex = 0 Then
            nCards = 0
        Else
            nCards = NCardsComboBoxDATab.SelectedIndex + 1
        End If
        upcard = UCComboBoxDATab.SelectedIndex + 1
        orMore = OrMoreCheckDATab.Checked
        orLess = OrLessCheckDATab.Checked
        includedHand = DAHand
        exactMatch = ExactMatchCheckDATab.Checked

        'First empty the box
        For index = HandListBoxDATab.Items.Count - 1 To 0 Step -1
            HandListBoxDATab.Items.RemoveAt(index)
        Next index

        'Then load the box
        For index = 1 To Results.NumPHands
            listHand = False
            If Results.DoubleNeeded(index, upcard) Then
                If Not exactMatch Then
                    If total = 0 Or Results.PlayerHands(index).Hand.Total = total Then
                        If either Or Results.PlayerHands(index).Hand.Soft = soft Then
                            If nCards = 0 Or (Results.PlayerHands(index).Hand.NumCards = nCards) Or (orMore And Results.PlayerHands(index).Hand.NumCards >= nCards) Or (orLess And Results.PlayerHands(index).Hand.NumCards <= nCards) Then
                                If Results.PlayerHands(index).Hand.Includes(includedHand) Then
                                    listHand = True
                                End If
                            End If
                        End If
                    End If
                Else
                    If DAHand.SameAs(Results.PlayerHands(index).Hand) Then
                        listHand = True
                    End If
                End If
            End If
            If listHand Then
                HandListBoxDATab.Items.Add(GetHandString(Results.PlayerHands(index).Hand))
            End If
        Next index
        ListSizeBoxDATab.Text = HandListBoxDATab.Items.Count
        ClearHandDAForm(False)
    End Sub

    Private Sub TotalComboBoxDATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TotalComboBoxDATab.SelectedIndexChanged
        LoadHandListDAForm()
    End Sub

    Private Sub EitherCheckDATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EitherCheckDATab.CheckedChanged
        If EitherCheckDATab.Checked Then
            HardOnlyCheckDATab.Enabled = False
            SoftOnlyCheckDATab.Enabled = False
            HardOnlyCheckDATab.Checked = True
            SoftOnlyCheckDATab.Checked = False
        Else
            HardOnlyCheckDATab.Enabled = True
            SoftOnlyCheckDATab.Enabled = True
        End If
        LoadHandListDAForm()
    End Sub

    Private Sub HardOnlyCheckDATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardOnlyCheckDATab.CheckedChanged
        If HardOnlyCheckDATab.Checked Then
            SoftOnlyCheckDATab.Checked = False
        Else
            SoftOnlyCheckDATab.Checked = True
        End If
        LoadHandListDAForm()
    End Sub

    Private Sub SoftOnlyCheckDATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftOnlyCheckDATab.CheckedChanged
        If SoftOnlyCheckDATab.Checked Then
            HardOnlyCheckDATab.Checked = False
        Else
            HardOnlyCheckDATab.Checked = True
        End If
        LoadHandListDAForm()
    End Sub

    Private Sub NCardsComboBoxDATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NCardsComboBoxDATab.SelectedIndexChanged
        If NCardsComboBoxDATab.SelectedIndex = 0 Then
            OrMoreCheckDATab.Enabled = False
            OrLessCheckDATab.Enabled = False
            OrMoreCheckDATab.Checked = False
            OrLessCheckDATab.Checked = False
        ElseIf NCardsComboBoxDATab.SelectedIndex = 1 Then
            OrMoreCheckDATab.Enabled = True
            OrLessCheckDATab.Enabled = False
            OrLessCheckDATab.Checked = False
        ElseIf NCardsComboBoxDATab.SelectedIndex = 20 Then
            OrMoreCheckDATab.Enabled = False
            OrLessCheckDATab.Enabled = True
            OrMoreCheckDATab.Checked = False
        Else
            OrMoreCheckDATab.Enabled = True
            OrLessCheckDATab.Enabled = True
        End If
        LoadHandListDAForm()
    End Sub

    Private Sub OrMoreCheckDATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrMoreCheckDATab.CheckedChanged
        If OrMoreCheckDATab.Checked Then
            OrLessCheckDATab.Checked = False
        End If
        LoadHandListDAForm()
    End Sub

    Private Sub OrLessCheckDATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrLessCheckDATab.CheckedChanged
        If OrLessCheckDATab.Checked Then
            OrMoreCheckDATab.Checked = False
        End If
        LoadHandListDAForm()
    End Sub

    Private Sub HandBoxDATab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles HandBoxDATab.Validating
        Dim handValid As Boolean

        handValid = ValidCardString(HandBoxDATab.Text)

        If handValid Then
            DAHand = GetStringHand(HandBoxDATab.Text)
            If DAHand.Total > 21 Then
                handValid = False
            End If
        End If

        If Not handValid Then
            MsgBox("This is not a valid non-busted player hand.", MsgBoxStyle.OKOnly)
            HandBoxDATab.Text = ""
            DAHand.Empty()
            e.Cancel = True
        End If

        If ExactMatchCheckDATab.Checked And (DAHand.NumCards < 2 Or DAHand.Total = 0) Then
            ExactMatchCheckDATab.Checked = False
        End If

        LoadHandListDAForm()
    End Sub

    Private Sub ExactMatchCheckDATab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExactMatchCheckDATab.CheckedChanged
        If DAHand.Total = 0 Then
            ExactMatchCheckDATab.Checked = False
        ElseIf DAHand.NumCards < 2 Then
            MsgBox("The hand must have at least two card for an exact match.", MsgBoxStyle.OKOnly)
            ExactMatchCheckDATab.Checked = False
        End If
        LoadHandListDAForm()
        If ExactMatchCheckDATab.Checked Then
            TotalLabelDATab.Enabled = False
            TotalComboBoxDATab.Enabled = False
            SoftLabelDATab.Enabled = False
            EitherCheckDATab.Enabled = False
            HardOnlyCheckDATab.Enabled = False
            SoftOnlyCheckDATab.Enabled = False
            NCardLabelDATab.Enabled = False
            NCardsComboBoxDATab.Enabled = False
            OrMoreCheckDATab.Enabled = False
            OrLessCheckDATab.Enabled = False
        Else
            TotalLabelDATab.Enabled = True
            TotalComboBoxDATab.Enabled = True
            SoftLabelDATab.Enabled = True
            EitherCheckDATab.Enabled = True
            If EitherCheckDATab.Checked Then
                HardOnlyCheckDATab.Enabled = False
                SoftOnlyCheckDATab.Enabled = False
            Else
                HardOnlyCheckDATab.Enabled = True
                SoftOnlyCheckDATab.Enabled = True
            End If
            NCardLabelDATab.Enabled = True
            NCardsComboBoxDATab.Enabled = True
            If NCardsComboBoxDATab.SelectedIndex = 0 Then
                OrMoreCheckDATab.Enabled = False
                OrLessCheckDATab.Enabled = False
                OrMoreCheckDATab.Checked = False
                OrLessCheckDATab.Checked = False
            ElseIf NCardsComboBoxDATab.SelectedIndex = 1 Then
                OrMoreCheckDATab.Enabled = True
                OrLessCheckDATab.Enabled = False
                OrLessCheckDATab.Checked = False
            ElseIf NCardsComboBoxDATab.SelectedIndex = 20 Then
                OrMoreCheckDATab.Enabled = False
                OrLessCheckDATab.Enabled = True
                OrMoreCheckDATab.Checked = False
            Else
                OrMoreCheckDATab.Enabled = True
                OrLessCheckDATab.Enabled = True
            End If
        End If
    End Sub

    Private Sub HandListBoxDATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandListBoxDATab.SelectedIndexChanged
        LoadHandDAForm()
    End Sub

    Private Sub UCComboBoxDATab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UCComboBoxDATab.SelectedIndexChanged
        LoadHandDAForm()
    End Sub

#End Region

#Region " Exceptions Tab "

    Private Sub ClearHandEForm(ByVal leaveName As Boolean)
        If Not leaveName Then
            HandNameBoxETab.Text = ""
            TotalDetailsBoxETab.Text = ""
            SoftDetailsCheckETab.Checked = False
            NCardsDetailsBoxETab.Text = ""
            UCDetailsBoxETab.Text = ""
        End If

        ExTypeBoxETab.Text = ""
        ProbBoxETab.Text = ""
        PaircardBoxETab.Text = ""
        StateBoxETab.Text = ""

        BaseNameBoxETab.Text = ""
        BaseStratETab.Text = ""
        BaseStratETab.BackColor = FormRules.ColorTable.C(0)
        FillNumberTextBox(BaseStandBoxETab, 0, 13, False)
        FillNumberTextBox(BaseDoubleBoxETab, 0, 13, False)
        FillNumberTextBox(BaseHitBoxETab, 0, 13, False)
        FillNumberTextBox(BaseSurrenderBoxETab, 0, 13, False)
        FillNumberTextBox(BaseSplitBoxETab, 0, 13, False)
        BaseHandUsedCheckETab.Checked = False

        ExNameBoxETab.Text = ""
        ExStratETab.Text = ""
        ExStratETab.BackColor = FormRules.ColorTable.C(0)
        FillNumberTextBox(ExStandBoxETab, 0, 13, False)
        FillNumberTextBox(ExDoubleBoxETab, 0, 13, False)
        FillNumberTextBox(ExHitBoxETab, 0, 13, False)
        FillNumberTextBox(ExSurrenderBoxETab, 0, 13, False)
        FillNumberTextBox(ExSplitBoxETab, 0, 13, False)
        ExHandUsedCheckETab.Text = False

    End Sub

    Private Sub LoadHandEForm()
        Dim paircard As Integer
        Dim hands As Integer
        Dim upcard As Integer
        Dim index As Integer
        Dim exStrat As BJCAStrategyClass
        Dim exPost As Boolean
        Dim baseStrat As BJCAStrategyClass
        Dim listIndex As Integer
        Dim stratsPresent As Boolean

        If HandListBoxETab.SelectedIndex >= 0 Then
            listIndex = CurrentEList(HandListBoxETab.SelectedIndex)
        End If

        If Not Results.ExceptionsList.L Is Nothing Then
            upcard = Results.ExceptionsList.L(listIndex).Upcard
            index = Results.ExceptionsList.L(listIndex).Index
            paircard = Results.ExceptionsList.L(listIndex).Paircard
            hands = Results.ExceptionsList.L(listIndex).SplitHand

            stratsPresent = False
            Select Case Results.ExceptionsList.L(listIndex).ExceptionType
                Case BJCAGlobalsClass.ExType.CDTDPre      'CD-TD
                    If Results.TD.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TD
                    exStrat = Results.Opt
                    exPost = False
                Case BJCAGlobalsClass.ExType.CDTCPre       'CD-TC
                    If Results.TC.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TC
                    exStrat = Results.Opt
                    exPost = False
                Case BJCAGlobalsClass.ExType.CDForcedPre      'CD-Forced
                    If Results.Forced.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.Forced
                    exStrat = Results.Opt
                    exPost = False
                Case BJCAGlobalsClass.ExType.ForcedTDPre      'Forced-TD
                    If Results.Forced.ComputeStrat And Results.TD.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TD
                    exStrat = Results.Forced
                    exPost = False
                Case BJCAGlobalsClass.ExType.ForcedTCPre      'Forced-TC
                    If Results.Forced.ComputeStrat And Results.TC.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TC
                    exStrat = Results.Forced
                    exPost = False
                Case BJCAGlobalsClass.ExType.TCTDPre      'TC-TD
                    If Results.TD.ComputeStrat And Results.TC.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TD
                    exStrat = Results.TC
                    exPost = False
                Case BJCAGlobalsClass.ExType.CDTDPost      'CD-TD
                    If Results.TD.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TD
                    exStrat = Results.Opt
                    exPost = True
                Case BJCAGlobalsClass.ExType.CDTCPost      'CD-TC
                    If Results.TC.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TC
                    exStrat = Results.Opt
                    exPost = True
                Case BJCAGlobalsClass.ExType.CDForcedPost      'CD-Forced
                    If Results.Forced.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.Forced
                    exStrat = Results.Opt
                    exPost = True
                Case BJCAGlobalsClass.ExType.CDCDPost     'CD-CD
                    If Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.Opt
                    exStrat = Results.Opt
                    exPost = True
                Case Else
                    If Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.Opt
                    exStrat = Results.Opt
                    exPost = True
            End Select

            HandNameBoxETab.Text = GetHandString(Results.PlayerHands(index).Hand)
            TotalDetailsBoxETab.Text = CStr(Results.PlayerHands(index).Hand.Total)
            SoftDetailsCheckETab.Checked = Results.PlayerHands(index).Hand.Soft
            NCardsDetailsBoxETab.Text = CStr(Results.PlayerHands(index).Hand.NumCards)
            UCDetailsBoxETab.Text = CStr(upcard)

            If stratsPresent Then
                BaseNameBoxETab.Text = baseStrat.Name
                ExNameBoxETab.Text = exStrat.Name

                ExTypeBoxETab.Text = Results.C.ExText(Results.ExceptionsList.L(listIndex).ExceptionType)
                ExRuleNameBoxETab.Text = Results.ExceptionsList.L(listIndex).ExceptionName
                ProbBoxETab.Text = CStr(Results.PlayerHands(index).HandEVs.Prob(upcard))
                If exPost Then
                    PaircardBoxETab.Text = CStr(paircard)
                    StateBoxETab.Text = Results.ExceptionsList.L(listIndex).ShoeState
                Else
                    PaircardBoxETab.Text = ""
                    StateBoxETab.Text = "Pre-Split"
                End If

                BaseStratETab.Text = Constants.StratShortText(baseStrat.HandEVs(index).EVs.Strat(upcard))
                BaseStratETab.BackColor = FormRules.ColorTable.C(baseStrat.HandEVs(index).EVs.Strat(upcard))
                FillNumberTextBox(BaseStandBoxETab, Results.PlayerHands(index).HandEVs.StandEV(upcard), 13, False)
                FillNumberTextBox(BaseDoubleBoxETab, Results.PlayerHands(index).HandEVs.DEV(upcard), 13, False)
                FillNumberTextBox(BaseHitBoxETab, baseStrat.HandEVs(index).EVs.HitEV(upcard), 13, False)
                FillNumberTextBox(BaseSurrenderBoxETab, Results.PlayerHands(index).HandEVs.SurrEV(upcard), 13, False)
                FillNumberTextBox(BaseSplitBoxETab, baseStrat.HandEVs(index).SplitEV(upcard), 13, False)
                BaseHandUsedCheckETab.Checked = baseStrat.HandEVs(index).HandUsed(upcard)

                If exPost Then
                    ExStratETab.Text = Constants.StratShortText(exStrat.HandEVs(index).SplitData(Results.PlayerHands(index).PairIndex(paircard), hands).Strat(upcard))
                    ExStratETab.BackColor = FormRules.ColorTable.C(exStrat.HandEVs(index).SplitData(Results.PlayerHands(index).PairIndex(paircard), hands).Strat(upcard))
                    FillNumberTextBox(ExStandBoxETab, Results.PlayerHands(index).SplitEVs(Results.PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard), 13, False)
                    FillNumberTextBox(ExDoubleBoxETab, Results.PlayerHands(index).SplitEVs(Results.PlayerHands(index).PairIndex(paircard), hands).DEV(upcard), 13, False)
                    FillNumberTextBox(ExHitBoxETab, exStrat.HandEVs(index).SplitData(Results.PlayerHands(index).PairIndex(paircard), hands).HitEV(upcard), 13, False)
                    FillNumberTextBox(ExSurrenderBoxETab, Results.PlayerHands(index).SplitEVs(Results.PlayerHands(index).PairIndex(paircard), hands).SurrEV(upcard), 13, False)
                    ExHandUsedCheckETab.Checked = exStrat.HandEVs(index).HandUsed(upcard)
                    FillNumberTextBox(ExSplitBoxETab, exStrat.HandEVs(index).SplitEV(upcard), 13, False)
                Else
                    ExStratETab.Text = Constants.StratShortText(exStrat.HandEVs(index).EVs.Strat(upcard))
                    ExStratETab.BackColor = FormRules.ColorTable.C(exStrat.HandEVs(index).EVs.Strat(upcard))
                    FillNumberTextBox(ExStandBoxETab, Results.PlayerHands(index).HandEVs.StandEV(upcard), 13, False)
                    FillNumberTextBox(ExDoubleBoxETab, Results.PlayerHands(index).HandEVs.DEV(upcard), 13, False)
                    FillNumberTextBox(ExHitBoxETab, exStrat.HandEVs(index).EVs.HitEV(upcard), 13, False)
                    FillNumberTextBox(ExSurrenderBoxETab, Results.PlayerHands(index).HandEVs.SurrEV(upcard), 13, False)
                    ExHandUsedCheckETab.Checked = exStrat.HandEVs(index).HandUsed(upcard)
                    FillNumberTextBox(ExSplitBoxETab, exStrat.HandEVs(index).SplitEV(upcard), 13, False)
                End If
            End If
        End If
    End Sub

    Private Sub LoadHandListEForm()
        Dim listIndex As Integer
        Dim includedHand As New BJCAHandClass
        Dim total As Integer
        Dim either As Boolean
        Dim soft As Integer
        Dim nCards As Integer
        Dim orMore As Boolean
        Dim orLess As Boolean
        Dim upcard As Integer
        Dim presplit As Boolean
        Dim postsplit As Boolean
        Dim listHand As Boolean
        Dim exactMatch As Boolean

        If TotalComboBoxETab.SelectedIndex = 0 Then
            total = 0
        Else
            total = TotalComboBoxETab.SelectedIndex + 3
        End If
        either = EitherCheckETab.Checked
        soft = SoftOnlyCheckETab.Checked
        If NCardsComboBoxETab.SelectedIndex = 0 Then
            nCards = 0
        Else
            nCards = NCardsComboBoxETab.SelectedIndex + 1
        End If
        orMore = OrMoreCheckETab.Checked
        orLess = OrLessCheckETab.Checked
        upcard = UCComboBoxETab.SelectedIndex
        presplit = PreSplitCheckETab.Checked
        postsplit = PostSplitCheckETab.Checked
        includedHand = EHand
        exactMatch = ExactMatchCheckETab.Checked

        'First empty the box
        For listIndex = HandListBoxETab.Items.Count - 1 To 0 Step -1
            HandListBoxETab.Items.RemoveAt(listIndex)
        Next listIndex
        ReDim CurrentEList(0)

        'Then load the box
        For listIndex = 0 To Results.ExceptionsList.NumExceptions - 1
            listHand = False
            If upcard = 0 Or upcard = Results.ExceptionsList.L(listIndex).Upcard Then
                If Not exactMatch Then
                    If total = 0 Or Results.PlayerHands(Results.ExceptionsList.L(listIndex).Index).Hand.Total = total Then
                        If either Or Results.PlayerHands(Results.ExceptionsList.L(listIndex).Index).Hand.Soft = soft Then
                            If nCards = 0 Or (Results.PlayerHands(Results.ExceptionsList.L(listIndex).Index).Hand.NumCards = nCards) Or (orMore And Results.PlayerHands(Results.ExceptionsList.L(listIndex).Index).Hand.NumCards >= nCards) Or (orLess And Results.PlayerHands(Results.ExceptionsList.L(listIndex).Index).Hand.NumCards <= nCards) Then
                                If Results.PlayerHands(Results.ExceptionsList.L(listIndex).Index).Hand.Includes(includedHand) Then
                                    listHand = True
                                End If
                            End If
                        End If
                    End If
                Else
                    If EHand.SameAs(Results.PlayerHands(Results.ExceptionsList.L(listIndex).Index).Hand) Then
                        listHand = True
                    End If
                End If
            End If
            If listHand Then
                Select Case ExTypeComboBoxETab.SelectedIndex
                    Case 0  'All Exceptions Allowed
                        Select Case Results.ExceptionsList.L(listIndex).ExceptionType
                            Case BJCAGlobalsClass.ExType.CDTDPre  'CD-TD Pre
                                If Not presplit Then listHand = False
                            Case BJCAGlobalsClass.ExType.CDTDPost  'CD-TD Post
                                If Not postsplit Then listHand = False
                            Case BJCAGlobalsClass.ExType.CDForcedPre  'CD-Forced Pre
                                If Not presplit Then listHand = False
                            Case BJCAGlobalsClass.ExType.CDForcedPost  'CD-Forced Post
                                If Not postsplit Then listHand = False
                            Case BJCAGlobalsClass.ExType.CDCDPost 'CD-CD
                                If Not postsplit Then listHand = False
                            Case Else
                                listHand = False
                        End Select
                    Case 1  'CD-TD Only
                        Select Case Results.ExceptionsList.L(listIndex).ExceptionType
                            Case BJCAGlobalsClass.ExType.CDTDPre  'CD-TD Pre
                                If Not presplit Then listHand = False
                            Case BJCAGlobalsClass.ExType.CDTDPost  'CD-TD Post
                                If Not postsplit Then listHand = False
                            Case BJCAGlobalsClass.ExType.CDForcedPre  'CD-Forced Pre
                                listHand = False
                            Case BJCAGlobalsClass.ExType.CDForcedPost  'CD-Forced Post
                                listHand = False
                            Case BJCAGlobalsClass.ExType.CDCDPost 'CD-CD
                                listHand = False
                            Case Else
                                listHand = False
                        End Select
                    Case 2  'CD-Forced Only
                        Select Case Results.ExceptionsList.L(listIndex).ExceptionType
                            Case BJCAGlobalsClass.ExType.CDTDPre  'CD-TD Pre
                                listHand = False
                            Case BJCAGlobalsClass.ExType.CDTDPost  'CD-TD Post
                                listHand = False
                            Case BJCAGlobalsClass.ExType.CDForcedPre  'CD-Forced Pre
                                If Not presplit Then listHand = False
                            Case BJCAGlobalsClass.ExType.CDForcedPost  'CD-Forced Post
                                If Not postsplit Then listHand = False
                            Case BJCAGlobalsClass.ExType.CDCDPost 'CD-CD
                                listHand = False
                            Case Else
                                listHand = False
                        End Select
                    Case 3  'CD-CD Only
                        Select Case Results.ExceptionsList.L(listIndex).ExceptionType
                            Case BJCAGlobalsClass.ExType.CDTDPre  'CD-TD Pre
                                listHand = False
                            Case BJCAGlobalsClass.ExType.CDTDPost  'CD-TD Post
                                listHand = False
                            Case BJCAGlobalsClass.ExType.CDForcedPre  'CD-TD Pre
                                listHand = False
                            Case BJCAGlobalsClass.ExType.CDForcedPost  'CD-TD Post
                                listHand = False
                            Case BJCAGlobalsClass.ExType.CDCDPost 'CD-CD
                                If Not postsplit Then listHand = False
                            Case Else
                                listHand = False
                        End Select
                End Select
            End If
            If listHand Then
                HandListBoxETab.Items.Add(GetHandString(Results.PlayerHands(Results.ExceptionsList.L(listIndex).Index).Hand))
                ReDim Preserve CurrentEList(HandListBoxETab.Items.Count)
                CurrentEList(HandListBoxETab.Items.Count - 1) = listIndex
            End If
        Next listIndex

        ListSizeBoxETab.Text = HandListBoxETab.Items.Count
        ClearHandEForm(False)
    End Sub

    Private Sub HandListBoxETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandListBoxETab.SelectedIndexChanged
        LoadHandEForm()
    End Sub

    Private Sub TotalComboBoxETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TotalComboBoxETab.SelectedIndexChanged
        LoadHandListEForm()
    End Sub

    Private Sub EitherCheckETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EitherCheckETab.CheckedChanged
        If EitherCheckETab.Checked Then
            HardOnlyCheckETab.Enabled = False
            SoftOnlyCheckETab.Enabled = False
            HardOnlyCheckETab.Checked = True
            SoftOnlyCheckETab.Checked = False
        Else
            HardOnlyCheckETab.Enabled = True
            SoftOnlyCheckETab.Enabled = True
        End If
        LoadHandListEForm()
    End Sub

    Private Sub HardOnlyCheckETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardOnlyCheckETab.CheckedChanged
        If HardOnlyCheckETab.Checked Then
            SoftOnlyCheckETab.Checked = False
        Else
            SoftOnlyCheckETab.Checked = True
        End If
        LoadHandListEForm()
    End Sub

    Private Sub SoftOnlyCheckETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftOnlyCheckETab.CheckedChanged
        If SoftOnlyCheckETab.Checked Then
            HardOnlyCheckETab.Checked = False
        Else
            HardOnlyCheckETab.Checked = True
        End If
        LoadHandListEForm()
    End Sub

    Private Sub NCardsComboBoxETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NCardsComboBoxETab.SelectedIndexChanged
        If NCardsComboBoxETab.SelectedIndex = 0 Then
            OrMoreCheckETab.Enabled = False
            OrLessCheckETab.Enabled = False
            OrMoreCheckETab.Checked = False
            OrLessCheckETab.Checked = False
        ElseIf NCardsComboBoxETab.SelectedIndex = 1 Then
            OrMoreCheckETab.Enabled = True
            OrLessCheckETab.Enabled = False
            OrLessCheckETab.Checked = False
        ElseIf NCardsComboBoxETab.SelectedIndex = 20 Then
            OrMoreCheckETab.Enabled = False
            OrLessCheckETab.Enabled = True
            OrMoreCheckETab.Checked = False
        Else
            OrMoreCheckETab.Enabled = True
            OrLessCheckETab.Enabled = True
        End If
        LoadHandListEForm()
    End Sub

    Private Sub OrMoreCheckETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrMoreCheckETab.CheckedChanged
        If OrMoreCheckETab.Checked Then
            OrLessCheckETab.Checked = False
        End If
        LoadHandListEForm()
    End Sub

    Private Sub OrLessCheckETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrLessCheckETab.CheckedChanged
        If OrLessCheckETab.Checked Then
            OrMoreCheckETab.Checked = False
        End If
        LoadHandListEForm()
    End Sub

    Private Sub UCComboBoxETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UCComboBoxETab.SelectedIndexChanged
        LoadHandListEForm()
    End Sub

    Private Sub HandBoxETab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles HandBoxETab.Validating
        Dim handValid As Boolean
        Dim i As Integer
        Dim card As String

        handValid = True
        For i = 0 To HandBoxETab.Text.Length - 1
            card = HandBoxETab.Text.Chars(i)
            Select Case card
                Case "A", "a", "1", "2", "3", "4", "5", "6", "7", "8", "9", "T", "t"
                Case Else
                    handValid = False
            End Select
        Next i

        If handValid Then
            EHand = GetStringHand(HandBoxETab.Text)
            If EHand.Total > 21 Then
                handValid = False
            End If
        End If

        If Not handValid Then
            MsgBox("This is not a valid non-busted player hand.", MsgBoxStyle.OKOnly)
            HandBoxETab.Text = ""
            EHand.Empty()
            e.Cancel = True
        End If

        If ExactMatchCheckETab.Checked And (EHand.NumCards < 2 Or EHand.Total = 0) Then
            ExactMatchCheckETab.Checked = False
        End If

        LoadHandListEForm()
    End Sub

    Private Sub ExactMatchCheckETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExactMatchCheckETab.CheckedChanged
        If EHand.Total = 0 Then
            ExactMatchCheckETab.Checked = False
        ElseIf EHand.NumCards < 2 Then
            MsgBox("The hand must have at least two card for an exact match.", MsgBoxStyle.OKOnly)
            ExactMatchCheckETab.Checked = False
        End If
        LoadHandListEForm()
        If ExactMatchCheckETab.Checked Then
            TotalLabelETab.Enabled = False
            TotalComboBoxETab.Enabled = False
            SoftLabelETab.Enabled = False
            EitherCheckETab.Enabled = False
            HardOnlyCheckETab.Enabled = False
            SoftOnlyCheckETab.Enabled = False
            NCardLabelETab.Enabled = False
            NCardsComboBoxETab.Enabled = False
            OrMoreCheckETab.Enabled = False
            OrLessCheckETab.Enabled = False
            UCLabelETab.Enabled = False
            UCComboBoxETab.Enabled = False
        Else
            TotalLabelETab.Enabled = True
            TotalComboBoxETab.Enabled = True
            SoftLabelETab.Enabled = True
            EitherCheckETab.Enabled = True
            If EitherCheckETab.Checked Then
                HardOnlyCheckETab.Enabled = False
                SoftOnlyCheckETab.Enabled = False
            Else
                HardOnlyCheckETab.Enabled = True
                SoftOnlyCheckETab.Enabled = True
            End If
            NCardLabelETab.Enabled = True
            NCardsComboBoxETab.Enabled = True
            If NCardsComboBoxETab.SelectedIndex = 0 Then
                OrMoreCheckETab.Enabled = False
                OrLessCheckETab.Enabled = False
                OrMoreCheckETab.Checked = False
                OrLessCheckETab.Checked = False
            ElseIf NCardsComboBoxETab.SelectedIndex = 1 Then
                OrMoreCheckETab.Enabled = True
                OrLessCheckETab.Enabled = False
                OrLessCheckETab.Checked = False
            ElseIf NCardsComboBoxETab.SelectedIndex = 20 Then
                OrMoreCheckETab.Enabled = False
                OrLessCheckETab.Enabled = True
                OrMoreCheckETab.Checked = False
            Else
                OrMoreCheckETab.Enabled = True
                OrLessCheckETab.Enabled = True
            End If
            UCLabelETab.Enabled = True
            UCComboBoxETab.Enabled = True
        End If
    End Sub

    Private Sub PreSplitCheckETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreSplitCheckETab.CheckedChanged
        If Not PreSplitCheckETab.Checked And Not PostSplitCheckETab.Checked Then
            PreSplitCheckETab.Checked = True
            PostSplitCheckETab.Checked = True
        End If
        If ExTypeComboBoxETab.SelectedIndex = 3 Then
            PreSplitCheckETab.Checked = False
            PostSplitCheckETab.Checked = True
        End If
        LoadHandListEForm()
    End Sub

    Private Sub PostSplitCheckETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostSplitCheckETab.CheckedChanged
        If Not PreSplitCheckETab.Checked And Not PostSplitCheckETab.Checked Then
            PreSplitCheckETab.Checked = True
            PostSplitCheckETab.Checked = True
        End If
        If ExTypeComboBoxETab.SelectedIndex = 3 Then
            PreSplitCheckETab.Checked = False
            PostSplitCheckETab.Checked = True
        End If
        LoadHandListEForm()
    End Sub

    Private Sub ExTypeComboBoxETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExTypeComboBoxETab.SelectedIndexChanged
        Select Case ExTypeComboBoxETab.SelectedIndex
            Case 3
                PreSplitCheckETab.Checked = False
                PostSplitCheckETab.Checked = True
        End Select
        LoadHandListEForm()
    End Sub

    Private Sub LoadExTypeComboBoxETab()
        ExTypeComboBoxETab.Items.Add("All")
        ExTypeComboBoxETab.Items.Add("CD-TD")
        ExTypeComboBoxETab.Items.Add("CD-Forced")
        ExTypeComboBoxETab.Items.Add("CD-CD")

        ExTypeComboBoxETab.SelectedIndex = 0
    End Sub

    Private Function GetEFormCurrentForcedRule() As Boolean
        Dim paircard As Integer
        Dim hands As Integer
        Dim upcard As Integer
        Dim index As Integer
        Dim exStrat As BJCAStrategyClass
        Dim exPost As Boolean
        Dim newStrat As Integer
        Dim listindex As Integer
        Dim ruleExists As Boolean

        ruleExists = True
        If HandListBoxETab.SelectedIndex >= 0 Then
            listindex = CurrentEList(HandListBoxETab.SelectedIndex)
        Else
            MsgBox("No exception is currently selected.")
            ruleExists = False
        End If

        If Not Results.ExceptionsList.L Is Nothing Then
            upcard = Results.ExceptionsList.L(listindex).Upcard
            index = Results.ExceptionsList.L(listindex).Index
            paircard = Results.ExceptionsList.L(listindex).Paircard
            hands = Results.ExceptionsList.L(listindex).SplitHand


            'Get the new strategy
            Select Case Results.ExceptionsList.L(listindex).ExceptionType
                Case BJCAGlobalsClass.ExType.CDTDPre, BJCAGlobalsClass.ExType.CDTCPre, BJCAGlobalsClass.ExType.CDForcedPre
                    exStrat = Results.Opt
                    exPost = False
                Case BJCAGlobalsClass.ExType.ForcedTDPre, BJCAGlobalsClass.ExType.ForcedTCPre
                    exStrat = Results.Forced
                    exPost = False
                Case BJCAGlobalsClass.ExType.TCTDPre
                    exStrat = Results.TC
                    exPost = False
                Case BJCAGlobalsClass.ExType.CDTDPost, BJCAGlobalsClass.ExType.CDTCPost, BJCAGlobalsClass.ExType.CDForcedPost, BJCAGlobalsClass.ExType.CDCDPost
                    exStrat = Results.Opt
                    exPost = True
                Case Else
                    exStrat = Results.Opt
                    exPost = True
            End Select
            If exPost Then
                newStrat = exStrat.HandEVs(index).SplitData(Results.PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
            Else
                newStrat = exStrat.HandEVs(index).EVs.Strat(upcard)
            End If

            With ForcedRule
                .Name = BJCAShared.GetHandString(Results.PlayerHands(index).Hand) + " v " + CStr(upcard) + " " + Results.C.StratShortText(newStrat)
                If exPost Then
                    .Name += " Post-Split"
                Else
                    .Name += " Pre-Split"
                End If
                .RuleOn = False
                .UseSpecificHand = True

                .Hand.Copy(Results.PlayerHands(index).Hand)
                .Hand.Total = Results.PlayerHands(index).Hand.Total
                .Hand.NumCards = Results.PlayerHands(index).Hand.NumCards
                .Hand.Soft = Results.PlayerHands(index).Hand.Soft
                .ExactMatch = True

                .OrMore = False
                .OrLess = False

                .PreSplit = Not exPost
                .PostSplit = exPost

                .Upcard = upcard
                .Strat = newStrat
            End With
        Else
            MsgBox("There aren't any listed exceptions.")
            ruleExists = False
        End If

        Return ruleExists
    End Function

    Private Sub ExForcedButtonETab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExForcedButtonETab.Click
        If GetEFormCurrentForcedRule() Then
            AddForcedRule(False)
        End If
    End Sub

#End Region

#Region " N Card Exceptions Tab "

    Private Sub ClearHandNCEForm(ByVal leaveName As Boolean)
        If Not leaveName Then
            TotalDetailsBoxNCETab.Text = ""
            SoftDetailsCheckNCETab.Checked = False
            NCardsDetailsBoxNCETab.Text = ""
            UCDetailsBoxNCETab.Text = ""
        End If

        ExTypeBoxNCETab.Text = ""
        ExRuleNameBoxNCETab.Text = ""

        BaseNameBoxNCETab.Text = ""
        BaseStratNCETab.Text = ""
        BaseStratNCETab.BackColor = FormRules.ColorTable.C(0)
        FillNumberTextBox(BaseStandBoxNCETab, 0, 13, False)
        FillNumberTextBox(BaseDoubleBoxNCETab, 0, 13, False)
        FillNumberTextBox(BaseHitBoxNCETab, 0, 13, False)
        FillNumberTextBox(BaseSurrenderBoxNCETab, 0, 13, False)

        ExNameBoxNCETab.Text = ""
        ExStratNCETab.Text = ""
        ExStratNCETab.BackColor = FormRules.ColorTable.C(0)
        FillNumberTextBox(ExStandBoxNCETab, 0, 13, False)
        FillNumberTextBox(ExDoubleBoxNCETab, 0, 13, False)
        FillNumberTextBox(ExHitBoxNCETab, 0, 13, False)
        FillNumberTextBox(ExSurrenderBoxNCETab, 0, 13, False)

    End Sub

    Private Sub LoadHandNCEForm()
        Dim hands As Integer
        Dim upcard As Integer
        Dim total As Integer
        Dim soft As Integer
        Dim baseStrat As BJCAStrategyClass
        Dim listIndex As Integer
        Dim stratsPresent As Boolean

        If HandListBoxNCETab.SelectedIndex >= 0 Then
            listIndex = CurrentNCEList(HandListBoxNCETab.SelectedIndex)
        End If

        If Not Results.NCardExceptionsList.L Is Nothing Then
            upcard = Results.NCardExceptionsList.L(listIndex).Upcard
            total = Results.NCardExceptionsList.L(listIndex).Total
            soft = Results.NCardExceptionsList.L(listIndex).Soft + 1

            stratsPresent = False
            Select Case Results.NCardExceptionsList.L(listIndex).ExceptionType
                Case BJCAGlobalsClass.ExType.TDTD               'TD-TD
                    If Results.TD.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TD
                Case BJCAGlobalsClass.ExType.TCTC               'TC-TC
                    If Results.TC.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TC
                Case BJCAGlobalsClass.ExType.ForcedForced       'Forced-Forced
                    If Results.Forced.ComputeStrat And Results.Opt.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.Forced
                Case Else
                    If Results.TD.ComputeStrat Then stratsPresent = True
                    baseStrat = Results.TD
            End Select

            TotalDetailsBoxNCETab.Text = CStr(total)
            SoftDetailsCheckNCETab.Checked = Results.NCardExceptionsList.L(listIndex).Soft
            NCardsDetailsBoxNCETab.Text = CStr(Results.NCardExceptionsList.L(listIndex).NCards)
            UCDetailsBoxNCETab.Text = CStr(upcard)

            If stratsPresent Then
                BaseNameBoxNCETab.Text = baseStrat.Name
                ExNameBoxNCETab.Text = baseStrat.Name

                ExTypeBoxNCETab.Text = Results.C.ExText(Results.NCardExceptionsList.L(listIndex).ExceptionType)
                ExRuleNameBoxNCETab.Text = Results.NCardExceptionsList.L(listIndex).ExceptionName

                BaseStratNCETab.Text = Constants.StratShortText(baseStrat.StratTD(total, soft).Strat(upcard))
                BaseStratNCETab.BackColor = FormRules.ColorTable.C(baseStrat.StratTD(total, soft).Strat(upcard))
                FillNumberTextBox(BaseStandBoxNCETab, baseStrat.StratTD(total, soft).StratStandEV(upcard), 13, False)
                FillNumberTextBox(BaseDoubleBoxNCETab, baseStrat.StratTD(total, soft).StratDEV(upcard), 13, False)
                FillNumberTextBox(BaseHitBoxNCETab, baseStrat.StratTD(total, soft).StratHitEV(upcard), 13, False)
                FillNumberTextBox(BaseSurrenderBoxNCETab, baseStrat.StratTD(total, soft).StratSurrEV(upcard), 13, False)

                ExStratNCETab.Text = Constants.StratShortText(Results.NCardExceptionsList.L(listIndex).Strat)
                ExStratNCETab.BackColor = FormRules.ColorTable.C(Results.NCardExceptionsList.L(listIndex).Strat)
                FillNumberTextBox(ExStandBoxNCETab, Results.NCardExceptionsList.L(listIndex).SEV, 13, False)
                FillNumberTextBox(ExDoubleBoxNCETab, Results.NCardExceptionsList.L(listIndex).DEV, 13, False)
                FillNumberTextBox(ExHitBoxNCETab, Results.NCardExceptionsList.L(listIndex).HEV, 13, False)
                FillNumberTextBox(ExSurrenderBoxNCETab, Results.NCardExceptionsList.L(listIndex).SurrEV, 13, False)
            End If
        End If
    End Sub

    Private Sub LoadHandListNCEForm()
        Dim listIndex As Integer
        Dim total As Integer
        Dim either As Boolean
        Dim soft As Integer
        Dim nCards As Integer
        Dim upcard As Integer
        Dim listHand As Boolean

        If TotalComboBoxNCETab.SelectedIndex = 0 Then
            total = 0
        Else
            total = TotalComboBoxNCETab.SelectedIndex + 3
        End If
        either = EitherCheckNCETab.Checked
        soft = SoftOnlyCheckNCETab.Checked
        If NCardsComboBoxNCETab.SelectedIndex = 0 Then
            nCards = 0
        Else
            nCards = NCardsComboBoxNCETab.SelectedIndex + 1
        End If
        upcard = UCComboBoxNCETab.SelectedIndex

        'First empty the box
        For listIndex = HandListBoxNCETab.Items.Count - 1 To 0 Step -1
            HandListBoxNCETab.Items.RemoveAt(listIndex)
        Next listIndex
        ReDim CurrentNCEList(0)

        'Then load the box
        For listIndex = 0 To Results.NCardExceptionsList.NumExceptions - 1
            listHand = False
            If upcard = 0 Or upcard = Results.NCardExceptionsList.L(listIndex).Upcard Then
                If total = 0 Or Results.NCardExceptionsList.L(listIndex).Total = total Then
                    If either Or Results.NCardExceptionsList.L(listIndex).Soft = soft Then
                        If nCards = 0 Or (Results.NCardExceptionsList.L(listIndex).NCards = nCards) Then
                            listHand = True
                        End If
                    End If
                End If
            End If
            If listHand Then
                Select Case ExTypeComboBoxNCETab.SelectedIndex
                    Case 0  'All Exceptions Allowed
                        'listHand = True
                    Case 1  'TD-TD Only
                        Select Case Results.NCardExceptionsList.L(listIndex).ExceptionType
                            Case BJCAGlobalsClass.ExType.TDTD  'CD-TD
                            Case Else
                                listHand = False
                        End Select
                    Case 2  'TC-TC Only
                        Select Case Results.NCardExceptionsList.L(listIndex).ExceptionType
                            Case BJCAGlobalsClass.ExType.TCTC  'TC-TC
                            Case Else
                                listHand = False
                        End Select
                    Case 3  'Forced-Forced Only
                        Select Case Results.NCardExceptionsList.L(listIndex).ExceptionType
                            Case BJCAGlobalsClass.ExType.ForcedForced  'Forced-Forced
                            Case Else
                                listHand = False
                        End Select
                End Select
            End If
            If listHand Then
                HandListBoxNCETab.Items.Add(Results.NCardExceptionsList.L(listIndex).ExceptionName)
                ReDim Preserve CurrentNCEList(HandListBoxNCETab.Items.Count)
                CurrentNCEList(HandListBoxNCETab.Items.Count - 1) = listIndex
            End If
        Next listIndex

        ListSizeBoxNCETab.Text = HandListBoxNCETab.Items.Count
        ClearHandNCEForm(False)
    End Sub

    Private Sub HandListBoxNCETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandListBoxNCETab.SelectedIndexChanged
        LoadHandNCEForm()
    End Sub

    Private Sub TotalComboBoxNCETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TotalComboBoxNCETab.SelectedIndexChanged
        LoadHandListNCEForm()
    End Sub

    Private Sub EitherCheckNCETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EitherCheckNCETab.CheckedChanged
        If EitherCheckNCETab.Checked Then
            HardOnlyCheckNCETab.Enabled = False
            SoftOnlyCheckNCETab.Enabled = False
            HardOnlyCheckNCETab.Checked = True
            SoftOnlyCheckNCETab.Checked = False
        Else
            HardOnlyCheckNCETab.Enabled = True
            SoftOnlyCheckNCETab.Enabled = True
        End If
        LoadHandListNCEForm()
    End Sub

    Private Sub HardOnlyCheckNCETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardOnlyCheckNCETab.CheckedChanged
        If HardOnlyCheckNCETab.Checked Then
            SoftOnlyCheckNCETab.Checked = False
        Else
            SoftOnlyCheckNCETab.Checked = True
        End If
        LoadHandListNCEForm()
    End Sub

    Private Sub SoftOnlyCheckNCETab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftOnlyCheckNCETab.CheckedChanged
        If SoftOnlyCheckNCETab.Checked Then
            HardOnlyCheckNCETab.Checked = False
        Else
            HardOnlyCheckNCETab.Checked = True
        End If
        LoadHandListNCEForm()
    End Sub

    Private Sub NCardsComboBoxNCETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NCardsComboBoxNCETab.SelectedIndexChanged
        LoadHandListNCEForm()
    End Sub

    Private Sub UCComboBoxNCETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UCComboBoxNCETab.SelectedIndexChanged
        LoadHandListNCEForm()
    End Sub

    Private Sub ExTypeComboBoxNCETab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExTypeComboBoxNCETab.SelectedIndexChanged
        LoadHandListNCEForm()
    End Sub

    Private Sub LoadExTypeComboBoxNCETab()
        ExTypeComboBoxNCETab.Items.Add("All")
        ExTypeComboBoxNCETab.Items.Add("TD-TD")
        ExTypeComboBoxNCETab.Items.Add("TC-TC")
        ExTypeComboBoxNCETab.Items.Add("Forced-Forced")

        ExTypeComboBoxNCETab.SelectedIndex = 0
    End Sub

    Private Function GetNCEFormCurrentForcedRule() As Boolean
        Dim listindex As Integer
        Dim ruleExists As Boolean

        ruleExists = True
        If HandListBoxNCETab.SelectedIndex >= 0 Then
            listindex = CurrentNCEList(HandListBoxNCETab.SelectedIndex)
        Else
            MsgBox("No exception is currently selected.")
            ruleExists = False
        End If

        If Not Results.NCardExceptionsList.L Is Nothing Then
            With ForcedRule
                .Name = Results.NCardExceptionsList.L(listindex).ExceptionName
                .RuleOn = False
                .UseSpecificHand = False

                .Hand.Empty()
                .Hand.Total = Results.NCardExceptionsList.L(listindex).Total
                .Hand.NumCards = Results.NCardExceptionsList.L(listindex).NCards
                .Hand.Soft = Results.NCardExceptionsList.L(listindex).Soft
                .ExactMatch = False

                .OrMore = False
                .OrLess = False

                .PreSplit = True
                .PostSplit = True

                .Upcard = Results.NCardExceptionsList.L(listindex).Upcard
                .Strat = Results.NCardExceptionsList.L(listindex).Strat
            End With
        Else
            MsgBox("There aren't any listed N card exceptions.")
            ruleExists = False
        End If

        Return ruleExists
    End Function

    Private Sub ExForcedButtonNCETab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExForcedButtonNCETab.Click
        If GetNCEFormCurrentForcedRule() Then
            AddForcedRule(False)
        End If
    End Sub

    Private Sub ExAllForcedButtonNCETab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExAllForcedButtonNCETab.Click
        Dim listIndex As Integer
        Dim exIndex As Integer

        For listIndex = 0 To HandListBoxNCETab.Items.Count - 1
            exIndex = CurrentNCEList(listIndex)

            If Not Results.NCardExceptionsList.L Is Nothing Then
                With ForcedRule
                    .Name = Results.NCardExceptionsList.L(exIndex).ExceptionName
                    .RuleOn = False
                    .UseSpecificHand = False

                    .Hand.Empty()
                    .Hand.Total = Results.NCardExceptionsList.L(exIndex).Total
                    .Hand.NumCards = Results.NCardExceptionsList.L(exIndex).NCards
                    .Hand.Soft = Results.NCardExceptionsList.L(exIndex).Soft
                    .ExactMatch = False

                    .OrMore = False
                    .OrLess = False

                    .PreSplit = True
                    .PostSplit = True

                    .Upcard = Results.NCardExceptionsList.L(exIndex).Upcard
                    .Strat = Results.NCardExceptionsList.L(exIndex).Strat
                End With

                AddForcedRule(False)
            End If
        Next listIndex

    End Sub

#End Region

#Region " Forced Tab "

#Region " Forced General "

    Private Sub RecalcForcedStrat()
        Dim row As Integer
        Dim card As Integer

        Try
            Results.Forced.NCardOn = False

            GetFormForcedTables()
            Results.ForcedTableRulesList = CloneObject(FormRules.ForcedStrat.ForcedTableRulesList)

            GetForcedRulesOn()
            Results.ForcedRulesList = CloneObject(FormRules.ForcedStrat.ForcedRulesList)

            Results.ForcednCD = CInt(ForcednCDBoxFSTab.Text)
            Results.ForcedTablePreSplit = ForcedTablePreCheckFSTab.Checked
            Results.ForcedTablePostSplit = ForcedTablePostCheckFSTab.Checked

            Results.Forced.NCardsCD = CInt(ForcednCDBoxFSTab.Text)
            Results.Forced.ComputeStrat = True

            Results.ApplyForcedRules()
            Results.ComputeStrategy(Results.Forced)
            Results.ComputeGameEVsStrat(Results.Forced)

            LoadFormStrategyTables()
            LoadFormSummaryPage()
            LoadFormSplitTable()

            LoadHandHAForm()
            LoadHandSizeAnalysisForm()

            'Empty the Forced Rules Box on the Rules Page
            ForcedRulesBoxRTab.Items.Clear()

            'Fill the Forced Rules Box
            row = 0
            For card = 0 To Results.ForcedRulesList.NumRules - 1
                If Results.ForcedRulesList.L(card).RuleOn Then
                    ForcedRulesBoxRTab.Items.Add(Results.ForcedRulesList.L(card).Name)
                    row += 1
                End If
            Next card
            For card = 0 To Results.ForcedTableRulesList.NumRules - 1
                If Results.ForcedTableRulesList.L(card).RuleOn Then
                    ForcedRulesBoxRTab.Items.Add(Results.ForcedTableRulesList.L(card).Name)
                    row += 1
                End If
            Next card
            If row = 0 Then
                ForcedRulesBoxRTab.Items.Add("None")
            End If
        Catch
            MsgBox("An error has occurred - please restart.")
            Me.Close()
        End Try

    End Sub

    Private Sub CalcNCardStrat()
        Dim row As Integer
        Dim card As Integer

        '        Try
        Results.ForcedTablePreSplit = True
        Results.ForcedTablePostSplit = True

        Results.ComputeNCardStrat()

        Results.ApplyForcedRules()
        Results.ComputeStrategy(Results.Forced)
        Results.ComputeGameEVsStrat(Results.Forced)

        FormRules.ForcedStrat.ForcednCD = Results.Forced.NCardsCD
        FormRules.ForcedStrat.ForcedTablePreSplit = Results.ForcedTablePreSplit
        FormRules.ForcedStrat.ForcedTablePostSplit = Results.ForcedTablePostSplit
        FormRules.ForcedStrat.ForcedTableRulesList = CloneObject(Results.ForcedTableRulesList)
        FormRules.ForcedStrat.ForcedRulesList = CloneObject(Results.ForcedRulesList)

        LoadFormForcedStrat()
        LoadFormStrategyTables()
        LoadFormSummaryPage()
        LoadFormSplitTable()

        LoadHandHAForm()
        LoadHandSizeAnalysisForm()

        'Clear the Forced strat values on the EOR page if the strategy has changed
        For card = 1 To 11
            If Not EORs(card - 1) Is Nothing Then
                EORs(card - 1).ForcedNetGameEV = 0
                EORs(card - 1).ForcedNetGameEVEOR = 0
            End If
            FillNumberTextBox(EOREVsArray(1, card - 1), 0, 2, True)
            FillNumberTextBox(EORNetEVsArray(1, card - 1), 0, 2, True)
        Next card
        EORs(11).ForcedNetGameEV = 0
        If Results.Forced.ComputeStrat Then FillNumberTextBox(ForcedEVBoxEORTab, Results.Forced.GameEVs.NetGameEV, 15, True)

        'Empty the Forced Rules Box on the Rules Page
        ForcedRulesBoxRTab.Items.Clear()

        'Fill the Forced Rules Box
        row = 0
        For card = 0 To Results.ForcedRulesList.NumRules - 1
            If Results.ForcedRulesList.L(card).RuleOn Then
                ForcedRulesBoxRTab.Items.Add(Results.ForcedRulesList.L(card).Name)
                row += 1
            End If
        Next card
        For card = 0 To Results.ForcedTableRulesList.NumRules - 1
            If Results.ForcedTableRulesList.L(card).RuleOn Then
                ForcedRulesBoxRTab.Items.Add(Results.ForcedTableRulesList.L(card).Name)
                row += 1
            End If
        Next card
        If row = 0 Then
            ForcedRulesBoxRTab.Items.Add("None")
        End If

        ForcedButtonTDHardSoftTab.Checked = True
        SummaryTab.Show()

        '        Catch
        '            MsgBox("An error has occurred - please restart.")
        '            Me.Close()
        '        End Try

    End Sub

    Private Sub CalcNCardStratButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalcNCardStratButtonFSTab.Click
        CalcNCardStrat()
    End Sub

    Private Sub RecalcForcedStratButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecalcForcedStratButtonFSTab.Click
        Dim card As Integer

        RecalcForcedStrat()
        'Clear the Forced strat values on the EOR page if the strategy has changed
        For card = 1 To 11
            If Not EORs(card - 1) Is Nothing Then
                EORs(card - 1).ForcedNetGameEV = 0
                EORs(card - 1).ForcedNetGameEVEOR = 0
            End If
            FillNumberTextBox(EOREVsArray(1, card - 1), 0, 2, True)
            FillNumberTextBox(EORNetEVsArray(1, card - 1), 0, 2, True)
        Next card
        EORs(11).ForcedNetGameEV = 0
        If Results.Forced.ComputeStrat Then FillNumberTextBox(ForcedEVBoxEORTab, Results.Forced.GameEVs.NetGameEV, 15, True)

        ForcedButtonCDHardTab.Checked = True
        SummaryTab.Show()
    End Sub

    Private Sub ForcednCardBoxFTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ForcednCDBoxFSTab.Validating
        If Not CheckValidInteger(ForcednCDBoxFSTab.Text, 0, 21, False) Then
            MsgBox("Please either enter 0 or a number between 2 and 21.", MsgBoxStyle.OKOnly)
            ForcednCDBoxFSTab.Text = 2
            e.Cancel = True
        ElseIf (ForcednCDBoxFSTab.Text > 0 And ForcednCDBoxFSTab.Text < 2) Then
            MsgBox("Please either enter 0 or a number between 2 and 21.", MsgBoxStyle.OKOnly)
            ForcednCDBoxFSTab.Text = 2
            e.Cancel = True
        End If
    End Sub

    Private Sub ForcedTablePreCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedTablePreCheckFSTab.CheckedChanged
        If Not ForcedTablePreCheckFSTab.Checked And Not ForcedTablePostCheckFSTab.Checked Then
            MsgBox("If neither the pre-split nor the post-split boxes are checked then the forced rules tables will be ignored.", MsgBoxStyle.OKOnly)
        End If
    End Sub

    Private Sub ForcedTablePostCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedTablePostCheckFSTab.CheckedChanged
        If Not ForcedTablePreCheckFSTab.Checked And Not ForcedTablePostCheckFSTab.Checked Then
            MsgBox("If neither the pre-split nor the post-split boxes are checked then the forced rules tables will be ignored.", MsgBoxStyle.OKOnly)
        End If
    End Sub

#End Region

#Region " Forced Tables "

    Private Sub PopulateForcedTableUpcardLabels()
        PopulateUpcardLabels(HardTDGroupFSTab, 56, 24, 28)
        PopulateUpcardLabels(SoftTDGroupFSTab, 56, 24, 28)
        PopulateUpcardLabels(SoftCDGroupFSTab, 56, 24, 28)
        PopulateUpcardLabels(PairCDGroupFSTab, 56, 24, 28)
        PopulateUpcardLabels(HardCDTabFSTab, 88, 16, 28)
        PopulateUpcardLabels(HardCDTabFSTab, 504, 16, 28)
    End Sub

    Private Sub PopulateHardTDLabels()
        Dim total As Integer

        For total = 5 To 21
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(40, 20)
            label.Location = New System.Drawing.Point(8, 48 + (total - 5) * 24)
            label.Text = total
            label.Index = total - 5
            HardTDGroupFSTab.Controls.Add(label)
            HardTDForcedTableLabelArray(total - 5) = label

            'Add Handler to the general handler
            AddHandler label.Click, AddressOf HardTDForcedTableLabelArrayHandler_Click
        Next
    End Sub

    Private Sub PopulateSoftTDLabels()
        Dim total As Integer

        For total = 13 To 21
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(40, 20)
            label.Location = New System.Drawing.Point(8, 48 + (total - 13) * 24)
            label.Text = total
            label.Index = total - 13
            SoftTDGroupFSTab.Controls.Add(label)
            SoftTDForcedTableLabelArray(total - 13) = label

            'Add Handler to the general handler
            AddHandler label.Click, AddressOf SoftTDForcedTableLabelArrayHandler_Click
        Next
    End Sub

    Private Sub PopulateHardCDLabels()
        Dim row As Integer
        Dim total As Integer
        Dim card As Integer
        Dim card2 As Integer

        row = 0
        For total = 5 To 19
            If total < 13 Then
                card = 2
            Else
                card = total - 10
            End If
            card2 = total - card
            Do
                Dim label As New IndexedLabel
                label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                label.Size = New System.Drawing.Size(64, 20)
                If card2 = 10 Then
                    label.Text = CStr(card) + ", T  (" + CStr(card + card2) + ")"
                ElseIf (card + card2) < 10 Then
                    label.Text = CStr(card) + ", " + CStr(card2) + "   (" + CStr(card + card2) + ")"
                Else
                    label.Text = CStr(card) + ", " + CStr(card2) + "  (" + CStr(card + card2) + ")"
                End If

                If row < 18 Then
                    label.Location = New System.Drawing.Point(8, 40 + row * 24)
                Else
                    label.Location = New System.Drawing.Point(424, 40 + (row - 18) * 24)
                End If

                label.Index = row
                HardCDTabFSTab.Controls.Add(label)
                HardCDForcedTableLabelArray(row) = label

                'Add Handler to the general handler
                AddHandler label.Click, AddressOf HardCDForcedTableLabelArrayHandler_Click

                row = row + 1
                card = card + 1
                card2 = card2 - 1
            Loop Until (card >= card2)
        Next total
    End Sub

    Private Sub PopulateSoftCDLabels()
        Dim card As Integer

        For card = 2 To 10
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(40, 20)
            If card = 10 Then
                label.Text = "A, T"
            Else
                label.Text = "A, " + CStr(card)
            End If

            label.Location = New System.Drawing.Point(8, 48 + (card - 2) * 24)
            label.Index = card - 2
            SoftCDGroupFSTab.Controls.Add(label)
            SoftCDForcedTableLabelArray(card - 2) = label

            'Add Handler to the general handler
            AddHandler label.Click, AddressOf SoftCDForcedTableLabelArrayHandler_Click

        Next card
    End Sub

    Private Sub PopulatePairCDLabels()
        Dim card As Integer

        For card = 1 To 10
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(40, 20)
            If card = 1 Then
                label.Text = "A, A"
            ElseIf card = 10 Then
                label.Text = "T, T"
            Else
                label.Text = CStr(card) + ", " + CStr(card)
            End If

            label.Location = New System.Drawing.Point(8, 48 + (card - 1) * 24)
            label.Index = card - 1
            PairCDGroupFSTab.Controls.Add(label)
            PairCDForcedTableLabelArray(card - 1) = label

            'Add Handler to the general handler
            AddHandler label.Click, AddressOf PairCDForcedTableLabelArrayHandler_Click

        Next card
    End Sub

    Private Sub HardTDForcedTableLabelArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardTDForcedTableLabelArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = HardTDGroupFSTab
        HardTDGroupFSTab.Controls.Add(HardTDLabelComboboxFSTab)

        HardTDLabelComboboxFSTab.Items.Clear()
        HardTDLabelComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "xS", "xD", "xH", "xR"})
        HardTDLabelComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedLabel).Location.X + 35, DirectCast(sender, IndexedLabel).Location.Y - 24)
        HardTDLabelComboboxFSTab.Text = Constants.StratLongText(0)
        HardTDLabelComboboxFSTab.Visible = True
        HardTDLabelComboboxFSTab.DroppedDown = True
        HardTDLabelComboboxFSTab.Focus()
    End Sub

    Private Sub SoftTDForcedTableLabelArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftTDForcedTableLabelArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = SoftTDGroupFSTab
        SoftTDGroupFSTab.Controls.Add(SoftTDLabelComboboxFSTab)

        SoftTDLabelComboboxFSTab.Items.Clear()
        SoftTDLabelComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "xS", "xD", "xH", "xR"})
        SoftTDLabelComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedLabel).Location.X + 35, DirectCast(sender, IndexedLabel).Location.Y - 24)
        SoftTDLabelComboboxFSTab.Text = Constants.StratLongText(0)
        SoftTDLabelComboboxFSTab.Visible = True
        SoftTDLabelComboboxFSTab.DroppedDown = True
        SoftTDLabelComboboxFSTab.Focus()
    End Sub

    Private Sub HardCDForcedTableLabelArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardCDForcedTableLabelArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = HardCDTabFSTab
        HardCDTabFSTab.Controls.Add(HardCDLabelComboboxFSTab)

        HardCDLabelComboboxFSTab.Items.Clear()
        HardCDLabelComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "xS", "xD", "xH", "xR"})
        HardCDLabelComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedLabel).Location.X + 53, DirectCast(sender, IndexedLabel).Location.Y - 24)
        HardCDLabelComboboxFSTab.Text = Constants.StratLongText(0)
        HardCDLabelComboboxFSTab.Visible = True
        HardCDLabelComboboxFSTab.DroppedDown = True
        HardCDLabelComboboxFSTab.Focus()
    End Sub

    Private Sub SoftCDForcedTableLabelArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftCDForcedTableLabelArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = SoftCDGroupFSTab
        SoftCDGroupFSTab.Controls.Add(SoftCDLabelComboboxFSTab)

        SoftCDLabelComboboxFSTab.Items.Clear()
        SoftCDLabelComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "xS", "xD", "xH", "xR"})
        SoftCDLabelComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedLabel).Location.X + 35, DirectCast(sender, IndexedLabel).Location.Y - 24)
        SoftCDLabelComboboxFSTab.Text = Constants.StratLongText(0)
        SoftCDLabelComboboxFSTab.Visible = True
        SoftCDLabelComboboxFSTab.DroppedDown = True
        SoftCDLabelComboboxFSTab.Focus()
    End Sub

    Private Sub PairCDForcedTableLabelArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PairCDForcedTableLabelArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = PairCDGroupFSTab
        PairCDGroupFSTab.Controls.Add(PairCDLabelComboboxFSTab)

        PairCDLabelComboboxFSTab.Items.Clear()
        PairCDLabelComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "P", "PS", "PD", "PH", "xS", "xD", "xH", "xR", "xP"})
        PairCDLabelComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedLabel).Location.X + 35, DirectCast(sender, IndexedLabel).Location.Y - 24)
        PairCDLabelComboboxFSTab.Text = Constants.StratLongText(0)
        PairCDLabelComboboxFSTab.Visible = True
        PairCDLabelComboboxFSTab.DroppedDown = True
        PairCDLabelComboboxFSTab.Focus()
    End Sub

    Private Sub HardTDLabelComboboxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardTDLabelComboboxFSTab.SelectedIndexChanged
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If HardTDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If HardTDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                HardTDForcedTableArray(rowIndex, upcard).Index = index
                HardTDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                HardTDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        HardTDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub HardTDLabelComboboxFSTab_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardTDLabelComboboxFSTab.DropDown
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If HardTDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If HardTDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                HardTDForcedTableArray(rowIndex, upcard).Index = index
                HardTDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                HardTDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        HardTDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub SoftTDLabelComboboxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftTDLabelComboboxFSTab.SelectedIndexChanged
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If SoftTDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If SoftTDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                SoftTDForcedTableArray(rowIndex, upcard).Index = index
                SoftTDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                SoftTDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        SoftTDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub SoftTDLabelComboboxFSTab_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftTDLabelComboboxFSTab.DropDown
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If SoftTDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If SoftTDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                SoftTDForcedTableArray(rowIndex, upcard).Index = index
                SoftTDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                SoftTDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        SoftTDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub HardCDLabelComboboxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardCDLabelComboboxFSTab.SelectedIndexChanged
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If HardCDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If HardCDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                HardCDForcedTableArray(rowIndex, upcard).Index = index
                HardCDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                HardCDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        HardCDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub HardCDLabelComboboxFSTab_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardCDLabelComboboxFSTab.DropDown
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If HardCDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If HardCDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                HardCDForcedTableArray(rowIndex, upcard).Index = index
                HardCDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                HardCDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        HardCDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub SoftCDLabelComboboxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftCDLabelComboboxFSTab.SelectedIndexChanged
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If SoftCDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If SoftCDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                SoftCDForcedTableArray(rowIndex, upcard).Index = index
                SoftCDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                SoftCDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        SoftCDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub SoftCDLabelComboboxFSTab_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftCDLabelComboboxFSTab.DropDown
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If SoftCDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If SoftCDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                SoftCDForcedTableArray(rowIndex, upcard).Index = index
                SoftCDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                SoftCDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        SoftCDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub PairCDLabelComboboxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PairCDLabelComboboxFSTab.SelectedIndexChanged
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If PairCDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If PairCDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                PairCDForcedTableArray(rowIndex, upcard).Index = index
                PairCDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                PairCDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        PairCDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub PairCDLabelComboboxFSTab_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PairCDLabelComboboxFSTab.DropDown
        Dim index As Integer
        Dim rowIndex As Integer
        Dim upcard As Integer

        If PairCDLabelComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If PairCDLabelComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            rowIndex = DirectCast(PreviousObject, IndexedLabel).Index
            For upcard = 0 To 9
                PairCDForcedTableArray(rowIndex, upcard).Index = index
                PairCDForcedTableArray(rowIndex, upcard).Text = Constants.StratShortText(index)
                PairCDForcedTableArray(rowIndex, upcard).BackColor = FormRules.ColorTable.C(index)
            Next upcard
        End If
        PairCDLabelComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub PopulateHardTDTable()
        Dim total As Integer
        Dim upcard As Integer

        For total = 5 To 21
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(28, 20)
                box.ReadOnly = True
                box.Index = 0
                box.Text = Constants.StratShortText(box.Index)
                box.BackColor = FormRules.ColorTable.C(box.Index)
                box.Hand.Total = total
                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(56 + 9 * 28, 48 + (total - 5) * 24)
                Else
                    box.Location = New System.Drawing.Point(56 + (upcard - 1) * 28, 48 + (total - 5) * 24)
                End If

                HardTDGroupFSTab.Controls.Add(box)
                HardTDForcedTableArray(total - 5, upcard) = box

                'Add Handler to the general handler
                AddHandler box.Click, AddressOf HardTDForcedTableArrayHandler_Click
            Next upcard
        Next total
    End Sub

    Private Sub PopulateSoftTDTable()
        Dim total As Integer
        Dim upcard As Integer

        'Populate Soft TD Group
        For total = 13 To 21
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(28, 20)
                box.ReadOnly = True
                box.Index = 0
                box.Text = Constants.StratShortText(box.Index)
                box.BackColor = FormRules.ColorTable.C(box.Index)
                box.Hand.Total = total
                box.Hand.Soft = True
                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(56 + 9 * 28, 48 + (total - 13) * 24)
                Else
                    box.Location = New System.Drawing.Point(56 + (upcard - 1) * 28, 48 + (total - 13) * 24)
                End If

                SoftTDGroupFSTab.Controls.Add(box)
                SoftTDForcedTableArray(total - 13, upcard) = box

                'Add Handler to the general handler
                AddHandler box.Click, AddressOf SoftTDForcedTableArrayHandler_Click
            Next upcard
        Next
    End Sub

    Private Sub PopulateHardCDTable()
        Dim row As Integer
        Dim column As Integer
        Dim total As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim card2 As Integer

        row = 0
        column = 88
        For total = 5 To 19
            If total < 13 Then
                card = 2
            Else
                card = total - 10
            End If
            card2 = total - card
            Do
                For upcard = 0 To 9
                    Dim box As New IndexedTextBox

                    If row = 18 Then
                        row -= 18
                        column = 504
                    End If
                    box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                    box.Size = New System.Drawing.Size(28, 20)
                    box.ReadOnly = True
                    box.Index = 0
                    box.Text = Constants.StratShortText(box.Index)
                    box.BackColor = FormRules.ColorTable.C(box.Index)
                    box.Hand.Cards(card) = 1
                    box.Hand.Cards(card2) = 1
                    box.Hand.UpdateTotal()
                    If upcard = 0 Then
                        box.Location = New System.Drawing.Point(column + 9 * 28, 40 + row * 24)
                    Else
                        box.Location = New System.Drawing.Point(column + (upcard - 1) * 28, 40 + row * 24)
                    End If

                    HardCDTabFSTab.Controls.Add(box)
                    If column = 88 Then
                        HardCDForcedTableArray(row, upcard) = box
                    Else
                        HardCDForcedTableArray(row + 18, upcard) = box
                    End If

                    'Add Handler to the general handler
                    AddHandler box.Click, AddressOf HardCDForcedTableArrayHandler_Click
                Next upcard
                row = row + 1
                card = card + 1
                card2 = card2 - 1
            Loop Until (card >= card2)
        Next
    End Sub

    Private Sub PopulateSoftPairCDTables()
        Dim total As Integer
        Dim upcard As Integer

        'Populate Soft CD Group
        For total = 13 To 21
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(28, 20)
                box.ReadOnly = True
                box.Index = 0
                box.Text = Constants.StratShortText(box.Index)
                box.BackColor = FormRules.ColorTable.C(box.Index)
                box.Hand.Cards(1) = 1
                box.Hand.Cards(total - 11) = 1
                box.Hand.UpdateTotal()
                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(56 + 9 * 28, 48 + (total - 13) * 24)
                Else
                    box.Location = New System.Drawing.Point(56 + (upcard - 1) * 28, 48 + (total - 13) * 24)
                End If

                SoftCDGroupFSTab.Controls.Add(box)
                SoftCDForcedTableArray(total - 13, upcard) = box

                'Add Handler to the general handler
                AddHandler box.Click, AddressOf SoftCDForcedTableArrayHandler_Click
            Next upcard
        Next

        'Populate Pair CD Group
        For total = 1 To 10
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(28, 20)
                box.ReadOnly = True
                box.Index = 0
                box.Text = Constants.StratShortText(box.Index)
                box.BackColor = FormRules.ColorTable.C(box.Index)
                box.Hand.Cards(total) = 2
                box.Hand.UpdateTotal()
                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(56 + 9 * 28, 48 + (total - 1) * 24)
                Else
                    box.Location = New System.Drawing.Point(56 + (upcard - 1) * 28, 48 + (total - 1) * 24)
                End If

                PairCDGroupFSTab.Controls.Add(box)
                PairCDForcedTableArray(total - 1, upcard) = box

                'Add Handler to the general handler
                AddHandler box.Click, AddressOf PairCDForcedTableArrayHandler_Click
            Next upcard
        Next
    End Sub

    Private Sub HardTDForcedTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardTDForcedTableArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = HardTDGroupFSTab
        HardTDGroupFSTab.Controls.Add(ForcedTableComboboxFSTab)

        ForcedTableComboboxFSTab.Items.Clear()
        ForcedTableComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "xS", "xD", "xH", "xR"})
        ForcedTableComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedTextBox).Location.X + 24, DirectCast(sender, IndexedTextBox).Location.Y - 21)
        ForcedTableComboboxFSTab.Text = Constants.StratLongText(DirectCast(sender, IndexedTextBox).Index)
        ForcedTableComboboxFSTab.Visible = True
        ForcedTableComboboxFSTab.DroppedDown = True
        ForcedTableComboboxFSTab.Focus()
    End Sub

    Private Sub SoftTDForcedTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftTDForcedTableArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = SoftTDGroupFSTab
        SoftTDGroupFSTab.Controls.Add(ForcedTableComboboxFSTab)

        ForcedTableComboboxFSTab.Items.Clear()
        ForcedTableComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "xS", "xD", "xH", "xR"})
        ForcedTableComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedTextBox).Location.X + 24, DirectCast(sender, IndexedTextBox).Location.Y - 21)
        ForcedTableComboboxFSTab.Text = Constants.StratLongText(DirectCast(sender, IndexedTextBox).Index)
        ForcedTableComboboxFSTab.Visible = True
        ForcedTableComboboxFSTab.DroppedDown = True
        ForcedTableComboboxFSTab.Focus()

    End Sub

    Private Sub HardCDForcedTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardCDForcedTableArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = HardCDTabFSTab
        HardCDTabFSTab.Controls.Add(ForcedTableComboboxFSTab)

        ForcedTableComboboxFSTab.Items.Clear()
        ForcedTableComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "xS", "xD", "xH", "xR"})
        ForcedTableComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedTextBox).Location.X + 24, DirectCast(sender, IndexedTextBox).Location.Y - 21)
        ForcedTableComboboxFSTab.Text = Constants.StratLongText(DirectCast(sender, IndexedTextBox).Index)
        ForcedTableComboboxFSTab.Visible = True
        ForcedTableComboboxFSTab.DroppedDown = True
        ForcedTableComboboxFSTab.Focus()
    End Sub

    Private Sub SoftCDForcedTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftCDForcedTableArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = SoftCDGroupFSTab

        SoftCDGroupFSTab.Controls.Add(ForcedTableComboboxFSTab)
        ForcedTableComboboxFSTab.Items.Clear()
        ForcedTableComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "xS", "xD", "xH", "xR"})
        ForcedTableComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedTextBox).Location.X + 24, DirectCast(sender, IndexedTextBox).Location.Y - 21)
        ForcedTableComboboxFSTab.Text = Constants.StratLongText(DirectCast(sender, IndexedTextBox).Index)
        ForcedTableComboboxFSTab.Visible = True
        ForcedTableComboboxFSTab.DroppedDown = True
        ForcedTableComboboxFSTab.Focus()

    End Sub

    Private Sub PairCDForcedTableArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PairCDForcedTableArrayHandler.Click
        PreviousObject = sender
        PreviousGroup = PairCDGroupFSTab

        PairCDGroupFSTab.Controls.Add(ForcedTableComboboxFSTab)
        ForcedTableComboboxFSTab.Items.Clear()
        ForcedTableComboboxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "P", "PS", "PD", "PH", "xS", "xD", "xH", "xR", "xP"})
        ForcedTableComboboxFSTab.Location = New System.Drawing.Point(DirectCast(sender, IndexedTextBox).Location.X + 24, DirectCast(sender, IndexedTextBox).Location.Y - 21)
        ForcedTableComboboxFSTab.Text = Constants.StratLongText(DirectCast(sender, IndexedTextBox).Index)
        ForcedTableComboboxFSTab.Visible = True
        ForcedTableComboboxFSTab.DroppedDown = True
        ForcedTableComboboxFSTab.Focus()

    End Sub

    Private Sub ForcedTableComboboxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedTableComboboxFSTab.SelectedIndexChanged
        Dim index As Integer

        If ForcedTableComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If ForcedTableComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            PreviousObject.Index = index
            PreviousObject.Text = Constants.StratShortText(index)
            PreviousObject.BackColor = FormRules.ColorTable.C(index)
        End If
        ForcedTableComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub ForcedTableComboboxFSTab_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedTableComboboxFSTab.DropDown
        Dim index As Integer

        If ForcedTableComboboxFSTab.SelectedIndex > -1 Then
            For index = 0 To Constants.NumStrats - 1
                If ForcedTableComboboxFSTab.Text = Constants.StratShortText(index) Then
                    Exit For
                End If
            Next index
            PreviousObject.Index = index
            PreviousObject.Text = Constants.StratShortText(index)
            PreviousObject.BackColor = FormRules.ColorTable.C(index)
        End If
        ForcedTableComboboxFSTab.Visible = False
        PreviousGroup.Focus()
    End Sub

    Private Sub ClearForcedTablesButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearForcedTablesButtonFSTab.Click
        If MsgBox("Are you sure you would like to clear the Forced Strategy Tables?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            ClearForcedTables()
        End If
    End Sub

    Private Sub ClearForcedTables()
        Dim row As Integer
        Dim upcard As Integer

        For upcard = 0 To 9 Step 1
            'Clear Hard TD Table
            For row = 0 To 16
                HardTDForcedTableArray(row, upcard).Index = 0
                HardTDForcedTableArray(row, upcard).Text = Constants.StratShortText(0)
                HardTDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(0)
            Next row
            'Clear Hard CD Table
            For row = 0 To 35
                HardCDForcedTableArray(row, upcard).Index = 0
                HardCDForcedTableArray(row, upcard).Text = Constants.StratShortText(0)
                HardCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(0)
            Next row
            'Clear Soft Tables
            For row = 0 To 8
                SoftTDForcedTableArray(row, upcard).Index = 0
                SoftTDForcedTableArray(row, upcard).Text = Constants.StratShortText(0)
                SoftTDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(0)

                SoftCDForcedTableArray(row, upcard).Index = 0
                SoftCDForcedTableArray(row, upcard).Text = Constants.StratShortText(0)
                SoftCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(0)
            Next row
            'Clear Pair Tables
            For row = 0 To 9
                PairCDForcedTableArray(row, upcard).Index = 0
                PairCDForcedTableArray(row, upcard).Text = Constants.StratShortText(0)
                PairCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(0)
            Next row
        Next upcard
    End Sub

    Private Sub GetFormForcedTables()
        Dim row As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim UCStr As String
        Dim tempForcedRule As New BJCAForcedRulesClass

        FormRules.ForcedStrat.ForcedTableRulesList.Empty()

        tempForcedRule.ExactMatch = True
        tempForcedRule.RuleOn = True
        tempForcedRule.PreSplit = ForcedTablePreCheckFSTab.Checked
        tempForcedRule.PostSplit = ForcedTablePostCheckFSTab.Checked

        For upcard = 0 To 9 Step 1
            tempForcedRule.Upcard = upcard + 1
            Select Case upcard
                Case 0
                    UCStr = "A"
                Case 9
                    UCStr = "T"
                Case Else
                    UCStr = CStr(upcard + 1)
            End Select
            'Get Hard TD Table
            For row = 0 To 16
                If HardTDForcedTableArray(row, upcard).Index > 0 Then
                    tempForcedRule.Hand = CloneObject(HardTDForcedTableArray(row, upcard).Hand)
                    tempForcedRule.Strat = HardTDForcedTableArray(row, upcard).Index
                    tempForcedRule.UseSpecificHand = False
                    tempForcedRule.Name = "H" + CStr(tempForcedRule.Hand.Total) + " vs " + UCStr + " " + Constants.StratLongText(tempForcedRule.Strat)
                    FormRules.ForcedStrat.ForcedTableRulesList.AddForcedRule(tempForcedRule)
                End If
            Next row
            'Get Hard CD Table
            For row = 0 To 35
                If HardCDForcedTableArray(row, upcard).Index > 0 Then
                    tempForcedRule.Hand = CloneObject(HardCDForcedTableArray(row, upcard).Hand)
                    tempForcedRule.Strat = HardCDForcedTableArray(row, upcard).Index
                    tempForcedRule.UseSpecificHand = True
                    tempForcedRule.Name = ""
                    For card = 2 To 10
                        If tempForcedRule.Hand.Cards(card) > 0 Then
                            If card = 10 Then
                                tempForcedRule.Name += "T"
                            Else
                                tempForcedRule.Name += CStr(card)
                            End If
                        End If
                    Next card
                    tempForcedRule.Name += " vs " + UCStr + " " + Constants.StratLongText(tempForcedRule.Strat)
                    FormRules.ForcedStrat.ForcedTableRulesList.AddForcedRule(tempForcedRule)
                End If
            Next row
            'Get Soft Tables
            For row = 0 To 8
                If SoftTDForcedTableArray(row, upcard).Index > 0 Then
                    tempForcedRule.Hand = CloneObject(SoftTDForcedTableArray(row, upcard).Hand)
                    tempForcedRule.Strat = SoftTDForcedTableArray(row, upcard).Index
                    tempForcedRule.UseSpecificHand = False
                    tempForcedRule.Name = "S" + CStr(tempForcedRule.Hand.Total) + " vs " + UCStr + " " + Constants.StratLongText(tempForcedRule.Strat)
                    FormRules.ForcedStrat.ForcedTableRulesList.AddForcedRule(tempForcedRule)
                End If

                If SoftCDForcedTableArray(row, upcard).Index > 0 Then
                    tempForcedRule.Hand = CloneObject(SoftCDForcedTableArray(row, upcard).Hand)
                    tempForcedRule.Strat = SoftCDForcedTableArray(row, upcard).Index
                    tempForcedRule.UseSpecificHand = True
                    tempForcedRule.Name = "A" + CStr(tempForcedRule.Hand.Total - 11) + " vs " + UCStr + " " + Constants.StratLongText(tempForcedRule.Strat)
                    FormRules.ForcedStrat.ForcedTableRulesList.AddForcedRule(tempForcedRule)
                End If
            Next row
            'Get Pair Tables
            For row = 0 To 9
                If PairCDForcedTableArray(row, upcard).Index > 0 Then
                    tempForcedRule.Hand = CloneObject(PairCDForcedTableArray(row, upcard).Hand)
                    tempForcedRule.Strat = PairCDForcedTableArray(row, upcard).Index
                    tempForcedRule.UseSpecificHand = True
                    If (row = 0 Or row = 1) And tempForcedRule.Strat = Constants.Strat.P Then
                        tempForcedRule.Strat = Constants.Strat.PH
                    End If
                    Select Case row
                        Case 0
                            tempForcedRule.Name = "AA vs " + UCStr + " " + Constants.StratLongText(tempForcedRule.Strat)
                        Case 9
                            tempForcedRule.Name = "TT vs " + UCStr + " " + Constants.StratLongText(tempForcedRule.Strat)
                        Case Else
                            tempForcedRule.Name = CStr(row + 1) + CStr(row + 1) + " vs " + UCStr + " " + Constants.StratLongText(tempForcedRule.Strat)
                    End Select
                    FormRules.ForcedStrat.ForcedTableRulesList.AddForcedRule(tempForcedRule)
                End If
            Next row
        Next upcard
    End Sub

    Private Sub LoadFormForcedTables()
        Dim rule As Integer
        Dim card As Integer
        Dim row As Integer
        Dim pair As Boolean

        ClearForcedTables()
        For rule = 0 To FormRules.ForcedStrat.ForcedTableRulesList.NumRules - 1
            Select Case FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.NumCards
                Case 0      'TD Strat
                    If Not FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Soft Then
                        HardTDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 5, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Index = FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat
                        HardTDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 5, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Text = Constants.StratShortText(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                        HardTDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 5, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).BackColor = FormRules.ColorTable.C(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                    Else
                        SoftTDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 13, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Index = FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat
                        SoftTDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 13, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Text = Constants.StratShortText(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                        SoftTDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 13, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).BackColor = FormRules.ColorTable.C(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                    End If
                Case 2
                    If Not FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Soft Or FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Cards(1) = 2 Then
                        pair = False
                        For card = 1 To 10
                            If FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Cards(card) = 2 Then
                                pair = True
                                Exit For
                            End If
                        Next
                        If Not pair Then
                            For row = 0 To 35
                                If FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.SameAs(HardCDForcedTableArray(row, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Hand) Then
                                    HardCDForcedTableArray(row, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Index = FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat
                                    HardCDForcedTableArray(row, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Text = Constants.StratShortText(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                                    HardCDForcedTableArray(row, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).BackColor = FormRules.ColorTable.C(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                                    Exit For
                                End If
                            Next
                        Else
                            PairCDForcedTableArray(card - 1, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Index = FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat
                            PairCDForcedTableArray(card - 1, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Text = Constants.StratShortText(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                            PairCDForcedTableArray(card - 1, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).BackColor = FormRules.ColorTable.C(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                        End If
                    Else
                        SoftCDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 13, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Index = FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat
                        SoftCDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 13, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).Text = Constants.StratShortText(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                        SoftCDForcedTableArray(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Hand.Total - 13, FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Upcard - 1).BackColor = FormRules.ColorTable.C(FormRules.ForcedStrat.ForcedTableRulesList.L(rule).Strat)
                    End If
            End Select
        Next rule
    End Sub

    Private Sub SaveForcedTablesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveForcedTablesButtonFSTab.Click
        SaveForcedTablesFile()
    End Sub

    Private Sub LoadForcedTablesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadForcedTablesButtonFSTab.Click
        LoadForcedTablesFile()
    End Sub

    Private Sub SaveForcedTablesFile()
        Dim sfd As New SaveFileDialog
        Dim forcedtable As New BJCAForcedRulesTableClass

        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = Filenames.ForcedTablesFileExt
        sfd.FileName = GetFileName(Filenames.ForcedTablesFileName)
        sfd.InitialDirectory = Filenames.DefaultPath
        sfd.Filter = ("Forced Tables Files (*" + Filenames.ForcedTablesFileExt + ")|*" + Filenames.ForcedTablesFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            GetFormForcedTables()
            forcedtable.ForcedTableRulesList = CloneObject(FormRules.ForcedStrat.ForcedTableRulesList)
            forcedtable.ForcednCD = CInt(ForcednCDBoxFSTab.Text)
            forcedtable.ForcedTablePreSplit = ForcedTablePreCheckFSTab.Checked
            forcedtable.ForcedTablePostSplit = ForcedTablePostCheckFSTab.Checked
            SaveObjectFile(sfd.FileName, forcedtable)
            Filenames.ForcedTablesFileName = sfd.FileName
            Filenames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadForcedTablesFile(Optional ByVal getName As Boolean = True)
        Dim forcedtable As BJCAForcedRulesTableClass
        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                FormRules.ForcedStrat.ForcedTableRulesList = CType(LoadObjectFile(Filenames.ForcedTablesFileName), BJCAForcedRulesListClass)
                LoadFormForcedTables()
            Catch
                MsgBox("The file: " + GetFileName(Filenames.ForcedTablesFileName) + " is not a valid Forced Tables file.")
                RestoreDefaultForcedTables()
            Finally
            End Try
        Else
            Dim ofd As New OpenFileDialog
            Dim fileOK As Boolean

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = Filenames.ForcedTablesFileExt
            ofd.FileName = GetFileName(Filenames.ForcedTablesFileName)
            ofd.InitialDirectory = Filenames.DefaultPath
            ofd.Filter = ("Forced Tables Files (*" + Filenames.ForcedTablesFileExt + ")|*" + Filenames.ForcedTablesFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    forcedtable = CType(LoadObjectFile(ofd.FileName), BJCAForcedRulesTableClass)
                    FormRules.ForcedStrat.ForcedTableRulesList = CloneObject(forcedtable.ForcedTableRulesList)
                    ForcednCDBoxFSTab.Text = forcedtable.ForcednCD
                    ForcedTablePreCheckFSTab.Checked = forcedtable.ForcedTablePreSplit
                    ForcedTablePostCheckFSTab.Checked = forcedtable.ForcedTablePostSplit
                    Filenames.ForcedTablesFileName = ofd.FileName
                    Filenames.DefaultPath = GetPath(ofd.FileName)
                    fileOK = True
                Catch
                    MsgBox("The file: " + GetFileName(ofd.FileName) + " is not a valid Forced Tables file.")
                    fileOK = False
                End Try
                If fileOK Then
                    LoadFormForcedTables()
                End If
            End If
            ofd.Dispose()
        End If
    End Sub

    Private Sub RestoreDefaultForcedTables()
        FormRules.ForcedStrat.ForcedTableRulesList.Empty()
        Filenames.ForcedTablesFileName = "Default"
        LoadFormForcedTables()
    End Sub

    Private Sub CopyTDButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyTDButtonFSTab.Click
        Dim upcard As Integer
        Dim total As Integer

        If MsgBox("Are you sure you would like to copy the current TD strategies from the Strategy Tables?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            For total = 5 To 21
                For upcard = 0 To 9
                    CopyIndexedTextBoxContents(HardTDStratTableArray(total - 4, upcard), HardTDForcedTableArray(total - 5, upcard))
                Next upcard
            Next total
            For total = 0 To 8
                For upcard = 0 To 9
                    CopyIndexedTextBoxContents(SoftTDStratTableArray(total + 1, upcard), SoftTDForcedTableArray(total, upcard))
                Next upcard
            Next total
        End If
    End Sub

    Private Sub CopyCDButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyCDButtonFSTab.Click
        Dim upcard As Integer
        Dim total As Integer

        If MsgBox("Are you sure you would like to copy the current CD strategies from the Strategy Tables?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            For total = 0 To 35
                For upcard = 0 To 9
                    CopyIndexedTextBoxContents(HardCDStratTableArray(total, upcard), HardCDForcedTableArray(total, upcard))
                Next upcard
            Next total

            For total = 0 To 8
                For upcard = 0 To 9
                    CopyIndexedTextBoxContents(SoftCDStratTableArray(total, upcard), SoftCDForcedTableArray(total, upcard))
                Next upcard
            Next total
        End If
    End Sub

    Private Sub CopyPairsButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyPairsButtonFSTab.Click
        Dim upcard As Integer
        Dim total As Integer

        If MsgBox("Are you sure you would like to copy the current Pairs strategies from the Strategy Tables?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            For total = 0 To 9
                For upcard = 0 To 9
                    CopyIndexedTextBoxContents(PairCDStratTableArray(total, upcard), PairCDForcedTableArray(total, upcard))
                Next upcard
            Next total
        End If
    End Sub

#End Region

#Region " Forced Rules "

    Private Sub PopulateForcedRulesHandRulesTable()
        Dim column As Integer

        For column = 0 To 9 Step 1
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Location = New System.Drawing.Point(288 + 32 * column, 120)
            box.Size = New System.Drawing.Size(32, 20)
            box.Index = column

            Me.ForcedRulesHandArray(column) = box
            Me.ForcedRuleDetailsGroupFSTab.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.Validating, AddressOf ForcedRulesHandArrayHandler_Validating

        Next column
    End Sub

    Private Sub ForcedRulesHandArrayHandler_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ForcedRulesHandArrayHandler.Validating
        Dim i As Integer
        Dim tempTotal As Integer
        Dim tempSoft As Boolean
        Dim tempNumCards As Integer
        Dim tempNum As Integer

        If Not CheckValidInteger(DirectCast(sender, TextBox).Text, 0, Constants.MaxHandNumCards(DirectCast(sender, IndexedTextBox).Index), True) Then
            DirectCast(sender, IndexedTextBox).Text = 0
            e.Cancel = True
            Exit Sub
        End If

        tempTotal = 0
        tempSoft = False
        tempNumCards = 0
        For i = 1 To 10
            tempNum = ForcedRulesHandArray(i - 1).Text
            tempTotal += tempNum * i
            tempNumCards += tempNum
        Next
        If tempTotal < 12 And ForcedRulesHandArray(0).Text > 0 Then
            tempSoft = True
            tempTotal += 10
        End If
        If tempTotal > 21 Then
            MsgBox("Total of hand must be <=21.", MsgBoxStyle.OKOnly)
            DirectCast(sender, IndexedTextBox).Text = 0
            e.Cancel = True
        Else
            With ForcedRule.Hand
                For i = 1 To 10
                    .Cards(i) = ForcedRulesHandArray(i - 1).Text
                Next
                .Total = tempTotal
                .Soft = tempSoft
                .NumCards = tempNumCards
            End With
            TotalBoxFSTab.Text = tempTotal
            SoftCheckFSTab.Checked = tempSoft
            NCardsBoxFSTab.Text = tempNumCards
        End If

        'Make sure the strategy isn't split if you don't have a pair
        Select Case CurrentForcedRuleStrat
            Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                If NCardsBoxFSTab.Text <> 2 Then
                    ForcedRuleStratBoxFSTab.Text = Constants.StratShortText(0)
                    ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(0)
                    CurrentForcedRuleStrat = 0
                Else
                    For i = 1 To 10
                        If ForcedRule.Hand.Cards(i) <> 0 And ForcedRule.Hand.Cards(i) <> 2 Then
                            ForcedRuleStratBoxFSTab.Text = Constants.StratShortText(0)
                            ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(0)
                            CurrentForcedRuleStrat = 0
                            Exit For
                        End If
                    Next
                End If
        End Select
        Select Case CurrentForcedRuleStrat
            Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                ExactMatchCheckFSTab.Checked = True
        End Select

        ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
    End Sub

    Private Sub ClearForcedRule()
        ForcedRule.Empty()
        LoadFormForcedRule(-1)
        ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
    End Sub

    Private Sub ClearForcedRuleButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearForcedRuleButtonFSTab.Click
        ClearForcedRule()
    End Sub

    Private Sub LoadFormForcedRulesList(ByRef frList As BJCAForcedRulesListClass, Optional ByVal append As Boolean = False)
        Dim rule As Integer
        Dim tempList As BJCAForcedRulesListClass

        tempList = CloneObject(frList)
        If Not append Then
            DeleteAllForcedRules()
        End If
        FormRules.ForcedStrat.ForcedRulesList = CloneObject(tempList)
        For rule = 0 To FormRules.ForcedStrat.ForcedRulesList.NumRules - 1
            ForcedRulesCheckListBoxFSTab.Items.Add(FormRules.ForcedStrat.ForcedRulesList.L(rule).Name, FormRules.ForcedStrat.ForcedRulesList.L(rule).RuleOn)
        Next rule
        ClearForcedRule()
    End Sub

    Private Sub GetFormCurrentForcedRule()
        Dim card As Integer

        With ForcedRule
            .Name = ForcedRuleNameBoxFSTab.Text
            .RuleOn = False
            .UseSpecificHand = HandCompCheckFSTab.Checked

            For card = 0 To 9
                .Hand.Cards(card + 1) = CInt(ForcedRulesHandArray(card).Text)
            Next card
            If Not HandCompCheckFSTab.Checked Then
                If HandTotalComboBoxFSTab.SelectedIndex = 0 Then
                    .Hand.Total = 0
                Else
                    .Hand.Total = HandTotalComboBoxFSTab.SelectedIndex + 3
                End If
                .Hand.Soft = HandSoftCheckFSTab.Checked
                If HandNCardsComboBoxFSTab.SelectedIndex = 0 Then
                    .Hand.NumCards = 0
                Else
                    .Hand.NumCards = HandNCardsComboBoxFSTab.SelectedIndex + 1
                End If
            Else
                .Hand.Total = TotalBoxFSTab.Text
                .Hand.NumCards = NCardsBoxFSTab.Text
                .Hand.Soft = SoftCheckFSTab.Checked
            End If
            .ExactMatch = ExactMatchCheckFSTab.Checked

            .OrMore = OrMoreCheckFSTab.Checked
            .OrLess = OrLessCheckFSTab.Checked

            .PreSplit = PreSplitCheckFSTab.Checked
            .PostSplit = PostSplitCheckFSTab.Checked

            .Upcard = UpcardComboBoxFSTab.SelectedIndex
            .Strat = CurrentForcedRuleStrat
        End With
    End Sub

    Private Sub LoadFormForcedRule(ByVal index As Integer)
        Dim card As Integer

        ForcedRuleNameBoxFSTab.Text = ForcedRule.Name
        If Not ForcedRule.UseSpecificHand Then
            HandTotalSizeCheckFSTab.Checked = True
            HandCompCheckFSTab.Checked = False
        Else
            HandTotalSizeCheckFSTab.Checked = False
            HandCompCheckFSTab.Checked = True
            HandTotalLabelFSTab.Enabled = False
            HandSoftLabelFSTab.Enabled = False
            HandNCardsLabelFSTab.Enabled = False

            HandTotalComboBoxFSTab.Enabled = False
            HandSoftCheckFSTab.Enabled = False
            HandNCardsComboBoxFSTab.Enabled = False
            OrMoreCheckFSTab.Enabled = False
            OrLessCheckFSTab.Enabled = False

            CAceLabelFSTab.Enabled = True
            C2LabelFSTab.Enabled = True
            C3LabelFSTab.Enabled = True
            C4LabelFSTab.Enabled = True
            C5LabelFSTab.Enabled = True
            C6LabelFSTab.Enabled = True
            C7LabelFSTab.Enabled = True
            C8LabelFSTab.Enabled = True
            C9LabelFSTab.Enabled = True
            C10LabelFSTab.Enabled = True
            TotalLabelFSTab.Enabled = True
            SoftLabelFSTab.Enabled = True
            NCardsLabelFSTab.Enabled = True
            For card = 0 To 9
                ForcedRulesHandArray(card).Enabled = True
            Next card
            ExactMatchCheckFSTab.Enabled = True
        End If
        If Not HandCompCheckFSTab.Checked Then
            If ForcedRule.Hand.Total = 0 Then
                HandTotalComboBoxFSTab.SelectedIndex = 0
            Else
                HandTotalComboBoxFSTab.SelectedIndex = ForcedRule.Hand.Total - 3
            End If
            HandSoftCheckFSTab.Checked = ForcedRule.Hand.Soft
            If ForcedRule.Hand.NumCards = 0 Then
                HandNCardsComboBoxFSTab.SelectedIndex = 0
            Else
                HandNCardsComboBoxFSTab.SelectedIndex = ForcedRule.Hand.NumCards - 1
            End If

            TotalBoxFSTab.Text = 0
            SoftCheckFSTab.Checked = False
            NCardsBoxFSTab.Text = 0

            HandTotalLabelFSTab.Enabled = True
            HandSoftLabelFSTab.Enabled = True
            HandNCardsLabelFSTab.Enabled = True

            HandTotalComboBoxFSTab.Enabled = True
            HandSoftCheckFSTab.Enabled = True
            HandNCardsComboBoxFSTab.Enabled = True
            If HandNCardsComboBoxFSTab.SelectedIndex = 0 Then
                OrMoreCheckFSTab.Enabled = False
                OrLessCheckFSTab.Enabled = False
            ElseIf HandNCardsComboBoxFSTab.SelectedIndex = 1 Then
                OrMoreCheckFSTab.Enabled = True
                OrLessCheckFSTab.Enabled = False
            ElseIf HandNCardsComboBoxFSTab.SelectedIndex = 20 Then
                OrMoreCheckFSTab.Enabled = False
                OrLessCheckFSTab.Enabled = True
            Else
                OrMoreCheckFSTab.Enabled = True
                OrLessCheckFSTab.Enabled = True
            End If

            CAceLabelFSTab.Enabled = False
            C2LabelFSTab.Enabled = False
            C3LabelFSTab.Enabled = False
            C4LabelFSTab.Enabled = False
            C5LabelFSTab.Enabled = False
            C6LabelFSTab.Enabled = False
            C7LabelFSTab.Enabled = False
            C8LabelFSTab.Enabled = False
            C9LabelFSTab.Enabled = False
            C10LabelFSTab.Enabled = False
            TotalLabelFSTab.Enabled = False
            SoftLabelFSTab.Enabled = False
            NCardsLabelFSTab.Enabled = False
            For card = 0 To 9
                ForcedRulesHandArray(card).Enabled = False
            Next card
            ExactMatchCheckFSTab.Enabled = False
        Else
            TotalBoxFSTab.Text = ForcedRule.Hand.Total
            SoftCheckFSTab.Checked = ForcedRule.Hand.Soft
            NCardsBoxFSTab.Text = ForcedRule.Hand.NumCards

            HandTotalComboBoxFSTab.SelectedIndex = 0
            HandSoftCheckFSTab.Checked = False
            HandNCardsComboBoxFSTab.SelectedIndex = 0
        End If

        For card = 1 To 10
            ForcedRulesHandArray(card - 1).Text = ForcedRule.Hand.Cards(card)
        Next card

        ExactMatchCheckFSTab.Checked = ForcedRule.ExactMatch

        OrMoreCheckFSTab.Checked = ForcedRule.OrMore
        OrLessCheckFSTab.Checked = ForcedRule.OrLess

        If Not PostSplitCheckFSTab.Checked Then
            If ForcedRule.PreSplit Then
                PreSplitCheckFSTab.Checked = ForcedRule.PreSplit
                PostSplitCheckFSTab.Checked = ForcedRule.PostSplit
            Else
                PostSplitCheckFSTab.Checked = ForcedRule.PostSplit
                PreSplitCheckFSTab.Checked = ForcedRule.PreSplit
            End If
        ElseIf Not PreSplitCheckFSTab.Checked Then
            If ForcedRule.PostSplit Then
                PostSplitCheckFSTab.Checked = ForcedRule.PostSplit
                PreSplitCheckFSTab.Checked = ForcedRule.PreSplit
            Else
                PreSplitCheckFSTab.Checked = ForcedRule.PreSplit
                PostSplitCheckFSTab.Checked = ForcedRule.PostSplit
            End If
        Else
            PostSplitCheckFSTab.Checked = ForcedRule.PostSplit
            PreSplitCheckFSTab.Checked = ForcedRule.PreSplit
        End If

        UpcardComboBoxFSTab.SelectedIndex = ForcedRule.Upcard
        CurrentForcedRuleStrat = ForcedRule.Strat
        ForcedRuleStratBoxFSTab.Text = Constants.StratShortText(CurrentForcedRuleStrat)
        ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(CurrentForcedRuleStrat)

        ForcedRulesCheckListBoxFSTab.SelectedIndex = index
    End Sub

    Private Sub ForcedRulesCheckListBoxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedRulesCheckListBoxFSTab.SelectedIndexChanged
        If ForcedRulesCheckListBoxFSTab.SelectedIndex >= 0 Then
            ForcedRule = CType(CloneObject(FormRules.ForcedStrat.ForcedRulesList.L(ForcedRulesCheckListBoxFSTab.SelectedIndex)), BJCAForcedRulesClass)
            LoadFormForcedRule(ForcedRulesCheckListBoxFSTab.SelectedIndex)
        End If
    End Sub

    Private Sub MoveForcedRulesUpButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveForcedRulesUpButtonFSTab.Click
        Dim currentRule As Integer
        Dim numRules As Integer
        Dim tempbox As New Windows.Forms.CheckedListBox
        Dim rule As Integer

        numRules = ForcedRulesCheckListBoxFSTab.Items.Count()
        currentRule = ForcedRulesCheckListBoxFSTab.SelectedIndex()

        If currentRule > 0 And numRules > 1 Then
            'First move the actual rule in the list
            FormRules.ForcedStrat.ForcedRulesList.MoveForcedRuleUp(currentRule)

            'Then move the name of the rule in the list
            For rule = 0 To currentRule - 2
                tempbox.Items.Add(ForcedRulesCheckListBoxFSTab.Items(rule), ForcedRulesCheckListBoxFSTab.GetItemChecked(rule))
            Next rule
            tempbox.Items.Add(ForcedRulesCheckListBoxFSTab.Items(currentRule), ForcedRulesCheckListBoxFSTab.GetItemChecked(currentRule))
            tempbox.Items.Add(ForcedRulesCheckListBoxFSTab.Items(currentRule - 1), ForcedRulesCheckListBoxFSTab.GetItemChecked(currentRule - 1))
            For rule = currentRule + 1 To numRules - 1
                tempbox.Items.Add(ForcedRulesCheckListBoxFSTab.Items(rule), ForcedRulesCheckListBoxFSTab.GetItemChecked(rule))
            Next rule
            For rule = numRules - 1 To 0 Step -1
                ForcedRulesCheckListBoxFSTab.Items.RemoveAt(rule)
            Next rule
            For rule = 0 To numRules - 1
                ForcedRulesCheckListBoxFSTab.Items.Add(tempbox.Items(rule), tempbox.GetItemChecked(rule))
            Next rule
            ForcedRulesCheckListBoxFSTab.SelectedIndex = currentRule - 1
        End If
    End Sub

    Private Sub MoveForcedRulesDownButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveForcedRulesDownButtonFSTab.Click
        Dim currentRule As Integer
        Dim numRules As Integer
        Dim tempBox As New Windows.Forms.CheckedListBox
        Dim rule As Integer

        numRules = ForcedRulesCheckListBoxFSTab.Items.Count()
        currentRule = ForcedRulesCheckListBoxFSTab.SelectedIndex

        If currentRule < numRules - 1 And numRules > 1 Then
            'First move the actual rule in the list
            FormRules.ForcedStrat.ForcedRulesList.MoveForcedRuleDown(currentRule)

            'Then move the name of the rule in the list
            For rule = 0 To currentRule - 1
                tempBox.Items.Add(ForcedRulesCheckListBoxFSTab.Items(rule), ForcedRulesCheckListBoxFSTab.GetItemChecked(rule))
            Next rule
            tempBox.Items.Add(ForcedRulesCheckListBoxFSTab.Items(currentRule + 1), ForcedRulesCheckListBoxFSTab.GetItemChecked(currentRule + 1))
            tempBox.Items.Add(ForcedRulesCheckListBoxFSTab.Items(currentRule), ForcedRulesCheckListBoxFSTab.GetItemChecked(currentRule))
            For rule = currentRule + 2 To numRules - 1
                tempBox.Items.Add(ForcedRulesCheckListBoxFSTab.Items(rule), ForcedRulesCheckListBoxFSTab.GetItemChecked(rule))
            Next rule
            For rule = numRules - 1 To 0 Step -1
                ForcedRulesCheckListBoxFSTab.Items.RemoveAt(rule)
            Next rule
            For rule = 0 To numRules - 1
                ForcedRulesCheckListBoxFSTab.Items.Add(tempBox.Items(rule), tempBox.GetItemChecked(rule))
            Next rule
            ForcedRulesCheckListBoxFSTab.SelectedIndex = currentRule + 1

        End If
    End Sub

    Private Sub DeleteForcedRuleButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteForcedRuleButtonFSTab.Click
        Dim currentRule As Integer

        currentRule = ForcedRulesCheckListBoxFSTab.SelectedIndex
        If currentRule = -1 Then
            MsgBox("Please select a bonus rule you would like to delete.", MsgBoxStyle.OKOnly)
        Else
            If MsgBox("Are you sure you would like to delete the Forced Rule: " + FormRules.ForcedStrat.ForcedRulesList.L(currentRule).Name + "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                FormRules.ForcedStrat.ForcedRulesList.DeleteForcedRule(currentRule)
                ForcedRulesCheckListBoxFSTab.Items.RemoveAt(currentRule)
                ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
            End If
        End If
    End Sub

    Private Sub UpdateForcedRuleButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateForcedRuleButtonFSTab.Click
        Dim rule As Integer
        Dim rulePresent As Boolean
        Dim currentRule As Integer

        rulePresent = False
        For rule = 0 To FormRules.ForcedStrat.ForcedRulesList.NumRules - 1
            If FormRules.ForcedStrat.ForcedRulesList.L(rule).Name = ForcedRuleNameBoxFSTab.Text Then
                rulePresent = True
                currentRule = rule
            End If
        Next rule
        If rulePresent Then
            GetFormCurrentForcedRule()
            If Not (ForcedRule.Hand.NumCards > 0 Or ForcedRule.Hand.Total > 0 Or ForcedRule.Hand.Soft = True) Then
                MsgBox("This rule is empty.", MsgBoxStyle.OKOnly)
            Else
                FormRules.ForcedStrat.ForcedRulesList.L(currentRule) = CType(CloneObject(ForcedRule), BJCAForcedRulesClass)
                ForcedRulesCheckListBoxFSTab.SelectedIndex = currentRule
            End If
        Else
            ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
            MsgBox("The rule name does not match any names in the current rules list.", MsgBoxStyle.OKOnly)
        End If
    End Sub

    Private Sub AddForcedRule(Optional ByVal getFormRule As Boolean = True)
        Dim rule As Integer
        Dim rulePresent As Boolean
        Dim ruleName As String

        If getFormRule Then
            ruleName = ForcedRuleNameBoxFSTab.Text
        Else
            ruleName = ForcedRule.Name
        End If

        rulePresent = False
        For rule = 0 To FormRules.ForcedStrat.ForcedRulesList.NumRules - 1
            If FormRules.ForcedStrat.ForcedRulesList.L(rule).Name = ruleName Then
                rulePresent = True
                Exit For
            End If
        Next rule
        If rulePresent Then
            If getFormRule Then
                ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
                MsgBox("A rule by this name already exists.", MsgBoxStyle.OKOnly)
            End If
        Else
            If getFormRule Then GetFormCurrentForcedRule()
            If Not (ForcedRule.Hand.NumCards > 0 Or ForcedRule.Hand.Total > 0 Or ForcedRule.Hand.Soft = True) Then
                MsgBox("This rule is empty.", MsgBoxStyle.OKOnly)
            Else
                ForcedRule.RuleOn = False
                FormRules.ForcedStrat.ForcedRulesList.AddForcedRule(ForcedRule)
                ForcedRulesCheckListBoxFSTab.Items.Add(ForcedRule.Name, ForcedRule.RuleOn)
                ForcedRulesCheckListBoxFSTab.SelectedIndex = (FormRules.ForcedStrat.ForcedRulesList.NumRules - 1)
            End If
        End If

    End Sub

    Private Sub AddForcedRuleButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddForcedRuleButtonFSTab.Click
        AddForcedRule()
    End Sub

    Private Sub RestoreDefaultForcedRulesButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreDefaultForcedRulesButtonFSTab.Click
        Dim rule As Integer
        Dim j As Integer
        Dim rulePresent As Boolean

        rulePresent = False
        For rule = 0 To FormRules.DefaultForcedRulesList.NumRules - 1
            rulePresent = False
            For j = 0 To FormRules.ForcedStrat.ForcedRulesList.NumRules - 1
                If FormRules.DefaultForcedRulesList.L(rule).Name = FormRules.ForcedStrat.ForcedRulesList.L(j).Name Then
                    rulePresent = True
                    Exit For
                End If
            Next j
            If Not rulePresent Then
                FormRules.ForcedStrat.ForcedRulesList.AddForcedRule(FormRules.DefaultForcedRulesList.L(rule))
                ForcedRulesCheckListBoxFSTab.Items.Add(FormRules.DefaultForcedRulesList.L(rule).Name, FormRules.DefaultForcedRulesList.L(rule).RuleOn)
                ForcedRulesCheckListBoxFSTab.SelectedIndex = FormRules.ForcedStrat.ForcedRulesList.NumRules - 1
            End If
        Next rule
        ClearForcedRule()

    End Sub

    Private Sub UncheckAllForcedRulesButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UncheckAllForcedRulesButtonFSTab.Click
        Dim rule As Integer

        For rule = 0 To FormRules.ForcedStrat.ForcedRulesList.NumRules - 1
            ForcedRulesCheckListBoxFSTab.SetItemChecked(rule, False)
        Next rule
    End Sub

    Private Sub LoadStrategyComboBoxFSTab()
        Dim strat As Integer

        For strat = 1 To Constants.NumStrats - 1
            StrategyComboBoxFSTab.Items.Add(Constants.StratLongText(strat))
        Next strat
        StrategyComboBoxFSTab.SelectedIndex = 0
    End Sub

    Private Sub ForcedStratTabControlFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedStratTabControlFSTab.SelectedIndexChanged
        If ForcedStratTabControlFSTab.SelectedIndex = 0 Or ForcedStratTabControlFSTab.SelectedIndex = 4 Then
            CopyTDButtonFSTab.Visible = False
            CopyCDButtonFSTab.Visible = False
            CopyPairsButtonFSTab.Visible = False
            ClearForcedTablesButtonFSTab.Visible = False
            SaveForcedTablesButtonFSTab.Visible = False
            LoadForcedTablesButtonFSTab.Visible = False
        Else
            CopyTDButtonFSTab.Visible = True
            CopyCDButtonFSTab.Visible = True
            CopyPairsButtonFSTab.Visible = True
            ClearForcedTablesButtonFSTab.Visible = True
            SaveForcedTablesButtonFSTab.Visible = True
            LoadForcedTablesButtonFSTab.Visible = True
        End If
    End Sub

    Private Sub DeleteAllForcedRules()
        If ForcedRulesCheckListBoxFSTab.Items.Count > 0 Then
            Dim rule As Integer

            If ForcedRulesCheckListBoxFSTab.Items.Count = FormRules.ForcedStrat.ForcedRulesList.NumRules Then
                For rule = FormRules.ForcedStrat.ForcedRulesList.NumRules - 1 To 0 Step -1
                    FormRules.ForcedStrat.ForcedRulesList.DeleteForcedRule(rule)
                Next rule
            End If
            ForcedRulesCheckListBoxFSTab.Items.Clear()
        End If
    End Sub

    Private Sub DeleteAllForcedRulesButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAllForcedRulesButtonFSTab.Click
        If MsgBox("Are you sure you would like to delete all the Forced Rules in the list?", MsgBoxStyle.OKCancel) = MsgBoxResult.OK Then
            DeleteAllForcedRules()
        End If
    End Sub

    Private Sub RenameRuleButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameRuleButtonFSTab.Click
        Dim rule As Integer
        Dim renameForm As New BJCARenameForm

        rule = ForcedRulesCheckListBoxFSTab.SelectedIndex()
        If rule = -1 Then
            MsgBox("Please select a rule to rename")
        Else
            renameForm.CurrentNameBoxRForm.Text = FormRules.ForcedStrat.ForcedRulesList.L(rule).Name
            renameForm.ShowDialog()
            If renameForm.RenameOK Then
                ForcedRulesCheckListBoxFSTab.Items.Item(rule) = renameForm.NewNameBoxRForm.Text
                FormRules.ForcedStrat.ForcedRulesList.L(rule).Name = renameForm.NewNameBoxRForm.Text
                ForcedRule = CType(CloneObject(FormRules.ForcedStrat.ForcedRulesList.L(rule)), BJCAForcedRulesClass)
                LoadFormForcedRule(ForcedRulesCheckListBoxFSTab.SelectedIndex)
            End If
        End If
        renameForm.Dispose()
    End Sub

    Private Sub ForcedRuleStratBoxFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedRuleStratBoxFSTab.Click
        PreviousObject = ForcedRuleStratBoxFSTab
        PreviousGroup = ForcedRuleDetailsGroupFSTab

        StrategyComboBoxFSTab.Items.Clear()
        StrategyComboBoxFSTab.Items.AddRange(New Object() {"--", "S", "D", "DS", "H", "R", "RS", "P", "PS", "PD", "PH", "xS", "xD", "xH", "xR", "xP"})
        StrategyComboBoxFSTab.Text = Constants.StratLongText(CurrentForcedRuleStrat)
        StrategyComboBoxFSTab.Visible = True
        StrategyComboBoxFSTab.DroppedDown = True
        StrategyComboBoxFSTab.Focus()
    End Sub

    Private Sub StrategyComboBoxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StrategyComboBoxFSTab.SelectedIndexChanged
        Dim index As Integer
        Dim i As Integer

        If Not PreviousObject Is Nothing Then
            If StrategyComboBoxFSTab.SelectedIndex > -1 Then
                For index = 0 To Constants.NumStrats - 1
                    If StrategyComboBoxFSTab.Text = Constants.StratShortText(index) Then
                        Exit For
                    End If
                Next index
                CurrentForcedRuleStrat = index
                PreviousObject.Text = Constants.StratShortText(index)
                PreviousObject.BackColor = FormRules.ColorTable.C(index)

                Select Case CurrentForcedRuleStrat
                    Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                        If HandCompCheckFSTab.Checked Then
                            If NCardsBoxFSTab.Text <> 2 Then
                                PreviousObject.Text = Constants.StratShortText(0)
                                PreviousObject.BackColor = FormRules.ColorTable.C(0)
                                CurrentForcedRuleStrat = 0
                            Else
                                For i = 1 To 10
                                    If ForcedRule.Hand.Cards(i) <> 0 And ForcedRule.Hand.Cards(i) <> 2 Then
                                        PreviousObject.Text = Constants.StratShortText(0)
                                        PreviousObject.BackColor = FormRules.ColorTable.C(0)
                                        CurrentForcedRuleStrat = 0
                                        Exit For
                                    End If
                                Next
                            End If
                        Else
                            PreviousObject.Text = Constants.StratShortText(0)
                            PreviousObject.BackColor = FormRules.ColorTable.C(0)
                            CurrentForcedRuleStrat = 0
                        End If
                End Select
                Select Case CurrentForcedRuleStrat
                    Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                        ExactMatchCheckFSTab.Checked = True
                End Select
            End If
            StrategyComboBoxFSTab.Visible = False
            PreviousGroup.Focus()
            ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
        End If
    End Sub

    Private Sub StrategyComboBoxFSTab_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StrategyComboBoxFSTab.DropDown
        Dim index As Integer
        Dim i As Integer

        If Not PreviousObject Is Nothing Then
            If StrategyComboBoxFSTab.SelectedIndex > -1 Then
                For index = 0 To Constants.NumStrats - 1
                    If StrategyComboBoxFSTab.Text = Constants.StratShortText(index) Then
                        Exit For
                    End If
                Next index
                CurrentForcedRuleStrat = index
                PreviousObject.Text = Constants.StratShortText(index)
                PreviousObject.BackColor = FormRules.ColorTable.C(index)

                Select Case CurrentForcedRuleStrat
                    Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                        If HandCompCheckFSTab.Checked Then
                            If NCardsBoxFSTab.Text <> 2 Then
                                PreviousObject.Text = Constants.StratShortText(0)
                                PreviousObject.BackColor = FormRules.ColorTable.C(0)
                                CurrentForcedRuleStrat = 0
                            Else
                                For i = 1 To 10
                                    If ForcedRule.Hand.Cards(i) <> 0 And ForcedRule.Hand.Cards(i) <> 2 Then
                                        PreviousObject.Text = Constants.StratShortText(0)
                                        PreviousObject.BackColor = FormRules.ColorTable.C(0)
                                        CurrentForcedRuleStrat = 0
                                        Exit For
                                    End If
                                Next
                            End If
                        Else
                            PreviousObject.Text = Constants.StratShortText(0)
                            PreviousObject.BackColor = FormRules.ColorTable.C(0)
                            CurrentForcedRuleStrat = 0
                        End If
                End Select
                Select Case CurrentForcedRuleStrat
                    Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                        ExactMatchCheckFSTab.Checked = True
                End Select
            End If
            StrategyComboBoxFSTab.Visible = False
            PreviousGroup.Focus()
            ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
        End If
    End Sub

    Private Sub PreSplitCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreSplitCheckFSTab.CheckedChanged
        If Not PreSplitCheckFSTab.Checked And Not PostSplitCheckFSTab.Checked Then
            MsgBox("The rule must be applied pre-split, post-split or both pre- and post-split", MsgBoxStyle.OKOnly)
            PreSplitCheckFSTab.Checked = True
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub PostSplitCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostSplitCheckFSTab.CheckedChanged
        If Not PreSplitCheckFSTab.Checked And Not PostSplitCheckFSTab.Checked Then
            MsgBox("The rule must be applied pre-split, post-split or both pre- and post-split", MsgBoxStyle.OKOnly)
            PostSplitCheckFSTab.Checked = True
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub HandTotalComboBoxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandTotalComboBoxFSTab.SelectedIndexChanged
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub HandNCardsComboBoxFSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandNCardsComboBoxFSTab.SelectedIndexChanged
        If HandNCardsComboBoxFSTab.SelectedIndex = 0 Then
            OrMoreCheckFSTab.Enabled = False
            OrLessCheckFSTab.Enabled = False
            OrMoreCheckFSTab.Checked = False
            OrLessCheckFSTab.Checked = False
        ElseIf HandNCardsComboBoxFSTab.SelectedIndex = 1 Then
            OrMoreCheckFSTab.Enabled = True
            OrLessCheckFSTab.Enabled = False
            OrLessCheckFSTab.Checked = False
        ElseIf HandNCardsComboBoxFSTab.SelectedIndex = 20 Then
            OrMoreCheckFSTab.Enabled = False
            OrLessCheckFSTab.Enabled = True
            OrMoreCheckFSTab.Checked = False
        Else
            OrMoreCheckFSTab.Enabled = True
            OrLessCheckFSTab.Enabled = True
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub ExactMatchCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExactMatchCheckFSTab.CheckedChanged
        If Not ExactMatchCheckFSTab.Checked Then
            Select Case CurrentForcedRuleStrat
                Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                    ForcedRuleStratBoxFSTab.Text = Constants.StratShortText(0)
                    ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(0)
                    CurrentForcedRuleStrat = 0
            End Select
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub OrMoreCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrMoreCheckFSTab.CheckedChanged
        If OrMoreCheckFSTab.Checked Then
            OrLessCheckFSTab.Checked = False
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
    End Sub

    Private Sub OrLessCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrLessCheckFSTab.CheckedChanged
        If OrLessCheckFSTab.Checked Then
            OrMoreCheckFSTab.Checked = False
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
    End Sub

    Private Sub HandTotalSizeCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandTotalSizeCheckFSTab.CheckedChanged
        Dim card As Integer

        If HandTotalSizeCheckFSTab.Checked Then
            HandCompCheckFSTab.Checked = False
            HandTotalLabelFSTab.Enabled = True
            HandSoftLabelFSTab.Enabled = True
            HandNCardsLabelFSTab.Enabled = True

            HandTotalComboBoxFSTab.Enabled = True
            HandSoftCheckFSTab.Enabled = True
            HandNCardsComboBoxFSTab.Enabled = True
            If HandNCardsComboBoxFSTab.SelectedIndex = 0 Then
                OrMoreCheckFSTab.Enabled = False
                OrLessCheckFSTab.Enabled = False
                OrMoreCheckFSTab.Checked = False
                OrLessCheckFSTab.Checked = False
            ElseIf HandNCardsComboBoxFSTab.SelectedIndex = 1 Then
                OrMoreCheckFSTab.Enabled = True
                OrLessCheckFSTab.Enabled = False
                OrLessCheckFSTab.Checked = False
            ElseIf HandNCardsComboBoxFSTab.SelectedIndex = 20 Then
                OrMoreCheckFSTab.Enabled = False
                OrLessCheckFSTab.Enabled = True
                OrMoreCheckFSTab.Checked = False
            Else
                OrMoreCheckFSTab.Enabled = True
                OrLessCheckFSTab.Enabled = True
            End If

            CAceLabelFSTab.Enabled = False
            C2LabelFSTab.Enabled = False
            C3LabelFSTab.Enabled = False
            C4LabelFSTab.Enabled = False
            C5LabelFSTab.Enabled = False
            C6LabelFSTab.Enabled = False
            C7LabelFSTab.Enabled = False
            C8LabelFSTab.Enabled = False
            C9LabelFSTab.Enabled = False
            C10LabelFSTab.Enabled = False
            TotalLabelFSTab.Enabled = False
            SoftLabelFSTab.Enabled = False
            NCardsLabelFSTab.Enabled = False
            For card = 0 To 9
                ForcedRulesHandArray(card).Enabled = False
            Next card
            ExactMatchCheckFSTab.Enabled = False
        Else
            HandCompCheckFSTab.Checked = True
            HandTotalLabelFSTab.Enabled = False
            HandSoftLabelFSTab.Enabled = False
            HandNCardsLabelFSTab.Enabled = False

            HandTotalComboBoxFSTab.Enabled = False
            HandSoftCheckFSTab.Enabled = False
            HandNCardsComboBoxFSTab.Enabled = False
            OrMoreCheckFSTab.Enabled = False
            OrLessCheckFSTab.Enabled = False

            CAceLabelFSTab.Enabled = True
            C2LabelFSTab.Enabled = True
            C3LabelFSTab.Enabled = True
            C4LabelFSTab.Enabled = True
            C5LabelFSTab.Enabled = True
            C6LabelFSTab.Enabled = True
            C7LabelFSTab.Enabled = True
            C8LabelFSTab.Enabled = True
            C9LabelFSTab.Enabled = True
            C10LabelFSTab.Enabled = True
            TotalLabelFSTab.Enabled = True
            SoftLabelFSTab.Enabled = True
            NCardsLabelFSTab.Enabled = True
            For card = 0 To 9
                ForcedRulesHandArray(card).Enabled = True
            Next card
            ExactMatchCheckFSTab.Enabled = True
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
    End Sub

    Private Sub HandCompCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandCompCheckFSTab.CheckedChanged
        Dim card As Integer

        If HandCompCheckFSTab.Checked Then
            HandTotalSizeCheckFSTab.Checked = False
            HandTotalLabelFSTab.Enabled = False
            HandSoftLabelFSTab.Enabled = False
            HandNCardsLabelFSTab.Enabled = False

            HandTotalComboBoxFSTab.Enabled = False
            HandSoftCheckFSTab.Enabled = False
            HandNCardsComboBoxFSTab.Enabled = False
            OrMoreCheckFSTab.Enabled = False
            OrLessCheckFSTab.Enabled = False

            CAceLabelFSTab.Enabled = True
            C2LabelFSTab.Enabled = True
            C3LabelFSTab.Enabled = True
            C4LabelFSTab.Enabled = True
            C5LabelFSTab.Enabled = True
            C6LabelFSTab.Enabled = True
            C7LabelFSTab.Enabled = True
            C8LabelFSTab.Enabled = True
            C9LabelFSTab.Enabled = True
            C10LabelFSTab.Enabled = True
            TotalLabelFSTab.Enabled = True
            SoftLabelFSTab.Enabled = True
            NCardsLabelFSTab.Enabled = True
            For card = 0 To 9
                ForcedRulesHandArray(card).Enabled = True
            Next card
            ExactMatchCheckFSTab.Enabled = True
            If NCardsBoxFSTab.Text <> 2 Then
                ForcedRuleStratBoxFSTab.Text = Constants.StratShortText(0)
                ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(0)
                CurrentForcedRuleStrat = 0
            Else
                For card = 1 To 10
                    If ForcedRule.Hand.Cards(card) <> 0 And ForcedRule.Hand.Cards(card) <> 2 Then
                        ForcedRuleStratBoxFSTab.Text = Constants.StratShortText(0)
                        ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(0)
                        CurrentForcedRuleStrat = 0
                        Exit For
                    End If
                Next
            End If
            Select Case CurrentForcedRuleStrat
                Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                    ExactMatchCheckFSTab.Checked = True
            End Select
        Else
            HandTotalSizeCheckFSTab.Checked = True
            HandTotalLabelFSTab.Enabled = True
            HandSoftLabelFSTab.Enabled = True
            HandNCardsLabelFSTab.Enabled = True

            HandTotalComboBoxFSTab.Enabled = True
            HandSoftCheckFSTab.Enabled = True
            HandNCardsComboBoxFSTab.Enabled = True
            If HandNCardsComboBoxFSTab.SelectedIndex = 0 Then
                OrMoreCheckFSTab.Enabled = False
                OrLessCheckFSTab.Enabled = False
                OrMoreCheckFSTab.Checked = False
                OrLessCheckFSTab.Checked = False
            ElseIf HandNCardsComboBoxFSTab.SelectedIndex = 1 Then
                OrMoreCheckFSTab.Enabled = True
                OrLessCheckFSTab.Enabled = False
                OrLessCheckFSTab.Checked = False
            ElseIf HandNCardsComboBoxFSTab.SelectedIndex = 20 Then
                OrMoreCheckFSTab.Enabled = False
                OrLessCheckFSTab.Enabled = True
                OrMoreCheckFSTab.Checked = False
            Else
                OrMoreCheckFSTab.Enabled = True
                OrLessCheckFSTab.Enabled = True
            End If

            CAceLabelFSTab.Enabled = False
            C2LabelFSTab.Enabled = False
            C3LabelFSTab.Enabled = False
            C4LabelFSTab.Enabled = False
            C5LabelFSTab.Enabled = False
            C6LabelFSTab.Enabled = False
            C7LabelFSTab.Enabled = False
            C8LabelFSTab.Enabled = False
            C9LabelFSTab.Enabled = False
            C10LabelFSTab.Enabled = False
            TotalLabelFSTab.Enabled = False
            SoftLabelFSTab.Enabled = False
            NCardsLabelFSTab.Enabled = False
            For card = 0 To 9
                ForcedRulesHandArray(card).Enabled = False
            Next card
            ExactMatchCheckFSTab.Enabled = False
            Select Case CurrentForcedRuleStrat
                Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR, BJCAGlobalsClass.Strat.xP
                    ForcedRuleStratBoxFSTab.Text = Constants.StratShortText(0)
                    ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(0)
                    CurrentForcedRuleStrat = 0
            End Select
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
    End Sub

    Private Sub ForcedRuleNameBoxFSTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedRuleNameBoxFSTab.TextChanged
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub UpcardBoxFSTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub HandSoftCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandSoftCheckFSTab.CheckedChanged
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub GetForcedRulesOn()
        Dim rule As Integer

        For rule = 0 To FormRules.ForcedStrat.ForcedRulesList.NumRules - 1
            FormRules.ForcedStrat.ForcedRulesList.L(rule).RuleOn = ForcedRulesCheckListBoxFSTab.GetItemChecked(rule)
        Next rule
    End Sub

    Private Sub RestoreDefaultForcedRules()
        DeleteAllForcedRules()
        FormRules.ForcedStrat.ForcedRulesList = CloneObject(FormRules.DefaultForcedRulesList)
        Filenames.ForcedRulesFileName = "Default"
        LoadFormForcedRulesList(FormRules.ForcedStrat.ForcedRulesList)
        ClearForcedRule()
    End Sub

    Private Sub SaveForcedRulesFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavedForcedRulesButtonFSTab.Click
        SaveForcedRulesFile()
    End Sub

    Private Sub LoadForcedRulesFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadForcedRulesButtonFSTab.Click
        LoadForcedRulesFile()
    End Sub

    Private Sub SaveForcedRulesFile()
        Dim sfd As New SaveFileDialog

        GetForcedRulesOn()
        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = Filenames.ForcedRulesFileExt
        sfd.FileName = GetFileName(Filenames.ForcedRulesFileName)
        sfd.InitialDirectory = Filenames.DefaultPath
        sfd.Filter = ("Forced Rules Files (*" + Filenames.ForcedRulesFileExt + ")|*" + Filenames.ForcedRulesFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SaveObjectFile(sfd.FileName, FormRules.ForcedStrat.ForcedRulesList)
            Filenames.ForcedRulesFileName = sfd.FileName
            Filenames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadForcedRulesFile(Optional ByVal getName As Boolean = True)
        Dim tempList As New BJCAForcedRulesListClass

        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                FormRules.ForcedStrat.ForcedRulesList = CType(LoadObjectFile(Filenames.ForcedRulesFileName), BJCAForcedRulesListClass)
                LoadFormForcedRulesList(FormRules.ForcedStrat.ForcedRulesList)
            Catch
                MsgBox("The file: " + GetFileName(Filenames.ForcedRulesFileName) + " is not a valid Forced Rules file.")
                RestoreDefaultForcedRules()
            Finally
            End Try
        Else
            Dim ofd As New OpenFileDialog
            Dim fileOK As Boolean

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = Filenames.ForcedRulesFileExt
            ofd.FileName = GetFileName(Filenames.ForcedRulesFileName)
            ofd.InitialDirectory = Filenames.DefaultPath
            ofd.Filter = ("Forced Rules Files (*" + Filenames.ForcedRulesFileExt + ")|*" + Filenames.ForcedRulesFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    tempList = CType(LoadObjectFile(ofd.FileName), BJCAForcedRulesListClass)
                    DeleteAllForcedRules()
                    FormRules.ForcedStrat.ForcedRulesList = CloneObject(tempList)
                    Filenames.ForcedRulesFileName = ofd.FileName
                    Filenames.DefaultPath = GetPath(ofd.FileName)
                    fileOK = True
                Catch
                    MsgBox("The file: " + GetFileName(ofd.FileName) + " is not a valid Forced Rules file.")
                    fileOK = False
                End Try
                If fileOK Then
                    LoadFormForcedRulesList(FormRules.ForcedStrat.ForcedRulesList)
                End If
            End If
            ofd.Dispose()
        End If
    End Sub

#End Region

#End Region

#Region " EORs Tab "

#Region " EOR Summary Tab "

    Private Sub CalcEORsButtonEORTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalcEORsButtonEORTab.Click
        Dim tempRules As New BJCARulesClass
        Dim card As Integer
        Dim mincard As Integer
        Dim maxcard As Integer

        '        Try
        Results.CurrentShoe.Reset(Results.OriginalShoe)

        'Load and calculate the Current Forced Strategy
        RecalcForcedStrat()
        Results.FixForcedStrat(Results.Forced)

        'Copy the rules
        tempRules = CloneObject(Results.OriginalRules)
        tempRules.ComputeTD = True
        tempRules.ComputeTC = False
        tempRules.ComputeForced = True

        'Now actually calculate the EORs
        If EORCardComboboxEORTab.SelectedIndex = 0 Then
            mincard = 1
            maxcard = 10
        Else
            mincard = EORCardComboboxEORTab.SelectedIndex
            maxcard = EORCardComboboxEORTab.SelectedIndex
        End If
        For card = mincard To maxcard
            Dim tempCA As New BJCA

            If EORs(card) Is Nothing Then EORs(card) = New BJCAEORsClass
            If tempRules.Shoe.Cards(card) > 0 Then
                tempRules.Shoe.Deal(card, EORNcards)

                tempCA.BJCA(tempRules)
                tempCA.CopyEORForcedStrat(Results)
                tempCA.ComputeEORForcedStrat(tempCA.Forced)

                tempCA.CopyEOREVs(EORs(card), Results)
                EORs(card).ForcedNetGameEVEOR = EORs(card).ForcedNetGameEV - Results.Forced.GameEVs.NetGameEV
                EORs(card).CDNetGameEVEOR = EORs(card).CDNetGameEV - Results.Opt.GameEVs.NetGameEV
                EORs(card).TDNetGameEVEOR = EORs(card).TDNetGameEV - Results.TD.GameEVs.NetGameEV

                tempRules.Shoe.Undeal(card, EORNcards)
            End If

            tempCA = Nothing
        Next card

        LoadEORSummaryPageForm()
        LoadEORHandListForm()
        LoadEORHandForm()
        LoadEORTotalForm()
        '        Catch
        '            MsgBox("An error has occurred - please restart.")
        '            Me.Close()
        '        End Try
    End Sub

    Public Sub LoadEORSummaryPageForm()
        Dim card As Integer
        Dim minCard As Integer
        Dim maxCard As Integer
        Dim netProb As Double

        If Results.Forced.ComputeStrat Then FillNumberTextBox(ForcedEVBoxEORTab, Results.Forced.GameEVs.NetGameEV, 15, True)
        FillNumberTextBox(CDEVBoxEORTab, Results.Opt.GameEVs.NetGameEV, 15, True)
        If Results.TD.ComputeStrat Then FillNumberTextBox(TDEVBoxEORTab, Results.TD.GameEVs.NetGameEV, 15, True)

        If EORCardComboboxEORTab.SelectedIndex = 0 Then
            minCard = 1
            maxCard = 10
        Else
            minCard = EORCardComboboxEORTab.SelectedIndex
            maxCard = EORCardComboboxEORTab.SelectedIndex
        End If
        EORs(11).ForcedNetGameEV = 0
        EORs(11).CDNetGameEV = 0
        EORs(11).TDNetGameEV = 0
        netProb = 0
        For card = 1 To 10
            If Not EORs(card) Is Nothing Then
                netProb += Results.Opt.GameEVs.CardProbs(card)
            End If
        Next card
        For card = minCard To maxCard
            If Results.Opt.GameEVs.CardProbs(card) > 0 Then
                'EORs
                EOREVsArray(0, card - 1).Text = CStr(Math.Round(Results.Opt.GameEVs.CardProbs(card) * 100, 2)) + "%"
                FillNumberTextBox(EOREVsArray(1, card - 1), EORs(card).ForcedNetGameEVEOR, 15, True)
                FillNumberTextBox(EOREVsArray(2, card - 1), EORs(card).CDNetGameEVEOR, 15, True)
                FillNumberTextBox(EOREVsArray(3, card - 1), EORs(card).TDNetGameEVEOR, 15, True)
                EORs(11).ForcedNetGameEV += EORs(card).ForcedNetGameEVEOR * Results.Opt.GameEVs.CardProbs(card)
                EORs(11).CDNetGameEV += EORs(card).CDNetGameEVEOR * Results.Opt.GameEVs.CardProbs(card)
                EORs(11).TDNetGameEV += EORs(card).TDNetGameEVEOR * Results.Opt.GameEVs.CardProbs(card)

                'Net EVs
                EORNetEVsArray(0, card - 1).Text = CStr(Math.Round(Results.Opt.GameEVs.CardProbs(card) * 100, 2)) + "%"
                FillNumberTextBox(EORNetEVsArray(1, card - 1), EORs(card).ForcedNetGameEV, 15, True)
                FillNumberTextBox(EORNetEVsArray(2, card - 1), EORs(card).CDNetGameEV, 15, True)
                FillNumberTextBox(EORNetEVsArray(3, card - 1), EORs(card).TDNetGameEV, 15, True)
            End If
        Next card
        EOREVsArray(0, 10).Text = CStr(Math.Round(netProb * 100, 2)) + "%"
        FillNumberTextBox(EOREVsArray(1, 10), EORs(11).ForcedNetGameEV, 15, True)
        FillNumberTextBox(EOREVsArray(2, 10), EORs(11).CDNetGameEV, 15, True)
        FillNumberTextBox(EOREVsArray(3, 10), EORs(11).TDNetGameEV, 15, True)

    End Sub

    Private Sub TextButtonEORTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextButtonEORTab.Click
        Dim TextForm As New BJCATextForm
        Dim textString As String
        Dim upcard As Integer
        Dim column As Integer

        textString = "Net Game EVs"
        textString += Chr(13)
        textString += SetStringSize("TD EV:", 11, 11, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.TD.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
        textString += Chr(13)
        textString += SetStringSize("CD EV:", 11, 11, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
        textString += Chr(13)
        textString += SetStringSize("Forced EV:", 11, 11, StringAlignment.Near) + SetStringSize(CStr(Math.Round(Results.Forced.GameEVs.NetGameEV * 100, 15)) + "%", 19, 19, StringAlignment.Far)
        textString += Chr(13)
        textString += Chr(13)

        'Upcard EVs
        textString += "EOR"
        textString += Chr(13)
        textString += SetStringSize("", 9, 9, StringAlignment.Far)
        textString += GetUpcardLabelString(9, False)
        textString += Chr(13)

        textString += SetStringSize("Prob:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.CardProbs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("Forced:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            If Not EORs(column) Is Nothing Then
                textString += SetStringSize(CStr(Math.Round(EORs(column).ForcedNetGameEVEOR * 100, 2)) + "%", 9, 9, StringAlignment.Far)
            Else
                textString += SetStringSize("--", 9, 9, StringAlignment.Far)
            End If
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("CD:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            If Not EORs(column) Is Nothing Then
                textString += SetStringSize(CStr(Math.Round(EORs(column).CDNetGameEVEOR * 100, 2)) + "%", 9, 9, StringAlignment.Far)
            Else
                textString += SetStringSize("--", 9, 9, StringAlignment.Far)
            End If
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("TD:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            If Not EORs(column) Is Nothing Then
                textString += SetStringSize(CStr(Math.Round(EORs(column).TDNetGameEVEOR * 100, 2)) + "%", 9, 9, StringAlignment.Far)
            Else
                textString += SetStringSize("--", 9, 9, StringAlignment.Far)
            End If
        Next upcard
        textString += Chr(13)
        textString += Chr(13)

        'Player's First Card EVs
        textString += "Net EV with Card Removed"
        textString += Chr(13)
        textString += SetStringSize("", 9, 9, StringAlignment.Far)
        textString += GetUpcardLabelString(9, False)
        textString += Chr(13)
        textString += SetStringSize("Prob:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            textString += SetStringSize(CStr(Math.Round(Results.Opt.GameEVs.CardProbs(column) * 100, 2)) + "%", 9, 9, StringAlignment.Far)
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("Forced:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            If Not EORs(column) Is Nothing Then
                textString += SetStringSize(CStr(Math.Round(EORs(column).ForcedNetGameEV * 100, 2)) + "%", 9, 9, StringAlignment.Far)
            Else
                textString += SetStringSize("--", 9, 9, StringAlignment.Far)
            End If
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("CD:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            If Not EORs(column) Is Nothing Then
                textString += SetStringSize(CStr(Math.Round(EORs(column).CDNetGameEV * 100, 2)) + "%", 9, 9, StringAlignment.Far)
            Else
                textString += SetStringSize("--", 9, 9, StringAlignment.Far)
            End If
        Next upcard
        textString += Chr(13)
        textString += SetStringSize("TD:", 9, 9, StringAlignment.Near)
        For upcard = 0 To 9
            If upcard = 9 Then
                column = 1
            Else
                column = upcard + 2
            End If
            If Not EORs(column) Is Nothing Then
                textString += SetStringSize(CStr(Math.Round(EORs(column).TDNetGameEV * 100, 2)) + "%", 9, 9, StringAlignment.Far)
            Else
                textString += SetStringSize("--", 9, 9, StringAlignment.Far)
            End If
        Next upcard
        textString += Chr(13)
        textString += Chr(13)

        TextForm.RichTextBoxTextForm.Text = textString
        TextForm.Show()
    End Sub

    Private Sub ClearEORsButtonEORTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearEORsButtonEORTab.Click
        ClearFormSummaryEORPage()
    End Sub

    Private Sub PopulateSummaryEORUpcardLabels()
        PopulateUpcardLabels(SummaryEORTab, 190, 160, 56)
        PopulateUpcardLabels(SummaryEORTab, 190, 344, 56)
    End Sub

    Private Sub PopulateSummaryEORTables()
        Dim row As Integer
        Dim upcard As Integer

        For row = 0 To 3
            For upcard = 0 To 10
                Dim box As New IndexedTextBox

                'Populate the Upcard Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(56, 20)
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(175 + 9 * 56, 188 + row * 28)
                ElseIf upcard = 10 Then
                    box.Location = New System.Drawing.Point(175 + 10 * 56, 188 + row * 28)
                Else
                    box.Location = New System.Drawing.Point(175 + (upcard - 1) * 56, 188 + row * 28)
                End If
                If row = 0 Then
                    box.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
                End If

                SummaryEORTab.Controls.Add(box)
                EOREVsArray(row, upcard) = box
            Next upcard
        Next row

        For row = 0 To 3
            For upcard = 0 To 9
                Dim box As New IndexedTextBox

                'Populate the Player's Card Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.Size = New System.Drawing.Size(56, 20)
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                If upcard = 0 Then
                    box.Location = New System.Drawing.Point(175 + 9 * 56, 372 + row * 28)
                Else
                    box.Location = New System.Drawing.Point(175 + (upcard - 1) * 56, 372 + row * 28)
                End If
                If row = 0 Then
                    box.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
                End If

                SummaryEORTab.Controls.Add(box)
                EORNetEVsArray(row, upcard) = box
            Next upcard
        Next row

    End Sub

    Public Sub ClearFormSummaryEORPage()
        Dim card As Integer

        If Results.Forced.ComputeStrat Then FillNumberTextBox(ForcedEVBoxEORTab, Results.Forced.GameEVs.NetGameEV, 15, True)
        FillNumberTextBox(CDEVBoxEORTab, Results.Opt.GameEVs.NetGameEV, 15, True)
        If Results.TD.ComputeStrat Then FillNumberTextBox(TDEVBoxEORTab, Results.TD.GameEVs.NetGameEV, 15, True)

        For card = 1 To 11
            'EORs
            '            EOREVsArray(0, card - 1).Text = CStr(Math.Round(Results.Opt.GameEVs.CardProbs(card) * 100, 2)) + "%"
            If Not EORs(card - 1) Is Nothing Then
                EORs(card - 1).ForcedNetGameEVEOR = 0
                EORs(card - 1).CDNetGameEVEOR = 0
                EORs(card - 1).TDNetGameEVEOR = 0
                EORs(card - 1).ForcedNetGameEVEOR = 0
                EORs(card - 1).CDNetGameEV = 0
                EORs(card - 1).TDNetGameEV = 0
            End If
            FillNumberTextBox(EOREVsArray(1, card - 1), 0, 2, True)
            FillNumberTextBox(EOREVsArray(2, card - 1), 0, 2, True)
            FillNumberTextBox(EOREVsArray(3, card - 1), 0, 2, True)
        Next card

        For card = 1 To 10
            'Net EVs
            '            EORNetEVsArray(0, card - 1).Text = CStr(Math.Round(Results.Opt.GameEVs.CardProbs(card) * 100, 2)) + "%"
            FillNumberTextBox(EORNetEVsArray(1, card - 1), 0, 2, True)
            FillNumberTextBox(EORNetEVsArray(2, card - 1), 0, 2, True)
            FillNumberTextBox(EORNetEVsArray(3, card - 1), 0, 2, True)
        Next card

    End Sub

    Private Sub LoadEORCardComboBoxEORTab()
        Dim card As Integer

        EORCardComboboxEORTab.Items.Add("Compute All")
        For card = 1 To 10
            EORCardComboboxEORTab.Items.Add(CStr(card))
        Next card
        EORCardComboboxEORTab.SelectedIndex = 0
    End Sub

    Private Sub NCardEORBoxEORTab_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NCardEORBoxEORTab.Validating
        Dim minCards As Integer
        Dim i As Integer

        Results.CurrentShoe.Reset(Results.OriginalShoe)
        minCards = -1
        For i = 1 To 10
            If Results.CurrentShoe.Cards(i) > 0 Then
                If minCards = -1 Then
                    minCards = Results.CurrentShoe.Cards(i)
                ElseIf Results.CurrentShoe.Cards(i) > 0 And Results.CurrentShoe.Cards(i) < minCards Then
                    minCards = Results.CurrentShoe.Cards(i)
                End If
            End If
        Next i

        If CheckValidInteger(NCardEORBoxEORTab.Text, 1, minCards, True) Then
            EORNcards = CInt(NCardEORBoxEORTab.Text)
        Else
            NCardEORBoxEORTab.Text = EORNcards
        End If
    End Sub

#End Region

#Region " EOR Hand Tab "

    Private Sub PopulateHandEORTable()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 5
            For column = 0 To 8
                Dim box As New IndexedTextBox

                'Populate the column Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.ReadOnly = True
                box.TabStop = False
                box.Index = 0
                Select Case column
                    Case 0, 1, 2
                        If row = 0 And column <> 2 Then
                            box.Location = New System.Drawing.Point(144 + column * 60 + 6, 106 + row * 28)
                            box.Size = New System.Drawing.Size(48, 20)
                        Else
                            box.Location = New System.Drawing.Point(144 + column * 60, 106 + row * 28)
                            box.Size = New System.Drawing.Size(60, 20)
                        End If
                    Case 3, 4, 5
                        If row = 0 And column <> 5 Then
                            box.Location = New System.Drawing.Point(360 + (column - 3) * 60 + 6, 106 + row * 28)
                            box.Size = New System.Drawing.Size(48, 20)
                        Else
                            box.Location = New System.Drawing.Point(360 + (column - 3) * 60, 106 + row * 28)
                            box.Size = New System.Drawing.Size(60, 20)
                        End If
                    Case 6, 7, 8
                        If row = 0 And column <> 8 Then
                            box.Location = New System.Drawing.Point(576 + (column - 6) * 60 + 6, 106 + row * 28)
                            box.Size = New System.Drawing.Size(48, 20)
                        Else
                            box.Location = New System.Drawing.Point(576 + (column - 6) * 60, 106 + row * 28)
                            box.Size = New System.Drawing.Size(60, 20)
                        End If
                End Select

                HandDetailsGroupEORTab.Controls.Add(box)
                EORHandArray(row, column) = box
            Next column
        Next row
    End Sub

    Private Sub ClearHandEORForm(ByVal leaveName As Boolean)
        Dim row As Integer
        Dim column As Integer

        If Not leaveName Then
            HandNameBoxEORTab.Text = ""
            TotalDetailsBoxEORTab.Text = ""
            SoftDetailsCheckEORTab.Checked = False
            NCardsDetailsBoxEORTab.Text = ""
            UCDetailsBoxEORTab.Text = ""
            CardRemovedDetailsBoxEORTab.Text = ""
        End If

        For row = 0 To 5
            For column = 0 To 8
                Select Case row
                    Case 0
                        If column = 2 Or column = 5 Or column = 8 Then
                            FillNumberTextBox(EORHandArray(row, column), 0, 13, False)
                        Else
                            EORHandArray(row, column).Text = ""
                            EORHandArray(row, column).BackColor = FormRules.ColorTable.C(0)
                        End If
                    Case Else
                        FillNumberTextBox(EORHandArray(row, column), 0, 13, False)
                End Select
            Next column
        Next row

    End Sub

    Private Sub LoadEORHandForm()
        Dim cardRemoved As Integer
        Dim index As Integer
        Dim upcard As Integer
        Dim hand As New BJCAHandClass
        Dim stratNum As Integer
        Dim orderEVs(3, 2, 2) As Double 'Strategy, Original/New, FirstEV/SecondEV

        upcard = UCComboBoxEORTab.SelectedIndex + 1
        cardRemoved = CardRemovedComboBoxEORTab.SelectedIndex + 1

        If HandListBoxEORTab.SelectedIndex >= 0 And Not EORs(cardRemoved) Is Nothing Then
            hand = GetStringHand(HandListBoxEORTab.Items(HandListBoxEORTab.SelectedIndex))
            For index = 1 To Results.NumPHands
                If hand.SameAs(Results.PlayerHands(index).Hand) Then
                    Exit For
                End If
            Next index

            HandNameBoxEORTab.Text = GetHandString(Results.PlayerHands(index).Hand)
            TotalDetailsBoxEORTab.Text = CStr(Results.PlayerHands(index).Hand.Total)
            SoftDetailsCheckEORTab.Checked = Results.PlayerHands(index).Hand.Soft
            NCardsDetailsBoxEORTab.Text = CStr(Results.PlayerHands(index).Hand.NumCards)
            UCDetailsBoxEORTab.Text = CStr(upcard)
            CardRemovedDetailsBoxEORTab.Text = CStr(cardRemoved)

            If Results.PlayerHands(index).HandEVs.Prob(upcard) = 0 Or EORs(cardRemoved).EVs(index) Is Nothing Then
                ClearHandEORForm(True)
            Else
                'Figure out the EOR between the best 2 strategies
                If EORs(cardRemoved).EVs(index).StandEV(upcard) = 0 Then
                    ClearHandEORForm(True)
                    FillNumberTextBox(EORHandArray(0, 2), 0, 5, False)
                    FillNumberTextBox(EORHandArray(0, 5), 0, 5, False)
                    FillNumberTextBox(EORHandArray(0, 8), 0, 5, False)
                    EORHandArray(0, 2).Text = "N/A"
                    EORHandArray(0, 5).Text = "N/A"
                    EORHandArray(0, 8).Text = "N/A"
                Else
                    orderEVs(0, 0, 0) = Results.PlayerHands(index).HandEVs.StandEV(upcard)
                    orderEVs(0, 1, 0) = EORs(cardRemoved).EVs(index).StandEV(upcard)
                    If Results.PlayerHands(index).DPreallowed(upcard) And Results.PlayerHands(index).HandEVs.DEV(upcard) > orderEVs(0, 0, 0) Then
                        orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                        orderEVs(0, 0, 0) = Results.PlayerHands(index).HandEVs.DEV(upcard)
                        orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                        orderEVs(0, 1, 0) = EORs(cardRemoved).EVs(index).DEV(upcard)
                    ElseIf Results.PlayerHands(index).DPreallowed(upcard) Then
                        orderEVs(0, 0, 1) = Results.PlayerHands(index).HandEVs.DEV(upcard)
                        orderEVs(0, 1, 1) = EORs(cardRemoved).EVs(index).DEV(upcard)
                    End If
                    If Results.PlayerHands(index).RPreallowed(upcard) > 0 And Results.PlayerHands(index).HandEVs.SurrEV(upcard) > orderEVs(0, 0, 0) Then
                        orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                        orderEVs(0, 0, 0) = Results.PlayerHands(index).HandEVs.SurrEV(upcard)
                        orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                        orderEVs(0, 1, 0) = EORs(cardRemoved).EVs(index).SurrEV(upcard)
                    ElseIf Results.PlayerHands(index).RPreallowed(upcard) > 0 And (Results.PlayerHands(index).HandEVs.SurrEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                        orderEVs(0, 0, 1) = Results.PlayerHands(index).HandEVs.SurrEV(upcard)
                        orderEVs(0, 1, 1) = EORs(cardRemoved).EVs(index).SurrEV(upcard)
                    End If
                    'So far all the values are fixed so copy them to the other 2 strategies
                    For stratNum = 1 To 2
                        orderEVs(stratNum, 0, 0) = orderEVs(0, 0, 0)
                        orderEVs(stratNum, 0, 1) = orderEVs(0, 0, 1)
                        orderEVs(stratNum, 1, 0) = orderEVs(0, 1, 0)
                        orderEVs(stratNum, 1, 1) = orderEVs(0, 1, 1)
                    Next

                    'Forced Strat
                    If Results.Forced.HandEVs(index).EVs.HitEV(upcard) > orderEVs(0, 0, 0) Then
                        orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                        orderEVs(0, 0, 0) = Results.Forced.HandEVs(index).EVs.HitEV(upcard)
                        orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                        orderEVs(0, 1, 0) = EORs(cardRemoved).EVs(index).ForcedHitEV(upcard)
                    ElseIf Results.Forced.HandEVs(index).EVs.HitEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0 Then
                        orderEVs(0, 0, 1) = Results.Forced.HandEVs(index).EVs.HitEV(upcard)
                        orderEVs(0, 1, 1) = EORs(cardRemoved).EVs(index).ForcedHitEV(upcard)
                    End If
                    If Results.Forced.HandEVs(index).SplitEV(upcard) <> 0 And Results.Forced.HandEVs(index).SplitEV(upcard) > orderEVs(0, 0, 0) Then
                        orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                        orderEVs(0, 0, 0) = Results.Forced.HandEVs(index).SplitEV(upcard)
                        orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                        orderEVs(0, 1, 0) = EORs(cardRemoved).EVs(index).ForcedSplitEV(upcard)
                    ElseIf Results.Forced.HandEVs(index).SplitEV(upcard) <> 0 And (Results.Forced.HandEVs(index).SplitEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                        orderEVs(0, 0, 1) = Results.Forced.HandEVs(index).SplitEV(upcard)
                        orderEVs(0, 1, 1) = EORs(cardRemoved).EVs(index).ForcedSplitEV(upcard)
                    End If

                    'CD Strat
                    If Results.Opt.HandEVs(index).EVs.HitEV(upcard) > orderEVs(1, 0, 0) Then
                        orderEVs(1, 0, 1) = orderEVs(1, 0, 0)
                        orderEVs(1, 0, 0) = Results.Opt.HandEVs(index).EVs.HitEV(upcard)
                        orderEVs(1, 1, 1) = orderEVs(1, 1, 0)
                        orderEVs(1, 1, 0) = EORs(cardRemoved).EVs(index).CDHitEV(upcard)
                    ElseIf Results.Opt.HandEVs(index).EVs.HitEV(upcard) > orderEVs(1, 0, 1) Or orderEVs(1, 0, 1) = 0 Then
                        orderEVs(1, 0, 1) = Results.Opt.HandEVs(index).EVs.HitEV(upcard)
                        orderEVs(1, 1, 1) = EORs(cardRemoved).EVs(index).CDHitEV(upcard)
                    End If
                    If Results.Opt.HandEVs(index).SplitEV(upcard) <> 0 And Results.Opt.HandEVs(index).SplitEV(upcard) > orderEVs(1, 0, 0) Then
                        orderEVs(1, 0, 1) = orderEVs(1, 0, 0)
                        orderEVs(1, 0, 0) = Results.Opt.HandEVs(index).SplitEV(upcard)
                        orderEVs(1, 1, 1) = orderEVs(1, 1, 0)
                        orderEVs(1, 1, 0) = EORs(cardRemoved).EVs(index).CDSplitEV(upcard)
                    ElseIf Results.Opt.HandEVs(index).SplitEV(upcard) <> 0 And (Results.Opt.HandEVs(index).SplitEV(upcard) > orderEVs(1, 0, 1) Or orderEVs(1, 0, 1) = 0) Then
                        orderEVs(1, 0, 1) = Results.Opt.HandEVs(index).SplitEV(upcard)
                        orderEVs(1, 1, 1) = EORs(cardRemoved).EVs(index).CDSplitEV(upcard)
                    End If

                    'TD Strat
                    If Results.TD.HandEVs(index).EVs.HitEV(upcard) > orderEVs(2, 0, 0) Then
                        orderEVs(2, 0, 1) = orderEVs(2, 0, 0)
                        orderEVs(2, 0, 0) = Results.TD.HandEVs(index).EVs.HitEV(upcard)
                        orderEVs(2, 1, 1) = orderEVs(2, 1, 0)
                        orderEVs(2, 1, 0) = EORs(cardRemoved).EVs(index).TDHitEV(upcard)
                    ElseIf Results.TD.HandEVs(index).EVs.HitEV(upcard) > orderEVs(2, 0, 1) Or orderEVs(2, 0, 1) = 0 Then
                        orderEVs(2, 0, 1) = Results.TD.HandEVs(index).EVs.HitEV(upcard)
                        orderEVs(2, 1, 1) = EORs(cardRemoved).EVs(index).TDHitEV(upcard)
                    End If
                    If Results.TD.HandEVs(index).SplitEV(upcard) <> 0 And Results.TD.HandEVs(index).SplitEV(upcard) > orderEVs(2, 0, 0) Then
                        orderEVs(2, 0, 1) = orderEVs(2, 0, 0)
                        orderEVs(2, 0, 0) = Results.TD.HandEVs(index).SplitEV(upcard)
                        orderEVs(2, 1, 1) = orderEVs(2, 1, 0)
                        orderEVs(2, 1, 0) = EORs(cardRemoved).EVs(index).TDSplitEV(upcard)
                    ElseIf Results.TD.HandEVs(index).SplitEV(upcard) <> 0 And (Results.TD.HandEVs(index).SplitEV(upcard) > orderEVs(2, 0, 1) Or orderEVs(2, 0, 1) = 0) Then
                        orderEVs(2, 0, 1) = Results.TD.HandEVs(index).SplitEV(upcard)
                        orderEVs(2, 1, 1) = EORs(cardRemoved).EVs(index).TDSplitEV(upcard)
                    End If

                    'EOR = (NewFirst-NewSecond) - (OriginalFirst-OriginalSecond) 
                    FillNumberTextBox(EORHandArray(0, 2), (orderEVs(0, 1, 0) - orderEVs(0, 1, 1) - (orderEVs(0, 0, 0) - orderEVs(0, 0, 1))), 5, False)
                    FillNumberTextBox(EORHandArray(0, 5), (orderEVs(1, 1, 0) - orderEVs(1, 1, 1) - (orderEVs(1, 0, 0) - orderEVs(1, 0, 1))), 5, False)
                    FillNumberTextBox(EORHandArray(0, 8), (orderEVs(2, 1, 0) - orderEVs(2, 1, 1) - (orderEVs(2, 0, 0) - orderEVs(2, 0, 1))), 5, False)

                    FillStratTextBox(EORHandArray(0, 0), Results.Forced.HandEVs(index).EVs.Strat(upcard), False, FormRules.ColorTable)
                    FillStratTextBox(EORHandArray(0, 1), EORs(cardRemoved).EVs(index).ForcedStrat(upcard), False, FormRules.ColorTable)
                    FillStratTextBox(EORHandArray(0, 3), Results.Opt.HandEVs(index).EVs.Strat(upcard), False, FormRules.ColorTable)
                    FillStratTextBox(EORHandArray(0, 4), EORs(cardRemoved).EVs(index).CDStrat(upcard), False, FormRules.ColorTable)
                    FillStratTextBox(EORHandArray(0, 6), Results.TD.HandEVs(index).EVs.Strat(upcard), False, FormRules.ColorTable)
                    FillStratTextBox(EORHandArray(0, 7), EORs(cardRemoved).EVs(index).TDStrat(upcard), False, FormRules.ColorTable)

                    For stratNum = 0 To 2
                        FillNumberTextBox(EORHandArray(1, stratNum * 3), Results.PlayerHands(index).HandEVs.StandEV(upcard), 5, False)
                        FillNumberTextBox(EORHandArray(3, stratNum * 3), Results.PlayerHands(index).HandEVs.DEV(upcard), 5, False)
                        FillNumberTextBox(EORHandArray(4, stratNum * 3), Results.PlayerHands(index).HandEVs.SurrEV(upcard), 5, False)

                        FillNumberTextBox(EORHandArray(1, stratNum * 3 + 1), EORs(cardRemoved).EVs(index).StandEV(upcard), 5, False)
                        FillNumberTextBox(EORHandArray(3, stratNum * 3 + 1), EORs(cardRemoved).EVs(index).DEV(upcard), 5, False)
                        FillNumberTextBox(EORHandArray(4, stratNum * 3 + 1), EORs(cardRemoved).EVs(index).SurrEV(upcard), 5, False)

                        FillNumberTextBox(EORHandArray(1, stratNum * 3 + 2), EORs(cardRemoved).EVs(index).StandEV(upcard) - Results.PlayerHands(index).HandEVs.StandEV(upcard), 5, False)
                        FillNumberTextBox(EORHandArray(3, stratNum * 3 + 2), EORs(cardRemoved).EVs(index).DEV(upcard) - Results.PlayerHands(index).HandEVs.DEV(upcard), 5, False)
                        FillNumberTextBox(EORHandArray(4, stratNum * 3 + 2), EORs(cardRemoved).EVs(index).SurrEV(upcard) - Results.PlayerHands(index).HandEVs.SurrEV(upcard), 5, False)
                    Next

                    FillNumberTextBox(EORHandArray(2, 0), Results.Forced.HandEVs(index).EVs.HitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(2, 1), EORs(cardRemoved).EVs(index).ForcedHitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(2, 2), EORs(cardRemoved).EVs(index).ForcedHitEV(upcard) - Results.Forced.HandEVs(index).EVs.HitEV(upcard), 5, False)

                    FillNumberTextBox(EORHandArray(5, 0), Results.Forced.HandEVs(index).SplitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(5, 1), EORs(cardRemoved).EVs(index).ForcedSplitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(5, 2), EORs(cardRemoved).EVs(index).ForcedSplitEV(upcard) - Results.Forced.HandEVs(index).SplitEV(upcard), 5, False)

                    FillNumberTextBox(EORHandArray(2, 3), Results.Opt.HandEVs(index).EVs.HitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(2, 4), EORs(cardRemoved).EVs(index).CDHitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(2, 5), EORs(cardRemoved).EVs(index).CDHitEV(upcard) - Results.Opt.HandEVs(index).EVs.HitEV(upcard), 5, False)

                    FillNumberTextBox(EORHandArray(5, 3), Results.Opt.HandEVs(index).SplitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(5, 4), EORs(cardRemoved).EVs(index).CDSplitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(5, 5), EORs(cardRemoved).EVs(index).CDSplitEV(upcard) - Results.Opt.HandEVs(index).SplitEV(upcard), 5, False)

                    FillNumberTextBox(EORHandArray(2, 6), Results.TD.HandEVs(index).EVs.HitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(2, 7), EORs(cardRemoved).EVs(index).TDHitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(2, 8), EORs(cardRemoved).EVs(index).TDHitEV(upcard) - Results.TD.HandEVs(index).EVs.HitEV(upcard), 5, False)

                    FillNumberTextBox(EORHandArray(5, 6), Results.TD.HandEVs(index).SplitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(5, 7), EORs(cardRemoved).EVs(index).TDSplitEV(upcard), 5, False)
                    FillNumberTextBox(EORHandArray(5, 8), EORs(cardRemoved).EVs(index).TDSplitEV(upcard) - Results.TD.HandEVs(index).SplitEV(upcard), 5, False)
                End If
            End If
        End If

    End Sub

    Private Sub LoadEORHandListForm()
        Dim index As Integer
        Dim includedHand As New BJCAHandClass
        Dim total As Integer
        Dim either As Boolean
        Dim soft As Integer
        Dim nCards As Integer
        Dim orMore As Boolean
        Dim orLess As Boolean
        Dim listHand As Boolean
        Dim exactMatch As Boolean
        Dim upcard As Integer
        Dim cardRemoved As Integer

        If TotalComboBoxEORTab.SelectedIndex = 0 Then
            total = 0
        Else
            total = TotalComboBoxEORTab.SelectedIndex + 3
        End If
        either = EitherCheckEORTab.Checked
        soft = SoftOnlyCheckEORTab.Checked
        If NCardsComboBoxEORTab.SelectedIndex = 0 Then
            nCards = 0
        Else
            nCards = NCardsComboBoxEORTab.SelectedIndex + 1
        End If
        upcard = UCComboBoxEORTab.SelectedIndex + 1
        cardRemoved = CardRemovedComboBoxEORTab.SelectedIndex + 1
        orMore = OrMoreCheckEORTab.Checked
        orLess = OrLessCheckEORTab.Checked
        includedHand = EORHand
        exactMatch = ExactMatchCheckEORTab.Checked

        'First empty the box
        For index = HandListBoxEORTab.Items.Count - 1 To 0 Step -1
            HandListBoxEORTab.Items.RemoveAt(index)
        Next index

        'Then load the box
        For index = 1 To Results.NumPHands
            listHand = False
            If Results.PlayerHands(index).Hand.NumCards > 1 And Results.PlayerHands(index).HandEVs.Prob(upcard) > 0 And Not EORs(cardRemoved) Is Nothing Then
                If Not exactMatch Then
                    If total = 0 Or Results.PlayerHands(index).Hand.Total = total Then
                        If either Or Results.PlayerHands(index).Hand.Soft = soft Then
                            If nCards = 0 Or (Results.PlayerHands(index).Hand.NumCards = nCards) Or (orMore And Results.PlayerHands(index).Hand.NumCards >= nCards) Or (orLess And Results.PlayerHands(index).Hand.NumCards <= nCards) Then
                                If Results.PlayerHands(index).Hand.Includes(includedHand) Then
                                    If Not ChangedOnlyCheckBoxEORTab.Checked Then
                                        listHand = True
                                    ElseIf Not EORs(cardRemoved).EVs(index) Is Nothing Then
                                        If (Results.Forced.HandEVs(index).EVs.Strat(upcard) <> EORs(cardRemoved).EVs(index).ForcedStrat(upcard) Or Results.Opt.HandEVs(index).EVs.Strat(upcard) <> EORs(cardRemoved).EVs(index).CDStrat(upcard) Or Results.TD.HandEVs(index).EVs.Strat(upcard) <> EORs(cardRemoved).EVs(index).TDStrat(upcard)) Then
                                            listHand = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    If EORHand.SameAs(Results.PlayerHands(index).Hand) Then
                        listHand = True
                    End If
                End If
            End If
            If listHand Then
                HandListBoxEORTab.Items.Add(GetHandString(Results.PlayerHands(index).Hand))
            End If
        Next index
        ListSizeBoxEORTab.Text = HandListBoxEORTab.Items.Count
        ClearHandEORForm(False)
    End Sub

    Private Sub TotalComboBoxEORTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TotalComboBoxEORTab.SelectedIndexChanged
        LoadEORHandListForm()
    End Sub

    Private Sub ChangedOnlyCheckBoxEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangedOnlyCheckBoxEORTab.CheckedChanged
        LoadEORHandListForm()
    End Sub

    Private Sub EitherCheckEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EitherCheckEORTab.CheckedChanged
        If EitherCheckEORTab.Checked Then
            HardOnlyCheckEORTab.Enabled = False
            SoftOnlyCheckEORTab.Enabled = False
            HardOnlyCheckEORTab.Checked = True
            SoftOnlyCheckEORTab.Checked = False
        Else
            HardOnlyCheckEORTab.Enabled = True
            SoftOnlyCheckEORTab.Enabled = True
        End If
        LoadEORHandListForm()
    End Sub

    Private Sub HardOnlyCheckEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardOnlyCheckEORTab.CheckedChanged
        If HardOnlyCheckEORTab.Checked Then
            SoftOnlyCheckEORTab.Checked = False
        Else
            SoftOnlyCheckEORTab.Checked = True
        End If
        LoadEORHandListForm()
    End Sub

    Private Sub SoftOnlyCheckEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftOnlyCheckEORTab.CheckedChanged
        If SoftOnlyCheckEORTab.Checked Then
            HardOnlyCheckEORTab.Checked = False
        Else
            HardOnlyCheckEORTab.Checked = True
        End If
        LoadEORHandListForm()
    End Sub

    Private Sub NCardsComboBoxEORTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NCardsComboBoxEORTab.SelectedIndexChanged
        If NCardsComboBoxEORTab.SelectedIndex = 0 Then
            OrMoreCheckEORTab.Enabled = False
            OrLessCheckEORTab.Enabled = False
            OrMoreCheckEORTab.Checked = False
            OrLessCheckEORTab.Checked = False
        ElseIf NCardsComboBoxEORTab.SelectedIndex = 1 Then
            OrMoreCheckEORTab.Enabled = True
            OrLessCheckEORTab.Enabled = False
            OrLessCheckEORTab.Checked = False
        ElseIf NCardsComboBoxEORTab.SelectedIndex = 20 Then
            OrMoreCheckEORTab.Enabled = False
            OrLessCheckEORTab.Enabled = True
            OrMoreCheckEORTab.Checked = False
        Else
            OrMoreCheckEORTab.Enabled = True
            OrLessCheckEORTab.Enabled = True
        End If
        LoadEORHandListForm()
    End Sub

    Private Sub OrMoreCheckEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrMoreCheckEORTab.CheckedChanged
        If OrMoreCheckEORTab.Checked Then
            OrLessCheckEORTab.Checked = False
        End If
        LoadEORHandListForm()
    End Sub

    Private Sub OrLessCheckEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrLessCheckEORTab.CheckedChanged
        If OrLessCheckEORTab.Checked Then
            OrMoreCheckEORTab.Checked = False
        End If
        LoadEORHandListForm()
    End Sub

    Private Sub HandBoxEORTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles HandBoxEORTab.Validating
        Dim handValid As Boolean
        Dim i As Integer
        Dim card As String

        handValid = True
        For i = 0 To HandBoxEORTab.Text.Length - 1
            card = HandBoxEORTab.Text.Chars(i)
            Select Case card
                Case "A", "a", "2", "3", "4", "5", "6", "7", "8", "9", "T", "t"
                Case Else
                    handValid = False
            End Select
        Next i

        If handValid Then
            EORHand = GetStringHand(HandBoxEORTab.Text)
            If EORHand.Total > 21 Then
                handValid = False
            End If
        End If

        If Not handValid Then
            MsgBox("This is not a valid non-busted player hand.", MsgBoxStyle.OKOnly)
            HandBoxEORTab.Text = ""
            EORHand.Empty()
            e.Cancel = True
        End If

        If ExactMatchCheckEORTab.Checked And (EORHand.NumCards < 2 Or EORHand.Total = 0) Then
            ExactMatchCheckEORTab.Checked = False
        End If

        LoadEORHandListForm()
    End Sub

    Private Sub ExactMatchCheckEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExactMatchCheckEORTab.CheckedChanged
        If EORHand.Total = 0 Then
            ExactMatchCheckEORTab.Checked = False
        ElseIf EORHand.NumCards < 2 Then
            MsgBox("The hand must have at least two card for an exact match.", MsgBoxStyle.OKOnly)
            ExactMatchCheckEORTab.Checked = False
        End If
        If ExactMatchCheckEORTab.Checked Then
            TotalLabelEORTab.Enabled = False
            TotalComboBoxEORTab.Enabled = False
            SoftLabelEORTab.Enabled = False
            EitherCheckEORTab.Enabled = False
            HardOnlyCheckEORTab.Enabled = False
            SoftOnlyCheckEORTab.Enabled = False
            NCardLabelEORTab.Enabled = False
            NCardsComboBoxEORTab.Enabled = False
            OrMoreCheckEORTab.Enabled = False
            OrLessCheckEORTab.Enabled = False
        Else
            TotalLabelEORTab.Enabled = True
            TotalComboBoxEORTab.Enabled = True
            SoftLabelEORTab.Enabled = True
            EitherCheckEORTab.Enabled = True
            If EitherCheckEORTab.Checked Then
                HardOnlyCheckEORTab.Enabled = False
                SoftOnlyCheckEORTab.Enabled = False
            Else
                HardOnlyCheckEORTab.Enabled = True
                SoftOnlyCheckEORTab.Enabled = True
            End If
            NCardLabelEORTab.Enabled = True
            NCardsComboBoxEORTab.Enabled = True
            If NCardsComboBoxEORTab.SelectedIndex = 0 Then
                OrMoreCheckEORTab.Enabled = False
                OrLessCheckEORTab.Enabled = False
                OrMoreCheckEORTab.Checked = False
                OrLessCheckEORTab.Checked = False
            ElseIf NCardsComboBoxEORTab.SelectedIndex = 1 Then
                OrMoreCheckEORTab.Enabled = True
                OrLessCheckEORTab.Enabled = False
                OrLessCheckEORTab.Checked = False
            ElseIf NCardsComboBoxEORTab.SelectedIndex = 20 Then
                OrMoreCheckEORTab.Enabled = False
                OrLessCheckEORTab.Enabled = True
                OrMoreCheckEORTab.Checked = False
            Else
                OrMoreCheckEORTab.Enabled = True
                OrLessCheckEORTab.Enabled = True
            End If
        End If
        LoadEORHandListForm()
    End Sub

    Private Sub HandListBoxEORTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandListBoxEORTab.SelectedIndexChanged
        LoadEORHandForm()
    End Sub

    Private Sub UCComboBoxEORTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UCComboBoxEORTab.SelectedIndexChanged
        If ChangedOnlyCheckBoxEORTab.Checked Then
            LoadEORHandListForm()
        End If
        LoadEORHandForm()
    End Sub

    Private Sub CardRemovedComboBoxEORTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CardRemovedComboBoxEORTab.SelectedIndexChanged
        LoadEORHandListForm()
    End Sub

#End Region

#Region " EOR Total Tab "

    Private Sub PopulateHardEORTable()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 17
            For column = 0 To 14
                Dim box As New IndexedTextBox

                'Populate the column Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.ReadOnly = True
                box.TabStop = False
                box.Visible = False
                box.Index = 0
                Select Case column
                    Case 0, 1, 2
                        If column <> 2 Then
                            box.Location = New System.Drawing.Point(32 + column * 50 + 1, 108 + row * 28)
                            box.Size = New System.Drawing.Size(48, 20)
                        Else
                            box.Location = New System.Drawing.Point(32 + column * 50, 108 + row * 28)
                            box.Size = New System.Drawing.Size(50, 20)
                        End If
                    Case 3, 4, 5
                        box.Location = New System.Drawing.Point(188 + (column - 3) * 50, 108 + row * 28)
                        box.Size = New System.Drawing.Size(50, 20)
                    Case 6, 7, 8
                        box.Location = New System.Drawing.Point(344 + (column - 6) * 50, 108 + row * 28)
                        box.Size = New System.Drawing.Size(50, 20)
                    Case 9, 10, 11
                        box.Location = New System.Drawing.Point(500 + (column - 9) * 50, 108 + row * 28)
                        box.Size = New System.Drawing.Size(50, 20)
                    Case 12, 13, 14
                        box.Location = New System.Drawing.Point(656 + (column - 12) * 50, 108 + row * 28)
                        box.Size = New System.Drawing.Size(50, 20)
                End Select

                TotalTabEORTab.Controls.Add(box)
                EORHardTotalArray(row, column) = box
            Next column
        Next row
    End Sub

    Private Sub PopulateSoftEORTable()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 9
            For column = 0 To 14
                Dim box As New IndexedTextBox

                'Populate the column Table
                box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                box.ReadOnly = True
                box.TabStop = False
                box.Visible = False
                box.Index = 0
                Select Case column
                    Case 0, 1, 2
                        If column <> 2 Then
                            box.Location = New System.Drawing.Point(32 + column * 50 + 1, 108 + row * 28)
                            box.Size = New System.Drawing.Size(48, 20)
                        Else
                            box.Location = New System.Drawing.Point(32 + column * 50, 108 + row * 28)
                            box.Size = New System.Drawing.Size(50, 20)
                        End If
                    Case 3, 4, 5
                        box.Location = New System.Drawing.Point(188 + (column - 3) * 50, 108 + row * 28)
                        box.Size = New System.Drawing.Size(50, 20)
                    Case 6, 7, 8
                        box.Location = New System.Drawing.Point(344 + (column - 6) * 50, 108 + row * 28)
                        box.Size = New System.Drawing.Size(50, 20)
                    Case 9, 10, 11
                        box.Location = New System.Drawing.Point(500 + (column - 9) * 50, 108 + row * 28)
                        box.Size = New System.Drawing.Size(50, 20)
                    Case 12, 13, 14
                        box.Location = New System.Drawing.Point(656 + (column - 12) * 50, 108 + row * 28)
                        box.Size = New System.Drawing.Size(50, 20)
                End Select

                TotalTabEORTab.Controls.Add(box)
                EORSoftTotalArray(row, column) = box
            Next column
        Next row
    End Sub

    Private Sub PopulateHardEORLabels()
        Dim total As Integer

        For total = 4 To 21
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(32, 20)
            label.Location = New System.Drawing.Point(0, 108 + (total - 4) * 28)
            label.Visible = False
            label.Text = total
            label.Index = total - 4
            TotalTabEORTab.Controls.Add(label)
            HardEORLabelArray(total - 4) = label
        Next
    End Sub

    Private Sub PopulateSoftEORLabels()
        Dim total As Integer

        For total = 12 To 21
            Dim label As New IndexedLabel

            label.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            label.Size = New System.Drawing.Size(32, 20)
            label.Location = New System.Drawing.Point(0, 108 + (total - 12) * 28)
            label.Visible = False
            label.Text = total
            label.Index = total - 12
            TotalTabEORTab.Controls.Add(label)
            SoftEORLabelArray(total - 12) = label
        Next
    End Sub

    Private Sub ClearTotalEORForm()
        Dim total As Integer
        Dim column As Integer

        For total = 4 To 21
            For column = 0 To 14
                FillNumberTextBox(EORHardTotalArray(total - 4, column), 0, 13, False)
            Next column
        Next total
        For total = 12 To 21
            For column = 0 To 14
                FillNumberTextBox(EORSoftTotalArray(total - 12, column), 0, 13, False)
            Next column
        Next total
    End Sub

    Private Sub LoadEORTotalForm()
        Dim cardRemoved As Integer
        Dim upcard As Integer
        Dim orderEVs(1, 2, 2) As Double 'Strategy, Original/New, FirstEV/SecondEV
        Dim total As Integer

        ClearTotalEORForm()

        upcard = UCTotalComboBoxEORTab.SelectedIndex + 1
        cardRemoved = CardRemovedTotalComboBoxEORTab.SelectedIndex + 1

        If ForcedButtonEORTab.Checked Then
            For total = 4 To 21
                If Not Results.Forced.StratTD(total, False + 1) Is Nothing Then
                    FillStratTextBox(EORHardTotalArray(total - 4, 0), Results.Forced.StratTD(total, False + 1).Strat(upcard), False, FormRules.ColorTable)
                    FillNumberTextBox(EORHardTotalArray(total - 4, 3), Results.Forced.StratTD(total, False + 1).StratStandEV(upcard), 3, False)
                    FillNumberTextBox(EORHardTotalArray(total - 4, 6), Results.Forced.StratTD(total, False + 1).StratHitEV(upcard), 3, False)
                    FillNumberTextBox(EORHardTotalArray(total - 4, 9), Results.Forced.StratTD(total, False + 1).StratDEV(upcard), 3, False)
                    FillNumberTextBox(EORHardTotalArray(total - 4, 12), Results.Forced.StratTD(total, False + 1).StratSurrEV(upcard), 3, False)

                    If Not EORs(cardRemoved) Is Nothing Then
                        If Not EORs(cardRemoved).ForcedStratTD(total, False + 1) Is Nothing Then
                            FillStratTextBox(EORHardTotalArray(total - 4, 1), EORs(cardRemoved).ForcedStratTD(total, False + 1).Strat(upcard), False, FormRules.ColorTable)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 4), EORs(cardRemoved).ForcedStratTD(total, False + 1).StratStandEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 7), EORs(cardRemoved).ForcedStratTD(total, False + 1).StratHitEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 10), EORs(cardRemoved).ForcedStratTD(total, False + 1).StratDEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 13), EORs(cardRemoved).ForcedStratTD(total, False + 1).StratSurrEV(upcard), 3, False)

                            FillNumberTextBox(EORHardTotalArray(total - 4, 5), EORs(cardRemoved).ForcedStratTD(total, False + 1).StratStandEV(upcard) - Results.Forced.StratTD(total, False + 1).StratStandEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 8), EORs(cardRemoved).ForcedStratTD(total, False + 1).StratHitEV(upcard) - Results.Forced.StratTD(total, False + 1).StratHitEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 11), EORs(cardRemoved).ForcedStratTD(total, False + 1).StratDEV(upcard) - Results.Forced.StratTD(total, False + 1).StratDEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 14), EORs(cardRemoved).ForcedStratTD(total, False + 1).StratSurrEV(upcard) - Results.Forced.StratTD(total, False + 1).StratSurrEV(upcard), 3, False)

                            orderEVs(0, 0, 0) = Results.Forced.StratTD(total, False + 1).StratStandEV(upcard)
                            orderEVs(0, 1, 0) = EORs(cardRemoved).ForcedStratTD(total, False + 1).StratStandEV(upcard)
                            orderEVs(0, 0, 1) = 0
                            orderEVs(0, 1, 1) = 0
                            If Results.Forced.StratTD(total, False + 1).StratHitEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.Forced.StratTD(total, False + 1).StratHitEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).ForcedStratTD(total, False + 1).StratHitEV(upcard)
                            ElseIf Results.Forced.StratTD(total, False + 1).StratHitEV(upcard) <> 0 Then
                                orderEVs(0, 0, 1) = Results.Forced.StratTD(total, False + 1).StratHitEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).ForcedStratTD(total, False + 1).StratHitEV(upcard)
                            End If
                            If Results.Forced.StratTD(total, False + 1).StratDEV(upcard) <> 0 And Results.Forced.StratTD(total, False + 1).StratDEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.Forced.StratTD(total, False + 1).StratDEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).ForcedStratTD(total, False + 1).StratDEV(upcard)
                            ElseIf Results.Forced.StratTD(total, False + 1).StratDEV(upcard) <> 0 And (Results.Forced.StratTD(total, False + 1).StratDEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                                orderEVs(0, 0, 1) = Results.Forced.StratTD(total, False + 1).StratDEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).ForcedStratTD(total, False + 1).StratDEV(upcard)
                            End If
                            If Results.Forced.StratTD(total, False + 1).StratSurrEV(upcard) <> 0 And Results.Forced.StratTD(total, False + 1).StratSurrEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.Forced.StratTD(total, False + 1).StratSurrEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).ForcedStratTD(total, False + 1).StratSurrEV(upcard)
                            ElseIf Results.Forced.StratTD(total, False + 1).StratSurrEV(upcard) <> 0 And (Results.Forced.StratTD(total, False + 1).StratSurrEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                                orderEVs(0, 0, 1) = Results.Forced.StratTD(total, False + 1).StratSurrEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).ForcedStratTD(total, False + 1).StratSurrEV(upcard)
                            End If
                            FillNumberTextBox(EORHardTotalArray(total - 4, 2), ((orderEVs(0, 1, 0) - orderEVs(0, 1, 1)) - (orderEVs(0, 0, 0) - orderEVs(0, 0, 1))), 3, False)
                        End If
                    End If
                End If
            Next total
            For total = 12 To 21
                If Not Results.Forced.StratTD(total, True + 1) Is Nothing Then
                    FillStratTextBox(EORSoftTotalArray(total - 12, 0), Results.Forced.StratTD(total, True + 1).Strat(upcard), False, FormRules.ColorTable)
                    FillNumberTextBox(EORSoftTotalArray(total - 12, 3), Results.Forced.StratTD(total, True + 1).StratStandEV(upcard), 3, False)
                    FillNumberTextBox(EORSoftTotalArray(total - 12, 6), Results.Forced.StratTD(total, True + 1).StratHitEV(upcard), 3, False)
                    FillNumberTextBox(EORSoftTotalArray(total - 12, 9), Results.Forced.StratTD(total, True + 1).StratDEV(upcard), 3, False)
                    FillNumberTextBox(EORSoftTotalArray(total - 12, 12), Results.Forced.StratTD(total, True + 1).StratSurrEV(upcard), 3, False)

                    If Not EORs(cardRemoved) Is Nothing Then
                        If Not EORs(cardRemoved).ForcedStratTD(total, True + 1) Is Nothing Then
                            FillStratTextBox(EORSoftTotalArray(total - 12, 1), EORs(cardRemoved).ForcedStratTD(total, True + 1).Strat(upcard), False, FormRules.ColorTable)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 4), EORs(cardRemoved).ForcedStratTD(total, True + 1).StratStandEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 7), EORs(cardRemoved).ForcedStratTD(total, True + 1).StratHitEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 10), EORs(cardRemoved).ForcedStratTD(total, True + 1).StratDEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 13), EORs(cardRemoved).ForcedStratTD(total, True + 1).StratSurrEV(upcard), 3, False)

                            FillNumberTextBox(EORSoftTotalArray(total - 12, 5), EORs(cardRemoved).ForcedStratTD(total, True + 1).StratStandEV(upcard) - Results.Forced.StratTD(total, True + 1).StratStandEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 8), EORs(cardRemoved).ForcedStratTD(total, True + 1).StratHitEV(upcard) - Results.Forced.StratTD(total, True + 1).StratHitEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 11), EORs(cardRemoved).ForcedStratTD(total, True + 1).StratDEV(upcard) - Results.Forced.StratTD(total, True + 1).StratDEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 14), EORs(cardRemoved).ForcedStratTD(total, True + 1).StratSurrEV(upcard) - Results.Forced.StratTD(total, True + 1).StratSurrEV(upcard), 3, False)

                            orderEVs(0, 0, 0) = Results.Forced.StratTD(total, True + 1).StratStandEV(upcard)
                            orderEVs(0, 1, 0) = EORs(cardRemoved).ForcedStratTD(total, True + 1).StratStandEV(upcard)
                            orderEVs(0, 0, 1) = 0
                            orderEVs(0, 1, 1) = 0
                            If Results.Forced.StratTD(total, True + 1).StratHitEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.Forced.StratTD(total, True + 1).StratHitEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).ForcedStratTD(total, True + 1).StratHitEV(upcard)
                            ElseIf Results.Forced.StratTD(total, True + 1).StratHitEV(upcard) <> 0 Then
                                orderEVs(0, 0, 1) = Results.Forced.StratTD(total, True + 1).StratHitEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).ForcedStratTD(total, True + 1).StratHitEV(upcard)
                            End If
                            If Results.Forced.StratTD(total, True + 1).StratDEV(upcard) <> 0 And Results.Forced.StratTD(total, True + 1).StratDEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.Forced.StratTD(total, True + 1).StratDEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).ForcedStratTD(total, True + 1).StratDEV(upcard)
                            ElseIf Results.Forced.StratTD(total, True + 1).StratDEV(upcard) <> 0 And (Results.Forced.StratTD(total, True + 1).StratDEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                                orderEVs(0, 0, 1) = Results.Forced.StratTD(total, True + 1).StratDEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).ForcedStratTD(total, True + 1).StratDEV(upcard)
                            End If
                            If Results.Forced.StratTD(total, True + 1).StratSurrEV(upcard) <> 0 And Results.Forced.StratTD(total, True + 1).StratSurrEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.Forced.StratTD(total, True + 1).StratSurrEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).ForcedStratTD(total, True + 1).StratSurrEV(upcard)
                            ElseIf Results.Forced.StratTD(total, True + 1).StratSurrEV(upcard) <> 0 And (Results.Forced.StratTD(total, True + 1).StratSurrEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                                orderEVs(0, 0, 1) = Results.Forced.StratTD(total, True + 1).StratSurrEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).ForcedStratTD(total, True + 1).StratSurrEV(upcard)
                            End If
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 2), ((orderEVs(0, 1, 0) - orderEVs(0, 1, 1)) - (orderEVs(0, 0, 0) - orderEVs(0, 0, 1))), 3, False)
                        End If
                    End If
                End If
            Next total
        Else
            For total = 4 To 21
                If Not Results.TD.StratTD(total, False + 1) Is Nothing Then
                    FillStratTextBox(EORHardTotalArray(total - 4, 0), Results.TD.StratTD(total, False + 1).Strat(upcard), False, FormRules.ColorTable)
                    FillNumberTextBox(EORHardTotalArray(total - 4, 3), Results.TD.StratTD(total, False + 1).StratStandEV(upcard), 3, False)
                    FillNumberTextBox(EORHardTotalArray(total - 4, 6), Results.TD.StratTD(total, False + 1).StratHitEV(upcard), 3, False)
                    FillNumberTextBox(EORHardTotalArray(total - 4, 9), Results.TD.StratTD(total, False + 1).StratDEV(upcard), 3, False)
                    FillNumberTextBox(EORHardTotalArray(total - 4, 12), Results.TD.StratTD(total, False + 1).StratSurrEV(upcard), 3, False)

                    If Not EORs(cardRemoved) Is Nothing Then
                        If Not EORs(cardRemoved).TDStratTD(total, False + 1) Is Nothing Then
                            FillStratTextBox(EORHardTotalArray(total - 4, 1), EORs(cardRemoved).TDStratTD(total, False + 1).Strat(upcard), False, FormRules.ColorTable)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 4), EORs(cardRemoved).TDStratTD(total, False + 1).StratStandEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 7), EORs(cardRemoved).TDStratTD(total, False + 1).StratHitEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 10), EORs(cardRemoved).TDStratTD(total, False + 1).StratDEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 13), EORs(cardRemoved).TDStratTD(total, False + 1).StratSurrEV(upcard), 3, False)

                            FillNumberTextBox(EORHardTotalArray(total - 4, 5), EORs(cardRemoved).TDStratTD(total, False + 1).StratStandEV(upcard) - Results.TD.StratTD(total, False + 1).StratStandEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 8), EORs(cardRemoved).TDStratTD(total, False + 1).StratHitEV(upcard) - Results.TD.StratTD(total, False + 1).StratHitEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 11), EORs(cardRemoved).TDStratTD(total, False + 1).StratDEV(upcard) - Results.TD.StratTD(total, False + 1).StratDEV(upcard), 3, False)
                            FillNumberTextBox(EORHardTotalArray(total - 4, 14), EORs(cardRemoved).TDStratTD(total, False + 1).StratSurrEV(upcard) - Results.TD.StratTD(total, False + 1).StratSurrEV(upcard), 3, False)

                            orderEVs(0, 0, 0) = Results.TD.StratTD(total, False + 1).StratStandEV(upcard)
                            orderEVs(0, 1, 0) = EORs(cardRemoved).TDStratTD(total, False + 1).StratStandEV(upcard)
                            orderEVs(0, 0, 1) = 0
                            orderEVs(0, 1, 1) = 0
                            If Results.TD.StratTD(total, False + 1).StratHitEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.TD.StratTD(total, False + 1).StratHitEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).TDStratTD(total, False + 1).StratHitEV(upcard)
                            ElseIf Results.TD.StratTD(total, False + 1).StratHitEV(upcard) <> 0 Then
                                orderEVs(0, 0, 1) = Results.TD.StratTD(total, False + 1).StratHitEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).TDStratTD(total, False + 1).StratHitEV(upcard)
                            End If
                            If Results.TD.StratTD(total, False + 1).StratDEV(upcard) <> 0 And Results.TD.StratTD(total, False + 1).StratDEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.TD.StratTD(total, False + 1).StratDEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).TDStratTD(total, False + 1).StratDEV(upcard)
                            ElseIf Results.TD.StratTD(total, False + 1).StratDEV(upcard) <> 0 And (Results.TD.StratTD(total, False + 1).StratDEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                                orderEVs(0, 0, 1) = Results.TD.StratTD(total, False + 1).StratDEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).TDStratTD(total, False + 1).StratDEV(upcard)
                            End If
                            If Results.TD.StratTD(total, False + 1).StratSurrEV(upcard) <> 0 And Results.TD.StratTD(total, False + 1).StratSurrEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.TD.StratTD(total, False + 1).StratSurrEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).TDStratTD(total, False + 1).StratSurrEV(upcard)
                            ElseIf Results.TD.StratTD(total, False + 1).StratSurrEV(upcard) <> 0 And (Results.TD.StratTD(total, False + 1).StratSurrEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                                orderEVs(0, 0, 1) = Results.TD.StratTD(total, False + 1).StratSurrEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).TDStratTD(total, False + 1).StratSurrEV(upcard)
                            End If
                            FillNumberTextBox(EORHardTotalArray(total - 4, 2), ((orderEVs(0, 1, 0) - orderEVs(0, 1, 1)) - (orderEVs(0, 0, 0) - orderEVs(0, 0, 1))), 3, False)
                        End If
                    End If
                End If
            Next total
            For total = 12 To 21
                If Not Results.TD.StratTD(total, True + 1) Is Nothing Then
                    FillStratTextBox(EORSoftTotalArray(total - 12, 0), Results.TD.StratTD(total, True + 1).Strat(upcard), False, FormRules.ColorTable)
                    FillNumberTextBox(EORSoftTotalArray(total - 12, 3), Results.TD.StratTD(total, True + 1).StratStandEV(upcard), 3, False)
                    FillNumberTextBox(EORSoftTotalArray(total - 12, 6), Results.TD.StratTD(total, True + 1).StratHitEV(upcard), 3, False)
                    FillNumberTextBox(EORSoftTotalArray(total - 12, 9), Results.TD.StratTD(total, True + 1).StratDEV(upcard), 3, False)
                    FillNumberTextBox(EORSoftTotalArray(total - 12, 12), Results.TD.StratTD(total, True + 1).StratSurrEV(upcard), 3, False)

                    If Not EORs(cardRemoved) Is Nothing Then
                        If Not EORs(cardRemoved).TDStratTD(total, True + 1) Is Nothing Then
                            FillStratTextBox(EORSoftTotalArray(total - 12, 1), EORs(cardRemoved).TDStratTD(total, True + 1).Strat(upcard), False, FormRules.ColorTable)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 4), EORs(cardRemoved).TDStratTD(total, True + 1).StratStandEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 7), EORs(cardRemoved).TDStratTD(total, True + 1).StratHitEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 10), EORs(cardRemoved).TDStratTD(total, True + 1).StratDEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 13), EORs(cardRemoved).TDStratTD(total, True + 1).StratSurrEV(upcard), 3, False)

                            FillNumberTextBox(EORSoftTotalArray(total - 12, 5), EORs(cardRemoved).TDStratTD(total, True + 1).StratStandEV(upcard) - Results.TD.StratTD(total, True + 1).StratStandEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 8), EORs(cardRemoved).TDStratTD(total, True + 1).StratHitEV(upcard) - Results.TD.StratTD(total, True + 1).StratHitEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 11), EORs(cardRemoved).TDStratTD(total, True + 1).StratDEV(upcard) - Results.TD.StratTD(total, True + 1).StratDEV(upcard), 3, False)
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 14), EORs(cardRemoved).TDStratTD(total, True + 1).StratSurrEV(upcard) - Results.TD.StratTD(total, True + 1).StratSurrEV(upcard), 3, False)

                            orderEVs(0, 0, 0) = Results.TD.StratTD(total, True + 1).StratStandEV(upcard)
                            orderEVs(0, 1, 0) = EORs(cardRemoved).TDStratTD(total, True + 1).StratStandEV(upcard)
                            orderEVs(0, 0, 1) = 0
                            orderEVs(0, 1, 1) = 0
                            If Results.TD.StratTD(total, True + 1).StratHitEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.TD.StratTD(total, True + 1).StratHitEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).TDStratTD(total, True + 1).StratHitEV(upcard)
                            ElseIf Results.TD.StratTD(total, True + 1).StratHitEV(upcard) <> 0 Then
                                orderEVs(0, 0, 1) = Results.TD.StratTD(total, True + 1).StratHitEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).TDStratTD(total, True + 1).StratHitEV(upcard)
                            End If
                            If Results.TD.StratTD(total, True + 1).StratDEV(upcard) <> 0 And Results.TD.StratTD(total, True + 1).StratDEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.TD.StratTD(total, True + 1).StratDEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).TDStratTD(total, True + 1).StratDEV(upcard)
                            ElseIf Results.TD.StratTD(total, True + 1).StratDEV(upcard) <> 0 And (Results.TD.StratTD(total, True + 1).StratDEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                                orderEVs(0, 0, 1) = Results.TD.StratTD(total, True + 1).StratDEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).TDStratTD(total, True + 1).StratDEV(upcard)
                            End If
                            If Results.TD.StratTD(total, True + 1).StratSurrEV(upcard) <> 0 And Results.TD.StratTD(total, True + 1).StratSurrEV(upcard) > orderEVs(0, 0, 0) Then
                                orderEVs(0, 0, 1) = orderEVs(0, 0, 0)
                                orderEVs(0, 0, 0) = Results.TD.StratTD(total, True + 1).StratSurrEV(upcard)
                                orderEVs(0, 1, 1) = orderEVs(0, 1, 0)
                                orderEVs(0, 1, 0) = EORs(cardRemoved).TDStratTD(total, True + 1).StratSurrEV(upcard)
                            ElseIf Results.TD.StratTD(total, True + 1).StratSurrEV(upcard) <> 0 And (Results.TD.StratTD(total, True + 1).StratSurrEV(upcard) > orderEVs(0, 0, 1) Or orderEVs(0, 0, 1) = 0) Then
                                orderEVs(0, 0, 1) = Results.TD.StratTD(total, True + 1).StratSurrEV(upcard)
                                orderEVs(0, 1, 1) = EORs(cardRemoved).TDStratTD(total, True + 1).StratSurrEV(upcard)
                            End If
                            FillNumberTextBox(EORSoftTotalArray(total - 12, 2), ((orderEVs(0, 1, 0) - orderEVs(0, 1, 1)) - (orderEVs(0, 0, 0) - orderEVs(0, 0, 1))), 3, False)
                        End If
                    End If
                End If
            Next total
        End If

    End Sub

    Private Sub HardButtonEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardButtonEORTab.CheckedChanged
        Dim total As Integer
        Dim column As Integer

        If HardButtonEORTab.Checked Then
            For total = 12 To 21
                SoftEORLabelArray(total - 12).Visible = False
                For column = 0 To 14
                    EORSoftTotalArray(total - 12, column).Visible = False
                Next column
            Next total
            For total = 4 To 21
                HardEORLabelArray(total - 4).Visible = True
                For column = 0 To 14
                    EORHardTotalArray(total - 4, column).Visible = True
                Next column
            Next total
        Else
            For total = 4 To 21
                HardEORLabelArray(total - 4).Visible = False
                For column = 0 To 14
                    EORHardTotalArray(total - 4, column).Visible = False
                Next column
            Next total
            For total = 12 To 21
                SoftEORLabelArray(total - 12).Visible = True
                For column = 0 To 14
                    EORSoftTotalArray(total - 12, column).Visible = True
                Next column
            Next total
        End If
    End Sub

    Private Sub SoftButtonEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftButtonEORTab.CheckedChanged
        Dim total As Integer
        Dim column As Integer

        If HardButtonEORTab.Checked Then
            For total = 12 To 21
                SoftEORLabelArray(total - 12).Visible = False
                For column = 0 To 14
                    EORSoftTotalArray(total - 12, column).Visible = False
                Next column
            Next total
            For total = 4 To 21
                HardEORLabelArray(total - 4).Visible = True
                For column = 0 To 14
                    EORHardTotalArray(total - 4, column).Visible = True
                Next column
            Next total
        Else
            For total = 4 To 21
                HardEORLabelArray(total - 4).Visible = False
                For column = 0 To 14
                    EORHardTotalArray(total - 4, column).Visible = False
                Next column
            Next total
            For total = 12 To 21
                SoftEORLabelArray(total - 12).Visible = True
                For column = 0 To 14
                    EORSoftTotalArray(total - 12, column).Visible = True
                Next column
            Next total
        End If
    End Sub

    Private Sub ForcedButtonEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedButtonEORTab.CheckedChanged
        LoadEORTotalForm()
    End Sub

    Private Sub TDButtonEORTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDButtonEORTab.CheckedChanged
        LoadEORTotalForm()
    End Sub

    Private Sub UCTotalComboBoxEORTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UCTotalComboBoxEORTab.SelectedIndexChanged
        LoadEORTotalForm()
    End Sub

    Private Sub CardRemovedTotalComboBoxEORTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CardRemovedTotalComboBoxEORTab.SelectedIndexChanged
        LoadEORTotalForm()
    End Sub

#End Region

#End Region

#Region " Splits Tab "

    Private Sub PopulateSplitsTable()
        Dim card As Integer
        Dim row As Integer
        Dim upcard As Integer

        For card = 0 To 4
            For row = 0 To 2
                For upcard = 0 To 9
                    Dim box As New IndexedTextBox

                    'Populate the Split EVs Table
                    box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                    box.Size = New System.Drawing.Size(72, 20)
                    box.ReadOnly = True
                    box.TabStop = False
                    box.Index = 0
                    If upcard = 0 Then
                        box.Location = New System.Drawing.Point(86 + 9 * 72, 102 + card * 92 + row * 28)
                    Else
                        box.Location = New System.Drawing.Point(86 + (upcard - 1) * 72, 102 + card * 92 + row * 28)
                    End If

                    SplitsTab.Controls.Add(box)
                    SplitsArray(card, row, upcard) = box
                Next upcard
            Next row
        Next card

    End Sub

    Private Sub LoadFormSplitTableValues(ByRef cStrat As BJCAStrategyClass, ByVal first5 As Boolean, Optional ByVal eraseAll As Boolean = False)
        Dim card As Integer
        Dim splits As Integer
        Dim upcard As Integer
        Dim minCard As Integer
        Dim maxCard As Integer
        Dim arrayCard As Integer

        If first5 Then
            minCard = 1
            maxCard = 5
        Else
            minCard = 6
            maxCard = 10
        End If

        For card = minCard To maxCard
            If first5 Then
                arrayCard = card
            Else
                arrayCard = card - 5
            End If
            For upcard = 1 To 10
                For splits = 1 To Results.SPL
                    If Not cStrat.HandEVs(Results.SplitIndex(card, upcard)) Is Nothing Then
                        FillNumberTextBox(SplitsArray(arrayCard - 1, splits - 1, upcard - 1), cStrat.HandEVs(Results.SplitIndex(card, upcard)).SPLEV(upcard, splits), 6, False, (Not eraseAll And Results.SplitIndex(card, upcard) > 0))
                    Else
                        FillNumberTextBox(SplitsArray(arrayCard - 1, splits - 1, upcard - 1), 0, 6, False, False)
                    End If
                Next splits
            Next upcard
        Next card

    End Sub

    Private Sub LoadFormSplitTable()
        Dim first5 As Boolean

        If Ato5ButtonSplitTab.Checked Then
            Card1LabelSplitTab.Text = "A, A"
            Card2LabelSplitTab.Text = "2, 2"
            Card3LabelSplitTab.Text = "3, 3"
            Card4LabelSplitTab.Text = "4, 4"
            Card5LabelSplitTab.Text = "5, 5"
            first5 = True
        Else
            Card1LabelSplitTab.Text = "6, 6"
            Card2LabelSplitTab.Text = "7, 7"
            Card3LabelSplitTab.Text = "8, 8"
            Card4LabelSplitTab.Text = "9, 9"
            Card5LabelSplitTab.Text = "T, T"
            first5 = False
        End If
        If TDButtonSplitTab.Checked Then
            LoadFormSplitTableValues(Results.TD, first5, Not Results.TD.ComputeStrat)
        ElseIf TCButtonSplitTab.Checked Then
            LoadFormSplitTableValues(Results.TC, first5, Not Results.TC.ComputeStrat)
        ElseIf CDButtonSplitTab.Checked Then
            LoadFormSplitTableValues(Results.Opt, first5)
        Else
            LoadFormSplitTableValues(Results.Forced, first5, Not Results.Forced.ComputeStrat)
        End If

    End Sub

    Private Sub Ato5ButtonSplitTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ato5ButtonSplitTab.CheckedChanged
        LoadFormSplitTable()
    End Sub

    Private Sub SixtoTButtonSplitTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SixtoTButtonSplitTab.CheckedChanged
        LoadFormSplitTable()
    End Sub

    Private Sub TDButtonSplitTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDButtonSplitTab.CheckedChanged
        LoadFormSplitTable()
    End Sub

    Private Sub TCButtonSplitTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TCButtonSplitTab.CheckedChanged
        LoadFormSplitTable()
    End Sub

    Private Sub CDButtonSplitTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDButtonSplitTab.CheckedChanged
        LoadFormSplitTable()
    End Sub

    Private Sub ForcedButtonSplitTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedButtonSplitTab.CheckedChanged
        LoadFormSplitTable()
    End Sub

#End Region

#Region " Other Tab "

    Private Sub UpdateAllColors()
        Dim row As Integer
        Dim upcard As Integer

        For upcard = 0 To 9 Step 1
            'Recolor Hard TD Tables
            For row = 0 To 17
                HardTDStratTableArray(row, upcard).BackColor = FormRules.ColorTable.C(HardTDStratTableArray(row, upcard).Index)
            Next row
            For row = 0 To 16
                HardTDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(HardTDForcedTableArray(row, upcard).Index)
            Next row
            'Recolor Hard CD Tables
            For row = 0 To 35
                HardCDStratTableArray(row, upcard).BackColor = FormRules.ColorTable.C(HardCDStratTableArray(row, upcard).Index)

                HardCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(HardCDForcedTableArray(row, upcard).Index)
            Next row
            'Recolor Soft Tables
            For row = 0 To 8
                SoftCDStratTableArray(row, upcard).BackColor = FormRules.ColorTable.C(SoftCDStratTableArray(row, upcard).Index)

                SoftTDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(SoftTDForcedTableArray(row, upcard).Index)
                SoftCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(SoftCDForcedTableArray(row, upcard).Index)
            Next row
            'Recolor Pair Tables
            For row = 0 To 9
                SoftTDStratTableArray(row, upcard).BackColor = FormRules.ColorTable.C(SoftTDStratTableArray(row, upcard).Index)
                PairCDStratTableArray(row, upcard).BackColor = FormRules.ColorTable.C(PairCDStratTableArray(row, upcard).Index)

                PairCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(PairCDForcedTableArray(row, upcard).Index)
            Next row
        Next upcard

        ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(CurrentForcedRuleStrat)
        LoadHandHAForm()
        LoadHandSizeAnalysisForm()
        LoadHandEForm()

    End Sub

    Private Sub PopulateStratColorTable()
        Dim strat As Integer

        For strat = 0 To Constants.NumStrats - 1
            Dim box As New IndexedTextBox

            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(64, 20)
            box.ReadOnly = True
            box.BackColor = FormRules.ColorTable.C(strat)
            box.Text = Constants.StratLongText(strat)
            box.Index = strat
            If strat < 12 Then
                box.Location = New System.Drawing.Point(24, 32 + 24 * strat)
            ElseIf strat = 12 Or strat = 13 Then
                box.Location = New System.Drawing.Point(104, 32 + 24 * (strat - 11))
            ElseIf strat = 14 Or strat = 15 Then
                box.Location = New System.Drawing.Point(104, 32 + 24 * (strat - 10))
            Else
                box.Location = New System.Drawing.Point(104, 32 + 24 * (strat - 9))
            End If

            AddHandler box.Click, AddressOf StratColorBoxArrayHandler_Click

            'Add the CheckBox to the array so it can be accessed by index.
            StratColorBoxArray(strat) = box

            'Add the CheckBox to the Controls collection so it is visible.
            ColorTableGroupOTab.Controls.Add(box)

        Next strat
    End Sub

    Private Sub StratColorBoxArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StratColorBoxArrayHandler.Click
        Dim colorBox As New System.Windows.Forms.ColorDialog

        colorBox.ShowDialog()
        FormRules.ColorTable.C(DirectCast(sender, IndexedTextBox).Index) = colorBox.Color
        StratColorBoxArray(DirectCast(sender, IndexedTextBox).Index).BackColor = colorBox.Color

        'UpdateForcedColorTable.C()
        colorBox.Dispose()
        UpdateAllColors()
        ColorTableGroupOTab.Focus()
    End Sub

    Private Sub LoadFormColorTable()
        Dim strat As Integer

        For strat = 0 To Constants.NumStrats - 1
            StratColorBoxArray(strat).BackColor = FormRules.ColorTable.C(strat)
        Next strat
        UpdateAllColors()
    End Sub

    Private Sub RestoreDefaultColorTableButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreDefaultColorTableButtonOTab.Click
        If MsgBox("Restoring the default Color Table will erase any user selected colors." + Chr(13) + Chr(9) + Chr(9) + "Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreDefaultColorTable()
            LoadFormColorTable()
        End If
    End Sub

    Private Sub RestoreDefaultColorTable()
        FormRules.ColorTable = CloneObject(FormRules.DefaultColorTable)
        Filenames.ColorTableFileName = "Default"
        LoadFormColorTable()
    End Sub


#End Region























End Class
