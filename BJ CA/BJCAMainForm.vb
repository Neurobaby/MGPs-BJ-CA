Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports BJ_CA.BJCAShared
Imports System.Diagnostics.Process

Public Class BJCAMainForm
    Inherits System.Windows.Forms.Form

#Region " Test Area "

    Public Sub RunTest()
        Dim i As Integer
        Dim testobject As New BJCAFormRulesClass
        Dim testobject2 As New BJCAFormRulesClass
        Dim runningtime As Double
        Dim runningtime2 As Double

        runningtime = Environment.TickCount
        For i = 1 To 1000000
            testobject.DefineDefaultGeneralRules()
        Next i
        runningtime = Environment.TickCount - runningtime

        runningtime2 = Environment.TickCount
        '        For i = 1 To 5000
        '        testobject = BJCAMethods.CloneObject(testobject2)
        '        Next i
        runningtime2 = Environment.TickCount - runningtime2
        MsgBox("Brute force: " + CStr(runningtime) + Chr(13) + "Cloned: " + CStr(runningtime2), MsgBoxStyle.OKOnly)

        i = 1
    End Sub

#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeBJCAForm()
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
    Friend WithEvents MainRulesTab As System.Windows.Forms.TabPage
    Friend WithEvents DoubleRulesTab As System.Windows.Forms.TabPage
    Friend WithEvents SurrenderRulesTab As System.Windows.Forms.TabPage
    Friend WithEvents ShoeOptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents SplitOptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents BonusRulesTab As System.Windows.Forms.TabPage
    Friend WithEvents MainTabs As System.Windows.Forms.TabControl
    Friend WithEvents NDecksLabelMTab As System.Windows.Forms.Label
    Friend WithEvents NDecksBoxMTab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedShoeCheckMTab As System.Windows.Forms.CheckBox
    Friend WithEvents InfiniteDecksButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents FiniteDecksButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents SMACheckMTab As System.Windows.Forms.CheckBox
    Friend WithEvents SPL3ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents SPL1ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents SPL2ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents NSPlitsLabelMTab As System.Windows.Forms.Label
    Friend WithEvents SPL0ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents HSACheckMTab As System.Windows.Forms.CheckBox
    Friend WithEvents D1011ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents D91011ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents DOAButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents DANCheckMTab As System.Windows.Forms.CheckBox
    Friend WithEvents DASCheckMTab As System.Windows.Forms.CheckBox
    Friend WithEvents SurrenderPaysLabelMTab As System.Windows.Forms.Label
    Friend WithEvents ESButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents LSButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents DANCheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents DASCheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents DToggleAllCheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents DTableButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents DTableButtonDTab As System.Windows.Forms.RadioButton
    Friend WithEvents D1011ButtonDTab As System.Windows.Forms.RadioButton
    Friend WithEvents D91011ButtonDTab As System.Windows.Forms.RadioButton
    Friend WithEvents DOAButtonDTab As System.Windows.Forms.RadioButton
    Friend WithEvents ShoeOptionGroupMTab As System.Windows.Forms.GroupBox
    Friend WithEvents SplitGroupMTab As System.Windows.Forms.GroupBox
    Friend WithEvents DoubleGroupMTab As System.Windows.Forms.GroupBox
    Friend WithEvents SurrenderGroupMTab As System.Windows.Forms.GroupBox
    Friend WithEvents DoubleGroupDTab As System.Windows.Forms.GroupBox
    Friend WithEvents ShoeOptionGroupShTab As System.Windows.Forms.GroupBox
    Friend WithEvents NDecksLabelShTab As System.Windows.Forms.Label
    Friend WithEvents NDecksBoxShTab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedShoeCheckShTab As System.Windows.Forms.CheckBox
    Friend WithEvents InfiniteDecksButtonShTab As System.Windows.Forms.RadioButton
    Friend WithEvents FiniteDecksButtonShTab As System.Windows.Forms.RadioButton
    Friend WithEvents CopyRefShoeButtonShTab As System.Windows.Forms.Button
    Friend WithEvents STableButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents STableButtonSTab As System.Windows.Forms.RadioButton
    Friend WithEvents ESButtonSTab As System.Windows.Forms.RadioButton
    Friend WithEvents LSButtonSTab As System.Windows.Forms.RadioButton
    Friend WithEvents NSButtonSTab As System.Windows.Forms.RadioButton
    Friend WithEvents NSButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents SANCheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents SASCheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents DoubleTableGroup As System.Windows.Forms.GroupBox
    Friend WithEvents HardLabelDTab As System.Windows.Forms.Label
    Friend WithEvents SurrenderTableGroup As System.Windows.Forms.GroupBox
    Friend WithEvents SoftLabelDTab As System.Windows.Forms.Label
    Friend WithEvents SpecialRulesTab As System.Windows.Forms.TabPage
    Friend WithEvents HoleCardGroupMTab As System.Windows.Forms.GroupBox
    Friend WithEvents OBBOButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents ENHCButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents BBOButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents OBOButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents SurrenderTo1LabelMTab As System.Windows.Forms.Label
    Friend WithEvents DealerRulesGroupMTab As System.Windows.Forms.GroupBox
    Friend WithEvents BJTo1LabelMTab As System.Windows.Forms.Label
    Friend WithEvents BJPaysBoxMTab As System.Windows.Forms.TextBox
    Friend WithEvents BJPaysLabelMTab As System.Windows.Forms.Label
    Friend WithEvents Stand18ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents Stand17ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents DealerStandsLabelMTab As System.Windows.Forms.Label
    Friend WithEvents DCheckLabelMTab As System.Windows.Forms.Label
    Friend WithEvents CheckTenCheckMTab As System.Windows.Forms.CheckBox
    Friend WithEvents CheckAceCheckMTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandTotalLabelSRTab As System.Windows.Forms.Label
    Friend WithEvents T17LabelSRTab As System.Windows.Forms.Label
    Friend WithEvents BJLabelSRTab As System.Windows.Forms.Label
    Friend WithEvents T21LabelSRTab As System.Windows.Forms.Label
    Friend WithEvents T20LabelSRTab As System.Windows.Forms.Label
    Friend WithEvents T19LabelSRTab As System.Windows.Forms.Label
    Friend WithEvents T18LabelSRTab As System.Windows.Forms.Label
    Friend WithEvents OtherOptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents SplitRulesGroupSpTab As System.Windows.Forms.GroupBox
    Friend WithEvents SMACheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents SPL3ButtonSpTab As System.Windows.Forms.RadioButton
    Friend WithEvents SPL1ButtonSpTab As System.Windows.Forms.RadioButton
    Friend WithEvents SPL2ButtonSpTab As System.Windows.Forms.RadioButton
    Friend WithEvents NSplitsLabelSpTab As System.Windows.Forms.Label
    Friend WithEvents SPL0ButtonSpTab As System.Windows.Forms.RadioButton
    Friend WithEvents HSACheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents BJSplitTensCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents SASCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents DASCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents CDSplitGroupSpTab As System.Windows.Forms.GroupBox
    Friend WithEvents CDZButtonSpTab As System.Windows.Forms.RadioButton
    Friend WithEvents CDSplitLabelSpTab As System.Windows.Forms.Label
    Friend WithEvents CDPButtonSpTab As System.Windows.Forms.RadioButton
    Friend WithEvents CDPNButtonSpTab As System.Windows.Forms.RadioButton
    Friend WithEvents TDPlusCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents TCPlusCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents SMultiLabelDTab As System.Windows.Forms.Label
    Friend WithEvents STCLabelDTab As System.Windows.Forms.Label
    Friend WithEvents HMultiLabelDTab As System.Windows.Forms.Label
    Friend WithEvents HTCLabelDTab As System.Windows.Forms.Label
    Friend WithEvents PTotLabelDTab As System.Windows.Forms.Label
    Friend WithEvents SurrenderRulesGroupSTab As System.Windows.Forms.GroupBox
    Friend WithEvents UseDefaultCheckMTab As System.Windows.Forms.CheckBox
    Friend WithEvents DiamondsLabelShTab As System.Windows.Forms.Label
    Friend WithEvents ToggleAllComboSTab As System.Windows.Forms.ComboBox
    Friend WithEvents NetSuitLabelShTab As System.Windows.Forms.Label
    Friend WithEvents SplitPairsAllowedGroupSpTab As System.Windows.Forms.GroupBox
    Friend WithEvents ToggleAllCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents ToOneLabelSTab As System.Windows.Forms.Label
    Friend WithEvents SurrenderPaysLabelSTab As System.Windows.Forms.Label
    Friend WithEvents CAceSTab As System.Windows.Forms.Label
    Friend WithEvents C3STab As System.Windows.Forms.Label
    Friend WithEvents C4STab As System.Windows.Forms.Label
    Friend WithEvents C5STab As System.Windows.Forms.Label
    Friend WithEvents C6STab As System.Windows.Forms.Label
    Friend WithEvents C7STab As System.Windows.Forms.Label
    Friend WithEvents C8STab As System.Windows.Forms.Label
    Friend WithEvents C9STab As System.Windows.Forms.Label
    Friend WithEvents C10STab As System.Windows.Forms.Label
    Friend WithEvents C2STab As System.Windows.Forms.Label
    Friend WithEvents UpcardLabelSTab As System.Windows.Forms.Label
    Friend WithEvents ToggleAllSTab As System.Windows.Forms.Label
    Friend WithEvents PairLabelSpTab As System.Windows.Forms.Label
    Friend WithEvents HardCDTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents SoftPairsCDTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents OptionsTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents ForcedTab As System.Windows.Forms.TabPage
    Friend WithEvents OtherTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents UncheckAllForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents RestoreDefaultForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents MoveForcedRulesDownButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents MoveForcedRulesUpButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents UpdateForcedRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents DeleteForcedRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents ForcedRuleLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ForcedRuleDetailsGroupFSTab As System.Windows.Forms.GroupBox
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
    Friend WithEvents HandSoftCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandSoftLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HandTotalLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents AddForcedRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents ForcedRulesCheckListBoxFSTab As System.Windows.Forms.CheckedListBox
    Friend WithEvents PairForcedCDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents TotalForcedCDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents PairCDGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents SoftCDGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents ForcedStratTabControlFSTab As System.Windows.Forms.TabControl
    Friend WithEvents HardCDHand2LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HardCDHand1LabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ForcedTableComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents ColorTableGroupOTab As System.Windows.Forms.GroupBox
    Friend WithEvents HardSoftTDTabFSTab As System.Windows.Forms.TabPage
    Friend WithEvents SoftTDGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents SoftTotalTDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HardTDGroupFSTab As System.Windows.Forms.GroupBox
    Friend WithEvents HardTotalTDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ForcedShoeGroupShTab As System.Windows.Forms.GroupBox
    Friend WithEvents RestoreSuitsButtonShTab As System.Windows.Forms.Button
    Friend WithEvents SpadesLabelShTab As System.Windows.Forms.Label
    Friend WithEvents ClubsLabelShTab As System.Windows.Forms.Label
    Friend WithEvents HeartsLabelShTab As System.Windows.Forms.Label
    Friend WithEvents NetForcedCardsLabelShTab As System.Windows.Forms.Label
    Friend WithEvents NetForcedCardsBoxShTab As System.Windows.Forms.TextBox
    Friend WithEvents ReferenceShoeGroupShTab As System.Windows.Forms.GroupBox
    Friend WithEvents NetRefCardsLabelShTab As System.Windows.Forms.Label
    Friend WithEvents NetRefCardsBoxShTab As System.Windows.Forms.TextBox
    Friend WithEvents RefDecks3LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecks10LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecks4LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecks5LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecks2LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecks6LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecks7LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecks8LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecks9LabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecksBoxShTab As System.Windows.Forms.TextBox
    Friend WithEvents RefDecksALabelShTab As System.Windows.Forms.Label
    Friend WithEvents RefDecksLabelShTab As System.Windows.Forms.Label
    Friend WithEvents LoadForcedTablesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents SaveForcedTablesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents ClearForcedTablesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents LoadForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents SavedForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents RenameRuleButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents RestoreDefaultColorTableButtonOTab As System.Windows.Forms.Button
    Friend WithEvents SaveColorTableFileButtonOTab As System.Windows.Forms.Button
    Friend WithEvents LoadColorTableFileButtonOTab As System.Windows.Forms.Button
    Friend WithEvents SaveDoubleTableFileButtonDTab As System.Windows.Forms.Button
    Friend WithEvents LoadDoubleTableFileButtonDTab As System.Windows.Forms.Button
    Friend WithEvents LoadForcedShoeFileButtonShTab As System.Windows.Forms.Button
    Friend WithEvents SaveForcedShoeFileButtonShTab As System.Windows.Forms.Button
    Friend WithEvents ForcedWarningLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ES10ButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents ES10ButtonSTab As System.Windows.Forms.RadioButton
    Friend WithEvents PDTiesGroupSRTab As System.Windows.Forms.GroupBox
    Friend WithEvents PFPayoffLabelSRTab As System.Windows.Forms.Label
    Friend WithEvents PDTiesLabelSRTab As System.Windows.Forms.Label
    Friend WithEvents BJSPlitAcesCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents PDTiesToggleAllBoxSRTab As System.Windows.Forms.ComboBox
    Friend WithEvents ToggleAllLabelSRTab As System.Windows.Forms.Label
    Friend WithEvents DeleteAllForcedRulesButtonFSTab As System.Windows.Forms.Button
    Friend WithEvents ForcedRuleStratBoxFSTab As System.Windows.Forms.TextBox
    Friend WithEvents StrategyComboBoxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents UCAllowedGroupOTab As System.Windows.Forms.GroupBox
    Friend WithEvents UpcardLabelOTab As System.Windows.Forms.Label
    Friend WithEvents ToggleAllCheckOTab As System.Windows.Forms.CheckBox
    Friend WithEvents MacauSurrender2to10CheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents MacauSurrenderAceCheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents ForcednCDLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents ForcednCDBoxFSTab As System.Windows.Forms.TextBox
    Friend WithEvents ForcedTablePostCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents ForcedTablePreCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents DSoftAllHardCheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents DSACheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents DSoft19Hard9CheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents SurrDBJPaysBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents To1DBJPaysLabelSTab As System.Windows.Forms.Label
    Friend WithEvents SurrDBJPaysLabelSTab As System.Windows.Forms.Label
    Friend WithEvents DSACheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents SSACheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents SSACheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents SurrPaysBoxMTab As System.Windows.Forms.TextBox
    Friend WithEvents SurrPaysBoxSTab As System.Windows.Forms.TextBox
    Friend WithEvents SurrDBJCheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents PairsRuleApplyLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents HardTDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents SoftTDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents HardCDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents PairCDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents SoftCDLabelComboboxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents BonusTabControlBTab As System.Windows.Forms.TabControl
    Friend WithEvents BJBonusesTabBTab As System.Windows.Forms.TabPage
    Friend WithEvents BonusRulesTabBTab As System.Windows.Forms.TabPage
    Friend WithEvents DeleteAllBonusRulesButtonBTab As System.Windows.Forms.Button
    Friend WithEvents RenameRuleButtonBTab As System.Windows.Forms.Button
    Friend WithEvents LoadBonusRulesButtonBTab As System.Windows.Forms.Button
    Friend WithEvents UncheckBonusRulesButtonBTab As System.Windows.Forms.Button
    Friend WithEvents SaveBonusRulesButtonBTab As System.Windows.Forms.Button
    Friend WithEvents RestoreDefaultRulesButtonBTab As System.Windows.Forms.Button
    Friend WithEvents MoveRuleDownButtonBTab As System.Windows.Forms.Button
    Friend WithEvents MoveRuleUpButtonBTab As System.Windows.Forms.Button
    Friend WithEvents UpdateRuleButtonBTab As System.Windows.Forms.Button
    Friend WithEvents DeleteRuleButtonBTab As System.Windows.Forms.Button
    Friend WithEvents BonusRulesApplyLabelBTab As System.Windows.Forms.Label
    Friend WithEvents BonusRuleDetailsGroupBTab As System.Windows.Forms.GroupBox
    Friend WithEvents HandContinuesCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents SpecificTenCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents SpecificTenFractionLabelBTab As System.Windows.Forms.Label
    Friend WithEvents SpecificTenFractionBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents DealerBJLabelBTab As System.Windows.Forms.Label
    Friend WithEvents ExactMatchCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents GeneralLabelBTab As System.Windows.Forms.Label
    Friend WithEvents PayoffGeneralBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents SuitLabelBTab As System.Windows.Forms.Label
    Friend WithEvents SoftCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents HeartsButtonBTab As System.Windows.Forms.RadioButton
    Friend WithEvents DiamondsButtonBTab As System.Windows.Forms.RadioButton
    Friend WithEvents ClubsButtonBTab As System.Windows.Forms.RadioButton
    Friend WithEvents SpadesButtonBTab As System.Windows.Forms.RadioButton
    Friend WithEvents SuitedLabelBTab As System.Windows.Forms.Label
    Friend WithEvents OrLessCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents ClearRuleButtonBTab As System.Windows.Forms.Button
    Friend WithEvents PostSplitCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents PreSplitCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents RuleNameBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents RuleNameBTab As System.Windows.Forms.Label
    Friend WithEvents HandCompCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandTotalSizeCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents SpecificSuitCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents SuitedCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents MustWinCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents PayoffLabelBTab As System.Windows.Forms.Label
    Friend WithEvents PayoffSuitedBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents TotalBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents NCardsBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents OrMoreCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandNCardsLabelBTab As System.Windows.Forms.Label
    Friend WithEvents TotalLabelBTab As System.Windows.Forms.Label
    Friend WithEvents SoftLabelBTab As System.Windows.Forms.Label
    Friend WithEvents NCardsLabelBTab As System.Windows.Forms.Label
    Friend WithEvents C3LabelBTab As System.Windows.Forms.Label
    Friend WithEvents C10LabelBTab As System.Windows.Forms.Label
    Friend WithEvents C4LabelBTab As System.Windows.Forms.Label
    Friend WithEvents C5LabelBTab As System.Windows.Forms.Label
    Friend WithEvents C2LabelBTab As System.Windows.Forms.Label
    Friend WithEvents C6LabelBTab As System.Windows.Forms.Label
    Friend WithEvents C7LabelBTab As System.Windows.Forms.Label
    Friend WithEvents C8LabelBTab As System.Windows.Forms.Label
    Friend WithEvents C9LabelBTab As System.Windows.Forms.Label
    Friend WithEvents CAceLabelBTab As System.Windows.Forms.Label
    Friend WithEvents HardOnlyCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents SoftOnlyCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandSoftLabelBTab As System.Windows.Forms.Label
    Friend WithEvents EitherCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents HandTotalLabelBTab As System.Windows.Forms.Label
    Friend WithEvents DealerUCPayoffLabelBTab As System.Windows.Forms.Label
    Friend WithEvents DealerUCLabelBTab As System.Windows.Forms.Label
    Friend WithEvents AddRuleButtonBTab As System.Windows.Forms.Button
    Friend WithEvents BonusRulesCheckListBoxBTab As System.Windows.Forms.CheckedListBox
    Friend WithEvents AceUpCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents TenUpCheckBTab As System.Windows.Forms.CheckBox
    Friend WithEvents LoadResultsButtonMTab As System.Windows.Forms.Button
    Friend WithEvents RowClickLabelFSTab As System.Windows.Forms.Label
    Friend WithEvents UpcardComboBoxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents DealerUpcardComboBoxBTab As System.Windows.Forms.ComboBox
    Friend WithEvents DDRCheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents RDACheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents RDDepthBoxDTab As System.Windows.Forms.TextBox
    Friend WithEvents RDDepthLabelDTab As System.Windows.Forms.Label
    Friend WithEvents PostDoubleCheckFSTab As System.Windows.Forms.CheckBox
    Friend WithEvents PayoffSpecificSuitBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents PayoffSpecificSuitBJBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents PayoffSuitedBJBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents PayoffGeneralBJBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents PayoffUCGeneralBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents PayoffUCSuitedBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents PayoffUCSpecificSuitBoxBTab As System.Windows.Forms.TextBox
    Friend WithEvents HandTotalComboBoxBTab As System.Windows.Forms.ComboBox
    Friend WithEvents HandNCardsComboBoxBTab As System.Windows.Forms.ComboBox
    Friend WithEvents HandTotalComboBoxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents HandNCardsComboBoxFSTab As System.Windows.Forms.ComboBox
    Friend WithEvents DDRPSCheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents DDRPSCheckSTab As System.Windows.Forms.CheckBox
    Friend WithEvents DDRPSCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents RDAPSCheckSpTab As System.Windows.Forms.CheckBox
    Friend WithEvents Spec10GroupBJTab As System.Windows.Forms.GroupBox
    Friend WithEvents Payoff2LabelBJTab As System.Windows.Forms.Label
    Friend WithEvents Spec10LabelBJTab As System.Windows.Forms.Label
    Friend WithEvents Spec10BoxlBJTab As System.Windows.Forms.TextBox
    Friend WithEvents Spec10FractionLabelBJTab As System.Windows.Forms.Label
    Friend WithEvents Spec10FractionBoxBJTab As System.Windows.Forms.TextBox
    Friend WithEvents Spec10SpecSuitBoxBJTab As System.Windows.Forms.TextBox
    Friend WithEvents Spec10SuitLabelBJTab As System.Windows.Forms.Label
    Friend WithEvents Spec10HeartsButtonBJTab As System.Windows.Forms.RadioButton
    Friend WithEvents Spec10DiamondsButtonBJTab As System.Windows.Forms.RadioButton
    Friend WithEvents Spec10ClubsButtonBJTab As System.Windows.Forms.RadioButton
    Friend WithEvents Spec10SpadesButtonBJTab As System.Windows.Forms.RadioButton
    Friend WithEvents Spec10SuitedLabelBJTab As System.Windows.Forms.Label
    Friend WithEvents Spec10SuitedBoxBJTab As System.Windows.Forms.TextBox
    Friend WithEvents GeneralBJGroupBJTab As System.Windows.Forms.GroupBox
    Friend WithEvents SuitLabelBJTab As System.Windows.Forms.Label
    Friend WithEvents SpecificSuitBJBoxBJTab As System.Windows.Forms.TextBox
    Friend WithEvents HeartsButtonBJTab As System.Windows.Forms.RadioButton
    Friend WithEvents DiamondsButtonBJTab As System.Windows.Forms.RadioButton
    Friend WithEvents ClubsButtonBJTab As System.Windows.Forms.RadioButton
    Friend WithEvents SpadesButtonBJTab As System.Windows.Forms.RadioButton
    Friend WithEvents SuitedLabelBJTab As System.Windows.Forms.Label
    Friend WithEvents PayoffLabelBJTab As System.Windows.Forms.Label
    Friend WithEvents SuitedBJBoxBJTab As System.Windows.Forms.TextBox
    Friend WithEvents BJPaysBoxBJTab As System.Windows.Forms.TextBox
    Friend WithEvents BJPaysLabelBJTab As System.Windows.Forms.Label
    Friend WithEvents BJSPlitTensCheckBJTab As System.Windows.Forms.CheckBox
    Friend WithEvents BJSPlitAcesCheckBJTab As System.Windows.Forms.CheckBox
    Friend WithEvents SuitedBJMustWinCheckBJTab As System.Windows.Forms.CheckBox
    Friend WithEvents Spec10BJMustWinCheckBJTab As System.Windows.Forms.CheckBox
    Friend WithEvents MaxDealerCardsBoxSRTab As System.Windows.Forms.TextBox
    Friend WithEvents MaxDealerCardsLabelSRTab As System.Windows.Forms.Label
    Friend WithEvents MaxPlayerCardsBoxSRTab As System.Windows.Forms.TextBox
    Friend WithEvents MaxPlayerCardsLabelSRTab As System.Windows.Forms.Label
    Friend WithEvents NoteLabelBJTab As System.Windows.Forms.Label
    Friend WithEvents Note2LabelBJTab As System.Windows.Forms.Label
    Friend WithEvents RDADDRLabelDTab As System.Windows.Forms.Label
    Friend WithEvents RDADDRLabelSTab As System.Windows.Forms.Label
    Friend WithEvents RDADDRLabelSpTab As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
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
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DDRCheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents RDAPSCheckDTab As System.Windows.Forms.CheckBox
    Friend WithEvents AOBBOButtonMTab As System.Windows.Forms.RadioButton
    Friend WithEvents SpanishDecksCheckBoxMTab As System.Windows.Forms.CheckBox
    Friend WithEvents SpanishDecksRefCheckBoxShTab As System.Windows.Forms.CheckBox
    Friend WithEvents SpanishDecksCheckBoxShTab As System.Windows.Forms.CheckBox
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents FileMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents NewMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents OpenRuleSetMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents SaveRuleSetMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents OptionsMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents SetDefaultsMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents RestoreDefaultsMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents AboutMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents RealtimeMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents CalcNowMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents StartRTMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents StrategiesMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents ComputeTDMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents ComputeTCMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents ComputeForcedMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents UseDPDictionaryMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents SaveDPDictionaryMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents PSExceptionsMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents DDREarlyButtonDTab As System.Windows.Forms.RadioButton
    Friend WithEvents DDRLateButtonDTab As System.Windows.Forms.RadioButton
    Friend WithEvents DDREarlyButtonSTab As System.Windows.Forms.RadioButton
    Friend WithEvents DDRLateButtonSTab As System.Windows.Forms.RadioButton
    Friend WithEvents P21AutowinCheckBoxDTab As System.Windows.Forms.CheckBox
    Friend WithEvents P21AutowinCheckBoxSRTab As System.Windows.Forms.CheckBox
    Friend WithEvents HelpMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents HelpFileMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents RTSPL1EstMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents RTSmallMenuItem As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(BJCAMainForm))
        Me.MainTabs = New System.Windows.Forms.TabControl
        Me.MainRulesTab = New System.Windows.Forms.TabPage
        Me.LoadResultsButtonMTab = New System.Windows.Forms.Button
        Me.HoleCardGroupMTab = New System.Windows.Forms.GroupBox
        Me.AOBBOButtonMTab = New System.Windows.Forms.RadioButton
        Me.DCheckLabelMTab = New System.Windows.Forms.Label
        Me.UseDefaultCheckMTab = New System.Windows.Forms.CheckBox
        Me.CheckTenCheckMTab = New System.Windows.Forms.CheckBox
        Me.CheckAceCheckMTab = New System.Windows.Forms.CheckBox
        Me.OBBOButtonMTab = New System.Windows.Forms.RadioButton
        Me.ENHCButtonMTab = New System.Windows.Forms.RadioButton
        Me.BBOButtonMTab = New System.Windows.Forms.RadioButton
        Me.OBOButtonMTab = New System.Windows.Forms.RadioButton
        Me.ShoeOptionGroupMTab = New System.Windows.Forms.GroupBox
        Me.SpanishDecksCheckBoxMTab = New System.Windows.Forms.CheckBox
        Me.NDecksLabelMTab = New System.Windows.Forms.Label
        Me.NDecksBoxMTab = New System.Windows.Forms.TextBox
        Me.ForcedShoeCheckMTab = New System.Windows.Forms.CheckBox
        Me.InfiniteDecksButtonMTab = New System.Windows.Forms.RadioButton
        Me.FiniteDecksButtonMTab = New System.Windows.Forms.RadioButton
        Me.SplitGroupMTab = New System.Windows.Forms.GroupBox
        Me.SMACheckMTab = New System.Windows.Forms.CheckBox
        Me.SPL3ButtonMTab = New System.Windows.Forms.RadioButton
        Me.SPL1ButtonMTab = New System.Windows.Forms.RadioButton
        Me.SPL2ButtonMTab = New System.Windows.Forms.RadioButton
        Me.NSPlitsLabelMTab = New System.Windows.Forms.Label
        Me.SPL0ButtonMTab = New System.Windows.Forms.RadioButton
        Me.HSACheckMTab = New System.Windows.Forms.CheckBox
        Me.DoubleGroupMTab = New System.Windows.Forms.GroupBox
        Me.DTableButtonMTab = New System.Windows.Forms.RadioButton
        Me.D1011ButtonMTab = New System.Windows.Forms.RadioButton
        Me.D91011ButtonMTab = New System.Windows.Forms.RadioButton
        Me.DOAButtonMTab = New System.Windows.Forms.RadioButton
        Me.DANCheckMTab = New System.Windows.Forms.CheckBox
        Me.DASCheckMTab = New System.Windows.Forms.CheckBox
        Me.SurrenderGroupMTab = New System.Windows.Forms.GroupBox
        Me.ES10ButtonMTab = New System.Windows.Forms.RadioButton
        Me.STableButtonMTab = New System.Windows.Forms.RadioButton
        Me.SurrenderTo1LabelMTab = New System.Windows.Forms.Label
        Me.SurrPaysBoxMTab = New System.Windows.Forms.TextBox
        Me.SurrenderPaysLabelMTab = New System.Windows.Forms.Label
        Me.ESButtonMTab = New System.Windows.Forms.RadioButton
        Me.LSButtonMTab = New System.Windows.Forms.RadioButton
        Me.NSButtonMTab = New System.Windows.Forms.RadioButton
        Me.DealerRulesGroupMTab = New System.Windows.Forms.GroupBox
        Me.BJTo1LabelMTab = New System.Windows.Forms.Label
        Me.BJPaysBoxMTab = New System.Windows.Forms.TextBox
        Me.BJPaysLabelMTab = New System.Windows.Forms.Label
        Me.Stand18ButtonMTab = New System.Windows.Forms.RadioButton
        Me.Stand17ButtonMTab = New System.Windows.Forms.RadioButton
        Me.DealerStandsLabelMTab = New System.Windows.Forms.Label
        Me.DoubleRulesTab = New System.Windows.Forms.TabPage
        Me.DDREarlyButtonDTab = New System.Windows.Forms.RadioButton
        Me.DDRLateButtonDTab = New System.Windows.Forms.RadioButton
        Me.P21AutowinCheckBoxDTab = New System.Windows.Forms.CheckBox
        Me.RDADDRLabelDTab = New System.Windows.Forms.Label
        Me.RDAPSCheckDTab = New System.Windows.Forms.CheckBox
        Me.DDRPSCheckDTab = New System.Windows.Forms.CheckBox
        Me.RDACheckDTab = New System.Windows.Forms.CheckBox
        Me.RDDepthBoxDTab = New System.Windows.Forms.TextBox
        Me.RDDepthLabelDTab = New System.Windows.Forms.Label
        Me.DDRCheckDTab = New System.Windows.Forms.CheckBox
        Me.DSoft19Hard9CheckDTab = New System.Windows.Forms.CheckBox
        Me.DoubleGroupDTab = New System.Windows.Forms.GroupBox
        Me.DTableButtonDTab = New System.Windows.Forms.RadioButton
        Me.D1011ButtonDTab = New System.Windows.Forms.RadioButton
        Me.D91011ButtonDTab = New System.Windows.Forms.RadioButton
        Me.DOAButtonDTab = New System.Windows.Forms.RadioButton
        Me.DoubleTableGroup = New System.Windows.Forms.GroupBox
        Me.SMultiLabelDTab = New System.Windows.Forms.Label
        Me.STCLabelDTab = New System.Windows.Forms.Label
        Me.HMultiLabelDTab = New System.Windows.Forms.Label
        Me.HTCLabelDTab = New System.Windows.Forms.Label
        Me.SoftLabelDTab = New System.Windows.Forms.Label
        Me.DToggleAllCheckDTab = New System.Windows.Forms.CheckBox
        Me.HardLabelDTab = New System.Windows.Forms.Label
        Me.PTotLabelDTab = New System.Windows.Forms.Label
        Me.SaveDoubleTableFileButtonDTab = New System.Windows.Forms.Button
        Me.LoadDoubleTableFileButtonDTab = New System.Windows.Forms.Button
        Me.DANCheckDTab = New System.Windows.Forms.CheckBox
        Me.DASCheckDTab = New System.Windows.Forms.CheckBox
        Me.DSACheckDTab = New System.Windows.Forms.CheckBox
        Me.DSoftAllHardCheckDTab = New System.Windows.Forms.CheckBox
        Me.SurrenderRulesTab = New System.Windows.Forms.TabPage
        Me.DDREarlyButtonSTab = New System.Windows.Forms.RadioButton
        Me.DDRLateButtonSTab = New System.Windows.Forms.RadioButton
        Me.RDADDRLabelSTab = New System.Windows.Forms.Label
        Me.DDRPSCheckSTab = New System.Windows.Forms.CheckBox
        Me.DDRCheckSTab = New System.Windows.Forms.CheckBox
        Me.SSACheckSTab = New System.Windows.Forms.CheckBox
        Me.SurrDBJCheckSTab = New System.Windows.Forms.CheckBox
        Me.To1DBJPaysLabelSTab = New System.Windows.Forms.Label
        Me.SurrDBJPaysBoxSTab = New System.Windows.Forms.TextBox
        Me.SurrDBJPaysLabelSTab = New System.Windows.Forms.Label
        Me.MacauSurrenderAceCheckSTab = New System.Windows.Forms.CheckBox
        Me.MacauSurrender2to10CheckSTab = New System.Windows.Forms.CheckBox
        Me.SASCheckSTab = New System.Windows.Forms.CheckBox
        Me.SANCheckSTab = New System.Windows.Forms.CheckBox
        Me.SurrenderTableGroup = New System.Windows.Forms.GroupBox
        Me.CAceSTab = New System.Windows.Forms.Label
        Me.C3STab = New System.Windows.Forms.Label
        Me.C4STab = New System.Windows.Forms.Label
        Me.C5STab = New System.Windows.Forms.Label
        Me.C6STab = New System.Windows.Forms.Label
        Me.C7STab = New System.Windows.Forms.Label
        Me.C8STab = New System.Windows.Forms.Label
        Me.C9STab = New System.Windows.Forms.Label
        Me.C10STab = New System.Windows.Forms.Label
        Me.C2STab = New System.Windows.Forms.Label
        Me.UpcardLabelSTab = New System.Windows.Forms.Label
        Me.ToggleAllSTab = New System.Windows.Forms.Label
        Me.ToggleAllComboSTab = New System.Windows.Forms.ComboBox
        Me.SurrenderRulesGroupSTab = New System.Windows.Forms.GroupBox
        Me.ES10ButtonSTab = New System.Windows.Forms.RadioButton
        Me.STableButtonSTab = New System.Windows.Forms.RadioButton
        Me.ESButtonSTab = New System.Windows.Forms.RadioButton
        Me.LSButtonSTab = New System.Windows.Forms.RadioButton
        Me.NSButtonSTab = New System.Windows.Forms.RadioButton
        Me.ToOneLabelSTab = New System.Windows.Forms.Label
        Me.SurrPaysBoxSTab = New System.Windows.Forms.TextBox
        Me.SurrenderPaysLabelSTab = New System.Windows.Forms.Label
        Me.SplitOptionsTab = New System.Windows.Forms.TabPage
        Me.RDADDRLabelSpTab = New System.Windows.Forms.Label
        Me.RDAPSCheckSpTab = New System.Windows.Forms.CheckBox
        Me.DDRPSCheckSpTab = New System.Windows.Forms.CheckBox
        Me.SSACheckSpTab = New System.Windows.Forms.CheckBox
        Me.SplitPairsAllowedGroupSpTab = New System.Windows.Forms.GroupBox
        Me.ToggleAllCheckSpTab = New System.Windows.Forms.CheckBox
        Me.PairLabelSpTab = New System.Windows.Forms.Label
        Me.TCPlusCheckSpTab = New System.Windows.Forms.CheckBox
        Me.TDPlusCheckSpTab = New System.Windows.Forms.CheckBox
        Me.CDSplitGroupSpTab = New System.Windows.Forms.GroupBox
        Me.CDPNButtonSpTab = New System.Windows.Forms.RadioButton
        Me.CDPButtonSpTab = New System.Windows.Forms.RadioButton
        Me.CDSplitLabelSpTab = New System.Windows.Forms.Label
        Me.CDZButtonSpTab = New System.Windows.Forms.RadioButton
        Me.BJSplitTensCheckSpTab = New System.Windows.Forms.CheckBox
        Me.BJSPlitAcesCheckSpTab = New System.Windows.Forms.CheckBox
        Me.SASCheckSpTab = New System.Windows.Forms.CheckBox
        Me.DSACheckSpTab = New System.Windows.Forms.CheckBox
        Me.DASCheckSpTab = New System.Windows.Forms.CheckBox
        Me.SplitRulesGroupSpTab = New System.Windows.Forms.GroupBox
        Me.SPL3ButtonSpTab = New System.Windows.Forms.RadioButton
        Me.SPL1ButtonSpTab = New System.Windows.Forms.RadioButton
        Me.SPL2ButtonSpTab = New System.Windows.Forms.RadioButton
        Me.NSplitsLabelSpTab = New System.Windows.Forms.Label
        Me.SPL0ButtonSpTab = New System.Windows.Forms.RadioButton
        Me.HSACheckSpTab = New System.Windows.Forms.CheckBox
        Me.SMACheckSpTab = New System.Windows.Forms.CheckBox
        Me.SpecialRulesTab = New System.Windows.Forms.TabPage
        Me.P21AutowinCheckBoxSRTab = New System.Windows.Forms.CheckBox
        Me.MaxPlayerCardsBoxSRTab = New System.Windows.Forms.TextBox
        Me.MaxPlayerCardsLabelSRTab = New System.Windows.Forms.Label
        Me.MaxDealerCardsBoxSRTab = New System.Windows.Forms.TextBox
        Me.MaxDealerCardsLabelSRTab = New System.Windows.Forms.Label
        Me.PDTiesGroupSRTab = New System.Windows.Forms.GroupBox
        Me.ToggleAllLabelSRTab = New System.Windows.Forms.Label
        Me.PDTiesToggleAllBoxSRTab = New System.Windows.Forms.ComboBox
        Me.PDTiesLabelSRTab = New System.Windows.Forms.Label
        Me.PFPayoffLabelSRTab = New System.Windows.Forms.Label
        Me.BJLabelSRTab = New System.Windows.Forms.Label
        Me.T21LabelSRTab = New System.Windows.Forms.Label
        Me.T20LabelSRTab = New System.Windows.Forms.Label
        Me.T19LabelSRTab = New System.Windows.Forms.Label
        Me.T18LabelSRTab = New System.Windows.Forms.Label
        Me.T17LabelSRTab = New System.Windows.Forms.Label
        Me.HandTotalLabelSRTab = New System.Windows.Forms.Label
        Me.ShoeOptionsTab = New System.Windows.Forms.TabPage
        Me.LoadForcedShoeFileButtonShTab = New System.Windows.Forms.Button
        Me.SaveForcedShoeFileButtonShTab = New System.Windows.Forms.Button
        Me.ForcedShoeGroupShTab = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.RestoreSuitsButtonShTab = New System.Windows.Forms.Button
        Me.NetSuitLabelShTab = New System.Windows.Forms.Label
        Me.SpadesLabelShTab = New System.Windows.Forms.Label
        Me.ClubsLabelShTab = New System.Windows.Forms.Label
        Me.HeartsLabelShTab = New System.Windows.Forms.Label
        Me.DiamondsLabelShTab = New System.Windows.Forms.Label
        Me.NetForcedCardsLabelShTab = New System.Windows.Forms.Label
        Me.NetForcedCardsBoxShTab = New System.Windows.Forms.TextBox
        Me.CopyRefShoeButtonShTab = New System.Windows.Forms.Button
        Me.ReferenceShoeGroupShTab = New System.Windows.Forms.GroupBox
        Me.SpanishDecksRefCheckBoxShTab = New System.Windows.Forms.CheckBox
        Me.NetRefCardsLabelShTab = New System.Windows.Forms.Label
        Me.NetRefCardsBoxShTab = New System.Windows.Forms.TextBox
        Me.RefDecks3LabelShTab = New System.Windows.Forms.Label
        Me.RefDecks10LabelShTab = New System.Windows.Forms.Label
        Me.RefDecks4LabelShTab = New System.Windows.Forms.Label
        Me.RefDecks5LabelShTab = New System.Windows.Forms.Label
        Me.RefDecks2LabelShTab = New System.Windows.Forms.Label
        Me.RefDecks6LabelShTab = New System.Windows.Forms.Label
        Me.RefDecks7LabelShTab = New System.Windows.Forms.Label
        Me.RefDecks8LabelShTab = New System.Windows.Forms.Label
        Me.RefDecks9LabelShTab = New System.Windows.Forms.Label
        Me.RefDecksBoxShTab = New System.Windows.Forms.TextBox
        Me.RefDecksALabelShTab = New System.Windows.Forms.Label
        Me.RefDecksLabelShTab = New System.Windows.Forms.Label
        Me.ShoeOptionGroupShTab = New System.Windows.Forms.GroupBox
        Me.SpanishDecksCheckBoxShTab = New System.Windows.Forms.CheckBox
        Me.NDecksLabelShTab = New System.Windows.Forms.Label
        Me.NDecksBoxShTab = New System.Windows.Forms.TextBox
        Me.ForcedShoeCheckShTab = New System.Windows.Forms.CheckBox
        Me.InfiniteDecksButtonShTab = New System.Windows.Forms.RadioButton
        Me.FiniteDecksButtonShTab = New System.Windows.Forms.RadioButton
        Me.BonusRulesTab = New System.Windows.Forms.TabPage
        Me.BonusTabControlBTab = New System.Windows.Forms.TabControl
        Me.BJBonusesTabBTab = New System.Windows.Forms.TabPage
        Me.Note2LabelBJTab = New System.Windows.Forms.Label
        Me.NoteLabelBJTab = New System.Windows.Forms.Label
        Me.Spec10GroupBJTab = New System.Windows.Forms.GroupBox
        Me.Spec10BJMustWinCheckBJTab = New System.Windows.Forms.CheckBox
        Me.Payoff2LabelBJTab = New System.Windows.Forms.Label
        Me.Spec10LabelBJTab = New System.Windows.Forms.Label
        Me.Spec10BoxlBJTab = New System.Windows.Forms.TextBox
        Me.Spec10FractionLabelBJTab = New System.Windows.Forms.Label
        Me.Spec10FractionBoxBJTab = New System.Windows.Forms.TextBox
        Me.Spec10SpecSuitBoxBJTab = New System.Windows.Forms.TextBox
        Me.Spec10SuitLabelBJTab = New System.Windows.Forms.Label
        Me.Spec10HeartsButtonBJTab = New System.Windows.Forms.RadioButton
        Me.Spec10DiamondsButtonBJTab = New System.Windows.Forms.RadioButton
        Me.Spec10ClubsButtonBJTab = New System.Windows.Forms.RadioButton
        Me.Spec10SpadesButtonBJTab = New System.Windows.Forms.RadioButton
        Me.Spec10SuitedLabelBJTab = New System.Windows.Forms.Label
        Me.Spec10SuitedBoxBJTab = New System.Windows.Forms.TextBox
        Me.GeneralBJGroupBJTab = New System.Windows.Forms.GroupBox
        Me.SuitLabelBJTab = New System.Windows.Forms.Label
        Me.SpecificSuitBJBoxBJTab = New System.Windows.Forms.TextBox
        Me.HeartsButtonBJTab = New System.Windows.Forms.RadioButton
        Me.DiamondsButtonBJTab = New System.Windows.Forms.RadioButton
        Me.ClubsButtonBJTab = New System.Windows.Forms.RadioButton
        Me.SpadesButtonBJTab = New System.Windows.Forms.RadioButton
        Me.SuitedLabelBJTab = New System.Windows.Forms.Label
        Me.PayoffLabelBJTab = New System.Windows.Forms.Label
        Me.SuitedBJBoxBJTab = New System.Windows.Forms.TextBox
        Me.BJPaysBoxBJTab = New System.Windows.Forms.TextBox
        Me.BJPaysLabelBJTab = New System.Windows.Forms.Label
        Me.SuitedBJMustWinCheckBJTab = New System.Windows.Forms.CheckBox
        Me.BJSPlitTensCheckBJTab = New System.Windows.Forms.CheckBox
        Me.BJSPlitAcesCheckBJTab = New System.Windows.Forms.CheckBox
        Me.BonusRulesTabBTab = New System.Windows.Forms.TabPage
        Me.DeleteAllBonusRulesButtonBTab = New System.Windows.Forms.Button
        Me.RenameRuleButtonBTab = New System.Windows.Forms.Button
        Me.LoadBonusRulesButtonBTab = New System.Windows.Forms.Button
        Me.UncheckBonusRulesButtonBTab = New System.Windows.Forms.Button
        Me.SaveBonusRulesButtonBTab = New System.Windows.Forms.Button
        Me.RestoreDefaultRulesButtonBTab = New System.Windows.Forms.Button
        Me.MoveRuleDownButtonBTab = New System.Windows.Forms.Button
        Me.MoveRuleUpButtonBTab = New System.Windows.Forms.Button
        Me.UpdateRuleButtonBTab = New System.Windows.Forms.Button
        Me.DeleteRuleButtonBTab = New System.Windows.Forms.Button
        Me.BonusRulesApplyLabelBTab = New System.Windows.Forms.Label
        Me.BonusRuleDetailsGroupBTab = New System.Windows.Forms.GroupBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.HandNCardsComboBoxBTab = New System.Windows.Forms.ComboBox
        Me.HandTotalComboBoxBTab = New System.Windows.Forms.ComboBox
        Me.PayoffUCSpecificSuitBoxBTab = New System.Windows.Forms.TextBox
        Me.PayoffUCSuitedBoxBTab = New System.Windows.Forms.TextBox
        Me.PayoffSpecificSuitBJBoxBTab = New System.Windows.Forms.TextBox
        Me.PayoffSuitedBJBoxBTab = New System.Windows.Forms.TextBox
        Me.DealerUpcardComboBoxBTab = New System.Windows.Forms.ComboBox
        Me.HandContinuesCheckBTab = New System.Windows.Forms.CheckBox
        Me.SpecificTenCheckBTab = New System.Windows.Forms.CheckBox
        Me.SpecificTenFractionLabelBTab = New System.Windows.Forms.Label
        Me.SpecificTenFractionBoxBTab = New System.Windows.Forms.TextBox
        Me.DealerBJLabelBTab = New System.Windows.Forms.Label
        Me.PayoffGeneralBJBoxBTab = New System.Windows.Forms.TextBox
        Me.ExactMatchCheckBTab = New System.Windows.Forms.CheckBox
        Me.GeneralLabelBTab = New System.Windows.Forms.Label
        Me.PayoffGeneralBoxBTab = New System.Windows.Forms.TextBox
        Me.SuitLabelBTab = New System.Windows.Forms.Label
        Me.PayoffSpecificSuitBoxBTab = New System.Windows.Forms.TextBox
        Me.SoftCheckBTab = New System.Windows.Forms.CheckBox
        Me.HeartsButtonBTab = New System.Windows.Forms.RadioButton
        Me.DiamondsButtonBTab = New System.Windows.Forms.RadioButton
        Me.ClubsButtonBTab = New System.Windows.Forms.RadioButton
        Me.SpadesButtonBTab = New System.Windows.Forms.RadioButton
        Me.SuitedLabelBTab = New System.Windows.Forms.Label
        Me.OrLessCheckBTab = New System.Windows.Forms.CheckBox
        Me.ClearRuleButtonBTab = New System.Windows.Forms.Button
        Me.PostSplitCheckBTab = New System.Windows.Forms.CheckBox
        Me.PreSplitCheckBTab = New System.Windows.Forms.CheckBox
        Me.RuleNameBoxBTab = New System.Windows.Forms.TextBox
        Me.RuleNameBTab = New System.Windows.Forms.Label
        Me.HandCompCheckBTab = New System.Windows.Forms.CheckBox
        Me.HandTotalSizeCheckBTab = New System.Windows.Forms.CheckBox
        Me.SpecificSuitCheckBTab = New System.Windows.Forms.CheckBox
        Me.SuitedCheckBTab = New System.Windows.Forms.CheckBox
        Me.MustWinCheckBTab = New System.Windows.Forms.CheckBox
        Me.PayoffLabelBTab = New System.Windows.Forms.Label
        Me.PayoffSuitedBoxBTab = New System.Windows.Forms.TextBox
        Me.TotalBoxBTab = New System.Windows.Forms.TextBox
        Me.NCardsBoxBTab = New System.Windows.Forms.TextBox
        Me.OrMoreCheckBTab = New System.Windows.Forms.CheckBox
        Me.HandNCardsLabelBTab = New System.Windows.Forms.Label
        Me.TotalLabelBTab = New System.Windows.Forms.Label
        Me.SoftLabelBTab = New System.Windows.Forms.Label
        Me.NCardsLabelBTab = New System.Windows.Forms.Label
        Me.C3LabelBTab = New System.Windows.Forms.Label
        Me.C10LabelBTab = New System.Windows.Forms.Label
        Me.C4LabelBTab = New System.Windows.Forms.Label
        Me.C5LabelBTab = New System.Windows.Forms.Label
        Me.C2LabelBTab = New System.Windows.Forms.Label
        Me.C6LabelBTab = New System.Windows.Forms.Label
        Me.C7LabelBTab = New System.Windows.Forms.Label
        Me.C8LabelBTab = New System.Windows.Forms.Label
        Me.C9LabelBTab = New System.Windows.Forms.Label
        Me.CAceLabelBTab = New System.Windows.Forms.Label
        Me.HardOnlyCheckBTab = New System.Windows.Forms.CheckBox
        Me.SoftOnlyCheckBTab = New System.Windows.Forms.CheckBox
        Me.HandSoftLabelBTab = New System.Windows.Forms.Label
        Me.EitherCheckBTab = New System.Windows.Forms.CheckBox
        Me.HandTotalLabelBTab = New System.Windows.Forms.Label
        Me.DealerUCPayoffLabelBTab = New System.Windows.Forms.Label
        Me.DealerUCLabelBTab = New System.Windows.Forms.Label
        Me.PayoffUCGeneralBoxBTab = New System.Windows.Forms.TextBox
        Me.AceUpCheckBTab = New System.Windows.Forms.CheckBox
        Me.TenUpCheckBTab = New System.Windows.Forms.CheckBox
        Me.AddRuleButtonBTab = New System.Windows.Forms.Button
        Me.BonusRulesCheckListBoxBTab = New System.Windows.Forms.CheckedListBox
        Me.ForcedTab = New System.Windows.Forms.TabPage
        Me.ForcedStratTabControlFSTab = New System.Windows.Forms.TabControl
        Me.OptionsTabFSTab = New System.Windows.Forms.TabPage
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
        Me.HandNCardsComboBoxFSTab = New System.Windows.Forms.ComboBox
        Me.HandTotalComboBoxFSTab = New System.Windows.Forms.ComboBox
        Me.PostDoubleCheckFSTab = New System.Windows.Forms.CheckBox
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
        Me.HandSoftCheckFSTab = New System.Windows.Forms.CheckBox
        Me.HandSoftLabelFSTab = New System.Windows.Forms.Label
        Me.HandTotalLabelFSTab = New System.Windows.Forms.Label
        Me.AddForcedRuleButtonFSTab = New System.Windows.Forms.Button
        Me.ForcedRulesCheckListBoxFSTab = New System.Windows.Forms.CheckedListBox
        Me.LoadForcedTablesButtonFSTab = New System.Windows.Forms.Button
        Me.SaveForcedTablesButtonFSTab = New System.Windows.Forms.Button
        Me.ClearForcedTablesButtonFSTab = New System.Windows.Forms.Button
        Me.OtherOptionsTab = New System.Windows.Forms.TabPage
        Me.UCAllowedGroupOTab = New System.Windows.Forms.GroupBox
        Me.ToggleAllCheckOTab = New System.Windows.Forms.CheckBox
        Me.UpcardLabelOTab = New System.Windows.Forms.Label
        Me.ColorTableGroupOTab = New System.Windows.Forms.GroupBox
        Me.RestoreDefaultColorTableButtonOTab = New System.Windows.Forms.Button
        Me.SaveColorTableFileButtonOTab = New System.Windows.Forms.Button
        Me.LoadColorTableFileButtonOTab = New System.Windows.Forms.Button
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.FileMenuItem = New System.Windows.Forms.MenuItem
        Me.NewMenuItem = New System.Windows.Forms.MenuItem
        Me.OpenRuleSetMenuItem = New System.Windows.Forms.MenuItem
        Me.SaveRuleSetMenuItem = New System.Windows.Forms.MenuItem
        Me.OptionsMenuItem = New System.Windows.Forms.MenuItem
        Me.SetDefaultsMenuItem = New System.Windows.Forms.MenuItem
        Me.RestoreDefaultsMenuItem = New System.Windows.Forms.MenuItem
        Me.UseDPDictionaryMenuItem = New System.Windows.Forms.MenuItem
        Me.SaveDPDictionaryMenuItem = New System.Windows.Forms.MenuItem
        Me.PSExceptionsMenuItem = New System.Windows.Forms.MenuItem
        Me.StrategiesMenuItem = New System.Windows.Forms.MenuItem
        Me.ComputeTDMenuItem = New System.Windows.Forms.MenuItem
        Me.ComputeTCMenuItem = New System.Windows.Forms.MenuItem
        Me.ComputeForcedMenuItem = New System.Windows.Forms.MenuItem
        Me.HelpMenuItem = New System.Windows.Forms.MenuItem
        Me.AboutMenuItem = New System.Windows.Forms.MenuItem
        Me.HelpFileMenuItem = New System.Windows.Forms.MenuItem
        Me.RealtimeMenuItem = New System.Windows.Forms.MenuItem
        Me.StartRTMenuItem = New System.Windows.Forms.MenuItem
        Me.RTSPL1EstMenuItem = New System.Windows.Forms.MenuItem
        Me.RTSmallMenuItem = New System.Windows.Forms.MenuItem
        Me.CalcNowMenuItem = New System.Windows.Forms.MenuItem
        Me.MainTabs.SuspendLayout()
        Me.MainRulesTab.SuspendLayout()
        Me.HoleCardGroupMTab.SuspendLayout()
        Me.ShoeOptionGroupMTab.SuspendLayout()
        Me.SplitGroupMTab.SuspendLayout()
        Me.DoubleGroupMTab.SuspendLayout()
        Me.SurrenderGroupMTab.SuspendLayout()
        Me.DealerRulesGroupMTab.SuspendLayout()
        Me.DoubleRulesTab.SuspendLayout()
        Me.DoubleGroupDTab.SuspendLayout()
        Me.DoubleTableGroup.SuspendLayout()
        Me.SurrenderRulesTab.SuspendLayout()
        Me.SurrenderTableGroup.SuspendLayout()
        Me.SurrenderRulesGroupSTab.SuspendLayout()
        Me.SplitOptionsTab.SuspendLayout()
        Me.SplitPairsAllowedGroupSpTab.SuspendLayout()
        Me.CDSplitGroupSpTab.SuspendLayout()
        Me.SplitRulesGroupSpTab.SuspendLayout()
        Me.SpecialRulesTab.SuspendLayout()
        Me.PDTiesGroupSRTab.SuspendLayout()
        Me.ShoeOptionsTab.SuspendLayout()
        Me.ForcedShoeGroupShTab.SuspendLayout()
        Me.ReferenceShoeGroupShTab.SuspendLayout()
        Me.ShoeOptionGroupShTab.SuspendLayout()
        Me.BonusRulesTab.SuspendLayout()
        Me.BonusTabControlBTab.SuspendLayout()
        Me.BJBonusesTabBTab.SuspendLayout()
        Me.Spec10GroupBJTab.SuspendLayout()
        Me.GeneralBJGroupBJTab.SuspendLayout()
        Me.BonusRulesTabBTab.SuspendLayout()
        Me.BonusRuleDetailsGroupBTab.SuspendLayout()
        Me.ForcedTab.SuspendLayout()
        Me.ForcedStratTabControlFSTab.SuspendLayout()
        Me.OptionsTabFSTab.SuspendLayout()
        Me.HardSoftTDTabFSTab.SuspendLayout()
        Me.SoftTDGroupFSTab.SuspendLayout()
        Me.HardTDGroupFSTab.SuspendLayout()
        Me.SoftPairsCDTabFSTab.SuspendLayout()
        Me.PairCDGroupFSTab.SuspendLayout()
        Me.SoftCDGroupFSTab.SuspendLayout()
        Me.HardCDTabFSTab.SuspendLayout()
        Me.OtherTabFSTab.SuspendLayout()
        Me.ForcedRuleDetailsGroupFSTab.SuspendLayout()
        Me.OtherOptionsTab.SuspendLayout()
        Me.UCAllowedGroupOTab.SuspendLayout()
        Me.ColorTableGroupOTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainTabs
        '
        Me.MainTabs.Controls.Add(Me.MainRulesTab)
        Me.MainTabs.Controls.Add(Me.DoubleRulesTab)
        Me.MainTabs.Controls.Add(Me.SurrenderRulesTab)
        Me.MainTabs.Controls.Add(Me.SplitOptionsTab)
        Me.MainTabs.Controls.Add(Me.SpecialRulesTab)
        Me.MainTabs.Controls.Add(Me.ShoeOptionsTab)
        Me.MainTabs.Controls.Add(Me.BonusRulesTab)
        Me.MainTabs.Controls.Add(Me.ForcedTab)
        Me.MainTabs.Controls.Add(Me.OtherOptionsTab)
        Me.MainTabs.Location = New System.Drawing.Point(0, 8)
        Me.MainTabs.Name = "MainTabs"
        Me.MainTabs.SelectedIndex = 0
        Me.MainTabs.Size = New System.Drawing.Size(874, 646)
        Me.MainTabs.TabIndex = 0
        '
        'MainRulesTab
        '
        Me.MainRulesTab.Controls.Add(Me.LoadResultsButtonMTab)
        Me.MainRulesTab.Controls.Add(Me.HoleCardGroupMTab)
        Me.MainRulesTab.Controls.Add(Me.ShoeOptionGroupMTab)
        Me.MainRulesTab.Controls.Add(Me.SplitGroupMTab)
        Me.MainRulesTab.Controls.Add(Me.DoubleGroupMTab)
        Me.MainRulesTab.Controls.Add(Me.SurrenderGroupMTab)
        Me.MainRulesTab.Controls.Add(Me.DealerRulesGroupMTab)
        Me.MainRulesTab.Location = New System.Drawing.Point(4, 25)
        Me.MainRulesTab.Name = "MainRulesTab"
        Me.MainRulesTab.Size = New System.Drawing.Size(866, 617)
        Me.MainRulesTab.TabIndex = 0
        Me.MainRulesTab.Text = "Main Rules"
        '
        'LoadResultsButtonMTab
        '
        Me.LoadResultsButtonMTab.Location = New System.Drawing.Point(352, 560)
        Me.LoadResultsButtonMTab.Name = "LoadResultsButtonMTab"
        Me.LoadResultsButtonMTab.Size = New System.Drawing.Size(163, 37)
        Me.LoadResultsButtonMTab.TabIndex = 8
        Me.LoadResultsButtonMTab.Text = "Load Results File"
        Me.LoadResultsButtonMTab.Visible = False
        '
        'HoleCardGroupMTab
        '
        Me.HoleCardGroupMTab.Controls.Add(Me.AOBBOButtonMTab)
        Me.HoleCardGroupMTab.Controls.Add(Me.DCheckLabelMTab)
        Me.HoleCardGroupMTab.Controls.Add(Me.UseDefaultCheckMTab)
        Me.HoleCardGroupMTab.Controls.Add(Me.CheckTenCheckMTab)
        Me.HoleCardGroupMTab.Controls.Add(Me.CheckAceCheckMTab)
        Me.HoleCardGroupMTab.Controls.Add(Me.OBBOButtonMTab)
        Me.HoleCardGroupMTab.Controls.Add(Me.ENHCButtonMTab)
        Me.HoleCardGroupMTab.Controls.Add(Me.BBOButtonMTab)
        Me.HoleCardGroupMTab.Controls.Add(Me.OBOButtonMTab)
        Me.HoleCardGroupMTab.Location = New System.Drawing.Point(29, 314)
        Me.HoleCardGroupMTab.Name = "HoleCardGroupMTab"
        Me.HoleCardGroupMTab.Size = New System.Drawing.Size(355, 212)
        Me.HoleCardGroupMTab.TabIndex = 2
        Me.HoleCardGroupMTab.TabStop = False
        Me.HoleCardGroupMTab.Text = "Hole Card Rules"
        '
        'AOBBOButtonMTab
        '
        Me.AOBBOButtonMTab.Location = New System.Drawing.Point(200, 72)
        Me.AOBBOButtonMTab.Name = "AOBBOButtonMTab"
        Me.AOBBOButtonMTab.Size = New System.Drawing.Size(128, 37)
        Me.AOBBOButtonMTab.TabIndex = 8
        Me.AOBBOButtonMTab.TabStop = True
        Me.AOBBOButtonMTab.Text = "Australian OBBO (AOBBO)"
        '
        'DCheckLabelMTab
        '
        Me.DCheckLabelMTab.Location = New System.Drawing.Point(104, 152)
        Me.DCheckLabelMTab.Name = "DCheckLabelMTab"
        Me.DCheckLabelMTab.Size = New System.Drawing.Size(134, 18)
        Me.DCheckLabelMTab.TabIndex = 7
        Me.DCheckLabelMTab.Text = "Dealer Checks Under"
        '
        'UseDefaultCheckMTab
        '
        Me.UseDefaultCheckMTab.Location = New System.Drawing.Point(19, 175)
        Me.UseDefaultCheckMTab.Name = "UseDefaultCheckMTab"
        Me.UseDefaultCheckMTab.Size = New System.Drawing.Size(72, 19)
        Me.UseDefaultCheckMTab.TabIndex = 4
        Me.UseDefaultCheckMTab.Text = "Default"
        '
        'CheckTenCheckMTab
        '
        Me.CheckTenCheckMTab.Location = New System.Drawing.Point(250, 175)
        Me.CheckTenCheckMTab.Name = "CheckTenCheckMTab"
        Me.CheckTenCheckMTab.Size = New System.Drawing.Size(96, 19)
        Me.CheckTenCheckMTab.TabIndex = 6
        Me.CheckTenCheckMTab.Text = "Under Ten"
        '
        'CheckAceCheckMTab
        '
        Me.CheckAceCheckMTab.Location = New System.Drawing.Point(130, 175)
        Me.CheckAceCheckMTab.Name = "CheckAceCheckMTab"
        Me.CheckAceCheckMTab.Size = New System.Drawing.Size(96, 19)
        Me.CheckAceCheckMTab.TabIndex = 5
        Me.CheckAceCheckMTab.Text = "Under Ace"
        '
        'OBBOButtonMTab
        '
        Me.OBBOButtonMTab.Location = New System.Drawing.Point(16, 112)
        Me.OBBOButtonMTab.Name = "OBBOButtonMTab"
        Me.OBBOButtonMTab.Size = New System.Drawing.Size(152, 37)
        Me.OBBOButtonMTab.TabIndex = 3
        Me.OBBOButtonMTab.TabStop = True
        Me.OBBOButtonMTab.Text = "Literal Original && Busted Bets  (OBBO)"
        '
        'ENHCButtonMTab
        '
        Me.ENHCButtonMTab.Location = New System.Drawing.Point(200, 32)
        Me.ENHCButtonMTab.Name = "ENHCButtonMTab"
        Me.ENHCButtonMTab.Size = New System.Drawing.Size(152, 37)
        Me.ENHCButtonMTab.TabIndex = 1
        Me.ENHCButtonMTab.TabStop = True
        Me.ENHCButtonMTab.Text = "European No Hole Card   (ENHC)"
        '
        'BBOButtonMTab
        '
        Me.BBOButtonMTab.Location = New System.Drawing.Point(16, 72)
        Me.BBOButtonMTab.Name = "BBOButtonMTab"
        Me.BBOButtonMTab.Size = New System.Drawing.Size(165, 37)
        Me.BBOButtonMTab.TabIndex = 2
        Me.BBOButtonMTab.TabStop = True
        Me.BBOButtonMTab.Text = "Busted Bets + 1 (BBO)"
        '
        'OBOButtonMTab
        '
        Me.OBOButtonMTab.Location = New System.Drawing.Point(16, 32)
        Me.OBOButtonMTab.Name = "OBOButtonMTab"
        Me.OBOButtonMTab.Size = New System.Drawing.Size(125, 37)
        Me.OBOButtonMTab.TabIndex = 0
        Me.OBOButtonMTab.TabStop = True
        Me.OBOButtonMTab.Text = "American Rules   (OBO)"
        '
        'ShoeOptionGroupMTab
        '
        Me.ShoeOptionGroupMTab.Controls.Add(Me.SpanishDecksCheckBoxMTab)
        Me.ShoeOptionGroupMTab.Controls.Add(Me.NDecksLabelMTab)
        Me.ShoeOptionGroupMTab.Controls.Add(Me.NDecksBoxMTab)
        Me.ShoeOptionGroupMTab.Controls.Add(Me.ForcedShoeCheckMTab)
        Me.ShoeOptionGroupMTab.Controls.Add(Me.InfiniteDecksButtonMTab)
        Me.ShoeOptionGroupMTab.Controls.Add(Me.FiniteDecksButtonMTab)
        Me.ShoeOptionGroupMTab.Location = New System.Drawing.Point(29, 18)
        Me.ShoeOptionGroupMTab.Name = "ShoeOptionGroupMTab"
        Me.ShoeOptionGroupMTab.Size = New System.Drawing.Size(355, 120)
        Me.ShoeOptionGroupMTab.TabIndex = 0
        Me.ShoeOptionGroupMTab.TabStop = False
        Me.ShoeOptionGroupMTab.Text = "Shoe Options"
        '
        'SpanishDecksCheckBoxMTab
        '
        Me.SpanishDecksCheckBoxMTab.Location = New System.Drawing.Point(232, 48)
        Me.SpanishDecksCheckBoxMTab.Name = "SpanishDecksCheckBoxMTab"
        Me.SpanishDecksCheckBoxMTab.Size = New System.Drawing.Size(120, 24)
        Me.SpanishDecksCheckBoxMTab.TabIndex = 4
        Me.SpanishDecksCheckBoxMTab.Text = "Spanish Decks"
        '
        'NDecksLabelMTab
        '
        Me.NDecksLabelMTab.Location = New System.Drawing.Point(10, 83)
        Me.NDecksLabelMTab.Name = "NDecksLabelMTab"
        Me.NDecksLabelMTab.Size = New System.Drawing.Size(144, 28)
        Me.NDecksLabelMTab.TabIndex = 0
        Me.NDecksLabelMTab.Text = "Number of Decks"
        Me.NDecksLabelMTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NDecksBoxMTab
        '
        Me.NDecksBoxMTab.Location = New System.Drawing.Point(230, 83)
        Me.NDecksBoxMTab.Name = "NDecksBoxMTab"
        Me.NDecksBoxMTab.Size = New System.Drawing.Size(77, 22)
        Me.NDecksBoxMTab.TabIndex = 3
        Me.NDecksBoxMTab.Text = ""
        Me.NDecksBoxMTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ForcedShoeCheckMTab
        '
        Me.ForcedShoeCheckMTab.Location = New System.Drawing.Point(10, 48)
        Me.ForcedShoeCheckMTab.Name = "ForcedShoeCheckMTab"
        Me.ForcedShoeCheckMTab.Size = New System.Drawing.Size(163, 24)
        Me.ForcedShoeCheckMTab.TabIndex = 2
        Me.ForcedShoeCheckMTab.Text = "Used Forced Shoe"
        '
        'InfiniteDecksButtonMTab
        '
        Me.InfiniteDecksButtonMTab.Location = New System.Drawing.Point(232, 24)
        Me.InfiniteDecksButtonMTab.Name = "InfiniteDecksButtonMTab"
        Me.InfiniteDecksButtonMTab.Size = New System.Drawing.Size(116, 24)
        Me.InfiniteDecksButtonMTab.TabIndex = 1
        Me.InfiniteDecksButtonMTab.TabStop = True
        Me.InfiniteDecksButtonMTab.Text = "Infinite Decks"
        '
        'FiniteDecksButtonMTab
        '
        Me.FiniteDecksButtonMTab.Location = New System.Drawing.Point(10, 24)
        Me.FiniteDecksButtonMTab.Name = "FiniteDecksButtonMTab"
        Me.FiniteDecksButtonMTab.Size = New System.Drawing.Size(124, 22)
        Me.FiniteDecksButtonMTab.TabIndex = 0
        Me.FiniteDecksButtonMTab.TabStop = True
        Me.FiniteDecksButtonMTab.Text = "Finite Decks"
        '
        'SplitGroupMTab
        '
        Me.SplitGroupMTab.Controls.Add(Me.SMACheckMTab)
        Me.SplitGroupMTab.Controls.Add(Me.SPL3ButtonMTab)
        Me.SplitGroupMTab.Controls.Add(Me.SPL1ButtonMTab)
        Me.SplitGroupMTab.Controls.Add(Me.SPL2ButtonMTab)
        Me.SplitGroupMTab.Controls.Add(Me.NSPlitsLabelMTab)
        Me.SplitGroupMTab.Controls.Add(Me.SPL0ButtonMTab)
        Me.SplitGroupMTab.Controls.Add(Me.HSACheckMTab)
        Me.SplitGroupMTab.Location = New System.Drawing.Point(490, 388)
        Me.SplitGroupMTab.Name = "SplitGroupMTab"
        Me.SplitGroupMTab.Size = New System.Drawing.Size(345, 138)
        Me.SplitGroupMTab.TabIndex = 5
        Me.SplitGroupMTab.TabStop = False
        Me.SplitGroupMTab.Text = "Split Rules"
        '
        'SMACheckMTab
        '
        Me.SMACheckMTab.Location = New System.Drawing.Point(211, 83)
        Me.SMACheckMTab.Name = "SMACheckMTab"
        Me.SMACheckMTab.Size = New System.Drawing.Size(125, 37)
        Me.SMACheckMTab.TabIndex = 5
        Me.SMACheckMTab.Text = "Split Multiple Aces   (SMA)"
        '
        'SPL3ButtonMTab
        '
        Me.SPL3ButtonMTab.Location = New System.Drawing.Point(278, 46)
        Me.SPL3ButtonMTab.Name = "SPL3ButtonMTab"
        Me.SPL3ButtonMTab.Size = New System.Drawing.Size(39, 28)
        Me.SPL3ButtonMTab.TabIndex = 3
        Me.SPL3ButtonMTab.TabStop = True
        Me.SPL3ButtonMTab.Text = "3"
        '
        'SPL1ButtonMTab
        '
        Me.SPL1ButtonMTab.Location = New System.Drawing.Point(144, 46)
        Me.SPL1ButtonMTab.Name = "SPL1ButtonMTab"
        Me.SPL1ButtonMTab.Size = New System.Drawing.Size(38, 28)
        Me.SPL1ButtonMTab.TabIndex = 1
        Me.SPL1ButtonMTab.TabStop = True
        Me.SPL1ButtonMTab.Text = "1"
        '
        'SPL2ButtonMTab
        '
        Me.SPL2ButtonMTab.Location = New System.Drawing.Point(211, 46)
        Me.SPL2ButtonMTab.Name = "SPL2ButtonMTab"
        Me.SPL2ButtonMTab.Size = New System.Drawing.Size(48, 28)
        Me.SPL2ButtonMTab.TabIndex = 2
        Me.SPL2ButtonMTab.TabStop = True
        Me.SPL2ButtonMTab.Text = "2"
        '
        'NSPlitsLabelMTab
        '
        Me.NSPlitsLabelMTab.Location = New System.Drawing.Point(48, 24)
        Me.NSPlitsLabelMTab.Name = "NSPlitsLabelMTab"
        Me.NSPlitsLabelMTab.Size = New System.Drawing.Size(272, 18)
        Me.NSPlitsLabelMTab.TabIndex = 13
        Me.NSPlitsLabelMTab.Text = "Number of Splits Allowed   (SPLn)"
        Me.NSPlitsLabelMTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SPL0ButtonMTab
        '
        Me.SPL0ButtonMTab.Location = New System.Drawing.Point(48, 46)
        Me.SPL0ButtonMTab.Name = "SPL0ButtonMTab"
        Me.SPL0ButtonMTab.Size = New System.Drawing.Size(67, 28)
        Me.SPL0ButtonMTab.TabIndex = 0
        Me.SPL0ButtonMTab.TabStop = True
        Me.SPL0ButtonMTab.Text = "None"
        '
        'HSACheckMTab
        '
        Me.HSACheckMTab.Location = New System.Drawing.Point(48, 83)
        Me.HSACheckMTab.Name = "HSACheckMTab"
        Me.HSACheckMTab.Size = New System.Drawing.Size(125, 37)
        Me.HSACheckMTab.TabIndex = 4
        Me.HSACheckMTab.Text = "Hit to Split Aces   (HSA)"
        '
        'DoubleGroupMTab
        '
        Me.DoubleGroupMTab.Controls.Add(Me.DTableButtonMTab)
        Me.DoubleGroupMTab.Controls.Add(Me.D1011ButtonMTab)
        Me.DoubleGroupMTab.Controls.Add(Me.D91011ButtonMTab)
        Me.DoubleGroupMTab.Controls.Add(Me.DOAButtonMTab)
        Me.DoubleGroupMTab.Controls.Add(Me.DANCheckMTab)
        Me.DoubleGroupMTab.Controls.Add(Me.DASCheckMTab)
        Me.DoubleGroupMTab.Location = New System.Drawing.Point(490, 18)
        Me.DoubleGroupMTab.Name = "DoubleGroupMTab"
        Me.DoubleGroupMTab.Size = New System.Drawing.Size(345, 167)
        Me.DoubleGroupMTab.TabIndex = 3
        Me.DoubleGroupMTab.TabStop = False
        Me.DoubleGroupMTab.Text = "Double Rules"
        '
        'DTableButtonMTab
        '
        Me.DTableButtonMTab.Location = New System.Drawing.Point(182, 120)
        Me.DTableButtonMTab.Name = "DTableButtonMTab"
        Me.DTableButtonMTab.Size = New System.Drawing.Size(144, 37)
        Me.DTableButtonMTab.TabIndex = 5
        Me.DTableButtonMTab.TabStop = True
        Me.DTableButtonMTab.Text = "Use Double Table"
        '
        'D1011ButtonMTab
        '
        Me.D1011ButtonMTab.Location = New System.Drawing.Point(19, 120)
        Me.D1011ButtonMTab.Name = "D1011ButtonMTab"
        Me.D1011ButtonMTab.Size = New System.Drawing.Size(144, 37)
        Me.D1011ButtonMTab.TabIndex = 4
        Me.D1011ButtonMTab.TabStop = True
        Me.D1011ButtonMTab.Text = "Double 10, 11 Only   (D10)"
        '
        'D91011ButtonMTab
        '
        Me.D91011ButtonMTab.Location = New System.Drawing.Point(182, 74)
        Me.D91011ButtonMTab.Name = "D91011ButtonMTab"
        Me.D91011ButtonMTab.Size = New System.Drawing.Size(144, 37)
        Me.D91011ButtonMTab.TabIndex = 3
        Me.D91011ButtonMTab.TabStop = True
        Me.D91011ButtonMTab.Text = "Double 9, 10, 11   (D9)"
        '
        'DOAButtonMTab
        '
        Me.DOAButtonMTab.Location = New System.Drawing.Point(19, 74)
        Me.DOAButtonMTab.Name = "DOAButtonMTab"
        Me.DOAButtonMTab.Size = New System.Drawing.Size(135, 37)
        Me.DOAButtonMTab.TabIndex = 2
        Me.DOAButtonMTab.TabStop = True
        Me.DOAButtonMTab.Text = "Double Any Total   (DOA)"
        '
        'DANCheckMTab
        '
        Me.DANCheckMTab.Location = New System.Drawing.Point(182, 28)
        Me.DANCheckMTab.Name = "DANCheckMTab"
        Me.DANCheckMTab.Size = New System.Drawing.Size(154, 37)
        Me.DANCheckMTab.TabIndex = 1
        Me.DANCheckMTab.Text = "Double Any Number of Cards   (DAN)"
        '
        'DASCheckMTab
        '
        Me.DASCheckMTab.Location = New System.Drawing.Point(19, 28)
        Me.DASCheckMTab.Name = "DASCheckMTab"
        Me.DASCheckMTab.Size = New System.Drawing.Size(144, 37)
        Me.DASCheckMTab.TabIndex = 0
        Me.DASCheckMTab.Text = "Double After Split   (DAS/NDAS)"
        '
        'SurrenderGroupMTab
        '
        Me.SurrenderGroupMTab.Controls.Add(Me.ES10ButtonMTab)
        Me.SurrenderGroupMTab.Controls.Add(Me.STableButtonMTab)
        Me.SurrenderGroupMTab.Controls.Add(Me.SurrenderTo1LabelMTab)
        Me.SurrenderGroupMTab.Controls.Add(Me.SurrPaysBoxMTab)
        Me.SurrenderGroupMTab.Controls.Add(Me.SurrenderPaysLabelMTab)
        Me.SurrenderGroupMTab.Controls.Add(Me.ESButtonMTab)
        Me.SurrenderGroupMTab.Controls.Add(Me.LSButtonMTab)
        Me.SurrenderGroupMTab.Controls.Add(Me.NSButtonMTab)
        Me.SurrenderGroupMTab.Location = New System.Drawing.Point(490, 203)
        Me.SurrenderGroupMTab.Name = "SurrenderGroupMTab"
        Me.SurrenderGroupMTab.Size = New System.Drawing.Size(345, 166)
        Me.SurrenderGroupMTab.TabIndex = 4
        Me.SurrenderGroupMTab.TabStop = False
        Me.SurrenderGroupMTab.Text = "Surrender Rules"
        '
        'ES10ButtonMTab
        '
        Me.ES10ButtonMTab.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ES10ButtonMTab.Location = New System.Drawing.Point(182, 28)
        Me.ES10ButtonMTab.Name = "ES10ButtonMTab"
        Me.ES10ButtonMTab.Size = New System.Drawing.Size(154, 37)
        Me.ES10ButtonMTab.TabIndex = 3
        Me.ES10ButtonMTab.TabStop = True
        Me.ES10ButtonMTab.Text = "ES vs 2-10; NS vs A (ES10)"
        Me.ES10ButtonMTab.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'STableButtonMTab
        '
        Me.STableButtonMTab.Location = New System.Drawing.Point(182, 83)
        Me.STableButtonMTab.Name = "STableButtonMTab"
        Me.STableButtonMTab.Size = New System.Drawing.Size(154, 19)
        Me.STableButtonMTab.TabIndex = 5
        Me.STableButtonMTab.TabStop = True
        Me.STableButtonMTab.Text = "Use Surrender Table"
        '
        'SurrenderTo1LabelMTab
        '
        Me.SurrenderTo1LabelMTab.Location = New System.Drawing.Point(240, 120)
        Me.SurrenderTo1LabelMTab.Name = "SurrenderTo1LabelMTab"
        Me.SurrenderTo1LabelMTab.Size = New System.Drawing.Size(38, 18)
        Me.SurrenderTo1LabelMTab.TabIndex = 11
        Me.SurrenderTo1LabelMTab.Text = "to 1"
        '
        'SurrPaysBoxMTab
        '
        Me.SurrPaysBoxMTab.BackColor = System.Drawing.SystemColors.Window
        Me.SurrPaysBoxMTab.ForeColor = System.Drawing.SystemColors.WindowText
        Me.SurrPaysBoxMTab.Location = New System.Drawing.Point(192, 120)
        Me.SurrPaysBoxMTab.Name = "SurrPaysBoxMTab"
        Me.SurrPaysBoxMTab.Size = New System.Drawing.Size(38, 22)
        Me.SurrPaysBoxMTab.TabIndex = 6
        Me.SurrPaysBoxMTab.Text = ""
        Me.SurrPaysBoxMTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SurrenderPaysLabelMTab
        '
        Me.SurrenderPaysLabelMTab.Location = New System.Drawing.Point(86, 120)
        Me.SurrenderPaysLabelMTab.Name = "SurrenderPaysLabelMTab"
        Me.SurrenderPaysLabelMTab.Size = New System.Drawing.Size(106, 27)
        Me.SurrenderPaysLabelMTab.TabIndex = 10
        Me.SurrenderPaysLabelMTab.Text = "Surrender Pays"
        '
        'ESButtonMTab
        '
        Me.ESButtonMTab.Location = New System.Drawing.Point(19, 83)
        Me.ESButtonMTab.Name = "ESButtonMTab"
        Me.ESButtonMTab.Size = New System.Drawing.Size(163, 19)
        Me.ESButtonMTab.TabIndex = 2
        Me.ESButtonMTab.TabStop = True
        Me.ESButtonMTab.Text = "Early Surrender   (ES)"
        '
        'LSButtonMTab
        '
        Me.LSButtonMTab.Location = New System.Drawing.Point(19, 55)
        Me.LSButtonMTab.Name = "LSButtonMTab"
        Me.LSButtonMTab.Size = New System.Drawing.Size(154, 19)
        Me.LSButtonMTab.TabIndex = 1
        Me.LSButtonMTab.TabStop = True
        Me.LSButtonMTab.Text = "Late Surrender   (LS)"
        '
        'NSButtonMTab
        '
        Me.NSButtonMTab.Location = New System.Drawing.Point(19, 28)
        Me.NSButtonMTab.Name = "NSButtonMTab"
        Me.NSButtonMTab.Size = New System.Drawing.Size(154, 18)
        Me.NSButtonMTab.TabIndex = 0
        Me.NSButtonMTab.TabStop = True
        Me.NSButtonMTab.Text = "No Surrender   (NS)"
        '
        'DealerRulesGroupMTab
        '
        Me.DealerRulesGroupMTab.Controls.Add(Me.BJTo1LabelMTab)
        Me.DealerRulesGroupMTab.Controls.Add(Me.BJPaysBoxMTab)
        Me.DealerRulesGroupMTab.Controls.Add(Me.BJPaysLabelMTab)
        Me.DealerRulesGroupMTab.Controls.Add(Me.Stand18ButtonMTab)
        Me.DealerRulesGroupMTab.Controls.Add(Me.Stand17ButtonMTab)
        Me.DealerRulesGroupMTab.Controls.Add(Me.DealerStandsLabelMTab)
        Me.DealerRulesGroupMTab.Location = New System.Drawing.Point(29, 157)
        Me.DealerRulesGroupMTab.Name = "DealerRulesGroupMTab"
        Me.DealerRulesGroupMTab.Size = New System.Drawing.Size(355, 138)
        Me.DealerRulesGroupMTab.TabIndex = 1
        Me.DealerRulesGroupMTab.TabStop = False
        Me.DealerRulesGroupMTab.Text = "Dealer && Blackjack Rules"
        '
        'BJTo1LabelMTab
        '
        Me.BJTo1LabelMTab.Location = New System.Drawing.Point(278, 28)
        Me.BJTo1LabelMTab.Name = "BJTo1LabelMTab"
        Me.BJTo1LabelMTab.Size = New System.Drawing.Size(29, 18)
        Me.BJTo1LabelMTab.TabIndex = 4
        Me.BJTo1LabelMTab.Text = "to 1"
        '
        'BJPaysBoxMTab
        '
        Me.BJPaysBoxMTab.Location = New System.Drawing.Point(240, 28)
        Me.BJPaysBoxMTab.Name = "BJPaysBoxMTab"
        Me.BJPaysBoxMTab.Size = New System.Drawing.Size(29, 22)
        Me.BJPaysBoxMTab.TabIndex = 0
        Me.BJPaysBoxMTab.Text = ""
        Me.BJPaysBoxMTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BJPaysLabelMTab
        '
        Me.BJPaysLabelMTab.Location = New System.Drawing.Point(29, 28)
        Me.BJPaysLabelMTab.Name = "BJPaysLabelMTab"
        Me.BJPaysLabelMTab.Size = New System.Drawing.Size(105, 26)
        Me.BJPaysLabelMTab.TabIndex = 3
        Me.BJPaysLabelMTab.Text = "Blackjack Pays"
        '
        'Stand18ButtonMTab
        '
        Me.Stand18ButtonMTab.Location = New System.Drawing.Point(240, 102)
        Me.Stand18ButtonMTab.Name = "Stand18ButtonMTab"
        Me.Stand18ButtonMTab.Size = New System.Drawing.Size(86, 27)
        Me.Stand18ButtonMTab.TabIndex = 2
        Me.Stand18ButtonMTab.TabStop = True
        Me.Stand18ButtonMTab.Text = "18   (H17)"
        '
        'Stand17ButtonMTab
        '
        Me.Stand17ButtonMTab.Location = New System.Drawing.Point(240, 74)
        Me.Stand17ButtonMTab.Name = "Stand17ButtonMTab"
        Me.Stand17ButtonMTab.Size = New System.Drawing.Size(86, 28)
        Me.Stand17ButtonMTab.TabIndex = 1
        Me.Stand17ButtonMTab.TabStop = True
        Me.Stand17ButtonMTab.Text = "17   (S17)"
        '
        'DealerStandsLabelMTab
        '
        Me.DealerStandsLabelMTab.Location = New System.Drawing.Point(29, 83)
        Me.DealerStandsLabelMTab.Name = "DealerStandsLabelMTab"
        Me.DealerStandsLabelMTab.Size = New System.Drawing.Size(144, 28)
        Me.DealerStandsLabelMTab.TabIndex = 5
        Me.DealerStandsLabelMTab.Text = "Dealer Stands on Soft"
        '
        'DoubleRulesTab
        '
        Me.DoubleRulesTab.Controls.Add(Me.DDREarlyButtonDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DDRLateButtonDTab)
        Me.DoubleRulesTab.Controls.Add(Me.P21AutowinCheckBoxDTab)
        Me.DoubleRulesTab.Controls.Add(Me.RDADDRLabelDTab)
        Me.DoubleRulesTab.Controls.Add(Me.RDAPSCheckDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DDRPSCheckDTab)
        Me.DoubleRulesTab.Controls.Add(Me.RDACheckDTab)
        Me.DoubleRulesTab.Controls.Add(Me.RDDepthBoxDTab)
        Me.DoubleRulesTab.Controls.Add(Me.RDDepthLabelDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DDRCheckDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DSoft19Hard9CheckDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DoubleGroupDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DoubleTableGroup)
        Me.DoubleRulesTab.Controls.Add(Me.DANCheckDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DASCheckDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DSACheckDTab)
        Me.DoubleRulesTab.Controls.Add(Me.DSoftAllHardCheckDTab)
        Me.DoubleRulesTab.Location = New System.Drawing.Point(4, 25)
        Me.DoubleRulesTab.Name = "DoubleRulesTab"
        Me.DoubleRulesTab.Size = New System.Drawing.Size(866, 617)
        Me.DoubleRulesTab.TabIndex = 1
        Me.DoubleRulesTab.Text = "Double"
        '
        'DDREarlyButtonDTab
        '
        Me.DDREarlyButtonDTab.Enabled = False
        Me.DDREarlyButtonDTab.Location = New System.Drawing.Point(256, 440)
        Me.DDREarlyButtonDTab.Name = "DDREarlyButtonDTab"
        Me.DDREarlyButtonDTab.Size = New System.Drawing.Size(88, 19)
        Me.DDREarlyButtonDTab.TabIndex = 58
        Me.DDREarlyButtonDTab.TabStop = True
        Me.DDREarlyButtonDTab.Text = "DDR Early"
        '
        'DDRLateButtonDTab
        '
        Me.DDRLateButtonDTab.Enabled = False
        Me.DDRLateButtonDTab.Location = New System.Drawing.Point(256, 416)
        Me.DDRLateButtonDTab.Name = "DDRLateButtonDTab"
        Me.DDRLateButtonDTab.Size = New System.Drawing.Size(88, 19)
        Me.DDRLateButtonDTab.TabIndex = 57
        Me.DDRLateButtonDTab.TabStop = True
        Me.DDRLateButtonDTab.Text = "DDR Late"
        '
        'P21AutowinCheckBoxDTab
        '
        Me.P21AutowinCheckBoxDTab.Location = New System.Drawing.Point(24, 360)
        Me.P21AutowinCheckBoxDTab.Name = "P21AutowinCheckBoxDTab"
        Me.P21AutowinCheckBoxDTab.Size = New System.Drawing.Size(192, 40)
        Me.P21AutowinCheckBoxDTab.TabIndex = 56
        Me.P21AutowinCheckBoxDTab.Text = "Player 21 Always Wins Including BJ/Post-Double"
        '
        'RDADDRLabelDTab
        '
        Me.RDADDRLabelDTab.Location = New System.Drawing.Point(24, 572)
        Me.RDADDRLabelDTab.Name = "RDADDRLabelDTab"
        Me.RDADDRLabelDTab.Size = New System.Drawing.Size(240, 37)
        Me.RDADDRLabelDTab.TabIndex = 18
        Me.RDADDRLabelDTab.Text = "*Note: Redoubles and Double Down Rescue are always played optimally."
        '
        'RDAPSCheckDTab
        '
        Me.RDAPSCheckDTab.Location = New System.Drawing.Point(24, 504)
        Me.RDAPSCheckDTab.Name = "RDAPSCheckDTab"
        Me.RDAPSCheckDTab.Size = New System.Drawing.Size(211, 24)
        Me.RDAPSCheckDTab.TabIndex = 17
        Me.RDAPSCheckDTab.Text = "Redoubling Allowed After Split"
        '
        'DDRPSCheckDTab
        '
        Me.DDRPSCheckDTab.Location = New System.Drawing.Point(24, 440)
        Me.DDRPSCheckDTab.Name = "DDRPSCheckDTab"
        Me.DDRPSCheckDTab.Size = New System.Drawing.Size(230, 27)
        Me.DDRPSCheckDTab.TabIndex = 16
        Me.DDRPSCheckDTab.Text = "Double Down Rescue After Split"
        '
        'RDACheckDTab
        '
        Me.RDACheckDTab.Location = New System.Drawing.Point(24, 480)
        Me.RDACheckDTab.Name = "RDACheckDTab"
        Me.RDACheckDTab.Size = New System.Drawing.Size(182, 24)
        Me.RDACheckDTab.TabIndex = 13
        Me.RDACheckDTab.Text = "Redoubling Allowed"
        '
        'RDDepthBoxDTab
        '
        Me.RDDepthBoxDTab.BackColor = System.Drawing.SystemColors.Window
        Me.RDDepthBoxDTab.Location = New System.Drawing.Point(200, 535)
        Me.RDDepthBoxDTab.Name = "RDDepthBoxDTab"
        Me.RDDepthBoxDTab.Size = New System.Drawing.Size(39, 22)
        Me.RDDepthBoxDTab.TabIndex = 14
        Me.RDDepthBoxDTab.Text = ""
        Me.RDDepthBoxDTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RDDepthLabelDTab
        '
        Me.RDDepthLabelDTab.Location = New System.Drawing.Point(24, 535)
        Me.RDDepthLabelDTab.Name = "RDDepthLabelDTab"
        Me.RDDepthLabelDTab.Size = New System.Drawing.Size(173, 37)
        Me.RDDepthLabelDTab.TabIndex = 15
        Me.RDDepthLabelDTab.Text = "Number of Doubles Allowed"
        '
        'DDRCheckDTab
        '
        Me.DDRCheckDTab.Location = New System.Drawing.Point(24, 408)
        Me.DDRCheckDTab.Name = "DDRCheckDTab"
        Me.DDRCheckDTab.Size = New System.Drawing.Size(201, 32)
        Me.DDRCheckDTab.TabIndex = 7
        Me.DDRCheckDTab.Text = "Surrender after Double  (Double Down Rescue)"
        '
        'DSoft19Hard9CheckDTab
        '
        Me.DSoft19Hard9CheckDTab.Location = New System.Drawing.Point(24, 320)
        Me.DSoft19Hard9CheckDTab.Name = "DSoft19Hard9CheckDTab"
        Me.DSoft19Hard9CheckDTab.Size = New System.Drawing.Size(240, 27)
        Me.DSoft19Hard9CheckDTab.TabIndex = 5
        Me.DSoft19Hard9CheckDTab.Text = "Doubled Soft 19 Counts as Hard 9"
        '
        'DoubleGroupDTab
        '
        Me.DoubleGroupDTab.Controls.Add(Me.DTableButtonDTab)
        Me.DoubleGroupDTab.Controls.Add(Me.D1011ButtonDTab)
        Me.DoubleGroupDTab.Controls.Add(Me.D91011ButtonDTab)
        Me.DoubleGroupDTab.Controls.Add(Me.DOAButtonDTab)
        Me.DoubleGroupDTab.Location = New System.Drawing.Point(24, 16)
        Me.DoubleGroupDTab.Name = "DoubleGroupDTab"
        Me.DoubleGroupDTab.Size = New System.Drawing.Size(259, 148)
        Me.DoubleGroupDTab.TabIndex = 0
        Me.DoubleGroupDTab.TabStop = False
        Me.DoubleGroupDTab.Text = "Double Rules"
        '
        'DTableButtonDTab
        '
        Me.DTableButtonDTab.Location = New System.Drawing.Point(19, 111)
        Me.DTableButtonDTab.Name = "DTableButtonDTab"
        Me.DTableButtonDTab.Size = New System.Drawing.Size(211, 27)
        Me.DTableButtonDTab.TabIndex = 3
        Me.DTableButtonDTab.TabStop = True
        Me.DTableButtonDTab.Text = "Use Double Table"
        '
        'D1011ButtonDTab
        '
        Me.D1011ButtonDTab.Location = New System.Drawing.Point(19, 83)
        Me.D1011ButtonDTab.Name = "D1011ButtonDTab"
        Me.D1011ButtonDTab.Size = New System.Drawing.Size(211, 28)
        Me.D1011ButtonDTab.TabIndex = 2
        Me.D1011ButtonDTab.TabStop = True
        Me.D1011ButtonDTab.Text = "Double 10, 11 Only   (D10)"
        '
        'D91011ButtonDTab
        '
        Me.D91011ButtonDTab.Location = New System.Drawing.Point(19, 55)
        Me.D91011ButtonDTab.Name = "D91011ButtonDTab"
        Me.D91011ButtonDTab.Size = New System.Drawing.Size(192, 28)
        Me.D91011ButtonDTab.TabIndex = 1
        Me.D91011ButtonDTab.TabStop = True
        Me.D91011ButtonDTab.Text = "Double 9, 10, 11   (D9)"
        '
        'DOAButtonDTab
        '
        Me.DOAButtonDTab.Location = New System.Drawing.Point(19, 28)
        Me.DOAButtonDTab.Name = "DOAButtonDTab"
        Me.DOAButtonDTab.Size = New System.Drawing.Size(183, 27)
        Me.DOAButtonDTab.TabIndex = 0
        Me.DOAButtonDTab.TabStop = True
        Me.DOAButtonDTab.Text = "Double Any Total   (DOA)"
        '
        'DoubleTableGroup
        '
        Me.DoubleTableGroup.Controls.Add(Me.SMultiLabelDTab)
        Me.DoubleTableGroup.Controls.Add(Me.STCLabelDTab)
        Me.DoubleTableGroup.Controls.Add(Me.HMultiLabelDTab)
        Me.DoubleTableGroup.Controls.Add(Me.HTCLabelDTab)
        Me.DoubleTableGroup.Controls.Add(Me.SoftLabelDTab)
        Me.DoubleTableGroup.Controls.Add(Me.DToggleAllCheckDTab)
        Me.DoubleTableGroup.Controls.Add(Me.HardLabelDTab)
        Me.DoubleTableGroup.Controls.Add(Me.PTotLabelDTab)
        Me.DoubleTableGroup.Controls.Add(Me.SaveDoubleTableFileButtonDTab)
        Me.DoubleTableGroup.Controls.Add(Me.LoadDoubleTableFileButtonDTab)
        Me.DoubleTableGroup.Location = New System.Drawing.Point(509, 18)
        Me.DoubleTableGroup.Name = "DoubleTableGroup"
        Me.DoubleTableGroup.Size = New System.Drawing.Size(326, 510)
        Me.DoubleTableGroup.TabIndex = 6
        Me.DoubleTableGroup.TabStop = False
        Me.DoubleTableGroup.Text = "Double Allowed Based on Player Totals"
        '
        'SMultiLabelDTab
        '
        Me.SMultiLabelDTab.Location = New System.Drawing.Point(259, 65)
        Me.SMultiLabelDTab.Name = "SMultiLabelDTab"
        Me.SMultiLabelDTab.Size = New System.Drawing.Size(63, 18)
        Me.SMultiLabelDTab.TabIndex = 86
        Me.SMultiLabelDTab.Text = "Multicard"
        '
        'STCLabelDTab
        '
        Me.STCLabelDTab.Location = New System.Drawing.Point(211, 65)
        Me.STCLabelDTab.Name = "STCLabelDTab"
        Me.STCLabelDTab.Size = New System.Drawing.Size(48, 18)
        Me.STCLabelDTab.TabIndex = 85
        Me.STCLabelDTab.Text = "2-Card"
        '
        'HMultiLabelDTab
        '
        Me.HMultiLabelDTab.Location = New System.Drawing.Point(144, 65)
        Me.HMultiLabelDTab.Name = "HMultiLabelDTab"
        Me.HMultiLabelDTab.Size = New System.Drawing.Size(62, 18)
        Me.HMultiLabelDTab.TabIndex = 83
        Me.HMultiLabelDTab.Text = "Multicard"
        '
        'HTCLabelDTab
        '
        Me.HTCLabelDTab.Location = New System.Drawing.Point(96, 65)
        Me.HTCLabelDTab.Name = "HTCLabelDTab"
        Me.HTCLabelDTab.Size = New System.Drawing.Size(48, 18)
        Me.HTCLabelDTab.TabIndex = 82
        Me.HTCLabelDTab.Text = "2-Card"
        '
        'SoftLabelDTab
        '
        Me.SoftLabelDTab.Location = New System.Drawing.Point(240, 46)
        Me.SoftLabelDTab.Name = "SoftLabelDTab"
        Me.SoftLabelDTab.Size = New System.Drawing.Size(38, 19)
        Me.SoftLabelDTab.TabIndex = 81
        Me.SoftLabelDTab.Text = "Soft"
        '
        'DToggleAllCheckDTab
        '
        Me.DToggleAllCheckDTab.Location = New System.Drawing.Point(10, 28)
        Me.DToggleAllCheckDTab.Name = "DToggleAllCheckDTab"
        Me.DToggleAllCheckDTab.Size = New System.Drawing.Size(96, 20)
        Me.DToggleAllCheckDTab.TabIndex = 0
        Me.DToggleAllCheckDTab.Text = "Toggle All"
        '
        'HardLabelDTab
        '
        Me.HardLabelDTab.Location = New System.Drawing.Point(125, 46)
        Me.HardLabelDTab.Name = "HardLabelDTab"
        Me.HardLabelDTab.Size = New System.Drawing.Size(48, 19)
        Me.HardLabelDTab.TabIndex = 69
        Me.HardLabelDTab.Text = "Hard"
        '
        'PTotLabelDTab
        '
        Me.PTotLabelDTab.Location = New System.Drawing.Point(10, 65)
        Me.PTotLabelDTab.Name = "PTotLabelDTab"
        Me.PTotLabelDTab.Size = New System.Drawing.Size(86, 18)
        Me.PTotLabelDTab.TabIndex = 50
        Me.PTotLabelDTab.Text = "Player Total"
        '
        'SaveDoubleTableFileButtonDTab
        '
        Me.SaveDoubleTableFileButtonDTab.Location = New System.Drawing.Point(16, 464)
        Me.SaveDoubleTableFileButtonDTab.Name = "SaveDoubleTableFileButtonDTab"
        Me.SaveDoubleTableFileButtonDTab.Size = New System.Drawing.Size(135, 28)
        Me.SaveDoubleTableFileButtonDTab.TabIndex = 100
        Me.SaveDoubleTableFileButtonDTab.Text = "Save Double Table"
        '
        'LoadDoubleTableFileButtonDTab
        '
        Me.LoadDoubleTableFileButtonDTab.Location = New System.Drawing.Point(176, 464)
        Me.LoadDoubleTableFileButtonDTab.Name = "LoadDoubleTableFileButtonDTab"
        Me.LoadDoubleTableFileButtonDTab.Size = New System.Drawing.Size(134, 28)
        Me.LoadDoubleTableFileButtonDTab.TabIndex = 101
        Me.LoadDoubleTableFileButtonDTab.Text = "Load Double Table"
        '
        'DANCheckDTab
        '
        Me.DANCheckDTab.Location = New System.Drawing.Point(24, 176)
        Me.DANCheckDTab.Name = "DANCheckDTab"
        Me.DANCheckDTab.Size = New System.Drawing.Size(259, 27)
        Me.DANCheckDTab.TabIndex = 1
        Me.DANCheckDTab.Text = "Double Any Number of Cards   (DAN)"
        '
        'DASCheckDTab
        '
        Me.DASCheckDTab.Location = New System.Drawing.Point(24, 216)
        Me.DASCheckDTab.Name = "DASCheckDTab"
        Me.DASCheckDTab.Size = New System.Drawing.Size(230, 27)
        Me.DASCheckDTab.TabIndex = 2
        Me.DASCheckDTab.Text = "Double After Split   (DAS/NDAS)"
        '
        'DSACheckDTab
        '
        Me.DSACheckDTab.Location = New System.Drawing.Point(24, 240)
        Me.DSACheckDTab.Name = "DSACheckDTab"
        Me.DSACheckDTab.Size = New System.Drawing.Size(249, 28)
        Me.DSACheckDTab.TabIndex = 3
        Me.DSACheckDTab.Text = "Double of Split Aces Allowed (DSA)"
        '
        'DSoftAllHardCheckDTab
        '
        Me.DSoftAllHardCheckDTab.Location = New System.Drawing.Point(24, 280)
        Me.DSoftAllHardCheckDTab.Name = "DSoftAllHardCheckDTab"
        Me.DSoftAllHardCheckDTab.Size = New System.Drawing.Size(240, 37)
        Me.DSoftAllHardCheckDTab.TabIndex = 4
        Me.DSoftAllHardCheckDTab.Text = "Doubled Soft Hands Count as Hard (All Totals)"
        '
        'SurrenderRulesTab
        '
        Me.SurrenderRulesTab.Controls.Add(Me.DDREarlyButtonSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.DDRLateButtonSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.RDADDRLabelSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.DDRPSCheckSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.DDRCheckSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SSACheckSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SurrDBJCheckSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.To1DBJPaysLabelSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SurrDBJPaysBoxSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SurrDBJPaysLabelSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.MacauSurrenderAceCheckSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.MacauSurrender2to10CheckSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SASCheckSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SANCheckSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SurrenderTableGroup)
        Me.SurrenderRulesTab.Controls.Add(Me.SurrenderRulesGroupSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.ToOneLabelSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SurrPaysBoxSTab)
        Me.SurrenderRulesTab.Controls.Add(Me.SurrenderPaysLabelSTab)
        Me.SurrenderRulesTab.Location = New System.Drawing.Point(4, 25)
        Me.SurrenderRulesTab.Name = "SurrenderRulesTab"
        Me.SurrenderRulesTab.Size = New System.Drawing.Size(866, 617)
        Me.SurrenderRulesTab.TabIndex = 2
        Me.SurrenderRulesTab.Text = "Surrender"
        Me.SurrenderRulesTab.ToolTipText = "Surrender of 5-card or more unbusted hands"
        '
        'DDREarlyButtonSTab
        '
        Me.DDREarlyButtonSTab.Enabled = False
        Me.DDREarlyButtonSTab.Location = New System.Drawing.Point(272, 488)
        Me.DDREarlyButtonSTab.Name = "DDREarlyButtonSTab"
        Me.DDREarlyButtonSTab.Size = New System.Drawing.Size(88, 19)
        Me.DDREarlyButtonSTab.TabIndex = 60
        Me.DDREarlyButtonSTab.TabStop = True
        Me.DDREarlyButtonSTab.Text = "DDR Early"
        '
        'DDRLateButtonSTab
        '
        Me.DDRLateButtonSTab.Enabled = False
        Me.DDRLateButtonSTab.Location = New System.Drawing.Point(272, 464)
        Me.DDRLateButtonSTab.Name = "DDRLateButtonSTab"
        Me.DDRLateButtonSTab.Size = New System.Drawing.Size(88, 19)
        Me.DDRLateButtonSTab.TabIndex = 59
        Me.DDRLateButtonSTab.TabStop = True
        Me.DDRLateButtonSTab.Text = "DDR Late"
        '
        'RDADDRLabelSTab
        '
        Me.RDADDRLabelSTab.Location = New System.Drawing.Point(29, 526)
        Me.RDADDRLabelSTab.Name = "RDADDRLabelSTab"
        Me.RDADDRLabelSTab.Size = New System.Drawing.Size(240, 37)
        Me.RDADDRLabelSTab.TabIndex = 19
        Me.RDADDRLabelSTab.Text = "*Note: Redoubles and Double Down Rescue are always played optimally."
        '
        'DDRPSCheckSTab
        '
        Me.DDRPSCheckSTab.Location = New System.Drawing.Point(29, 489)
        Me.DDRPSCheckSTab.Name = "DDRPSCheckSTab"
        Me.DDRPSCheckSTab.Size = New System.Drawing.Size(230, 28)
        Me.DDRPSCheckSTab.TabIndex = 17
        Me.DDRPSCheckSTab.Text = "Double Down Rescue After Split"
        '
        'DDRCheckSTab
        '
        Me.DDRCheckSTab.Location = New System.Drawing.Point(29, 452)
        Me.DDRCheckSTab.Name = "DDRCheckSTab"
        Me.DDRCheckSTab.Size = New System.Drawing.Size(201, 37)
        Me.DDRCheckSTab.TabIndex = 14
        Me.DDRCheckSTab.Text = "Surrender after Double  (Double Down Rescue)"
        '
        'SSACheckSTab
        '
        Me.SSACheckSTab.Location = New System.Drawing.Point(29, 397)
        Me.SSACheckSTab.Name = "SSACheckSTab"
        Me.SSACheckSTab.Size = New System.Drawing.Size(259, 37)
        Me.SSACheckSTab.TabIndex = 8
        Me.SSACheckSTab.Text = "Surrender of Split Aces Allowed (SSA)"
        '
        'SurrDBJCheckSTab
        '
        Me.SurrDBJCheckSTab.Location = New System.Drawing.Point(29, 194)
        Me.SurrDBJCheckSTab.Name = "SurrDBJCheckSTab"
        Me.SurrDBJCheckSTab.Size = New System.Drawing.Size(249, 18)
        Me.SurrDBJCheckSTab.TabIndex = 2
        Me.SurrDBJCheckSTab.Text = "Surrender Bonus against Dealer BJ"
        '
        'To1DBJPaysLabelSTab
        '
        Me.To1DBJPaysLabelSTab.Location = New System.Drawing.Point(173, 222)
        Me.To1DBJPaysLabelSTab.Name = "To1DBJPaysLabelSTab"
        Me.To1DBJPaysLabelSTab.Size = New System.Drawing.Size(144, 18)
        Me.To1DBJPaysLabelSTab.TabIndex = 13
        Me.To1DBJPaysLabelSTab.Text = "to 1 against Dealer BJ"
        '
        'SurrDBJPaysBoxSTab
        '
        Me.SurrDBJPaysBoxSTab.BackColor = System.Drawing.SystemColors.Window
        Me.SurrDBJPaysBoxSTab.Location = New System.Drawing.Point(125, 222)
        Me.SurrDBJPaysBoxSTab.Name = "SurrDBJPaysBoxSTab"
        Me.SurrDBJPaysBoxSTab.Size = New System.Drawing.Size(38, 22)
        Me.SurrDBJPaysBoxSTab.TabIndex = 3
        Me.SurrDBJPaysBoxSTab.Text = ""
        Me.SurrDBJPaysBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SurrDBJPaysLabelSTab
        '
        Me.SurrDBJPaysLabelSTab.Location = New System.Drawing.Point(29, 222)
        Me.SurrDBJPaysLabelSTab.Name = "SurrDBJPaysLabelSTab"
        Me.SurrDBJPaysLabelSTab.Size = New System.Drawing.Size(105, 36)
        Me.SurrDBJPaysLabelSTab.TabIndex = 12
        Me.SurrDBJPaysLabelSTab.Text = "Surrender Pays (ES Only)"
        '
        'MacauSurrenderAceCheckSTab
        '
        Me.MacauSurrenderAceCheckSTab.Location = New System.Drawing.Point(29, 296)
        Me.MacauSurrenderAceCheckSTab.Name = "MacauSurrenderAceCheckSTab"
        Me.MacauSurrenderAceCheckSTab.Size = New System.Drawing.Size(259, 32)
        Me.MacauSurrenderAceCheckSTab.TabIndex = 5
        Me.MacauSurrenderAceCheckSTab.Text = "5-card Surrender Against Upcard Ace"
        '
        'MacauSurrender2to10CheckSTab
        '
        Me.MacauSurrender2to10CheckSTab.Location = New System.Drawing.Point(29, 277)
        Me.MacauSurrender2to10CheckSTab.Name = "MacauSurrender2to10CheckSTab"
        Me.MacauSurrender2to10CheckSTab.Size = New System.Drawing.Size(288, 18)
        Me.MacauSurrender2to10CheckSTab.TabIndex = 4
        Me.MacauSurrender2to10CheckSTab.Text = "5-card Surrender Against Upcards 2 to 10"
        '
        'SASCheckSTab
        '
        Me.SASCheckSTab.Location = New System.Drawing.Point(29, 369)
        Me.SASCheckSTab.Name = "SASCheckSTab"
        Me.SASCheckSTab.Size = New System.Drawing.Size(192, 37)
        Me.SASCheckSTab.TabIndex = 7
        Me.SASCheckSTab.Text = "Surrender After Split (SAS)"
        '
        'SANCheckSTab
        '
        Me.SANCheckSTab.Location = New System.Drawing.Point(29, 351)
        Me.SANCheckSTab.Name = "SANCheckSTab"
        Me.SANCheckSTab.Size = New System.Drawing.Size(269, 18)
        Me.SANCheckSTab.TabIndex = 6
        Me.SANCheckSTab.Text = "Surrender Any Number of Cards (SAN)"
        '
        'SurrenderTableGroup
        '
        Me.SurrenderTableGroup.Controls.Add(Me.CAceSTab)
        Me.SurrenderTableGroup.Controls.Add(Me.C3STab)
        Me.SurrenderTableGroup.Controls.Add(Me.C4STab)
        Me.SurrenderTableGroup.Controls.Add(Me.C5STab)
        Me.SurrenderTableGroup.Controls.Add(Me.C6STab)
        Me.SurrenderTableGroup.Controls.Add(Me.C7STab)
        Me.SurrenderTableGroup.Controls.Add(Me.C8STab)
        Me.SurrenderTableGroup.Controls.Add(Me.C9STab)
        Me.SurrenderTableGroup.Controls.Add(Me.C10STab)
        Me.SurrenderTableGroup.Controls.Add(Me.C2STab)
        Me.SurrenderTableGroup.Controls.Add(Me.UpcardLabelSTab)
        Me.SurrenderTableGroup.Controls.Add(Me.ToggleAllSTab)
        Me.SurrenderTableGroup.Controls.Add(Me.ToggleAllComboSTab)
        Me.SurrenderTableGroup.Location = New System.Drawing.Point(528, 18)
        Me.SurrenderTableGroup.Name = "SurrenderTableGroup"
        Me.SurrenderTableGroup.Size = New System.Drawing.Size(307, 438)
        Me.SurrenderTableGroup.TabIndex = 9
        Me.SurrenderTableGroup.TabStop = False
        Me.SurrenderTableGroup.Text = "Surrender Allowed Based on Dealer Upcard"
        '
        'CAceSTab
        '
        Me.CAceSTab.Location = New System.Drawing.Point(56, 392)
        Me.CAceSTab.Name = "CAceSTab"
        Me.CAceSTab.Size = New System.Drawing.Size(19, 18)
        Me.CAceSTab.TabIndex = 13
        Me.CAceSTab.Text = "A"
        '
        'C3STab
        '
        Me.C3STab.Location = New System.Drawing.Point(56, 136)
        Me.C3STab.Name = "C3STab"
        Me.C3STab.Size = New System.Drawing.Size(19, 19)
        Me.C3STab.TabIndex = 12
        Me.C3STab.Text = "3"
        '
        'C4STab
        '
        Me.C4STab.Location = New System.Drawing.Point(56, 168)
        Me.C4STab.Name = "C4STab"
        Me.C4STab.Size = New System.Drawing.Size(19, 18)
        Me.C4STab.TabIndex = 11
        Me.C4STab.Text = "4"
        '
        'C5STab
        '
        Me.C5STab.Location = New System.Drawing.Point(56, 200)
        Me.C5STab.Name = "C5STab"
        Me.C5STab.Size = New System.Drawing.Size(19, 18)
        Me.C5STab.TabIndex = 10
        Me.C5STab.Text = "5"
        '
        'C6STab
        '
        Me.C6STab.Location = New System.Drawing.Point(56, 232)
        Me.C6STab.Name = "C6STab"
        Me.C6STab.Size = New System.Drawing.Size(19, 19)
        Me.C6STab.TabIndex = 9
        Me.C6STab.Text = "6"
        '
        'C7STab
        '
        Me.C7STab.Location = New System.Drawing.Point(56, 264)
        Me.C7STab.Name = "C7STab"
        Me.C7STab.Size = New System.Drawing.Size(19, 18)
        Me.C7STab.TabIndex = 8
        Me.C7STab.Text = "7"
        '
        'C8STab
        '
        Me.C8STab.Location = New System.Drawing.Point(56, 296)
        Me.C8STab.Name = "C8STab"
        Me.C8STab.Size = New System.Drawing.Size(19, 18)
        Me.C8STab.TabIndex = 7
        Me.C8STab.Text = "8"
        '
        'C9STab
        '
        Me.C9STab.Location = New System.Drawing.Point(56, 328)
        Me.C9STab.Name = "C9STab"
        Me.C9STab.Size = New System.Drawing.Size(19, 19)
        Me.C9STab.TabIndex = 6
        Me.C9STab.Text = "9"
        '
        'C10STab
        '
        Me.C10STab.Location = New System.Drawing.Point(56, 360)
        Me.C10STab.Name = "C10STab"
        Me.C10STab.Size = New System.Drawing.Size(19, 19)
        Me.C10STab.TabIndex = 5
        Me.C10STab.Text = "T"
        '
        'C2STab
        '
        Me.C2STab.Location = New System.Drawing.Point(56, 104)
        Me.C2STab.Name = "C2STab"
        Me.C2STab.Size = New System.Drawing.Size(19, 18)
        Me.C2STab.TabIndex = 4
        Me.C2STab.Text = "2"
        '
        'UpcardLabelSTab
        '
        Me.UpcardLabelSTab.Location = New System.Drawing.Point(29, 74)
        Me.UpcardLabelSTab.Name = "UpcardLabelSTab"
        Me.UpcardLabelSTab.Size = New System.Drawing.Size(86, 18)
        Me.UpcardLabelSTab.TabIndex = 3
        Me.UpcardLabelSTab.Text = "Dealer Card"
        '
        'ToggleAllSTab
        '
        Me.ToggleAllSTab.Location = New System.Drawing.Point(32, 32)
        Me.ToggleAllSTab.Name = "ToggleAllSTab"
        Me.ToggleAllSTab.Size = New System.Drawing.Size(77, 18)
        Me.ToggleAllSTab.TabIndex = 2
        Me.ToggleAllSTab.Text = "Toggle All"
        '
        'ToggleAllComboSTab
        '
        Me.ToggleAllComboSTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToggleAllComboSTab.Items.AddRange(New Object() {"No Surrender", "Late Surrender", "Early Surrender"})
        Me.ToggleAllComboSTab.Location = New System.Drawing.Point(152, 32)
        Me.ToggleAllComboSTab.Name = "ToggleAllComboSTab"
        Me.ToggleAllComboSTab.Size = New System.Drawing.Size(124, 24)
        Me.ToggleAllComboSTab.TabIndex = 1
        '
        'SurrenderRulesGroupSTab
        '
        Me.SurrenderRulesGroupSTab.Controls.Add(Me.ES10ButtonSTab)
        Me.SurrenderRulesGroupSTab.Controls.Add(Me.STableButtonSTab)
        Me.SurrenderRulesGroupSTab.Controls.Add(Me.ESButtonSTab)
        Me.SurrenderRulesGroupSTab.Controls.Add(Me.LSButtonSTab)
        Me.SurrenderRulesGroupSTab.Controls.Add(Me.NSButtonSTab)
        Me.SurrenderRulesGroupSTab.Location = New System.Drawing.Point(29, 18)
        Me.SurrenderRulesGroupSTab.Name = "SurrenderRulesGroupSTab"
        Me.SurrenderRulesGroupSTab.Size = New System.Drawing.Size(355, 120)
        Me.SurrenderRulesGroupSTab.TabIndex = 0
        Me.SurrenderRulesGroupSTab.TabStop = False
        Me.SurrenderRulesGroupSTab.Text = "Surrender Rules"
        '
        'ES10ButtonSTab
        '
        Me.ES10ButtonSTab.Location = New System.Drawing.Point(192, 28)
        Me.ES10ButtonSTab.Name = "ES10ButtonSTab"
        Me.ES10ButtonSTab.Size = New System.Drawing.Size(154, 46)
        Me.ES10ButtonSTab.TabIndex = 3
        Me.ES10ButtonSTab.TabStop = True
        Me.ES10ButtonSTab.Text = "ES vs 2-10; NS vs A (ES10)"
        '
        'STableButtonSTab
        '
        Me.STableButtonSTab.Location = New System.Drawing.Point(192, 83)
        Me.STableButtonSTab.Name = "STableButtonSTab"
        Me.STableButtonSTab.Size = New System.Drawing.Size(154, 19)
        Me.STableButtonSTab.TabIndex = 5
        Me.STableButtonSTab.TabStop = True
        Me.STableButtonSTab.Text = "Use Surrender Table"
        '
        'ESButtonSTab
        '
        Me.ESButtonSTab.Location = New System.Drawing.Point(19, 83)
        Me.ESButtonSTab.Name = "ESButtonSTab"
        Me.ESButtonSTab.Size = New System.Drawing.Size(163, 19)
        Me.ESButtonSTab.TabIndex = 2
        Me.ESButtonSTab.TabStop = True
        Me.ESButtonSTab.Text = "Early Surrender   (ES)"
        '
        'LSButtonSTab
        '
        Me.LSButtonSTab.Location = New System.Drawing.Point(19, 55)
        Me.LSButtonSTab.Name = "LSButtonSTab"
        Me.LSButtonSTab.Size = New System.Drawing.Size(154, 19)
        Me.LSButtonSTab.TabIndex = 1
        Me.LSButtonSTab.TabStop = True
        Me.LSButtonSTab.Text = "Late Surrender   (LS)"
        '
        'NSButtonSTab
        '
        Me.NSButtonSTab.Location = New System.Drawing.Point(19, 28)
        Me.NSButtonSTab.Name = "NSButtonSTab"
        Me.NSButtonSTab.Size = New System.Drawing.Size(154, 18)
        Me.NSButtonSTab.TabIndex = 0
        Me.NSButtonSTab.TabStop = True
        Me.NSButtonSTab.Text = "No Surrender   (NS)"
        '
        'ToOneLabelSTab
        '
        Me.ToOneLabelSTab.Location = New System.Drawing.Point(173, 157)
        Me.ToOneLabelSTab.Name = "ToOneLabelSTab"
        Me.ToOneLabelSTab.Size = New System.Drawing.Size(38, 18)
        Me.ToOneLabelSTab.TabIndex = 9
        Me.ToOneLabelSTab.Text = "to 1"
        '
        'SurrPaysBoxSTab
        '
        Me.SurrPaysBoxSTab.BackColor = System.Drawing.SystemColors.Window
        Me.SurrPaysBoxSTab.Location = New System.Drawing.Point(125, 157)
        Me.SurrPaysBoxSTab.Name = "SurrPaysBoxSTab"
        Me.SurrPaysBoxSTab.Size = New System.Drawing.Size(38, 22)
        Me.SurrPaysBoxSTab.TabIndex = 1
        Me.SurrPaysBoxSTab.Text = ""
        Me.SurrPaysBoxSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SurrenderPaysLabelSTab
        '
        Me.SurrenderPaysLabelSTab.Location = New System.Drawing.Point(29, 157)
        Me.SurrenderPaysLabelSTab.Name = "SurrenderPaysLabelSTab"
        Me.SurrenderPaysLabelSTab.Size = New System.Drawing.Size(105, 26)
        Me.SurrenderPaysLabelSTab.TabIndex = 3
        Me.SurrenderPaysLabelSTab.Text = "Surrender Pays"
        '
        'SplitOptionsTab
        '
        Me.SplitOptionsTab.Controls.Add(Me.RDADDRLabelSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.RDAPSCheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.DDRPSCheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.SSACheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.SplitPairsAllowedGroupSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.TCPlusCheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.TDPlusCheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.CDSplitGroupSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.BJSplitTensCheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.BJSPlitAcesCheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.SASCheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.DSACheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.DASCheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.SplitRulesGroupSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.HSACheckSpTab)
        Me.SplitOptionsTab.Controls.Add(Me.SMACheckSpTab)
        Me.SplitOptionsTab.Location = New System.Drawing.Point(4, 25)
        Me.SplitOptionsTab.Name = "SplitOptionsTab"
        Me.SplitOptionsTab.Size = New System.Drawing.Size(866, 617)
        Me.SplitOptionsTab.TabIndex = 4
        Me.SplitOptionsTab.Text = "Split"
        '
        'RDADDRLabelSpTab
        '
        Me.RDADDRLabelSpTab.Location = New System.Drawing.Point(29, 471)
        Me.RDADDRLabelSpTab.Name = "RDADDRLabelSpTab"
        Me.RDADDRLabelSpTab.Size = New System.Drawing.Size(240, 37)
        Me.RDADDRLabelSpTab.TabIndex = 20
        Me.RDADDRLabelSpTab.Text = "*Note: Redoubles and Double Down Rescue are always played optimally."
        '
        'RDAPSCheckSpTab
        '
        Me.RDAPSCheckSpTab.Location = New System.Drawing.Point(29, 434)
        Me.RDAPSCheckSpTab.Name = "RDAPSCheckSpTab"
        Me.RDAPSCheckSpTab.Size = New System.Drawing.Size(201, 28)
        Me.RDAPSCheckSpTab.TabIndex = 18
        Me.RDAPSCheckSpTab.Text = "Redouble Allowed After Split"
        '
        'DDRPSCheckSpTab
        '
        Me.DDRPSCheckSpTab.Location = New System.Drawing.Point(29, 406)
        Me.DDRPSCheckSpTab.Name = "DDRPSCheckSpTab"
        Me.DDRPSCheckSpTab.Size = New System.Drawing.Size(230, 28)
        Me.DDRPSCheckSpTab.TabIndex = 17
        Me.DDRPSCheckSpTab.Text = "Double Down Rescue After Split"
        '
        'SSACheckSpTab
        '
        Me.SSACheckSpTab.Location = New System.Drawing.Point(29, 203)
        Me.SSACheckSpTab.Name = "SSACheckSpTab"
        Me.SSACheckSpTab.Size = New System.Drawing.Size(259, 37)
        Me.SSACheckSpTab.TabIndex = 4
        Me.SSACheckSpTab.Text = "Surrender of Split Aces Allowed (SSA)"
        '
        'SplitPairsAllowedGroupSpTab
        '
        Me.SplitPairsAllowedGroupSpTab.Controls.Add(Me.ToggleAllCheckSpTab)
        Me.SplitPairsAllowedGroupSpTab.Controls.Add(Me.PairLabelSpTab)
        Me.SplitPairsAllowedGroupSpTab.Location = New System.Drawing.Point(528, 18)
        Me.SplitPairsAllowedGroupSpTab.Name = "SplitPairsAllowedGroupSpTab"
        Me.SplitPairsAllowedGroupSpTab.Size = New System.Drawing.Size(307, 318)
        Me.SplitPairsAllowedGroupSpTab.TabIndex = 9
        Me.SplitPairsAllowedGroupSpTab.TabStop = False
        Me.SplitPairsAllowedGroupSpTab.Text = "Split Allowed Based on Player Pair"
        '
        'ToggleAllCheckSpTab
        '
        Me.ToggleAllCheckSpTab.Location = New System.Drawing.Point(106, 28)
        Me.ToggleAllCheckSpTab.Name = "ToggleAllCheckSpTab"
        Me.ToggleAllCheckSpTab.Size = New System.Drawing.Size(96, 20)
        Me.ToggleAllCheckSpTab.TabIndex = 0
        Me.ToggleAllCheckSpTab.Text = "Toggle All"
        '
        'PairLabelSpTab
        '
        Me.PairLabelSpTab.Location = New System.Drawing.Point(134, 65)
        Me.PairLabelSpTab.Name = "PairLabelSpTab"
        Me.PairLabelSpTab.Size = New System.Drawing.Size(29, 18)
        Me.PairLabelSpTab.TabIndex = 50
        Me.PairLabelSpTab.Text = "Pair"
        '
        'TCPlusCheckSpTab
        '
        Me.TCPlusCheckSpTab.Location = New System.Drawing.Point(528, 504)
        Me.TCPlusCheckSpTab.Name = "TCPlusCheckSpTab"
        Me.TCPlusCheckSpTab.Size = New System.Drawing.Size(269, 27)
        Me.TCPlusCheckSpTab.TabIndex = 12
        Me.TCPlusCheckSpTab.Text = "Include Post Split Hands in TC Strategy"
        Me.TCPlusCheckSpTab.Visible = False
        '
        'TDPlusCheckSpTab
        '
        Me.TDPlusCheckSpTab.Location = New System.Drawing.Point(528, 472)
        Me.TDPlusCheckSpTab.Name = "TDPlusCheckSpTab"
        Me.TDPlusCheckSpTab.Size = New System.Drawing.Size(269, 28)
        Me.TDPlusCheckSpTab.TabIndex = 11
        Me.TDPlusCheckSpTab.Text = "Include Post Split Hands in TD Strategy"
        Me.TDPlusCheckSpTab.Visible = False
        '
        'CDSplitGroupSpTab
        '
        Me.CDSplitGroupSpTab.Controls.Add(Me.CDPNButtonSpTab)
        Me.CDSplitGroupSpTab.Controls.Add(Me.CDPButtonSpTab)
        Me.CDSplitGroupSpTab.Controls.Add(Me.CDSplitLabelSpTab)
        Me.CDSplitGroupSpTab.Controls.Add(Me.CDZButtonSpTab)
        Me.CDSplitGroupSpTab.Location = New System.Drawing.Point(528, 360)
        Me.CDSplitGroupSpTab.Name = "CDSplitGroupSpTab"
        Me.CDSplitGroupSpTab.Size = New System.Drawing.Size(307, 93)
        Me.CDSplitGroupSpTab.TabIndex = 10
        Me.CDSplitGroupSpTab.TabStop = False
        Me.CDSplitGroupSpTab.Text = "Composition Dependent Splits"
        '
        'CDPNButtonSpTab
        '
        Me.CDPNButtonSpTab.Location = New System.Drawing.Point(211, 55)
        Me.CDPNButtonSpTab.Name = "CDPNButtonSpTab"
        Me.CDPNButtonSpTab.Size = New System.Drawing.Size(77, 19)
        Me.CDPNButtonSpTab.TabIndex = 2
        Me.CDPNButtonSpTab.Text = "CD-PN"
        '
        'CDPButtonSpTab
        '
        Me.CDPButtonSpTab.Location = New System.Drawing.Point(125, 55)
        Me.CDPButtonSpTab.Name = "CDPButtonSpTab"
        Me.CDPButtonSpTab.Size = New System.Drawing.Size(67, 19)
        Me.CDPButtonSpTab.TabIndex = 1
        Me.CDPButtonSpTab.Text = "CD-P"
        '
        'CDSplitLabelSpTab
        '
        Me.CDSplitLabelSpTab.Location = New System.Drawing.Point(77, 28)
        Me.CDSplitLabelSpTab.Name = "CDSplitLabelSpTab"
        Me.CDSplitLabelSpTab.Size = New System.Drawing.Size(173, 18)
        Me.CDSplitLabelSpTab.TabIndex = 1
        Me.CDSplitLabelSpTab.Text = "CD Split Values Used Are"
        '
        'CDZButtonSpTab
        '
        Me.CDZButtonSpTab.Location = New System.Drawing.Point(29, 55)
        Me.CDZButtonSpTab.Name = "CDZButtonSpTab"
        Me.CDZButtonSpTab.Size = New System.Drawing.Size(67, 19)
        Me.CDZButtonSpTab.TabIndex = 0
        Me.CDZButtonSpTab.Text = "CDZ-"
        '
        'BJSplitTensCheckSpTab
        '
        Me.BJSplitTensCheckSpTab.Location = New System.Drawing.Point(29, 360)
        Me.BJSplitTensCheckSpTab.Name = "BJSplitTensCheckSpTab"
        Me.BJSplitTensCheckSpTab.Size = New System.Drawing.Size(230, 28)
        Me.BJSplitTensCheckSpTab.TabIndex = 8
        Me.BJSplitTensCheckSpTab.Text = "Blackjack Bonus After Split Tens"
        '
        'BJSPlitAcesCheckSpTab
        '
        Me.BJSPlitAcesCheckSpTab.Location = New System.Drawing.Point(29, 332)
        Me.BJSPlitAcesCheckSpTab.Name = "BJSPlitAcesCheckSpTab"
        Me.BJSPlitAcesCheckSpTab.Size = New System.Drawing.Size(230, 28)
        Me.BJSPlitAcesCheckSpTab.TabIndex = 7
        Me.BJSPlitAcesCheckSpTab.Text = "Blackjack Bonus After Split Aces"
        '
        'SASCheckSpTab
        '
        Me.SASCheckSpTab.Location = New System.Drawing.Point(29, 286)
        Me.SASCheckSpTab.Name = "SASCheckSpTab"
        Me.SASCheckSpTab.Size = New System.Drawing.Size(192, 28)
        Me.SASCheckSpTab.TabIndex = 6
        Me.SASCheckSpTab.Text = "Surrender After Split (SAS)"
        '
        'DSACheckSpTab
        '
        Me.DSACheckSpTab.Location = New System.Drawing.Point(29, 175)
        Me.DSACheckSpTab.Name = "DSACheckSpTab"
        Me.DSACheckSpTab.Size = New System.Drawing.Size(249, 37)
        Me.DSACheckSpTab.TabIndex = 3
        Me.DSACheckSpTab.Text = "Double of Split Aces Allowed (DSA)"
        '
        'DASCheckSpTab
        '
        Me.DASCheckSpTab.Location = New System.Drawing.Point(29, 258)
        Me.DASCheckSpTab.Name = "DASCheckSpTab"
        Me.DASCheckSpTab.Size = New System.Drawing.Size(230, 28)
        Me.DASCheckSpTab.TabIndex = 5
        Me.DASCheckSpTab.Text = "Double After Split   (DAS/NDAS)"
        '
        'SplitRulesGroupSpTab
        '
        Me.SplitRulesGroupSpTab.Controls.Add(Me.SPL3ButtonSpTab)
        Me.SplitRulesGroupSpTab.Controls.Add(Me.SPL1ButtonSpTab)
        Me.SplitRulesGroupSpTab.Controls.Add(Me.SPL2ButtonSpTab)
        Me.SplitRulesGroupSpTab.Controls.Add(Me.NSplitsLabelSpTab)
        Me.SplitRulesGroupSpTab.Controls.Add(Me.SPL0ButtonSpTab)
        Me.SplitRulesGroupSpTab.Location = New System.Drawing.Point(29, 18)
        Me.SplitRulesGroupSpTab.Name = "SplitRulesGroupSpTab"
        Me.SplitRulesGroupSpTab.Size = New System.Drawing.Size(345, 93)
        Me.SplitRulesGroupSpTab.TabIndex = 0
        Me.SplitRulesGroupSpTab.TabStop = False
        Me.SplitRulesGroupSpTab.Text = "Split Rules"
        '
        'SPL3ButtonSpTab
        '
        Me.SPL3ButtonSpTab.Location = New System.Drawing.Point(278, 55)
        Me.SPL3ButtonSpTab.Name = "SPL3ButtonSpTab"
        Me.SPL3ButtonSpTab.Size = New System.Drawing.Size(39, 19)
        Me.SPL3ButtonSpTab.TabIndex = 3
        Me.SPL3ButtonSpTab.TabStop = True
        Me.SPL3ButtonSpTab.Text = "3"
        '
        'SPL1ButtonSpTab
        '
        Me.SPL1ButtonSpTab.Location = New System.Drawing.Point(144, 55)
        Me.SPL1ButtonSpTab.Name = "SPL1ButtonSpTab"
        Me.SPL1ButtonSpTab.Size = New System.Drawing.Size(38, 19)
        Me.SPL1ButtonSpTab.TabIndex = 1
        Me.SPL1ButtonSpTab.TabStop = True
        Me.SPL1ButtonSpTab.Text = "1"
        '
        'SPL2ButtonSpTab
        '
        Me.SPL2ButtonSpTab.Location = New System.Drawing.Point(211, 55)
        Me.SPL2ButtonSpTab.Name = "SPL2ButtonSpTab"
        Me.SPL2ButtonSpTab.Size = New System.Drawing.Size(48, 19)
        Me.SPL2ButtonSpTab.TabIndex = 2
        Me.SPL2ButtonSpTab.TabStop = True
        Me.SPL2ButtonSpTab.Text = "2"
        '
        'NSplitsLabelSpTab
        '
        Me.NSplitsLabelSpTab.Location = New System.Drawing.Point(67, 28)
        Me.NSplitsLabelSpTab.Name = "NSplitsLabelSpTab"
        Me.NSplitsLabelSpTab.Size = New System.Drawing.Size(211, 18)
        Me.NSplitsLabelSpTab.TabIndex = 13
        Me.NSplitsLabelSpTab.Text = "Number of Splits Allowed   (SPLn)"
        '
        'SPL0ButtonSpTab
        '
        Me.SPL0ButtonSpTab.Location = New System.Drawing.Point(48, 55)
        Me.SPL0ButtonSpTab.Name = "SPL0ButtonSpTab"
        Me.SPL0ButtonSpTab.Size = New System.Drawing.Size(67, 19)
        Me.SPL0ButtonSpTab.TabIndex = 0
        Me.SPL0ButtonSpTab.TabStop = True
        Me.SPL0ButtonSpTab.Text = "None"
        '
        'HSACheckSpTab
        '
        Me.HSACheckSpTab.Location = New System.Drawing.Point(29, 148)
        Me.HSACheckSpTab.Name = "HSACheckSpTab"
        Me.HSACheckSpTab.Size = New System.Drawing.Size(393, 37)
        Me.HSACheckSpTab.TabIndex = 2
        Me.HSACheckSpTab.Text = "Hit to Split Aces   (HSA)  *Note: Does not imply DSA/SSA"
        '
        'SMACheckSpTab
        '
        Me.SMACheckSpTab.Location = New System.Drawing.Point(29, 120)
        Me.SMACheckSpTab.Name = "SMACheckSpTab"
        Me.SMACheckSpTab.Size = New System.Drawing.Size(192, 37)
        Me.SMACheckSpTab.TabIndex = 1
        Me.SMACheckSpTab.Text = "Split Multiple Aces   (SMA)"
        '
        'SpecialRulesTab
        '
        Me.SpecialRulesTab.Controls.Add(Me.P21AutowinCheckBoxSRTab)
        Me.SpecialRulesTab.Controls.Add(Me.MaxPlayerCardsBoxSRTab)
        Me.SpecialRulesTab.Controls.Add(Me.MaxPlayerCardsLabelSRTab)
        Me.SpecialRulesTab.Controls.Add(Me.MaxDealerCardsBoxSRTab)
        Me.SpecialRulesTab.Controls.Add(Me.MaxDealerCardsLabelSRTab)
        Me.SpecialRulesTab.Controls.Add(Me.PDTiesGroupSRTab)
        Me.SpecialRulesTab.Location = New System.Drawing.Point(4, 25)
        Me.SpecialRulesTab.Name = "SpecialRulesTab"
        Me.SpecialRulesTab.Size = New System.Drawing.Size(866, 617)
        Me.SpecialRulesTab.TabIndex = 7
        Me.SpecialRulesTab.Text = "Special Rules"
        '
        'P21AutowinCheckBoxSRTab
        '
        Me.P21AutowinCheckBoxSRTab.Location = New System.Drawing.Point(24, 24)
        Me.P21AutowinCheckBoxSRTab.Name = "P21AutowinCheckBoxSRTab"
        Me.P21AutowinCheckBoxSRTab.Size = New System.Drawing.Size(192, 40)
        Me.P21AutowinCheckBoxSRTab.TabIndex = 57
        Me.P21AutowinCheckBoxSRTab.Text = "Player 21 Always Wins Including BJ/Post-Double"
        '
        'MaxPlayerCardsBoxSRTab
        '
        Me.MaxPlayerCardsBoxSRTab.BackColor = System.Drawing.SystemColors.Window
        Me.MaxPlayerCardsBoxSRTab.ForeColor = System.Drawing.SystemColors.WindowText
        Me.MaxPlayerCardsBoxSRTab.Location = New System.Drawing.Point(136, 120)
        Me.MaxPlayerCardsBoxSRTab.Name = "MaxPlayerCardsBoxSRTab"
        Me.MaxPlayerCardsBoxSRTab.Size = New System.Drawing.Size(38, 22)
        Me.MaxPlayerCardsBoxSRTab.TabIndex = 53
        Me.MaxPlayerCardsBoxSRTab.Text = ""
        Me.MaxPlayerCardsBoxSRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.MaxPlayerCardsBoxSRTab.Visible = False
        '
        'MaxPlayerCardsLabelSRTab
        '
        Me.MaxPlayerCardsLabelSRTab.Location = New System.Drawing.Point(16, 120)
        Me.MaxPlayerCardsLabelSRTab.Name = "MaxPlayerCardsLabelSRTab"
        Me.MaxPlayerCardsLabelSRTab.Size = New System.Drawing.Size(115, 27)
        Me.MaxPlayerCardsLabelSRTab.TabIndex = 54
        Me.MaxPlayerCardsLabelSRTab.Text = "Max Player Cards"
        Me.MaxPlayerCardsLabelSRTab.Visible = False
        '
        'MaxDealerCardsBoxSRTab
        '
        Me.MaxDealerCardsBoxSRTab.BackColor = System.Drawing.SystemColors.Window
        Me.MaxDealerCardsBoxSRTab.ForeColor = System.Drawing.SystemColors.WindowText
        Me.MaxDealerCardsBoxSRTab.Location = New System.Drawing.Point(136, 88)
        Me.MaxDealerCardsBoxSRTab.Name = "MaxDealerCardsBoxSRTab"
        Me.MaxDealerCardsBoxSRTab.Size = New System.Drawing.Size(38, 22)
        Me.MaxDealerCardsBoxSRTab.TabIndex = 0
        Me.MaxDealerCardsBoxSRTab.Text = ""
        Me.MaxDealerCardsBoxSRTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.MaxDealerCardsBoxSRTab.Visible = False
        '
        'MaxDealerCardsLabelSRTab
        '
        Me.MaxDealerCardsLabelSRTab.Location = New System.Drawing.Point(16, 88)
        Me.MaxDealerCardsLabelSRTab.Name = "MaxDealerCardsLabelSRTab"
        Me.MaxDealerCardsLabelSRTab.Size = New System.Drawing.Size(115, 26)
        Me.MaxDealerCardsLabelSRTab.TabIndex = 52
        Me.MaxDealerCardsLabelSRTab.Text = "Max Dealer Cards"
        Me.MaxDealerCardsLabelSRTab.Visible = False
        '
        'PDTiesGroupSRTab
        '
        Me.PDTiesGroupSRTab.Controls.Add(Me.ToggleAllLabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.PDTiesToggleAllBoxSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.PDTiesLabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.PFPayoffLabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.BJLabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.T21LabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.T20LabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.T19LabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.T18LabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.T17LabelSRTab)
        Me.PDTiesGroupSRTab.Controls.Add(Me.HandTotalLabelSRTab)
        Me.PDTiesGroupSRTab.Location = New System.Drawing.Point(250, 18)
        Me.PDTiesGroupSRTab.Name = "PDTiesGroupSRTab"
        Me.PDTiesGroupSRTab.Size = New System.Drawing.Size(326, 278)
        Me.PDTiesGroupSRTab.TabIndex = 1
        Me.PDTiesGroupSRTab.TabStop = False
        Me.PDTiesGroupSRTab.Text = "Player-Dealer Payoffs"
        '
        'ToggleAllLabelSRTab
        '
        Me.ToggleAllLabelSRTab.Location = New System.Drawing.Point(29, 37)
        Me.ToggleAllLabelSRTab.Name = "ToggleAllLabelSRTab"
        Me.ToggleAllLabelSRTab.Size = New System.Drawing.Size(77, 18)
        Me.ToggleAllLabelSRTab.TabIndex = 92
        Me.ToggleAllLabelSRTab.Text = "Toggle All"
        '
        'PDTiesToggleAllBoxSRTab
        '
        Me.PDTiesToggleAllBoxSRTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PDTiesToggleAllBoxSRTab.Items.AddRange(New Object() {"Push", "Dealer Wins", "Player Wins", "Other"})
        Me.PDTiesToggleAllBoxSRTab.Location = New System.Drawing.Point(154, 37)
        Me.PDTiesToggleAllBoxSRTab.Name = "PDTiesToggleAllBoxSRTab"
        Me.PDTiesToggleAllBoxSRTab.Size = New System.Drawing.Size(124, 24)
        Me.PDTiesToggleAllBoxSRTab.TabIndex = 91
        '
        'PDTiesLabelSRTab
        '
        Me.PDTiesLabelSRTab.Location = New System.Drawing.Point(115, 80)
        Me.PDTiesLabelSRTab.Name = "PDTiesLabelSRTab"
        Me.PDTiesLabelSRTab.Size = New System.Drawing.Size(96, 28)
        Me.PDTiesLabelSRTab.TabIndex = 90
        Me.PDTiesLabelSRTab.Text = "Player-Dealer Ties"
        Me.PDTiesLabelSRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PFPayoffLabelSRTab
        '
        Me.PFPayoffLabelSRTab.Location = New System.Drawing.Point(232, 80)
        Me.PFPayoffLabelSRTab.Name = "PFPayoffLabelSRTab"
        Me.PFPayoffLabelSRTab.Size = New System.Drawing.Size(77, 32)
        Me.PFPayoffLabelSRTab.TabIndex = 69
        Me.PFPayoffLabelSRTab.Text = "Payoff/Loss to 1"
        Me.PFPayoffLabelSRTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BJLabelSRTab
        '
        Me.BJLabelSRTab.Location = New System.Drawing.Point(40, 240)
        Me.BJLabelSRTab.Name = "BJLabelSRTab"
        Me.BJLabelSRTab.Size = New System.Drawing.Size(24, 16)
        Me.BJLabelSRTab.TabIndex = 62
        Me.BJLabelSRTab.Text = "BJ"
        '
        'T21LabelSRTab
        '
        Me.T21LabelSRTab.Location = New System.Drawing.Point(40, 216)
        Me.T21LabelSRTab.Name = "T21LabelSRTab"
        Me.T21LabelSRTab.Size = New System.Drawing.Size(24, 16)
        Me.T21LabelSRTab.TabIndex = 61
        Me.T21LabelSRTab.Text = "21"
        '
        'T20LabelSRTab
        '
        Me.T20LabelSRTab.Location = New System.Drawing.Point(40, 192)
        Me.T20LabelSRTab.Name = "T20LabelSRTab"
        Me.T20LabelSRTab.Size = New System.Drawing.Size(24, 16)
        Me.T20LabelSRTab.TabIndex = 60
        Me.T20LabelSRTab.Text = "20"
        '
        'T19LabelSRTab
        '
        Me.T19LabelSRTab.Location = New System.Drawing.Point(40, 168)
        Me.T19LabelSRTab.Name = "T19LabelSRTab"
        Me.T19LabelSRTab.Size = New System.Drawing.Size(24, 16)
        Me.T19LabelSRTab.TabIndex = 59
        Me.T19LabelSRTab.Text = "19"
        '
        'T18LabelSRTab
        '
        Me.T18LabelSRTab.Location = New System.Drawing.Point(40, 144)
        Me.T18LabelSRTab.Name = "T18LabelSRTab"
        Me.T18LabelSRTab.Size = New System.Drawing.Size(24, 16)
        Me.T18LabelSRTab.TabIndex = 58
        Me.T18LabelSRTab.Text = "18"
        '
        'T17LabelSRTab
        '
        Me.T17LabelSRTab.Location = New System.Drawing.Point(40, 120)
        Me.T17LabelSRTab.Name = "T17LabelSRTab"
        Me.T17LabelSRTab.Size = New System.Drawing.Size(24, 16)
        Me.T17LabelSRTab.TabIndex = 57
        Me.T17LabelSRTab.Text = "17"
        '
        'HandTotalLabelSRTab
        '
        Me.HandTotalLabelSRTab.Location = New System.Drawing.Point(19, 78)
        Me.HandTotalLabelSRTab.Name = "HandTotalLabelSRTab"
        Me.HandTotalLabelSRTab.Size = New System.Drawing.Size(77, 19)
        Me.HandTotalLabelSRTab.TabIndex = 50
        Me.HandTotalLabelSRTab.Text = "Hand Total"
        '
        'ShoeOptionsTab
        '
        Me.ShoeOptionsTab.Controls.Add(Me.LoadForcedShoeFileButtonShTab)
        Me.ShoeOptionsTab.Controls.Add(Me.SaveForcedShoeFileButtonShTab)
        Me.ShoeOptionsTab.Controls.Add(Me.ForcedShoeGroupShTab)
        Me.ShoeOptionsTab.Controls.Add(Me.ReferenceShoeGroupShTab)
        Me.ShoeOptionsTab.Controls.Add(Me.ShoeOptionGroupShTab)
        Me.ShoeOptionsTab.ImeMode = System.Windows.Forms.ImeMode.On
        Me.ShoeOptionsTab.Location = New System.Drawing.Point(4, 25)
        Me.ShoeOptionsTab.Name = "ShoeOptionsTab"
        Me.ShoeOptionsTab.Size = New System.Drawing.Size(866, 617)
        Me.ShoeOptionsTab.TabIndex = 3
        Me.ShoeOptionsTab.Text = "Shoe"
        '
        'LoadForcedShoeFileButtonShTab
        '
        Me.LoadForcedShoeFileButtonShTab.Location = New System.Drawing.Point(442, 563)
        Me.LoadForcedShoeFileButtonShTab.Name = "LoadForcedShoeFileButtonShTab"
        Me.LoadForcedShoeFileButtonShTab.Size = New System.Drawing.Size(134, 28)
        Me.LoadForcedShoeFileButtonShTab.TabIndex = 4
        Me.LoadForcedShoeFileButtonShTab.Text = "Load Forced Shoe"
        '
        'SaveForcedShoeFileButtonShTab
        '
        Me.SaveForcedShoeFileButtonShTab.Location = New System.Drawing.Point(288, 563)
        Me.SaveForcedShoeFileButtonShTab.Name = "SaveForcedShoeFileButtonShTab"
        Me.SaveForcedShoeFileButtonShTab.Size = New System.Drawing.Size(134, 28)
        Me.SaveForcedShoeFileButtonShTab.TabIndex = 3
        Me.SaveForcedShoeFileButtonShTab.Text = "Save Forced  Shoe"
        '
        'ForcedShoeGroupShTab
        '
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label2)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label3)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label4)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label5)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label6)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label7)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label8)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label9)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label10)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.Label11)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.RestoreSuitsButtonShTab)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.NetSuitLabelShTab)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.SpadesLabelShTab)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.ClubsLabelShTab)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.HeartsLabelShTab)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.DiamondsLabelShTab)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.NetForcedCardsLabelShTab)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.NetForcedCardsBoxShTab)
        Me.ForcedShoeGroupShTab.Controls.Add(Me.CopyRefShoeButtonShTab)
        Me.ForcedShoeGroupShTab.Location = New System.Drawing.Point(29, 295)
        Me.ForcedShoeGroupShTab.Name = "ForcedShoeGroupShTab"
        Me.ForcedShoeGroupShTab.Size = New System.Drawing.Size(806, 250)
        Me.ForcedShoeGroupShTab.TabIndex = 2
        Me.ForcedShoeGroupShTab.TabStop = False
        Me.ForcedShoeGroupShTab.Text = "Forced Shoe"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(272, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 114
        Me.Label2.Text = "3"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(664, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 16)
        Me.Label3.TabIndex = 113
        Me.Label3.Text = "Ten"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(328, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 16)
        Me.Label4.TabIndex = 112
        Me.Label4.Text = "4"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(384, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 16)
        Me.Label5.TabIndex = 111
        Me.Label5.Text = "5"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(216, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 16)
        Me.Label6.TabIndex = 110
        Me.Label6.Text = "2"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(440, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 16)
        Me.Label7.TabIndex = 109
        Me.Label7.Text = "6"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(496, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 16)
        Me.Label8.TabIndex = 108
        Me.Label8.Text = "7"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(552, 32)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 16)
        Me.Label9.TabIndex = 107
        Me.Label9.Text = "8"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(608, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 16)
        Me.Label10.TabIndex = 106
        Me.Label10.Text = "9"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(160, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(32, 16)
        Me.Label11.TabIndex = 105
        Me.Label11.Text = "Ace"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RestoreSuitsButtonShTab
        '
        Me.RestoreSuitsButtonShTab.Location = New System.Drawing.Point(720, 166)
        Me.RestoreSuitsButtonShTab.Name = "RestoreSuitsButtonShTab"
        Me.RestoreSuitsButtonShTab.Size = New System.Drawing.Size(68, 61)
        Me.RestoreSuitsButtonShTab.TabIndex = 99
        Me.RestoreSuitsButtonShTab.Text = "Restore Suit Defaults"
        '
        'NetSuitLabelShTab
        '
        Me.NetSuitLabelShTab.Location = New System.Drawing.Point(56, 104)
        Me.NetSuitLabelShTab.Name = "NetSuitLabelShTab"
        Me.NetSuitLabelShTab.Size = New System.Drawing.Size(72, 16)
        Me.NetSuitLabelShTab.TabIndex = 104
        Me.NetSuitLabelShTab.Text = "Net"
        '
        'SpadesLabelShTab
        '
        Me.SpadesLabelShTab.Location = New System.Drawing.Point(56, 128)
        Me.SpadesLabelShTab.Name = "SpadesLabelShTab"
        Me.SpadesLabelShTab.Size = New System.Drawing.Size(72, 16)
        Me.SpadesLabelShTab.TabIndex = 103
        Me.SpadesLabelShTab.Text = "Spades"
        '
        'ClubsLabelShTab
        '
        Me.ClubsLabelShTab.Location = New System.Drawing.Point(56, 200)
        Me.ClubsLabelShTab.Name = "ClubsLabelShTab"
        Me.ClubsLabelShTab.Size = New System.Drawing.Size(72, 16)
        Me.ClubsLabelShTab.TabIndex = 102
        Me.ClubsLabelShTab.Text = "Clubs"
        '
        'HeartsLabelShTab
        '
        Me.HeartsLabelShTab.Location = New System.Drawing.Point(56, 152)
        Me.HeartsLabelShTab.Name = "HeartsLabelShTab"
        Me.HeartsLabelShTab.Size = New System.Drawing.Size(72, 16)
        Me.HeartsLabelShTab.TabIndex = 101
        Me.HeartsLabelShTab.Text = "Hearts"
        '
        'DiamondsLabelShTab
        '
        Me.DiamondsLabelShTab.Location = New System.Drawing.Point(56, 176)
        Me.DiamondsLabelShTab.Name = "DiamondsLabelShTab"
        Me.DiamondsLabelShTab.Size = New System.Drawing.Size(72, 16)
        Me.DiamondsLabelShTab.TabIndex = 100
        Me.DiamondsLabelShTab.Text = "Diamonds"
        '
        'NetForcedCardsLabelShTab
        '
        Me.NetForcedCardsLabelShTab.Location = New System.Drawing.Point(739, 18)
        Me.NetForcedCardsLabelShTab.Name = "NetForcedCardsLabelShTab"
        Me.NetForcedCardsLabelShTab.Size = New System.Drawing.Size(48, 28)
        Me.NetForcedCardsLabelShTab.TabIndex = 44
        Me.NetForcedCardsLabelShTab.Text = "Net Cards"
        Me.NetForcedCardsLabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NetForcedCardsBoxShTab
        '
        Me.NetForcedCardsBoxShTab.Enabled = False
        Me.NetForcedCardsBoxShTab.ImeMode = System.Windows.Forms.ImeMode.On
        Me.NetForcedCardsBoxShTab.Location = New System.Drawing.Point(739, 55)
        Me.NetForcedCardsBoxShTab.Name = "NetForcedCardsBoxShTab"
        Me.NetForcedCardsBoxShTab.Size = New System.Drawing.Size(48, 22)
        Me.NetForcedCardsBoxShTab.TabIndex = 99
        Me.NetForcedCardsBoxShTab.TabStop = False
        Me.NetForcedCardsBoxShTab.Text = ""
        Me.NetForcedCardsBoxShTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CopyRefShoeButtonShTab
        '
        Me.CopyRefShoeButtonShTab.Location = New System.Drawing.Point(19, 28)
        Me.CopyRefShoeButtonShTab.Name = "CopyRefShoeButtonShTab"
        Me.CopyRefShoeButtonShTab.Size = New System.Drawing.Size(106, 55)
        Me.CopyRefShoeButtonShTab.TabIndex = 0
        Me.CopyRefShoeButtonShTab.Text = "Copy Reference Shoe"
        '
        'ReferenceShoeGroupShTab
        '
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.SpanishDecksRefCheckBoxShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.NetRefCardsLabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.NetRefCardsBoxShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks3LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks10LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks4LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks5LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks2LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks6LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks7LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks8LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecks9LabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecksBoxShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecksALabelShTab)
        Me.ReferenceShoeGroupShTab.Controls.Add(Me.RefDecksLabelShTab)
        Me.ReferenceShoeGroupShTab.Location = New System.Drawing.Point(29, 166)
        Me.ReferenceShoeGroupShTab.Name = "ReferenceShoeGroupShTab"
        Me.ReferenceShoeGroupShTab.Size = New System.Drawing.Size(806, 102)
        Me.ReferenceShoeGroupShTab.TabIndex = 1
        Me.ReferenceShoeGroupShTab.TabStop = False
        Me.ReferenceShoeGroupShTab.Text = "Reference Shoe"
        '
        'SpanishDecksRefCheckBoxShTab
        '
        Me.SpanishDecksRefCheckBoxShTab.Location = New System.Drawing.Point(16, 72)
        Me.SpanishDecksRefCheckBoxShTab.Name = "SpanishDecksRefCheckBoxShTab"
        Me.SpanishDecksRefCheckBoxShTab.Size = New System.Drawing.Size(120, 24)
        Me.SpanishDecksRefCheckBoxShTab.TabIndex = 100
        Me.SpanishDecksRefCheckBoxShTab.Text = "Spanish Decks"
        '
        'NetRefCardsLabelShTab
        '
        Me.NetRefCardsLabelShTab.Location = New System.Drawing.Point(739, 28)
        Me.NetRefCardsLabelShTab.Name = "NetRefCardsLabelShTab"
        Me.NetRefCardsLabelShTab.Size = New System.Drawing.Size(48, 27)
        Me.NetRefCardsLabelShTab.TabIndex = 42
        Me.NetRefCardsLabelShTab.Text = "Net Cards"
        Me.NetRefCardsLabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NetRefCardsBoxShTab
        '
        Me.NetRefCardsBoxShTab.Enabled = False
        Me.NetRefCardsBoxShTab.ImeMode = System.Windows.Forms.ImeMode.On
        Me.NetRefCardsBoxShTab.Location = New System.Drawing.Point(739, 65)
        Me.NetRefCardsBoxShTab.Name = "NetRefCardsBoxShTab"
        Me.NetRefCardsBoxShTab.Size = New System.Drawing.Size(48, 22)
        Me.NetRefCardsBoxShTab.TabIndex = 99
        Me.NetRefCardsBoxShTab.TabStop = False
        Me.NetRefCardsBoxShTab.Text = ""
        Me.NetRefCardsBoxShTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RefDecks3LabelShTab
        '
        Me.RefDecks3LabelShTab.Location = New System.Drawing.Point(272, 40)
        Me.RefDecks3LabelShTab.Name = "RefDecks3LabelShTab"
        Me.RefDecks3LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks3LabelShTab.TabIndex = 40
        Me.RefDecks3LabelShTab.Text = "3"
        Me.RefDecks3LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecks10LabelShTab
        '
        Me.RefDecks10LabelShTab.Location = New System.Drawing.Point(664, 40)
        Me.RefDecks10LabelShTab.Name = "RefDecks10LabelShTab"
        Me.RefDecks10LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks10LabelShTab.TabIndex = 38
        Me.RefDecks10LabelShTab.Text = "Ten"
        Me.RefDecks10LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecks4LabelShTab
        '
        Me.RefDecks4LabelShTab.Location = New System.Drawing.Point(328, 40)
        Me.RefDecks4LabelShTab.Name = "RefDecks4LabelShTab"
        Me.RefDecks4LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks4LabelShTab.TabIndex = 36
        Me.RefDecks4LabelShTab.Text = "4"
        Me.RefDecks4LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecks5LabelShTab
        '
        Me.RefDecks5LabelShTab.Location = New System.Drawing.Point(384, 40)
        Me.RefDecks5LabelShTab.Name = "RefDecks5LabelShTab"
        Me.RefDecks5LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks5LabelShTab.TabIndex = 34
        Me.RefDecks5LabelShTab.Text = "5"
        Me.RefDecks5LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecks2LabelShTab
        '
        Me.RefDecks2LabelShTab.Location = New System.Drawing.Point(216, 40)
        Me.RefDecks2LabelShTab.Name = "RefDecks2LabelShTab"
        Me.RefDecks2LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks2LabelShTab.TabIndex = 32
        Me.RefDecks2LabelShTab.Text = "2"
        Me.RefDecks2LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecks6LabelShTab
        '
        Me.RefDecks6LabelShTab.Location = New System.Drawing.Point(440, 40)
        Me.RefDecks6LabelShTab.Name = "RefDecks6LabelShTab"
        Me.RefDecks6LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks6LabelShTab.TabIndex = 30
        Me.RefDecks6LabelShTab.Text = "6"
        Me.RefDecks6LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecks7LabelShTab
        '
        Me.RefDecks7LabelShTab.Location = New System.Drawing.Point(496, 40)
        Me.RefDecks7LabelShTab.Name = "RefDecks7LabelShTab"
        Me.RefDecks7LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks7LabelShTab.TabIndex = 28
        Me.RefDecks7LabelShTab.Text = "7"
        Me.RefDecks7LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecks8LabelShTab
        '
        Me.RefDecks8LabelShTab.Location = New System.Drawing.Point(552, 40)
        Me.RefDecks8LabelShTab.Name = "RefDecks8LabelShTab"
        Me.RefDecks8LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks8LabelShTab.TabIndex = 26
        Me.RefDecks8LabelShTab.Text = "8"
        Me.RefDecks8LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecks9LabelShTab
        '
        Me.RefDecks9LabelShTab.Location = New System.Drawing.Point(608, 40)
        Me.RefDecks9LabelShTab.Name = "RefDecks9LabelShTab"
        Me.RefDecks9LabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecks9LabelShTab.TabIndex = 24
        Me.RefDecks9LabelShTab.Text = "9"
        Me.RefDecks9LabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecksBoxShTab
        '
        Me.RefDecksBoxShTab.ImeMode = System.Windows.Forms.ImeMode.On
        Me.RefDecksBoxShTab.Location = New System.Drawing.Point(48, 40)
        Me.RefDecksBoxShTab.Name = "RefDecksBoxShTab"
        Me.RefDecksBoxShTab.Size = New System.Drawing.Size(48, 22)
        Me.RefDecksBoxShTab.TabIndex = 0
        Me.RefDecksBoxShTab.Text = ""
        Me.RefDecksBoxShTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RefDecksALabelShTab
        '
        Me.RefDecksALabelShTab.Location = New System.Drawing.Point(160, 40)
        Me.RefDecksALabelShTab.Name = "RefDecksALabelShTab"
        Me.RefDecksALabelShTab.Size = New System.Drawing.Size(32, 16)
        Me.RefDecksALabelShTab.TabIndex = 3
        Me.RefDecksALabelShTab.Text = "Ace"
        Me.RefDecksALabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RefDecksLabelShTab
        '
        Me.RefDecksLabelShTab.Location = New System.Drawing.Point(48, 16)
        Me.RefDecksLabelShTab.Name = "RefDecksLabelShTab"
        Me.RefDecksLabelShTab.Size = New System.Drawing.Size(48, 24)
        Me.RefDecksLabelShTab.TabIndex = 1
        Me.RefDecksLabelShTab.Text = "Decks"
        Me.RefDecksLabelShTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ShoeOptionGroupShTab
        '
        Me.ShoeOptionGroupShTab.Controls.Add(Me.SpanishDecksCheckBoxShTab)
        Me.ShoeOptionGroupShTab.Controls.Add(Me.NDecksLabelShTab)
        Me.ShoeOptionGroupShTab.Controls.Add(Me.NDecksBoxShTab)
        Me.ShoeOptionGroupShTab.Controls.Add(Me.ForcedShoeCheckShTab)
        Me.ShoeOptionGroupShTab.Controls.Add(Me.InfiniteDecksButtonShTab)
        Me.ShoeOptionGroupShTab.Controls.Add(Me.FiniteDecksButtonShTab)
        Me.ShoeOptionGroupShTab.Location = New System.Drawing.Point(240, 28)
        Me.ShoeOptionGroupShTab.Name = "ShoeOptionGroupShTab"
        Me.ShoeOptionGroupShTab.Size = New System.Drawing.Size(355, 120)
        Me.ShoeOptionGroupShTab.TabIndex = 0
        Me.ShoeOptionGroupShTab.TabStop = False
        Me.ShoeOptionGroupShTab.Text = "Shoe Options"
        '
        'SpanishDecksCheckBoxShTab
        '
        Me.SpanishDecksCheckBoxShTab.Location = New System.Drawing.Point(232, 48)
        Me.SpanishDecksCheckBoxShTab.Name = "SpanishDecksCheckBoxShTab"
        Me.SpanishDecksCheckBoxShTab.Size = New System.Drawing.Size(120, 24)
        Me.SpanishDecksCheckBoxShTab.TabIndex = 6
        Me.SpanishDecksCheckBoxShTab.Text = "Spanish Decks"
        '
        'NDecksLabelShTab
        '
        Me.NDecksLabelShTab.Location = New System.Drawing.Point(16, 80)
        Me.NDecksLabelShTab.Name = "NDecksLabelShTab"
        Me.NDecksLabelShTab.Size = New System.Drawing.Size(153, 28)
        Me.NDecksLabelShTab.TabIndex = 5
        Me.NDecksLabelShTab.Text = "Number of Decks"
        '
        'NDecksBoxShTab
        '
        Me.NDecksBoxShTab.Location = New System.Drawing.Point(232, 80)
        Me.NDecksBoxShTab.Name = "NDecksBoxShTab"
        Me.NDecksBoxShTab.Size = New System.Drawing.Size(77, 22)
        Me.NDecksBoxShTab.TabIndex = 3
        Me.NDecksBoxShTab.Text = ""
        Me.NDecksBoxShTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ForcedShoeCheckShTab
        '
        Me.ForcedShoeCheckShTab.Location = New System.Drawing.Point(16, 48)
        Me.ForcedShoeCheckShTab.Name = "ForcedShoeCheckShTab"
        Me.ForcedShoeCheckShTab.Size = New System.Drawing.Size(163, 19)
        Me.ForcedShoeCheckShTab.TabIndex = 2
        Me.ForcedShoeCheckShTab.Text = "Used Forced Shoe"
        '
        'InfiniteDecksButtonShTab
        '
        Me.InfiniteDecksButtonShTab.Location = New System.Drawing.Point(232, 24)
        Me.InfiniteDecksButtonShTab.Name = "InfiniteDecksButtonShTab"
        Me.InfiniteDecksButtonShTab.Size = New System.Drawing.Size(116, 24)
        Me.InfiniteDecksButtonShTab.TabIndex = 1
        Me.InfiniteDecksButtonShTab.TabStop = True
        Me.InfiniteDecksButtonShTab.Text = "Infinite Decks"
        '
        'FiniteDecksButtonShTab
        '
        Me.FiniteDecksButtonShTab.Location = New System.Drawing.Point(16, 24)
        Me.FiniteDecksButtonShTab.Name = "FiniteDecksButtonShTab"
        Me.FiniteDecksButtonShTab.Size = New System.Drawing.Size(124, 22)
        Me.FiniteDecksButtonShTab.TabIndex = 0
        Me.FiniteDecksButtonShTab.TabStop = True
        Me.FiniteDecksButtonShTab.Text = "Finite Decks"
        '
        'BonusRulesTab
        '
        Me.BonusRulesTab.Controls.Add(Me.BonusTabControlBTab)
        Me.BonusRulesTab.Location = New System.Drawing.Point(4, 25)
        Me.BonusRulesTab.Name = "BonusRulesTab"
        Me.BonusRulesTab.Size = New System.Drawing.Size(866, 617)
        Me.BonusRulesTab.TabIndex = 5
        Me.BonusRulesTab.Text = "Bonus Rules"
        '
        'BonusTabControlBTab
        '
        Me.BonusTabControlBTab.Controls.Add(Me.BJBonusesTabBTab)
        Me.BonusTabControlBTab.Controls.Add(Me.BonusRulesTabBTab)
        Me.BonusTabControlBTab.Location = New System.Drawing.Point(10, 9)
        Me.BonusTabControlBTab.Name = "BonusTabControlBTab"
        Me.BonusTabControlBTab.SelectedIndex = 0
        Me.BonusTabControlBTab.Size = New System.Drawing.Size(844, 600)
        Me.BonusTabControlBTab.TabIndex = 13
        '
        'BJBonusesTabBTab
        '
        Me.BJBonusesTabBTab.Controls.Add(Me.Note2LabelBJTab)
        Me.BJBonusesTabBTab.Controls.Add(Me.NoteLabelBJTab)
        Me.BJBonusesTabBTab.Controls.Add(Me.Spec10GroupBJTab)
        Me.BJBonusesTabBTab.Controls.Add(Me.GeneralBJGroupBJTab)
        Me.BJBonusesTabBTab.Controls.Add(Me.BJSPlitTensCheckBJTab)
        Me.BJBonusesTabBTab.Controls.Add(Me.BJSPlitAcesCheckBJTab)
        Me.BJBonusesTabBTab.Location = New System.Drawing.Point(4, 25)
        Me.BJBonusesTabBTab.Name = "BJBonusesTabBTab"
        Me.BJBonusesTabBTab.Size = New System.Drawing.Size(836, 571)
        Me.BJBonusesTabBTab.TabIndex = 0
        Me.BJBonusesTabBTab.Text = "BJ Bonuses"
        '
        'Note2LabelBJTab
        '
        Me.Note2LabelBJTab.Location = New System.Drawing.Point(19, 388)
        Me.Note2LabelBJTab.Name = "Note2LabelBJTab"
        Me.Note2LabelBJTab.Size = New System.Drawing.Size(327, 37)
        Me.Note2LabelBJTab.TabIndex = 160
        Me.Note2LabelBJTab.Text = "*Note:  Suited BJ Must Win applies to both General and Suited Specific Ten Bonuse" & _
        "s"
        Me.Note2LabelBJTab.Visible = False
        '
        'NoteLabelBJTab
        '
        Me.NoteLabelBJTab.Location = New System.Drawing.Point(19, 360)
        Me.NoteLabelBJTab.Name = "NoteLabelBJTab"
        Me.NoteLabelBJTab.Size = New System.Drawing.Size(327, 18)
        Me.NoteLabelBJTab.TabIndex = 159
        Me.NoteLabelBJTab.Text = "*Note:  Suited BJ bonuses are not applied post-split."
        '
        'Spec10GroupBJTab
        '
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10BJMustWinCheckBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Payoff2LabelBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10LabelBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10BoxlBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10FractionLabelBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10FractionBoxBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10SpecSuitBoxBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10SuitLabelBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10HeartsButtonBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10DiamondsButtonBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10ClubsButtonBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10SpadesButtonBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10SuitedLabelBJTab)
        Me.Spec10GroupBJTab.Controls.Add(Me.Spec10SuitedBoxBJTab)
        Me.Spec10GroupBJTab.Location = New System.Drawing.Point(269, 102)
        Me.Spec10GroupBJTab.Name = "Spec10GroupBJTab"
        Me.Spec10GroupBJTab.Size = New System.Drawing.Size(547, 249)
        Me.Spec10GroupBJTab.TabIndex = 158
        Me.Spec10GroupBJTab.TabStop = False
        Me.Spec10GroupBJTab.Text = "Specific Ten BJ Bonuses"
        Me.Spec10GroupBJTab.Visible = False
        '
        'Spec10BJMustWinCheckBJTab
        '
        Me.Spec10BJMustWinCheckBJTab.Location = New System.Drawing.Point(19, 157)
        Me.Spec10BJMustWinCheckBJTab.Name = "Spec10BJMustWinCheckBJTab"
        Me.Spec10BJMustWinCheckBJTab.Size = New System.Drawing.Size(259, 18)
        Me.Spec10BJMustWinCheckBJTab.TabIndex = 158
        Me.Spec10BJMustWinCheckBJTab.Text = "Non-Suited Specific Ten BJ Must Win"
        '
        'Payoff2LabelBJTab
        '
        Me.Payoff2LabelBJTab.Location = New System.Drawing.Point(221, 28)
        Me.Payoff2LabelBJTab.Name = "Payoff2LabelBJTab"
        Me.Payoff2LabelBJTab.Size = New System.Drawing.Size(48, 37)
        Me.Payoff2LabelBJTab.TabIndex = 156
        Me.Payoff2LabelBJTab.Text = "Payoff (to 1)"
        Me.Payoff2LabelBJTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Spec10LabelBJTab
        '
        Me.Spec10LabelBJTab.Location = New System.Drawing.Point(19, 74)
        Me.Spec10LabelBJTab.Name = "Spec10LabelBJTab"
        Me.Spec10LabelBJTab.Size = New System.Drawing.Size(183, 18)
        Me.Spec10LabelBJTab.TabIndex = 155
        Me.Spec10LabelBJTab.Text = "Specific Ten BJ Bonus"
        '
        'Spec10BoxlBJTab
        '
        Me.Spec10BoxlBJTab.Location = New System.Drawing.Point(221, 74)
        Me.Spec10BoxlBJTab.Name = "Spec10BoxlBJTab"
        Me.Spec10BoxlBJTab.Size = New System.Drawing.Size(38, 22)
        Me.Spec10BoxlBJTab.TabIndex = 9
        Me.Spec10BoxlBJTab.Text = ""
        Me.Spec10BoxlBJTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Spec10FractionLabelBJTab
        '
        Me.Spec10FractionLabelBJTab.Location = New System.Drawing.Point(288, 74)
        Me.Spec10FractionLabelBJTab.Name = "Spec10FractionLabelBJTab"
        Me.Spec10FractionLabelBJTab.Size = New System.Drawing.Size(202, 18)
        Me.Spec10FractionLabelBJTab.TabIndex = 153
        Me.Spec10FractionLabelBJTab.Text = "Specific Ten Fraction of All Tens"
        '
        'Spec10FractionBoxBJTab
        '
        Me.Spec10FractionBoxBJTab.Location = New System.Drawing.Point(490, 74)
        Me.Spec10FractionBoxBJTab.Name = "Spec10FractionBoxBJTab"
        Me.Spec10FractionBoxBJTab.Size = New System.Drawing.Size(38, 22)
        Me.Spec10FractionBoxBJTab.TabIndex = 10
        Me.Spec10FractionBoxBJTab.Text = ""
        Me.Spec10FractionBoxBJTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Spec10SpecSuitBoxBJTab
        '
        Me.Spec10SpecSuitBoxBJTab.Location = New System.Drawing.Point(221, 129)
        Me.Spec10SpecSuitBoxBJTab.Name = "Spec10SpecSuitBoxBJTab"
        Me.Spec10SpecSuitBoxBJTab.Size = New System.Drawing.Size(38, 22)
        Me.Spec10SpecSuitBoxBJTab.TabIndex = 12
        Me.Spec10SpecSuitBoxBJTab.Text = ""
        Me.Spec10SpecSuitBoxBJTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Spec10SuitLabelBJTab
        '
        Me.Spec10SuitLabelBJTab.Location = New System.Drawing.Point(19, 129)
        Me.Spec10SuitLabelBJTab.Name = "Spec10SuitLabelBJTab"
        Me.Spec10SuitLabelBJTab.Size = New System.Drawing.Size(192, 19)
        Me.Spec10SuitLabelBJTab.TabIndex = 151
        Me.Spec10SuitLabelBJTab.Text = "Specific Ten Diamonds Bonus"
        '
        'Spec10HeartsButtonBJTab
        '
        Me.Spec10HeartsButtonBJTab.Location = New System.Drawing.Point(202, 185)
        Me.Spec10HeartsButtonBJTab.Name = "Spec10HeartsButtonBJTab"
        Me.Spec10HeartsButtonBJTab.Size = New System.Drawing.Size(67, 27)
        Me.Spec10HeartsButtonBJTab.TabIndex = 14
        Me.Spec10HeartsButtonBJTab.Text = "Hearts"
        '
        'Spec10DiamondsButtonBJTab
        '
        Me.Spec10DiamondsButtonBJTab.Location = New System.Drawing.Point(96, 212)
        Me.Spec10DiamondsButtonBJTab.Name = "Spec10DiamondsButtonBJTab"
        Me.Spec10DiamondsButtonBJTab.Size = New System.Drawing.Size(96, 19)
        Me.Spec10DiamondsButtonBJTab.TabIndex = 15
        Me.Spec10DiamondsButtonBJTab.Text = "Diamonds"
        '
        'Spec10ClubsButtonBJTab
        '
        Me.Spec10ClubsButtonBJTab.Location = New System.Drawing.Point(202, 212)
        Me.Spec10ClubsButtonBJTab.Name = "Spec10ClubsButtonBJTab"
        Me.Spec10ClubsButtonBJTab.Size = New System.Drawing.Size(67, 19)
        Me.Spec10ClubsButtonBJTab.TabIndex = 16
        Me.Spec10ClubsButtonBJTab.Text = "Clubs"
        '
        'Spec10SpadesButtonBJTab
        '
        Me.Spec10SpadesButtonBJTab.Location = New System.Drawing.Point(96, 185)
        Me.Spec10SpadesButtonBJTab.Name = "Spec10SpadesButtonBJTab"
        Me.Spec10SpadesButtonBJTab.Size = New System.Drawing.Size(96, 27)
        Me.Spec10SpadesButtonBJTab.TabIndex = 13
        Me.Spec10SpadesButtonBJTab.Text = "Spades"
        '
        'Spec10SuitedLabelBJTab
        '
        Me.Spec10SuitedLabelBJTab.Location = New System.Drawing.Point(19, 102)
        Me.Spec10SuitedLabelBJTab.Name = "Spec10SuitedLabelBJTab"
        Me.Spec10SuitedLabelBJTab.Size = New System.Drawing.Size(192, 18)
        Me.Spec10SuitedLabelBJTab.TabIndex = 150
        Me.Spec10SuitedLabelBJTab.Text = "Specific Ten Suited BJ Bonus"
        '
        'Spec10SuitedBoxBJTab
        '
        Me.Spec10SuitedBoxBJTab.Location = New System.Drawing.Point(221, 102)
        Me.Spec10SuitedBoxBJTab.Name = "Spec10SuitedBoxBJTab"
        Me.Spec10SuitedBoxBJTab.Size = New System.Drawing.Size(38, 22)
        Me.Spec10SuitedBoxBJTab.TabIndex = 11
        Me.Spec10SuitedBoxBJTab.Text = ""
        Me.Spec10SuitedBoxBJTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GeneralBJGroupBJTab
        '
        Me.GeneralBJGroupBJTab.Controls.Add(Me.SuitLabelBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.SpecificSuitBJBoxBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.HeartsButtonBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.DiamondsButtonBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.ClubsButtonBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.SpadesButtonBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.SuitedLabelBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.PayoffLabelBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.SuitedBJBoxBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.BJPaysBoxBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.BJPaysLabelBJTab)
        Me.GeneralBJGroupBJTab.Controls.Add(Me.SuitedBJMustWinCheckBJTab)
        Me.GeneralBJGroupBJTab.Location = New System.Drawing.Point(19, 102)
        Me.GeneralBJGroupBJTab.Name = "GeneralBJGroupBJTab"
        Me.GeneralBJGroupBJTab.Size = New System.Drawing.Size(240, 249)
        Me.GeneralBJGroupBJTab.TabIndex = 157
        Me.GeneralBJGroupBJTab.TabStop = False
        Me.GeneralBJGroupBJTab.Text = "General BJ Bonuses"
        '
        'SuitLabelBJTab
        '
        Me.SuitLabelBJTab.Location = New System.Drawing.Point(19, 129)
        Me.SuitLabelBJTab.Name = "SuitLabelBJTab"
        Me.SuitLabelBJTab.Size = New System.Drawing.Size(115, 19)
        Me.SuitLabelBJTab.TabIndex = 134
        Me.SuitLabelBJTab.Text = "Diamonds Bonus"
        '
        'SpecificSuitBJBoxBJTab
        '
        Me.SpecificSuitBJBoxBJTab.Location = New System.Drawing.Point(163, 129)
        Me.SpecificSuitBJBoxBJTab.Name = "SpecificSuitBJBoxBJTab"
        Me.SpecificSuitBJBoxBJTab.Size = New System.Drawing.Size(39, 22)
        Me.SpecificSuitBJBoxBJTab.TabIndex = 4
        Me.SpecificSuitBJBoxBJTab.Text = ""
        Me.SpecificSuitBJBoxBJTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'HeartsButtonBJTab
        '
        Me.HeartsButtonBJTab.Location = New System.Drawing.Point(144, 185)
        Me.HeartsButtonBJTab.Name = "HeartsButtonBJTab"
        Me.HeartsButtonBJTab.Size = New System.Drawing.Size(67, 27)
        Me.HeartsButtonBJTab.TabIndex = 6
        Me.HeartsButtonBJTab.Text = "Hearts"
        '
        'DiamondsButtonBJTab
        '
        Me.DiamondsButtonBJTab.Location = New System.Drawing.Point(38, 212)
        Me.DiamondsButtonBJTab.Name = "DiamondsButtonBJTab"
        Me.DiamondsButtonBJTab.Size = New System.Drawing.Size(96, 19)
        Me.DiamondsButtonBJTab.TabIndex = 7
        Me.DiamondsButtonBJTab.Text = "Diamonds"
        '
        'ClubsButtonBJTab
        '
        Me.ClubsButtonBJTab.Location = New System.Drawing.Point(144, 212)
        Me.ClubsButtonBJTab.Name = "ClubsButtonBJTab"
        Me.ClubsButtonBJTab.Size = New System.Drawing.Size(67, 19)
        Me.ClubsButtonBJTab.TabIndex = 8
        Me.ClubsButtonBJTab.Text = "Clubs"
        '
        'SpadesButtonBJTab
        '
        Me.SpadesButtonBJTab.Location = New System.Drawing.Point(38, 185)
        Me.SpadesButtonBJTab.Name = "SpadesButtonBJTab"
        Me.SpadesButtonBJTab.Size = New System.Drawing.Size(96, 27)
        Me.SpadesButtonBJTab.TabIndex = 5
        Me.SpadesButtonBJTab.Text = "Spades"
        '
        'SuitedLabelBJTab
        '
        Me.SuitedLabelBJTab.Location = New System.Drawing.Point(19, 102)
        Me.SuitedLabelBJTab.Name = "SuitedLabelBJTab"
        Me.SuitedLabelBJTab.Size = New System.Drawing.Size(106, 18)
        Me.SuitedLabelBJTab.TabIndex = 133
        Me.SuitedLabelBJTab.Text = "Suited BJ Bonus"
        '
        'PayoffLabelBJTab
        '
        Me.PayoffLabelBJTab.Location = New System.Drawing.Point(163, 28)
        Me.PayoffLabelBJTab.Name = "PayoffLabelBJTab"
        Me.PayoffLabelBJTab.Size = New System.Drawing.Size(48, 37)
        Me.PayoffLabelBJTab.TabIndex = 132
        Me.PayoffLabelBJTab.Text = "Payoff (to 1)"
        Me.PayoffLabelBJTab.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'SuitedBJBoxBJTab
        '
        Me.SuitedBJBoxBJTab.Location = New System.Drawing.Point(163, 102)
        Me.SuitedBJBoxBJTab.Name = "SuitedBJBoxBJTab"
        Me.SuitedBJBoxBJTab.Size = New System.Drawing.Size(39, 22)
        Me.SuitedBJBoxBJTab.TabIndex = 3
        Me.SuitedBJBoxBJTab.Text = ""
        Me.SuitedBJBoxBJTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BJPaysBoxBJTab
        '
        Me.BJPaysBoxBJTab.Location = New System.Drawing.Point(163, 74)
        Me.BJPaysBoxBJTab.Name = "BJPaysBoxBJTab"
        Me.BJPaysBoxBJTab.Size = New System.Drawing.Size(39, 22)
        Me.BJPaysBoxBJTab.TabIndex = 2
        Me.BJPaysBoxBJTab.Text = ""
        Me.BJPaysBoxBJTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BJPaysLabelBJTab
        '
        Me.BJPaysLabelBJTab.Location = New System.Drawing.Point(19, 74)
        Me.BJPaysLabelBJTab.Name = "BJPaysLabelBJTab"
        Me.BJPaysLabelBJTab.Size = New System.Drawing.Size(106, 18)
        Me.BJPaysLabelBJTab.TabIndex = 142
        Me.BJPaysLabelBJTab.Text = "Blackjack Pays"
        '
        'SuitedBJMustWinCheckBJTab
        '
        Me.SuitedBJMustWinCheckBJTab.Location = New System.Drawing.Point(19, 157)
        Me.SuitedBJMustWinCheckBJTab.Name = "SuitedBJMustWinCheckBJTab"
        Me.SuitedBJMustWinCheckBJTab.Size = New System.Drawing.Size(154, 18)
        Me.SuitedBJMustWinCheckBJTab.TabIndex = 143
        Me.SuitedBJMustWinCheckBJTab.Text = "Suited BJ Must Win"
        '
        'BJSPlitTensCheckBJTab
        '
        Me.BJSPlitTensCheckBJTab.Location = New System.Drawing.Point(19, 55)
        Me.BJSPlitTensCheckBJTab.Name = "BJSPlitTensCheckBJTab"
        Me.BJSPlitTensCheckBJTab.Size = New System.Drawing.Size(231, 28)
        Me.BJSPlitTensCheckBJTab.TabIndex = 1
        Me.BJSPlitTensCheckBJTab.Text = "Blackjack Bonus After Split Tens"
        '
        'BJSPlitAcesCheckBJTab
        '
        Me.BJSPlitAcesCheckBJTab.Location = New System.Drawing.Point(19, 28)
        Me.BJSPlitAcesCheckBJTab.Name = "BJSPlitAcesCheckBJTab"
        Me.BJSPlitAcesCheckBJTab.Size = New System.Drawing.Size(231, 27)
        Me.BJSPlitAcesCheckBJTab.TabIndex = 0
        Me.BJSPlitAcesCheckBJTab.Text = "Blackjack Bonus After Split Aces"
        '
        'BonusRulesTabBTab
        '
        Me.BonusRulesTabBTab.Controls.Add(Me.DeleteAllBonusRulesButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.RenameRuleButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.LoadBonusRulesButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.UncheckBonusRulesButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.SaveBonusRulesButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.RestoreDefaultRulesButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.MoveRuleDownButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.MoveRuleUpButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.UpdateRuleButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.DeleteRuleButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.BonusRulesApplyLabelBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.BonusRuleDetailsGroupBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.AddRuleButtonBTab)
        Me.BonusRulesTabBTab.Controls.Add(Me.BonusRulesCheckListBoxBTab)
        Me.BonusRulesTabBTab.Location = New System.Drawing.Point(4, 25)
        Me.BonusRulesTabBTab.Name = "BonusRulesTabBTab"
        Me.BonusRulesTabBTab.Size = New System.Drawing.Size(836, 571)
        Me.BonusRulesTabBTab.TabIndex = 1
        Me.BonusRulesTabBTab.Text = "Other Bonus Rules"
        '
        'DeleteAllBonusRulesButtonBTab
        '
        Me.DeleteAllBonusRulesButtonBTab.Location = New System.Drawing.Point(653, 148)
        Me.DeleteAllBonusRulesButtonBTab.Name = "DeleteAllBonusRulesButtonBTab"
        Me.DeleteAllBonusRulesButtonBTab.Size = New System.Drawing.Size(125, 27)
        Me.DeleteAllBonusRulesButtonBTab.TabIndex = 11
        Me.DeleteAllBonusRulesButtonBTab.Text = "Delete All Rules"
        '
        'RenameRuleButtonBTab
        '
        Me.RenameRuleButtonBTab.Location = New System.Drawing.Point(653, 120)
        Me.RenameRuleButtonBTab.Name = "RenameRuleButtonBTab"
        Me.RenameRuleButtonBTab.Size = New System.Drawing.Size(125, 28)
        Me.RenameRuleButtonBTab.TabIndex = 10
        Me.RenameRuleButtonBTab.Text = "Rename Rule"
        '
        'LoadBonusRulesButtonBTab
        '
        Me.LoadBonusRulesButtonBTab.Location = New System.Drawing.Point(480, 185)
        Me.LoadBonusRulesButtonBTab.Name = "LoadBonusRulesButtonBTab"
        Me.LoadBonusRulesButtonBTab.Size = New System.Drawing.Size(163, 27)
        Me.LoadBonusRulesButtonBTab.TabIndex = 3
        Me.LoadBonusRulesButtonBTab.Text = "Load Bonus Rules File"
        '
        'UncheckBonusRulesButtonBTab
        '
        Me.UncheckBonusRulesButtonBTab.Location = New System.Drawing.Point(653, 92)
        Me.UncheckBonusRulesButtonBTab.Name = "UncheckBonusRulesButtonBTab"
        Me.UncheckBonusRulesButtonBTab.Size = New System.Drawing.Size(125, 28)
        Me.UncheckBonusRulesButtonBTab.TabIndex = 9
        Me.UncheckBonusRulesButtonBTab.Text = "Uncheck All Rules"
        '
        'SaveBonusRulesButtonBTab
        '
        Me.SaveBonusRulesButtonBTab.Location = New System.Drawing.Point(480, 212)
        Me.SaveBonusRulesButtonBTab.Name = "SaveBonusRulesButtonBTab"
        Me.SaveBonusRulesButtonBTab.Size = New System.Drawing.Size(163, 28)
        Me.SaveBonusRulesButtonBTab.TabIndex = 6
        Me.SaveBonusRulesButtonBTab.Text = "Save Bonus Rules File"
        '
        'RestoreDefaultRulesButtonBTab
        '
        Me.RestoreDefaultRulesButtonBTab.Location = New System.Drawing.Point(317, 212)
        Me.RestoreDefaultRulesButtonBTab.Name = "RestoreDefaultRulesButtonBTab"
        Me.RestoreDefaultRulesButtonBTab.Size = New System.Drawing.Size(163, 28)
        Me.RestoreDefaultRulesButtonBTab.TabIndex = 5
        Me.RestoreDefaultRulesButtonBTab.Text = "Restore Default Rules"
        '
        'MoveRuleDownButtonBTab
        '
        Me.MoveRuleDownButtonBTab.Location = New System.Drawing.Point(653, 65)
        Me.MoveRuleDownButtonBTab.Name = "MoveRuleDownButtonBTab"
        Me.MoveRuleDownButtonBTab.Size = New System.Drawing.Size(125, 27)
        Me.MoveRuleDownButtonBTab.TabIndex = 8
        Me.MoveRuleDownButtonBTab.Text = "Move Rule Down"
        '
        'MoveRuleUpButtonBTab
        '
        Me.MoveRuleUpButtonBTab.Location = New System.Drawing.Point(653, 37)
        Me.MoveRuleUpButtonBTab.Name = "MoveRuleUpButtonBTab"
        Me.MoveRuleUpButtonBTab.Size = New System.Drawing.Size(125, 28)
        Me.MoveRuleUpButtonBTab.TabIndex = 7
        Me.MoveRuleUpButtonBTab.Text = "Move Rule Up"
        '
        'UpdateRuleButtonBTab
        '
        Me.UpdateRuleButtonBTab.Location = New System.Drawing.Point(317, 185)
        Me.UpdateRuleButtonBTab.Name = "UpdateRuleButtonBTab"
        Me.UpdateRuleButtonBTab.Size = New System.Drawing.Size(163, 27)
        Me.UpdateRuleButtonBTab.TabIndex = 2
        Me.UpdateRuleButtonBTab.Text = "Update Bonus Rule"
        '
        'DeleteRuleButtonBTab
        '
        Me.DeleteRuleButtonBTab.Location = New System.Drawing.Point(154, 212)
        Me.DeleteRuleButtonBTab.Name = "DeleteRuleButtonBTab"
        Me.DeleteRuleButtonBTab.Size = New System.Drawing.Size(163, 28)
        Me.DeleteRuleButtonBTab.TabIndex = 4
        Me.DeleteRuleButtonBTab.Text = "Delete Bonus Rule"
        '
        'BonusRulesApplyLabelBTab
        '
        Me.BonusRulesApplyLabelBTab.Location = New System.Drawing.Point(346, 9)
        Me.BonusRulesApplyLabelBTab.Name = "BonusRulesApplyLabelBTab"
        Me.BonusRulesApplyLabelBTab.Size = New System.Drawing.Size(124, 19)
        Me.BonusRulesApplyLabelBTab.TabIndex = 16
        Me.BonusRulesApplyLabelBTab.Text = "Bonus Rule Applied"
        '
        'BonusRuleDetailsGroupBTab
        '
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.Label12)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HandNCardsComboBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HandTotalComboBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffUCSpecificSuitBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffUCSuitedBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffSpecificSuitBJBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffSuitedBJBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.DealerUpcardComboBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HandContinuesCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SpecificTenCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SpecificTenFractionLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SpecificTenFractionBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.DealerBJLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffGeneralBJBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.ExactMatchCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.GeneralLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffGeneralBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SuitLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffSpecificSuitBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SoftCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HeartsButtonBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.DiamondsButtonBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.ClubsButtonBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SpadesButtonBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SuitedLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.OrLessCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.ClearRuleButtonBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PostSplitCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PreSplitCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.RuleNameBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.RuleNameBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HandCompCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HandTotalSizeCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SpecificSuitCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SuitedCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.MustWinCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffSuitedBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.TotalBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.NCardsBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.OrMoreCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HandNCardsLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.TotalLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SoftLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.NCardsLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C3LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C10LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C4LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C5LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C2LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C6LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C7LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C8LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.C9LabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.CAceLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HardOnlyCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.SoftOnlyCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HandSoftLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.EitherCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.HandTotalLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.DealerUCPayoffLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.DealerUCLabelBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.PayoffUCGeneralBoxBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.AceUpCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Controls.Add(Me.TenUpCheckBTab)
        Me.BonusRuleDetailsGroupBTab.Location = New System.Drawing.Point(10, 249)
        Me.BonusRuleDetailsGroupBTab.Name = "BonusRuleDetailsGroupBTab"
        Me.BonusRuleDetailsGroupBTab.Size = New System.Drawing.Size(816, 314)
        Me.BonusRuleDetailsGroupBTab.TabIndex = 12
        Me.BonusRuleDetailsGroupBTab.TabStop = False
        Me.BonusRuleDetailsGroupBTab.Text = "Bonus Rule Details"
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(536, 288)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(272, 16)
        Me.Label12.TabIndex = 121
        Me.Label12.Text = "*Note: Bonus rules do not apply post-double."
        '
        'HandNCardsComboBoxBTab
        '
        Me.HandNCardsComboBoxBTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.HandNCardsComboBoxBTab.Items.AddRange(New Object() {"Any", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
        Me.HandNCardsComboBoxBTab.Location = New System.Drawing.Point(221, 120)
        Me.HandNCardsComboBoxBTab.Name = "HandNCardsComboBoxBTab"
        Me.HandNCardsComboBoxBTab.Size = New System.Drawing.Size(67, 24)
        Me.HandNCardsComboBoxBTab.TabIndex = 7
        '
        'HandTotalComboBoxBTab
        '
        Me.HandTotalComboBoxBTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.HandTotalComboBoxBTab.Items.AddRange(New Object() {"Any", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
        Me.HandTotalComboBoxBTab.Location = New System.Drawing.Point(38, 120)
        Me.HandTotalComboBoxBTab.Name = "HandTotalComboBoxBTab"
        Me.HandTotalComboBoxBTab.Size = New System.Drawing.Size(68, 24)
        Me.HandTotalComboBoxBTab.TabIndex = 3
        '
        'PayoffUCSpecificSuitBoxBTab
        '
        Me.PayoffUCSpecificSuitBoxBTab.Location = New System.Drawing.Point(269, 268)
        Me.PayoffUCSpecificSuitBoxBTab.Name = "PayoffUCSpecificSuitBoxBTab"
        Me.PayoffUCSpecificSuitBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffUCSpecificSuitBoxBTab.TabIndex = 120
        Me.PayoffUCSpecificSuitBoxBTab.Text = ""
        Me.PayoffUCSpecificSuitBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PayoffUCSuitedBoxBTab
        '
        Me.PayoffUCSuitedBoxBTab.Location = New System.Drawing.Point(269, 240)
        Me.PayoffUCSuitedBoxBTab.Name = "PayoffUCSuitedBoxBTab"
        Me.PayoffUCSuitedBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffUCSuitedBoxBTab.TabIndex = 119
        Me.PayoffUCSuitedBoxBTab.Text = ""
        Me.PayoffUCSuitedBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PayoffSpecificSuitBJBoxBTab
        '
        Me.PayoffSpecificSuitBJBoxBTab.Location = New System.Drawing.Point(154, 268)
        Me.PayoffSpecificSuitBJBoxBTab.Name = "PayoffSpecificSuitBJBoxBTab"
        Me.PayoffSpecificSuitBJBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffSpecificSuitBJBoxBTab.TabIndex = 118
        Me.PayoffSpecificSuitBJBoxBTab.Text = ""
        Me.PayoffSpecificSuitBJBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PayoffSuitedBJBoxBTab
        '
        Me.PayoffSuitedBJBoxBTab.Location = New System.Drawing.Point(154, 240)
        Me.PayoffSuitedBJBoxBTab.Name = "PayoffSuitedBJBoxBTab"
        Me.PayoffSuitedBJBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffSuitedBJBoxBTab.TabIndex = 117
        Me.PayoffSuitedBJBoxBTab.Text = ""
        Me.PayoffSuitedBJBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DealerUpcardComboBoxBTab
        '
        Me.DealerUpcardComboBoxBTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DealerUpcardComboBoxBTab.Items.AddRange(New Object() {"A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
        Me.DealerUpcardComboBoxBTab.Location = New System.Drawing.Point(317, 212)
        Me.DealerUpcardComboBoxBTab.Name = "DealerUpcardComboBoxBTab"
        Me.DealerUpcardComboBoxBTab.Size = New System.Drawing.Size(57, 24)
        Me.DealerUpcardComboBoxBTab.TabIndex = 18
        '
        'HandContinuesCheckBTab
        '
        Me.HandContinuesCheckBTab.Location = New System.Drawing.Point(653, 222)
        Me.HandContinuesCheckBTab.Name = "HandContinuesCheckBTab"
        Me.HandContinuesCheckBTab.Size = New System.Drawing.Size(125, 18)
        Me.HandContinuesCheckBTab.TabIndex = 27
        Me.HandContinuesCheckBTab.Text = "Hand Continues"
        '
        'SpecificTenCheckBTab
        '
        Me.SpecificTenCheckBTab.Location = New System.Drawing.Point(480, 166)
        Me.SpecificTenCheckBTab.Name = "SpecificTenCheckBTab"
        Me.SpecificTenCheckBTab.Size = New System.Drawing.Size(144, 19)
        Me.SpecificTenCheckBTab.TabIndex = 11
        Me.SpecificTenCheckBTab.Text = "Specific Ten Card"
        Me.SpecificTenCheckBTab.Visible = False
        '
        'SpecificTenFractionLabelBTab
        '
        Me.SpecificTenFractionLabelBTab.Location = New System.Drawing.Point(672, 157)
        Me.SpecificTenFractionLabelBTab.Name = "SpecificTenFractionLabelBTab"
        Me.SpecificTenFractionLabelBTab.Size = New System.Drawing.Size(125, 37)
        Me.SpecificTenFractionLabelBTab.TabIndex = 116
        Me.SpecificTenFractionLabelBTab.Text = "Specific Ten Fraction of All Tens"
        Me.SpecificTenFractionLabelBTab.Visible = False
        '
        'SpecificTenFractionBoxBTab
        '
        Me.SpecificTenFractionBoxBTab.Location = New System.Drawing.Point(624, 157)
        Me.SpecificTenFractionBoxBTab.Name = "SpecificTenFractionBoxBTab"
        Me.SpecificTenFractionBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.SpecificTenFractionBoxBTab.TabIndex = 12
        Me.SpecificTenFractionBoxBTab.Text = ""
        Me.SpecificTenFractionBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.SpecificTenFractionBoxBTab.Visible = False
        '
        'DealerBJLabelBTab
        '
        Me.DealerBJLabelBTab.Location = New System.Drawing.Point(154, 194)
        Me.DealerBJLabelBTab.Name = "DealerBJLabelBTab"
        Me.DealerBJLabelBTab.Size = New System.Drawing.Size(115, 18)
        Me.DealerBJLabelBTab.TabIndex = 114
        Me.DealerBJLabelBTab.Text = "Dealer BJ Payoff"
        '
        'PayoffGeneralBJBoxBTab
        '
        Me.PayoffGeneralBJBoxBTab.Location = New System.Drawing.Point(154, 212)
        Me.PayoffGeneralBJBoxBTab.Name = "PayoffGeneralBJBoxBTab"
        Me.PayoffGeneralBJBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffGeneralBJBoxBTab.TabIndex = 16
        Me.PayoffGeneralBJBoxBTab.Text = ""
        Me.PayoffGeneralBJBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ExactMatchCheckBTab
        '
        Me.ExactMatchCheckBTab.Location = New System.Drawing.Point(307, 166)
        Me.ExactMatchCheckBTab.Name = "ExactMatchCheckBTab"
        Me.ExactMatchCheckBTab.Size = New System.Drawing.Size(163, 19)
        Me.ExactMatchCheckBTab.TabIndex = 10
        Me.ExactMatchCheckBTab.Text = "Exact Match Required"
        '
        'GeneralLabelBTab
        '
        Me.GeneralLabelBTab.Location = New System.Drawing.Point(19, 212)
        Me.GeneralLabelBTab.Name = "GeneralLabelBTab"
        Me.GeneralLabelBTab.Size = New System.Drawing.Size(58, 19)
        Me.GeneralLabelBTab.TabIndex = 97
        Me.GeneralLabelBTab.Text = "General"
        '
        'PayoffGeneralBoxBTab
        '
        Me.PayoffGeneralBoxBTab.Location = New System.Drawing.Point(96, 212)
        Me.PayoffGeneralBoxBTab.Name = "PayoffGeneralBoxBTab"
        Me.PayoffGeneralBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffGeneralBoxBTab.TabIndex = 13
        Me.PayoffGeneralBoxBTab.Text = ""
        Me.PayoffGeneralBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SuitLabelBTab
        '
        Me.SuitLabelBTab.Location = New System.Drawing.Point(19, 268)
        Me.SuitLabelBTab.Name = "SuitLabelBTab"
        Me.SuitLabelBTab.Size = New System.Drawing.Size(67, 37)
        Me.SuitLabelBTab.TabIndex = 95
        Me.SuitLabelBTab.Text = "Diamonds Bonus"
        '
        'PayoffSpecificSuitBoxBTab
        '
        Me.PayoffSpecificSuitBoxBTab.Location = New System.Drawing.Point(96, 268)
        Me.PayoffSpecificSuitBoxBTab.Name = "PayoffSpecificSuitBoxBTab"
        Me.PayoffSpecificSuitBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffSpecificSuitBoxBTab.TabIndex = 15
        Me.PayoffSpecificSuitBoxBTab.Text = ""
        Me.PayoffSpecificSuitBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SoftCheckBTab
        '
        Me.SoftCheckBTab.Enabled = False
        Me.SoftCheckBTab.Location = New System.Drawing.Point(691, 120)
        Me.SoftCheckBTab.Name = "SoftCheckBTab"
        Me.SoftCheckBTab.Size = New System.Drawing.Size(19, 18)
        Me.SoftCheckBTab.TabIndex = 31
        Me.SoftCheckBTab.TabStop = False
        '
        'HeartsButtonBTab
        '
        Me.HeartsButtonBTab.Location = New System.Drawing.Point(576, 240)
        Me.HeartsButtonBTab.Name = "HeartsButtonBTab"
        Me.HeartsButtonBTab.Size = New System.Drawing.Size(67, 28)
        Me.HeartsButtonBTab.TabIndex = 24
        Me.HeartsButtonBTab.Text = "Hearts"
        '
        'DiamondsButtonBTab
        '
        Me.DiamondsButtonBTab.Location = New System.Drawing.Point(480, 268)
        Me.DiamondsButtonBTab.Name = "DiamondsButtonBTab"
        Me.DiamondsButtonBTab.Size = New System.Drawing.Size(96, 18)
        Me.DiamondsButtonBTab.TabIndex = 23
        Me.DiamondsButtonBTab.Text = "Diamonds"
        '
        'ClubsButtonBTab
        '
        Me.ClubsButtonBTab.Location = New System.Drawing.Point(576, 268)
        Me.ClubsButtonBTab.Name = "ClubsButtonBTab"
        Me.ClubsButtonBTab.Size = New System.Drawing.Size(67, 18)
        Me.ClubsButtonBTab.TabIndex = 25
        Me.ClubsButtonBTab.Text = "Clubs"
        '
        'SpadesButtonBTab
        '
        Me.SpadesButtonBTab.Location = New System.Drawing.Point(480, 240)
        Me.SpadesButtonBTab.Name = "SpadesButtonBTab"
        Me.SpadesButtonBTab.Size = New System.Drawing.Size(96, 28)
        Me.SpadesButtonBTab.TabIndex = 22
        Me.SpadesButtonBTab.Text = "Spades"
        '
        'SuitedLabelBTab
        '
        Me.SuitedLabelBTab.Location = New System.Drawing.Point(19, 240)
        Me.SuitedLabelBTab.Name = "SuitedLabelBTab"
        Me.SuitedLabelBTab.Size = New System.Drawing.Size(58, 18)
        Me.SuitedLabelBTab.TabIndex = 93
        Me.SuitedLabelBTab.Text = "Suited"
        '
        'OrLessCheckBTab
        '
        Me.OrLessCheckBTab.Location = New System.Drawing.Point(221, 166)
        Me.OrLessCheckBTab.Name = "OrLessCheckBTab"
        Me.OrLessCheckBTab.Size = New System.Drawing.Size(86, 19)
        Me.OrLessCheckBTab.TabIndex = 9
        Me.OrLessCheckBTab.Text = "Or Less"
        '
        'ClearRuleButtonBTab
        '
        Me.ClearRuleButtonBTab.Location = New System.Drawing.Point(682, 28)
        Me.ClearRuleButtonBTab.Name = "ClearRuleButtonBTab"
        Me.ClearRuleButtonBTab.Size = New System.Drawing.Size(86, 27)
        Me.ClearRuleButtonBTab.TabIndex = 99
        Me.ClearRuleButtonBTab.Text = "Clear Rule"
        '
        'PostSplitCheckBTab
        '
        Me.PostSplitCheckBTab.Location = New System.Drawing.Point(653, 268)
        Me.PostSplitCheckBTab.Name = "PostSplitCheckBTab"
        Me.PostSplitCheckBTab.Size = New System.Drawing.Size(125, 18)
        Me.PostSplitCheckBTab.TabIndex = 29
        Me.PostSplitCheckBTab.Text = "Apply Post-Split"
        '
        'PreSplitCheckBTab
        '
        Me.PreSplitCheckBTab.Location = New System.Drawing.Point(653, 249)
        Me.PreSplitCheckBTab.Name = "PreSplitCheckBTab"
        Me.PreSplitCheckBTab.Size = New System.Drawing.Size(125, 19)
        Me.PreSplitCheckBTab.TabIndex = 28
        Me.PreSplitCheckBTab.Text = "Apply Pre-Split"
        '
        'RuleNameBoxBTab
        '
        Me.RuleNameBoxBTab.Location = New System.Drawing.Point(192, 28)
        Me.RuleNameBoxBTab.Name = "RuleNameBoxBTab"
        Me.RuleNameBoxBTab.Size = New System.Drawing.Size(403, 22)
        Me.RuleNameBoxBTab.TabIndex = 0
        Me.RuleNameBoxBTab.Text = ""
        '
        'RuleNameBTab
        '
        Me.RuleNameBTab.Location = New System.Drawing.Point(38, 28)
        Me.RuleNameBTab.Name = "RuleNameBTab"
        Me.RuleNameBTab.Size = New System.Drawing.Size(125, 18)
        Me.RuleNameBTab.TabIndex = 77
        Me.RuleNameBTab.Text = "Bonus Rule Name"
        '
        'HandCompCheckBTab
        '
        Me.HandCompCheckBTab.Location = New System.Drawing.Point(307, 65)
        Me.HandCompCheckBTab.Name = "HandCompCheckBTab"
        Me.HandCompCheckBTab.Size = New System.Drawing.Size(250, 27)
        Me.HandCompCheckBTab.TabIndex = 2
        Me.HandCompCheckBTab.Text = "Rule Based on Hand Composition"
        '
        'HandTotalSizeCheckBTab
        '
        Me.HandTotalSizeCheckBTab.Location = New System.Drawing.Point(38, 65)
        Me.HandTotalSizeCheckBTab.Name = "HandTotalSizeCheckBTab"
        Me.HandTotalSizeCheckBTab.Size = New System.Drawing.Size(260, 27)
        Me.HandTotalSizeCheckBTab.TabIndex = 1
        Me.HandTotalSizeCheckBTab.Text = "Rule Based on Hand Total and Size"
        '
        'SpecificSuitCheckBTab
        '
        Me.SpecificSuitCheckBTab.Location = New System.Drawing.Point(480, 222)
        Me.SpecificSuitCheckBTab.Name = "SpecificSuitCheckBTab"
        Me.SpecificSuitCheckBTab.Size = New System.Drawing.Size(154, 18)
        Me.SpecificSuitCheckBTab.TabIndex = 21
        Me.SpecificSuitCheckBTab.Text = "Specific Suit Bonus"
        '
        'SuitedCheckBTab
        '
        Me.SuitedCheckBTab.Location = New System.Drawing.Point(480, 203)
        Me.SuitedCheckBTab.Name = "SuitedCheckBTab"
        Me.SuitedCheckBTab.Size = New System.Drawing.Size(125, 19)
        Me.SuitedCheckBTab.TabIndex = 20
        Me.SuitedCheckBTab.Text = "Suited Bonus"
        '
        'MustWinCheckBTab
        '
        Me.MustWinCheckBTab.Location = New System.Drawing.Point(653, 203)
        Me.MustWinCheckBTab.Name = "MustWinCheckBTab"
        Me.MustWinCheckBTab.Size = New System.Drawing.Size(125, 19)
        Me.MustWinCheckBTab.TabIndex = 26
        Me.MustWinCheckBTab.Text = "Hand Must Win"
        '
        'PayoffLabelBTab
        '
        Me.PayoffLabelBTab.Location = New System.Drawing.Point(96, 194)
        Me.PayoffLabelBTab.Name = "PayoffLabelBTab"
        Me.PayoffLabelBTab.Size = New System.Drawing.Size(48, 18)
        Me.PayoffLabelBTab.TabIndex = 67
        Me.PayoffLabelBTab.Text = "Payoff"
        '
        'PayoffSuitedBoxBTab
        '
        Me.PayoffSuitedBoxBTab.Location = New System.Drawing.Point(96, 240)
        Me.PayoffSuitedBoxBTab.Name = "PayoffSuitedBoxBTab"
        Me.PayoffSuitedBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffSuitedBoxBTab.TabIndex = 14
        Me.PayoffSuitedBoxBTab.Text = ""
        Me.PayoffSuitedBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalBoxBTab
        '
        Me.TotalBoxBTab.BackColor = System.Drawing.SystemColors.Window
        Me.TotalBoxBTab.Enabled = False
        Me.TotalBoxBTab.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TotalBoxBTab.Location = New System.Drawing.Point(624, 120)
        Me.TotalBoxBTab.Name = "TotalBoxBTab"
        Me.TotalBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.TotalBoxBTab.TabIndex = 30
        Me.TotalBoxBTab.TabStop = False
        Me.TotalBoxBTab.Text = ""
        Me.TotalBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NCardsBoxBTab
        '
        Me.NCardsBoxBTab.Enabled = False
        Me.NCardsBoxBTab.Location = New System.Drawing.Point(739, 120)
        Me.NCardsBoxBTab.Name = "NCardsBoxBTab"
        Me.NCardsBoxBTab.Size = New System.Drawing.Size(29, 22)
        Me.NCardsBoxBTab.TabIndex = 32
        Me.NCardsBoxBTab.TabStop = False
        Me.NCardsBoxBTab.Text = ""
        Me.NCardsBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OrMoreCheckBTab
        '
        Me.OrMoreCheckBTab.Location = New System.Drawing.Point(221, 148)
        Me.OrMoreCheckBTab.Name = "OrMoreCheckBTab"
        Me.OrMoreCheckBTab.Size = New System.Drawing.Size(86, 18)
        Me.OrMoreCheckBTab.TabIndex = 8
        Me.OrMoreCheckBTab.Text = "Or More"
        '
        'HandNCardsLabelBTab
        '
        Me.HandNCardsLabelBTab.Location = New System.Drawing.Point(221, 102)
        Me.HandNCardsLabelBTab.Name = "HandNCardsLabelBTab"
        Me.HandNCardsLabelBTab.Size = New System.Drawing.Size(57, 18)
        Me.HandNCardsLabelBTab.TabIndex = 58
        Me.HandNCardsLabelBTab.Text = "n Cards"
        Me.HandNCardsLabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TotalLabelBTab
        '
        Me.TotalLabelBTab.Location = New System.Drawing.Point(624, 102)
        Me.TotalLabelBTab.Name = "TotalLabelBTab"
        Me.TotalLabelBTab.Size = New System.Drawing.Size(38, 18)
        Me.TotalLabelBTab.TabIndex = 57
        Me.TotalLabelBTab.Text = "Total"
        '
        'SoftLabelBTab
        '
        Me.SoftLabelBTab.Location = New System.Drawing.Point(682, 102)
        Me.SoftLabelBTab.Name = "SoftLabelBTab"
        Me.SoftLabelBTab.Size = New System.Drawing.Size(38, 18)
        Me.SoftLabelBTab.TabIndex = 56
        Me.SoftLabelBTab.Text = "Soft"
        '
        'NCardsLabelBTab
        '
        Me.NCardsLabelBTab.Location = New System.Drawing.Point(730, 83)
        Me.NCardsLabelBTab.Name = "NCardsLabelBTab"
        Me.NCardsLabelBTab.Size = New System.Drawing.Size(48, 37)
        Me.NCardsLabelBTab.TabIndex = 55
        Me.NCardsLabelBTab.Text = "n Cards"
        Me.NCardsLabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C3LabelBTab
        '
        Me.C3LabelBTab.Location = New System.Drawing.Point(368, 104)
        Me.C3LabelBTab.Name = "C3LabelBTab"
        Me.C3LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C3LabelBTab.TabIndex = 54
        Me.C3LabelBTab.Text = "3"
        Me.C3LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C10LabelBTab
        '
        Me.C10LabelBTab.Location = New System.Drawing.Point(592, 104)
        Me.C10LabelBTab.Name = "C10LabelBTab"
        Me.C10LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C10LabelBTab.TabIndex = 53
        Me.C10LabelBTab.Text = "T"
        Me.C10LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C4LabelBTab
        '
        Me.C4LabelBTab.Location = New System.Drawing.Point(400, 104)
        Me.C4LabelBTab.Name = "C4LabelBTab"
        Me.C4LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C4LabelBTab.TabIndex = 52
        Me.C4LabelBTab.Text = "4"
        Me.C4LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C5LabelBTab
        '
        Me.C5LabelBTab.Location = New System.Drawing.Point(432, 104)
        Me.C5LabelBTab.Name = "C5LabelBTab"
        Me.C5LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C5LabelBTab.TabIndex = 51
        Me.C5LabelBTab.Text = "5"
        Me.C5LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C2LabelBTab
        '
        Me.C2LabelBTab.Location = New System.Drawing.Point(336, 104)
        Me.C2LabelBTab.Name = "C2LabelBTab"
        Me.C2LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C2LabelBTab.TabIndex = 50
        Me.C2LabelBTab.Text = "2"
        Me.C2LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C6LabelBTab
        '
        Me.C6LabelBTab.Location = New System.Drawing.Point(464, 104)
        Me.C6LabelBTab.Name = "C6LabelBTab"
        Me.C6LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C6LabelBTab.TabIndex = 49
        Me.C6LabelBTab.Text = "6"
        Me.C6LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C7LabelBTab
        '
        Me.C7LabelBTab.Location = New System.Drawing.Point(496, 104)
        Me.C7LabelBTab.Name = "C7LabelBTab"
        Me.C7LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C7LabelBTab.TabIndex = 48
        Me.C7LabelBTab.Text = "7"
        Me.C7LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C8LabelBTab
        '
        Me.C8LabelBTab.Location = New System.Drawing.Point(528, 104)
        Me.C8LabelBTab.Name = "C8LabelBTab"
        Me.C8LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C8LabelBTab.TabIndex = 47
        Me.C8LabelBTab.Text = "8"
        Me.C8LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C9LabelBTab
        '
        Me.C9LabelBTab.Location = New System.Drawing.Point(560, 104)
        Me.C9LabelBTab.Name = "C9LabelBTab"
        Me.C9LabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.C9LabelBTab.TabIndex = 46
        Me.C9LabelBTab.Text = "9"
        Me.C9LabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CAceLabelBTab
        '
        Me.CAceLabelBTab.Location = New System.Drawing.Point(304, 104)
        Me.CAceLabelBTab.Name = "CAceLabelBTab"
        Me.CAceLabelBTab.Size = New System.Drawing.Size(20, 16)
        Me.CAceLabelBTab.TabIndex = 45
        Me.CAceLabelBTab.Text = "A"
        Me.CAceLabelBTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HardOnlyCheckBTab
        '
        Me.HardOnlyCheckBTab.Location = New System.Drawing.Point(125, 148)
        Me.HardOnlyCheckBTab.Name = "HardOnlyCheckBTab"
        Me.HardOnlyCheckBTab.Size = New System.Drawing.Size(96, 18)
        Me.HardOnlyCheckBTab.TabIndex = 5
        Me.HardOnlyCheckBTab.Text = "Hard Only"
        '
        'SoftOnlyCheckBTab
        '
        Me.SoftOnlyCheckBTab.Location = New System.Drawing.Point(125, 166)
        Me.SoftOnlyCheckBTab.Name = "SoftOnlyCheckBTab"
        Me.SoftOnlyCheckBTab.Size = New System.Drawing.Size(86, 19)
        Me.SoftOnlyCheckBTab.TabIndex = 6
        Me.SoftOnlyCheckBTab.Text = "Soft Only"
        '
        'HandSoftLabelBTab
        '
        Me.HandSoftLabelBTab.Location = New System.Drawing.Point(125, 102)
        Me.HandSoftLabelBTab.Name = "HandSoftLabelBTab"
        Me.HandSoftLabelBTab.Size = New System.Drawing.Size(77, 18)
        Me.HandSoftLabelBTab.TabIndex = 5
        Me.HandSoftLabelBTab.Text = "Hand Soft"
        '
        'EitherCheckBTab
        '
        Me.EitherCheckBTab.Location = New System.Drawing.Point(125, 120)
        Me.EitherCheckBTab.Name = "EitherCheckBTab"
        Me.EitherCheckBTab.Size = New System.Drawing.Size(67, 18)
        Me.EitherCheckBTab.TabIndex = 4
        Me.EitherCheckBTab.Text = "Either"
        '
        'HandTotalLabelBTab
        '
        Me.HandTotalLabelBTab.Location = New System.Drawing.Point(38, 102)
        Me.HandTotalLabelBTab.Name = "HandTotalLabelBTab"
        Me.HandTotalLabelBTab.Size = New System.Drawing.Size(77, 18)
        Me.HandTotalLabelBTab.TabIndex = 3
        Me.HandTotalLabelBTab.Text = "Hand Total"
        '
        'DealerUCPayoffLabelBTab
        '
        Me.DealerUCPayoffLabelBTab.Location = New System.Drawing.Point(269, 194)
        Me.DealerUCPayoffLabelBTab.Name = "DealerUCPayoffLabelBTab"
        Me.DealerUCPayoffLabelBTab.Size = New System.Drawing.Size(115, 18)
        Me.DealerUCPayoffLabelBTab.TabIndex = 106
        Me.DealerUCPayoffLabelBTab.Text = "Dealer UC Payoff"
        '
        'DealerUCLabelBTab
        '
        Me.DealerUCLabelBTab.Location = New System.Drawing.Point(374, 212)
        Me.DealerUCLabelBTab.Name = "DealerUCLabelBTab"
        Me.DealerUCLabelBTab.Size = New System.Drawing.Size(77, 19)
        Me.DealerUCLabelBTab.TabIndex = 107
        Me.DealerUCLabelBTab.Text = "Dealer UC"
        '
        'PayoffUCGeneralBoxBTab
        '
        Me.PayoffUCGeneralBoxBTab.Location = New System.Drawing.Point(269, 212)
        Me.PayoffUCGeneralBoxBTab.Name = "PayoffUCGeneralBoxBTab"
        Me.PayoffUCGeneralBoxBTab.Size = New System.Drawing.Size(38, 22)
        Me.PayoffUCGeneralBoxBTab.TabIndex = 17
        Me.PayoffUCGeneralBoxBTab.Text = ""
        Me.PayoffUCGeneralBoxBTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'AceUpCheckBTab
        '
        Me.AceUpCheckBTab.Location = New System.Drawing.Point(202, 212)
        Me.AceUpCheckBTab.Name = "AceUpCheckBTab"
        Me.AceUpCheckBTab.Size = New System.Drawing.Size(57, 19)
        Me.AceUpCheckBTab.TabIndex = 27
        Me.AceUpCheckBTab.Text = "A up"
        '
        'TenUpCheckBTab
        '
        Me.TenUpCheckBTab.Location = New System.Drawing.Point(202, 231)
        Me.TenUpCheckBTab.Name = "TenUpCheckBTab"
        Me.TenUpCheckBTab.Size = New System.Drawing.Size(57, 18)
        Me.TenUpCheckBTab.TabIndex = 28
        Me.TenUpCheckBTab.Text = "T up"
        '
        'AddRuleButtonBTab
        '
        Me.AddRuleButtonBTab.Location = New System.Drawing.Point(154, 185)
        Me.AddRuleButtonBTab.Name = "AddRuleButtonBTab"
        Me.AddRuleButtonBTab.Size = New System.Drawing.Size(163, 27)
        Me.AddRuleButtonBTab.TabIndex = 1
        Me.AddRuleButtonBTab.Text = "Add Bonus Rule"
        '
        'BonusRulesCheckListBoxBTab
        '
        Me.BonusRulesCheckListBoxBTab.Location = New System.Drawing.Point(154, 37)
        Me.BonusRulesCheckListBoxBTab.Name = "BonusRulesCheckListBoxBTab"
        Me.BonusRulesCheckListBoxBTab.Size = New System.Drawing.Size(489, 140)
        Me.BonusRulesCheckListBoxBTab.TabIndex = 0
        '
        'ForcedTab
        '
        Me.ForcedTab.Controls.Add(Me.ForcedStratTabControlFSTab)
        Me.ForcedTab.Controls.Add(Me.LoadForcedTablesButtonFSTab)
        Me.ForcedTab.Controls.Add(Me.SaveForcedTablesButtonFSTab)
        Me.ForcedTab.Controls.Add(Me.ClearForcedTablesButtonFSTab)
        Me.ForcedTab.Location = New System.Drawing.Point(4, 25)
        Me.ForcedTab.Name = "ForcedTab"
        Me.ForcedTab.Size = New System.Drawing.Size(866, 617)
        Me.ForcedTab.TabIndex = 11
        Me.ForcedTab.Text = "Forced Strategy"
        '
        'ForcedStratTabControlFSTab
        '
        Me.ForcedStratTabControlFSTab.Controls.Add(Me.OptionsTabFSTab)
        Me.ForcedStratTabControlFSTab.Controls.Add(Me.HardSoftTDTabFSTab)
        Me.ForcedStratTabControlFSTab.Controls.Add(Me.SoftPairsCDTabFSTab)
        Me.ForcedStratTabControlFSTab.Controls.Add(Me.HardCDTabFSTab)
        Me.ForcedStratTabControlFSTab.Controls.Add(Me.OtherTabFSTab)
        Me.ForcedStratTabControlFSTab.Location = New System.Drawing.Point(19, 9)
        Me.ForcedStratTabControlFSTab.Name = "ForcedStratTabControlFSTab"
        Me.ForcedStratTabControlFSTab.SelectedIndex = 0
        Me.ForcedStratTabControlFSTab.Size = New System.Drawing.Size(826, 554)
        Me.ForcedStratTabControlFSTab.TabIndex = 126
        '
        'OptionsTabFSTab
        '
        Me.OptionsTabFSTab.Controls.Add(Me.PairsRuleApplyLabelFSTab)
        Me.OptionsTabFSTab.Controls.Add(Me.ForcedTablePostCheckFSTab)
        Me.OptionsTabFSTab.Controls.Add(Me.ForcedTablePreCheckFSTab)
        Me.OptionsTabFSTab.Controls.Add(Me.ForcednCDLabelFSTab)
        Me.OptionsTabFSTab.Controls.Add(Me.ForcednCDBoxFSTab)
        Me.OptionsTabFSTab.Controls.Add(Me.ForcedWarningLabelFSTab)
        Me.OptionsTabFSTab.Location = New System.Drawing.Point(4, 25)
        Me.OptionsTabFSTab.Name = "OptionsTabFSTab"
        Me.OptionsTabFSTab.Size = New System.Drawing.Size(818, 525)
        Me.OptionsTabFSTab.TabIndex = 3
        Me.OptionsTabFSTab.Text = "Forced Strategy Options"
        '
        'PairsRuleApplyLabelFSTab
        '
        Me.PairsRuleApplyLabelFSTab.Location = New System.Drawing.Point(250, 392)
        Me.PairsRuleApplyLabelFSTab.Name = "PairsRuleApplyLabelFSTab"
        Me.PairsRuleApplyLabelFSTab.Size = New System.Drawing.Size(307, 64)
        Me.PairsRuleApplyLabelFSTab.TabIndex = 7
        Me.PairsRuleApplyLabelFSTab.Text = "*Note: Table Total based rules will not apply to pairs and only exact match ""Othe" & _
        "r"" rules will.  AA/22 pairs will assume to have a secondary Hit strategy if assi" & _
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
        Me.ForcednCDBoxFSTab.Location = New System.Drawing.Point(232, 28)
        Me.ForcednCDBoxFSTab.Name = "ForcednCDBoxFSTab"
        Me.ForcednCDBoxFSTab.Size = New System.Drawing.Size(40, 22)
        Me.ForcednCDBoxFSTab.TabIndex = 0
        Me.ForcednCDBoxFSTab.Text = ""
        '
        'ForcedWarningLabelFSTab
        '
        Me.ForcedWarningLabelFSTab.Location = New System.Drawing.Point(250, 471)
        Me.ForcedWarningLabelFSTab.Name = "ForcedWarningLabelFSTab"
        Me.ForcedWarningLabelFSTab.Size = New System.Drawing.Size(307, 27)
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
        Me.HardSoftTDTabFSTab.Size = New System.Drawing.Size(818, 525)
        Me.HardSoftTDTabFSTab.TabIndex = 5
        Me.HardSoftTDTabFSTab.Text = "TD Hard/Soft"
        '
        'RowClickLabelFSTab
        '
        Me.RowClickLabelFSTab.Location = New System.Drawing.Point(442, 288)
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
        Me.PairCDLabelComboboxFSTab.TabStop = False
        Me.PairCDLabelComboboxFSTab.Visible = False
        '
        'SoftCDLabelComboboxFSTab
        '
        Me.SoftCDLabelComboboxFSTab.Location = New System.Drawing.Point(624, 471)
        Me.SoftCDLabelComboboxFSTab.Name = "SoftCDLabelComboboxFSTab"
        Me.SoftCDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
        Me.SoftCDLabelComboboxFSTab.TabIndex = 228
        Me.SoftCDLabelComboboxFSTab.TabStop = False
        Me.SoftCDLabelComboboxFSTab.Visible = False
        '
        'HardCDLabelComboboxFSTab
        '
        Me.HardCDLabelComboboxFSTab.Location = New System.Drawing.Point(624, 443)
        Me.HardCDLabelComboboxFSTab.Name = "HardCDLabelComboboxFSTab"
        Me.HardCDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
        Me.HardCDLabelComboboxFSTab.TabIndex = 227
        Me.HardCDLabelComboboxFSTab.TabStop = False
        Me.HardCDLabelComboboxFSTab.Visible = False
        '
        'SoftTDLabelComboboxFSTab
        '
        Me.SoftTDLabelComboboxFSTab.Location = New System.Drawing.Point(720, 415)
        Me.SoftTDLabelComboboxFSTab.Name = "SoftTDLabelComboboxFSTab"
        Me.SoftTDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
        Me.SoftTDLabelComboboxFSTab.TabIndex = 226
        Me.SoftTDLabelComboboxFSTab.TabStop = False
        Me.SoftTDLabelComboboxFSTab.Visible = False
        '
        'HardTDLabelComboboxFSTab
        '
        Me.HardTDLabelComboboxFSTab.Location = New System.Drawing.Point(624, 415)
        Me.HardTDLabelComboboxFSTab.Name = "HardTDLabelComboboxFSTab"
        Me.HardTDLabelComboboxFSTab.Size = New System.Drawing.Size(96, 24)
        Me.HardTDLabelComboboxFSTab.TabIndex = 225
        Me.HardTDLabelComboboxFSTab.TabStop = False
        Me.HardTDLabelComboboxFSTab.Visible = False
        '
        'SoftTDGroupFSTab
        '
        Me.SoftTDGroupFSTab.Controls.Add(Me.SoftTotalTDLabelFSTab)
        Me.SoftTDGroupFSTab.Location = New System.Drawing.Point(442, 9)
        Me.SoftTDGroupFSTab.Name = "SoftTDGroupFSTab"
        Me.SoftTDGroupFSTab.Size = New System.Drawing.Size(355, 271)
        Me.SoftTDGroupFSTab.TabIndex = 223
        Me.SoftTDGroupFSTab.TabStop = False
        Me.SoftTDGroupFSTab.Text = "Soft Total Forced TD Strategy"
        '
        'SoftTotalTDLabelFSTab
        '
        Me.SoftTotalTDLabelFSTab.Location = New System.Drawing.Point(16, 24)
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
        Me.ForcedTableComboboxFSTab.TabStop = False
        Me.ForcedTableComboboxFSTab.Visible = False
        '
        'HardTDGroupFSTab
        '
        Me.HardTDGroupFSTab.Controls.Add(Me.HardTotalTDLabelFSTab)
        Me.HardTDGroupFSTab.Location = New System.Drawing.Point(29, 9)
        Me.HardTDGroupFSTab.Name = "HardTDGroupFSTab"
        Me.HardTDGroupFSTab.Size = New System.Drawing.Size(355, 463)
        Me.HardTDGroupFSTab.TabIndex = 224
        Me.HardTDGroupFSTab.TabStop = False
        Me.HardTDGroupFSTab.Text = "Hard Total Forced TD Strategy"
        '
        'HardTotalTDLabelFSTab
        '
        Me.HardTotalTDLabelFSTab.Location = New System.Drawing.Point(16, 24)
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
        Me.SoftPairsCDTabFSTab.Size = New System.Drawing.Size(818, 525)
        Me.SoftPairsCDTabFSTab.TabIndex = 1
        Me.SoftPairsCDTabFSTab.Text = "CD Soft/Pairs"
        '
        'PairCDGroupFSTab
        '
        Me.PairCDGroupFSTab.Controls.Add(Me.PairForcedCDLabelFSTab)
        Me.PairCDGroupFSTab.Location = New System.Drawing.Point(442, 9)
        Me.PairCDGroupFSTab.Name = "PairCDGroupFSTab"
        Me.PairCDGroupFSTab.Size = New System.Drawing.Size(355, 295)
        Me.PairCDGroupFSTab.TabIndex = 11
        Me.PairCDGroupFSTab.TabStop = False
        Me.PairCDGroupFSTab.Text = "Pairs Forced Strategy"
        '
        'PairForcedCDLabelFSTab
        '
        Me.PairForcedCDLabelFSTab.Location = New System.Drawing.Point(16, 24)
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
        Me.SoftCDGroupFSTab.Size = New System.Drawing.Size(355, 271)
        Me.SoftCDGroupFSTab.TabIndex = 10
        Me.SoftCDGroupFSTab.TabStop = False
        Me.SoftCDGroupFSTab.Text = "Soft Total Forced Strategy"
        '
        'TotalForcedCDLabelFSTab
        '
        Me.TotalForcedCDLabelFSTab.Location = New System.Drawing.Point(16, 24)
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
        Me.HardCDTabFSTab.Size = New System.Drawing.Size(818, 525)
        Me.HardCDTabFSTab.TabIndex = 0
        Me.HardCDTabFSTab.Text = "CD Hard"
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
        Me.OtherTabFSTab.Size = New System.Drawing.Size(818, 525)
        Me.OtherTabFSTab.TabIndex = 4
        Me.OtherTabFSTab.Text = "Other Forced Rules"
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
        Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandNCardsComboBoxFSTab)
        Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandTotalComboBoxFSTab)
        Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.PostDoubleCheckFSTab)
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
        Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandSoftCheckFSTab)
        Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandSoftLabelFSTab)
        Me.ForcedRuleDetailsGroupFSTab.Controls.Add(Me.HandTotalLabelFSTab)
        Me.ForcedRuleDetailsGroupFSTab.Location = New System.Drawing.Point(19, 240)
        Me.ForcedRuleDetailsGroupFSTab.Name = "ForcedRuleDetailsGroupFSTab"
        Me.ForcedRuleDetailsGroupFSTab.Size = New System.Drawing.Size(787, 268)
        Me.ForcedRuleDetailsGroupFSTab.TabIndex = 12
        Me.ForcedRuleDetailsGroupFSTab.TabStop = False
        Me.ForcedRuleDetailsGroupFSTab.Text = "Forced Rule Details"
        '
        'C3LabelFSTab
        '
        Me.C3LabelFSTab.Location = New System.Drawing.Point(360, 104)
        Me.C3LabelFSTab.Name = "C3LabelFSTab"
        Me.C3LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C3LabelFSTab.TabIndex = 233
        Me.C3LabelFSTab.Text = "3"
        Me.C3LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C10LabelFSTab
        '
        Me.C10LabelFSTab.Location = New System.Drawing.Point(584, 104)
        Me.C10LabelFSTab.Name = "C10LabelFSTab"
        Me.C10LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C10LabelFSTab.TabIndex = 232
        Me.C10LabelFSTab.Text = "T"
        Me.C10LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C4LabelFSTab
        '
        Me.C4LabelFSTab.Location = New System.Drawing.Point(392, 104)
        Me.C4LabelFSTab.Name = "C4LabelFSTab"
        Me.C4LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C4LabelFSTab.TabIndex = 231
        Me.C4LabelFSTab.Text = "4"
        Me.C4LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C5LabelFSTab
        '
        Me.C5LabelFSTab.Location = New System.Drawing.Point(424, 104)
        Me.C5LabelFSTab.Name = "C5LabelFSTab"
        Me.C5LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C5LabelFSTab.TabIndex = 230
        Me.C5LabelFSTab.Text = "5"
        Me.C5LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C2LabelFSTab
        '
        Me.C2LabelFSTab.Location = New System.Drawing.Point(328, 104)
        Me.C2LabelFSTab.Name = "C2LabelFSTab"
        Me.C2LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C2LabelFSTab.TabIndex = 229
        Me.C2LabelFSTab.Text = "2"
        Me.C2LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C6LabelFSTab
        '
        Me.C6LabelFSTab.Location = New System.Drawing.Point(456, 104)
        Me.C6LabelFSTab.Name = "C6LabelFSTab"
        Me.C6LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C6LabelFSTab.TabIndex = 228
        Me.C6LabelFSTab.Text = "6"
        Me.C6LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C7LabelFSTab
        '
        Me.C7LabelFSTab.Location = New System.Drawing.Point(488, 104)
        Me.C7LabelFSTab.Name = "C7LabelFSTab"
        Me.C7LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C7LabelFSTab.TabIndex = 227
        Me.C7LabelFSTab.Text = "7"
        Me.C7LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C8LabelFSTab
        '
        Me.C8LabelFSTab.Location = New System.Drawing.Point(520, 104)
        Me.C8LabelFSTab.Name = "C8LabelFSTab"
        Me.C8LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C8LabelFSTab.TabIndex = 226
        Me.C8LabelFSTab.Text = "8"
        Me.C8LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C9LabelFSTab
        '
        Me.C9LabelFSTab.Location = New System.Drawing.Point(552, 104)
        Me.C9LabelFSTab.Name = "C9LabelFSTab"
        Me.C9LabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.C9LabelFSTab.TabIndex = 225
        Me.C9LabelFSTab.Text = "9"
        Me.C9LabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CAceLabelFSTab
        '
        Me.CAceLabelFSTab.Location = New System.Drawing.Point(296, 104)
        Me.CAceLabelFSTab.Name = "CAceLabelFSTab"
        Me.CAceLabelFSTab.Size = New System.Drawing.Size(20, 16)
        Me.CAceLabelFSTab.TabIndex = 224
        Me.CAceLabelFSTab.Text = "A"
        Me.CAceLabelFSTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HandNCardsComboBoxFSTab
        '
        Me.HandNCardsComboBoxFSTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.HandNCardsComboBoxFSTab.Items.AddRange(New Object() {"Any", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"})
        Me.HandNCardsComboBoxFSTab.Location = New System.Drawing.Point(202, 120)
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
        'PostDoubleCheckFSTab
        '
        Me.PostDoubleCheckFSTab.Location = New System.Drawing.Point(29, 240)
        Me.PostDoubleCheckFSTab.Name = "PostDoubleCheckFSTab"
        Me.PostDoubleCheckFSTab.Size = New System.Drawing.Size(144, 18)
        Me.PostDoubleCheckFSTab.TabIndex = 11
        Me.PostDoubleCheckFSTab.Text = "Apply Post-Double"
        Me.PostDoubleCheckFSTab.Visible = False
        '
        'UpcardComboBoxFSTab
        '
        Me.UpcardComboBoxFSTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UpcardComboBoxFSTab.Items.AddRange(New Object() {"All", "A", "2", "3", "4", "5", "6", "7", "8", "9", "T"})
        Me.UpcardComboBoxFSTab.Location = New System.Drawing.Point(202, 222)
        Me.UpcardComboBoxFSTab.Name = "UpcardComboBoxFSTab"
        Me.UpcardComboBoxFSTab.Size = New System.Drawing.Size(57, 24)
        Me.UpcardComboBoxFSTab.TabIndex = 12
        '
        'ForcedRuleStratBoxFSTab
        '
        Me.ForcedRuleStratBoxFSTab.Location = New System.Drawing.Point(634, 203)
        Me.ForcedRuleStratBoxFSTab.Name = "ForcedRuleStratBoxFSTab"
        Me.ForcedRuleStratBoxFSTab.ReadOnly = True
        Me.ForcedRuleStratBoxFSTab.Size = New System.Drawing.Size(28, 22)
        Me.ForcedRuleStratBoxFSTab.TabIndex = 13
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
        Me.StrategyComboBoxFSTab.TabStop = False
        Me.StrategyComboBoxFSTab.Visible = False
        '
        'DealerUpcardLabelFSTab
        '
        Me.DealerUpcardLabelFSTab.Location = New System.Drawing.Point(182, 203)
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
        Me.SoftCheckFSTab.Enabled = False
        Me.SoftCheckFSTab.Location = New System.Drawing.Point(691, 120)
        Me.SoftCheckFSTab.Name = "SoftCheckFSTab"
        Me.SoftCheckFSTab.Size = New System.Drawing.Size(19, 18)
        Me.SoftCheckFSTab.TabIndex = 31
        Me.SoftCheckFSTab.TabStop = False
        '
        'OrLessCheckFSTab
        '
        Me.OrLessCheckFSTab.Location = New System.Drawing.Point(202, 166)
        Me.OrLessCheckFSTab.Name = "OrLessCheckFSTab"
        Me.OrLessCheckFSTab.Size = New System.Drawing.Size(86, 19)
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
        Me.TotalBoxFSTab.Enabled = False
        Me.TotalBoxFSTab.Location = New System.Drawing.Point(634, 120)
        Me.TotalBoxFSTab.Name = "TotalBoxFSTab"
        Me.TotalBoxFSTab.Size = New System.Drawing.Size(28, 22)
        Me.TotalBoxFSTab.TabIndex = 30
        Me.TotalBoxFSTab.TabStop = False
        Me.TotalBoxFSTab.Text = ""
        Me.TotalBoxFSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NCardsBoxFSTab
        '
        Me.NCardsBoxFSTab.Enabled = False
        Me.NCardsBoxFSTab.Location = New System.Drawing.Point(730, 120)
        Me.NCardsBoxFSTab.Name = "NCardsBoxFSTab"
        Me.NCardsBoxFSTab.Size = New System.Drawing.Size(28, 22)
        Me.NCardsBoxFSTab.TabIndex = 32
        Me.NCardsBoxFSTab.TabStop = False
        Me.NCardsBoxFSTab.Text = ""
        Me.NCardsBoxFSTab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OrMoreCheckFSTab
        '
        Me.OrMoreCheckFSTab.Location = New System.Drawing.Point(202, 148)
        Me.OrMoreCheckFSTab.Name = "OrMoreCheckFSTab"
        Me.OrMoreCheckFSTab.Size = New System.Drawing.Size(86, 18)
        Me.OrMoreCheckFSTab.TabIndex = 6
        Me.OrMoreCheckFSTab.Text = "Or More"
        '
        'HandNCardsLabelFSTab
        '
        Me.HandNCardsLabelFSTab.Location = New System.Drawing.Point(202, 102)
        Me.HandNCardsLabelFSTab.Name = "HandNCardsLabelFSTab"
        Me.HandNCardsLabelFSTab.Size = New System.Drawing.Size(57, 18)
        Me.HandNCardsLabelFSTab.TabIndex = 58
        Me.HandNCardsLabelFSTab.Text = "n Cards"
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
        Me.LoadForcedTablesButtonFSTab.Location = New System.Drawing.Point(518, 582)
        Me.LoadForcedTablesButtonFSTab.Name = "LoadForcedTablesButtonFSTab"
        Me.LoadForcedTablesButtonFSTab.Size = New System.Drawing.Size(135, 27)
        Me.LoadForcedTablesButtonFSTab.TabIndex = 902
        Me.LoadForcedTablesButtonFSTab.Text = "Load TD/CD Table"
        '
        'SaveForcedTablesButtonFSTab
        '
        Me.SaveForcedTablesButtonFSTab.Location = New System.Drawing.Point(365, 582)
        Me.SaveForcedTablesButtonFSTab.Name = "SaveForcedTablesButtonFSTab"
        Me.SaveForcedTablesButtonFSTab.Size = New System.Drawing.Size(134, 27)
        Me.SaveForcedTablesButtonFSTab.TabIndex = 901
        Me.SaveForcedTablesButtonFSTab.Text = "Save TD/CD Table"
        '
        'ClearForcedTablesButtonFSTab
        '
        Me.ClearForcedTablesButtonFSTab.Location = New System.Drawing.Point(211, 582)
        Me.ClearForcedTablesButtonFSTab.Name = "ClearForcedTablesButtonFSTab"
        Me.ClearForcedTablesButtonFSTab.Size = New System.Drawing.Size(135, 27)
        Me.ClearForcedTablesButtonFSTab.TabIndex = 900
        Me.ClearForcedTablesButtonFSTab.Text = "Clear TD/CD Table"
        '
        'OtherOptionsTab
        '
        Me.OtherOptionsTab.Controls.Add(Me.UCAllowedGroupOTab)
        Me.OtherOptionsTab.Controls.Add(Me.ColorTableGroupOTab)
        Me.OtherOptionsTab.Location = New System.Drawing.Point(4, 25)
        Me.OtherOptionsTab.Name = "OtherOptionsTab"
        Me.OtherOptionsTab.Size = New System.Drawing.Size(866, 617)
        Me.OtherOptionsTab.TabIndex = 8
        Me.OtherOptionsTab.Text = "Other"
        '
        'UCAllowedGroupOTab
        '
        Me.UCAllowedGroupOTab.Controls.Add(Me.ToggleAllCheckOTab)
        Me.UCAllowedGroupOTab.Controls.Add(Me.UpcardLabelOTab)
        Me.UCAllowedGroupOTab.Location = New System.Drawing.Point(29, 18)
        Me.UCAllowedGroupOTab.Name = "UCAllowedGroupOTab"
        Me.UCAllowedGroupOTab.Size = New System.Drawing.Size(307, 318)
        Me.UCAllowedGroupOTab.TabIndex = 0
        Me.UCAllowedGroupOTab.TabStop = False
        Me.UCAllowedGroupOTab.Text = "Upcards Allowed"
        '
        'ToggleAllCheckOTab
        '
        Me.ToggleAllCheckOTab.Location = New System.Drawing.Point(106, 28)
        Me.ToggleAllCheckOTab.Name = "ToggleAllCheckOTab"
        Me.ToggleAllCheckOTab.Size = New System.Drawing.Size(96, 20)
        Me.ToggleAllCheckOTab.TabIndex = 0
        Me.ToggleAllCheckOTab.Text = "Toggle All"
        '
        'UpcardLabelOTab
        '
        Me.UpcardLabelOTab.Location = New System.Drawing.Point(125, 65)
        Me.UpcardLabelOTab.Name = "UpcardLabelOTab"
        Me.UpcardLabelOTab.Size = New System.Drawing.Size(57, 18)
        Me.UpcardLabelOTab.TabIndex = 50
        Me.UpcardLabelOTab.Text = "Upcard"
        '
        'ColorTableGroupOTab
        '
        Me.ColorTableGroupOTab.Controls.Add(Me.RestoreDefaultColorTableButtonOTab)
        Me.ColorTableGroupOTab.Controls.Add(Me.SaveColorTableFileButtonOTab)
        Me.ColorTableGroupOTab.Controls.Add(Me.LoadColorTableFileButtonOTab)
        Me.ColorTableGroupOTab.Location = New System.Drawing.Point(614, 18)
        Me.ColorTableGroupOTab.Name = "ColorTableGroupOTab"
        Me.ColorTableGroupOTab.Size = New System.Drawing.Size(221, 499)
        Me.ColorTableGroupOTab.TabIndex = 5
        Me.ColorTableGroupOTab.TabStop = False
        Me.ColorTableGroupOTab.Text = "Strategy Colors"
        '
        'RestoreDefaultColorTableButtonOTab
        '
        Me.RestoreDefaultColorTableButtonOTab.Location = New System.Drawing.Point(48, 378)
        Me.RestoreDefaultColorTableButtonOTab.Name = "RestoreDefaultColorTableButtonOTab"
        Me.RestoreDefaultColorTableButtonOTab.Size = New System.Drawing.Size(134, 28)
        Me.RestoreDefaultColorTableButtonOTab.TabIndex = 100
        Me.RestoreDefaultColorTableButtonOTab.Text = "Restore Defaults"
        '
        'SaveColorTableFileButtonOTab
        '
        Me.SaveColorTableFileButtonOTab.Location = New System.Drawing.Point(48, 415)
        Me.SaveColorTableFileButtonOTab.Name = "SaveColorTableFileButtonOTab"
        Me.SaveColorTableFileButtonOTab.Size = New System.Drawing.Size(134, 28)
        Me.SaveColorTableFileButtonOTab.TabIndex = 101
        Me.SaveColorTableFileButtonOTab.Text = "Save Colors Table"
        '
        'LoadColorTableFileButtonOTab
        '
        Me.LoadColorTableFileButtonOTab.Location = New System.Drawing.Point(48, 452)
        Me.LoadColorTableFileButtonOTab.Name = "LoadColorTableFileButtonOTab"
        Me.LoadColorTableFileButtonOTab.Size = New System.Drawing.Size(134, 28)
        Me.LoadColorTableFileButtonOTab.TabIndex = 102
        Me.LoadColorTableFileButtonOTab.Text = "Load Colors Table"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.FileMenuItem, Me.OptionsMenuItem, Me.StrategiesMenuItem, Me.HelpMenuItem, Me.RealtimeMenuItem, Me.CalcNowMenuItem})
        '
        'FileMenuItem
        '
        Me.FileMenuItem.Index = 0
        Me.FileMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.NewMenuItem, Me.OpenRuleSetMenuItem, Me.SaveRuleSetMenuItem})
        Me.FileMenuItem.Text = "File"
        '
        'NewMenuItem
        '
        Me.NewMenuItem.Index = 0
        Me.NewMenuItem.Text = "New"
        '
        'OpenRuleSetMenuItem
        '
        Me.OpenRuleSetMenuItem.Index = 1
        Me.OpenRuleSetMenuItem.Text = "Open Rule Set"
        '
        'SaveRuleSetMenuItem
        '
        Me.SaveRuleSetMenuItem.Index = 2
        Me.SaveRuleSetMenuItem.Text = "Save Rule Set"
        '
        'OptionsMenuItem
        '
        Me.OptionsMenuItem.Index = 1
        Me.OptionsMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.SetDefaultsMenuItem, Me.RestoreDefaultsMenuItem, Me.UseDPDictionaryMenuItem, Me.SaveDPDictionaryMenuItem, Me.PSExceptionsMenuItem})
        Me.OptionsMenuItem.Text = "Options"
        '
        'SetDefaultsMenuItem
        '
        Me.SetDefaultsMenuItem.Index = 0
        Me.SetDefaultsMenuItem.Text = "Set Defaults"
        '
        'RestoreDefaultsMenuItem
        '
        Me.RestoreDefaultsMenuItem.Index = 1
        Me.RestoreDefaultsMenuItem.Text = "Restore Original Defaults"
        '
        'UseDPDictionaryMenuItem
        '
        Me.UseDPDictionaryMenuItem.Checked = True
        Me.UseDPDictionaryMenuItem.Index = 2
        Me.UseDPDictionaryMenuItem.Text = "Use Dealer Probs File"
        '
        'SaveDPDictionaryMenuItem
        '
        Me.SaveDPDictionaryMenuItem.Checked = True
        Me.SaveDPDictionaryMenuItem.Index = 3
        Me.SaveDPDictionaryMenuItem.Text = "Update Dealer Probs File"
        '
        'PSExceptionsMenuItem
        '
        Me.PSExceptionsMenuItem.Index = 4
        Me.PSExceptionsMenuItem.Text = "Include Post-Split Exceptions"
        '
        'StrategiesMenuItem
        '
        Me.StrategiesMenuItem.Index = 2
        Me.StrategiesMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ComputeTDMenuItem, Me.ComputeTCMenuItem, Me.ComputeForcedMenuItem})
        Me.StrategiesMenuItem.Text = "Strategies"
        '
        'ComputeTDMenuItem
        '
        Me.ComputeTDMenuItem.Checked = True
        Me.ComputeTDMenuItem.Index = 0
        Me.ComputeTDMenuItem.Text = "Total Dependent"
        '
        'ComputeTCMenuItem
        '
        Me.ComputeTCMenuItem.Checked = True
        Me.ComputeTCMenuItem.Index = 1
        Me.ComputeTCMenuItem.Text = "2-Card"
        '
        'ComputeForcedMenuItem
        '
        Me.ComputeForcedMenuItem.Checked = True
        Me.ComputeForcedMenuItem.Index = 2
        Me.ComputeForcedMenuItem.Text = "Forced"
        '
        'HelpMenuItem
        '
        Me.HelpMenuItem.Index = 3
        Me.HelpMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.AboutMenuItem, Me.HelpFileMenuItem})
        Me.HelpMenuItem.Text = "Help"
        '
        'AboutMenuItem
        '
        Me.AboutMenuItem.Index = 0
        Me.AboutMenuItem.Text = "About"
        '
        'HelpFileMenuItem
        '
        Me.HelpFileMenuItem.Index = 1
        Me.HelpFileMenuItem.Text = "MGP's BJ CA Help"
        '
        'RealtimeMenuItem
        '
        Me.RealtimeMenuItem.Index = 4
        Me.RealtimeMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.StartRTMenuItem, Me.RTSPL1EstMenuItem, Me.RTSmallMenuItem})
        Me.RealtimeMenuItem.Text = "Realtime"
        '
        'StartRTMenuItem
        '
        Me.StartRTMenuItem.Index = 0
        Me.StartRTMenuItem.Text = "Start Realtime Analysis!"
        '
        'RTSPL1EstMenuItem
        '
        Me.RTSPL1EstMenuItem.Checked = True
        Me.RTSPL1EstMenuItem.Index = 1
        Me.RTSPL1EstMenuItem.Text = "Use Single Split Estimates"
        '
        'RTSmallMenuItem
        '
        Me.RTSmallMenuItem.Index = 2
        Me.RTSmallMenuItem.Text = "Use Small Form"
        '
        'CalcNowMenuItem
        '
        Me.CalcNowMenuItem.Index = 5
        Me.CalcNowMenuItem.Text = "Calculate Now!"
        '
        'BJCAMainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(873, 654)
        Me.Controls.Add(Me.MainTabs)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "BJCAMainForm"
        Me.Text = "MGP's Blackjack Combinatorial Analyzer"
        Me.MainTabs.ResumeLayout(False)
        Me.MainRulesTab.ResumeLayout(False)
        Me.HoleCardGroupMTab.ResumeLayout(False)
        Me.ShoeOptionGroupMTab.ResumeLayout(False)
        Me.SplitGroupMTab.ResumeLayout(False)
        Me.DoubleGroupMTab.ResumeLayout(False)
        Me.SurrenderGroupMTab.ResumeLayout(False)
        Me.DealerRulesGroupMTab.ResumeLayout(False)
        Me.DoubleRulesTab.ResumeLayout(False)
        Me.DoubleGroupDTab.ResumeLayout(False)
        Me.DoubleTableGroup.ResumeLayout(False)
        Me.SurrenderRulesTab.ResumeLayout(False)
        Me.SurrenderTableGroup.ResumeLayout(False)
        Me.SurrenderRulesGroupSTab.ResumeLayout(False)
        Me.SplitOptionsTab.ResumeLayout(False)
        Me.SplitPairsAllowedGroupSpTab.ResumeLayout(False)
        Me.CDSplitGroupSpTab.ResumeLayout(False)
        Me.SplitRulesGroupSpTab.ResumeLayout(False)
        Me.SpecialRulesTab.ResumeLayout(False)
        Me.PDTiesGroupSRTab.ResumeLayout(False)
        Me.ShoeOptionsTab.ResumeLayout(False)
        Me.ForcedShoeGroupShTab.ResumeLayout(False)
        Me.ReferenceShoeGroupShTab.ResumeLayout(False)
        Me.ShoeOptionGroupShTab.ResumeLayout(False)
        Me.BonusRulesTab.ResumeLayout(False)
        Me.BonusTabControlBTab.ResumeLayout(False)
        Me.BJBonusesTabBTab.ResumeLayout(False)
        Me.Spec10GroupBJTab.ResumeLayout(False)
        Me.GeneralBJGroupBJTab.ResumeLayout(False)
        Me.BonusRulesTabBTab.ResumeLayout(False)
        Me.BonusRuleDetailsGroupBTab.ResumeLayout(False)
        Me.ForcedTab.ResumeLayout(False)
        Me.ForcedStratTabControlFSTab.ResumeLayout(False)
        Me.OptionsTabFSTab.ResumeLayout(False)
        Me.HardSoftTDTabFSTab.ResumeLayout(False)
        Me.SoftTDGroupFSTab.ResumeLayout(False)
        Me.HardTDGroupFSTab.ResumeLayout(False)
        Me.SoftPairsCDTabFSTab.ResumeLayout(False)
        Me.PairCDGroupFSTab.ResumeLayout(False)
        Me.SoftCDGroupFSTab.ResumeLayout(False)
        Me.HardCDTabFSTab.ResumeLayout(False)
        Me.OtherTabFSTab.ResumeLayout(False)
        Me.ForcedRuleDetailsGroupFSTab.ResumeLayout(False)
        Me.OtherOptionsTab.ResumeLayout(False)
        Me.UCAllowedGroupOTab.ResumeLayout(False)
        Me.ColorTableGroupOTab.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " General Declarations "

    Private PreviousObject As System.Object
    Private PreviousGroup As System.Object
    Private BonusRule As New BJCABonusRulesClass
    Private ForcedRule As New BJCAForcedRulesClass

    Private Rules As New BJCARulesClass
    Private FormRules As New BJCAFormRulesClass
    Private DefaultFormRules As New BJCAFormRulesClass

    Private Constants As New BJCAGlobalsClass

    Private INIPath As String
    Private ProbsPath As String

#End Region

#Region " Tables Declarations "

    Private DChecksTotalsArray(17) As IndexedCheckBox
    Private DChecksArray(17, 3) As IndexedCheckBox
    Friend WithEvents DChecksTotalsArrayHandler As CheckBox
    Friend WithEvents DChecksArrayHandler As CheckBox
    Private SComboArray(9) As IndexedComboBox
    Friend WithEvents SComboArrayHandler As ComboBox
    Private PairChecksArray(9) As CheckBox
    Private UCChecksArray(9) As CheckBox

    Private ReferenceShoeArray(9) As IndexedTextBox
    Private ForcedShoeArray(9) As IndexedTextBox
    Private NetSuitsArray(9) As IndexedTextBox
    Private SpadesArray(9) As IndexedTextBox
    Private HeartsArray(9) As IndexedTextBox
    Private DiamondsArray(9) As IndexedTextBox
    Private ClubsArray(9) As IndexedTextBox
    Friend WithEvents ForcedShoeArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents SuitsArrayHandler As System.Windows.Forms.TextBox

    Private BonusRulesHandArray(9) As IndexedTextBox
    Friend WithEvents BonusRulesHandArrayHandler As System.Windows.Forms.TextBox

    Private StratColorBoxArray() As IndexedTextBox
    Friend WithEvents StratColorBoxArrayHandler As System.Windows.Forms.TextBox

    Private ForcedRulesHandArray(9) As IndexedTextBox
    Friend WithEvents ForcedRulesHandArrayHandler As System.Windows.Forms.TextBox

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

    Private PDTiesArray(5) As IndexedTextBox
    Friend WithEvents PDTiesArrayHandler As System.Windows.Forms.TextBox
    Private PDTiesComboArray(5) As IndexedComboBox
    Friend WithEvents PDTiesComboArrayHandler As System.Windows.Forms.ComboBox

#End Region

#Region " Main Form General Methods "

    Private Sub CalculateNow()
        Dim Results As New BJCA
        Dim ResultsForm As New BJCAResultsForm

        '       Try
        GetFormRules()
        Results.BJCA(Rules)

        ResultsForm.FormRules.FileNames = FormRules.FileNames
        ResultsForm.Results = Results
        ResultsForm.FormRules = CloneObject(FormRules)
        ResultsForm.LoadFormResults()

        ResultsForm.Show()
        '        Catch
        '        MsgBox("An error has occurred - please restart.")
        '        Me.Close()
        '        End Try
    End Sub

    Private Sub InitializeBJCAForm()
        '        RunTest()               'Used for debugging

        ReDim StratColorBoxArray(Constants.NumStrats)
        PopulateDCheckTotalsTable()
        PopulateDCheckTable()
        PopulateSComboTable()
        PopulateForcedShoeTables()
        PopulateSpCheckTable()
        PopulateUCCheckTable()
        PopulateBonusRulesTable()
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

        PopulateStratColorTable()
        PopulatePDTiesArray()
        PopulatePDTiesListArray()

        ClearBonusRule()
        LoadINI()
    End Sub

    Private Sub RestoreDefaultGeneralRules()
        FormRules.General = CloneObject(DefaultFormRules.General)
        LoadFormGeneralRules()
    End Sub

    Private Sub RestoreDefaultBonusRules()
        DeleteAllBonusRules()
        FormRules.BonusRulesList = CloneObject(DefaultFormRules.BonusRulesList)
        LoadFormBonusRulesList(FormRules.BonusRulesList)
        ClearBonusRule()
    End Sub

    Private Sub RestoreDefaultForcedTables()
        FormRules.ForcedStrat.ForcedTableRulesList = CloneObject(DefaultFormRules.ForcedStrat.ForcedTableRulesList)
        LoadFormForcedTables()
    End Sub

    Private Sub RestoreDefaultForcedRules()
        DeleteAllForcedRules()
        FormRules.ForcedStrat.ForcedRulesList = CloneObject(DefaultFormRules.ForcedStrat.ForcedRulesList)
        LoadFormForcedRulesList(FormRules.ForcedStrat.ForcedRulesList)
        ClearForcedRule()
    End Sub

    Private Sub RestoreDefaultColorTable()
        FormRules.ColorTable = CloneObject(DefaultFormRules.ColorTable)
        LoadFormColorTable()
    End Sub

    Private Sub RestoreDefaultDoubleTable()
        DToggleAllCheckDTab.Checked = True
        DToggleAllCheckDTab.Checked = False
        FormRules.DoubleTable = CloneObject(DefaultFormRules.DoubleTable)
    End Sub

    Private Sub RestoreDefaultForcedShoe()
        FormRules.ForcedShoe.Reset(DefaultFormRules.ForcedShoe)
        LoadFormForcedShoe()
    End Sub

    Private Sub RestoreDefaultFileSetNames()
        FormRules.FileNames = CloneObject(DefaultFormRules.FileNames)
    End Sub

    Private Sub RestoreCurrentDefaults()
        RestoreDefaultGeneralRules()
        RestoreDefaultBonusRules()
        RestoreDefaultForcedTables()
        RestoreDefaultForcedRules()
        RestoreDefaultColorTable()
        RestoreDefaultDoubleTable()
        RestoreDefaultForcedShoe()
        RestoreDefaultFileSetNames()
    End Sub

    Private Sub RestoreOriginalDefaults()
        DefaultFormRules = Nothing
        DefaultFormRules = New BJCAFormRulesClass

        RestoreDefaultGeneralRules()
        RestoreDefaultBonusRules()
        RestoreDefaultForcedTables()
        RestoreDefaultForcedRules()
        RestoreDefaultColorTable()
        RestoreDefaultDoubleTable()
        RestoreDefaultForcedShoe()
        RestoreDefaultFileSetNames()
    End Sub

    Public Sub GetFormGeneralRules()
        Dim i As Integer

        FormRules.General.FiniteDecks = FiniteDecksButtonMTab.Checked
        FormRules.General.InfiniteDecks = InfiniteDecksButtonMTab.Checked
        FormRules.General.UseForcedShoe = ForcedShoeCheckMTab.Checked
        FormRules.General.NDecks = NDecksBoxMTab.Text
        FormRules.General.SpanishDecks = SpanishDecksCheckBoxMTab.Checked

        FormRules.General.BJPays = BJPaysBoxMTab.Text
        FormRules.General.S17 = Stand17ButtonMTab.Checked
        FormRules.General.S18 = Stand18ButtonMTab.Checked

        FormRules.General.OBO = OBOButtonMTab.Checked
        FormRules.General.ENHC = ENHCButtonMTab.Checked
        FormRules.General.BBO = BBOButtonMTab.Checked
        FormRules.General.OBBO = OBBOButtonMTab.Checked
        FormRules.General.AOBBO = AOBBOButtonMTab.Checked
        FormRules.General.UseDefault = UseDefaultCheckMTab.Checked
        FormRules.General.CheckAce = CheckAceCheckMTab.Checked
        FormRules.General.CheckTen = CheckTenCheckMTab.Checked

        FormRules.General.DAS = DASCheckMTab.Checked
        FormRules.General.DAN = DANCheckMTab.Checked
        FormRules.General.DOA = DOAButtonMTab.Checked
        FormRules.General.D91011 = D91011ButtonMTab.Checked
        FormRules.General.D1011 = D1011ButtonMTab.Checked
        FormRules.General.UseDTable = DTableButtonMTab.Checked

        FormRules.General.NS = NSButtonMTab.Checked
        FormRules.General.LS = LSButtonMTab.Checked
        FormRules.General.ES = ESButtonMTab.Checked
        FormRules.General.ES10 = ES10ButtonMTab.Checked
        FormRules.General.UseSTable = STableButtonMTab.Checked
        FormRules.General.SurrPays = SurrPaysBoxMTab.Text

        FormRules.General.SPL0 = SPL0ButtonMTab.Checked
        FormRules.General.SPL1 = SPL1ButtonMTab.Checked
        FormRules.General.SPL2 = SPL2ButtonMTab.Checked
        FormRules.General.SPL3 = SPL3ButtonMTab.Checked
        FormRules.General.HSA = HSACheckMTab.Checked
        FormRules.General.SMA = SMACheckMTab.Checked

        FormRules.General.DSoftAllHard = DSoftAllHardCheckDTab.Checked
        FormRules.General.DSoft19Hard = DSoft19Hard9CheckDTab.Checked
        FormRules.General.DSA = DSACheckDTab.Checked
        FormRules.General.DDR = DDRCheckDTab.Checked
        FormRules.General.DDRPS = DDRPSCheckDTab.Checked
        If DDRLateButtonSTab.Checked Then
            FormRules.General.DDRType = Constants.Surr.LS
        Else
            FormRules.General.DDRType = Constants.Surr.ES
        End If
        FormRules.General.RDA = RDACheckDTab.Checked
        FormRules.General.RDAPS = RDAPSCheckDTab.Checked
        FormRules.General.RDDepth = CInt(RDDepthBoxDTab.Text)

        FormRules.General.SurrDBJPays = SurrDBJPaysBoxSTab.Text
        FormRules.General.SAN = SANCheckSTab.Checked
        FormRules.General.SAS = SASCheckSpTab.Checked
        FormRules.General.SSA = SSACheckSTab.Checked
        FormRules.General.MacauSurrender2to10 = MacauSurrender2to10CheckSTab.Checked
        FormRules.General.MacauSurrenderAce = MacauSurrenderAceCheckSTab.Checked
        FormRules.General.SurrToggleAll = ToggleAllComboSTab.SelectedIndex
        For i = 0 To 9
            FormRules.General.SurrComboValueArray(i) = SComboArray(i).SelectedIndex
        Next i

        FormRules.General.BJSPlitAces = BJSPlitAcesCheckSpTab.Checked
        FormRules.General.BJSplitTens = BJSplitTensCheckSpTab.Checked
        FormRules.General.SpToggleAll = ToggleAllCheckSpTab.Checked
        For i = 0 To 9
            FormRules.General.SpCheckValueArray(i) = PairChecksArray(i).Checked
        Next
        FormRules.General.CDZ = CDZButtonSpTab.Checked
        FormRules.General.CDP = CDPButtonSpTab.Checked
        FormRules.General.CDPN = CDPNButtonSpTab.Checked
        FormRules.General.TDPlus = TDPlusCheckSpTab.Checked
        FormRules.General.TCPlus = TCPlusCheckSpTab.Checked

        FormRules.General.P21Autowin = P21AutowinCheckBoxDTab.Checked
        FormRules.General.PDTiesToggleAll = PDTiesToggleAllBoxSRTab.SelectedIndex
        For i = 0 To 5
            FormRules.General.PDTiesComboValueArray(i) = PDTiesComboArray(i).SelectedIndex
            FormRules.General.PDTiesValueArray(i) = PDTiesArray(i).Text
        Next i

        FormRules.General.RefDecks = RefDecksBoxShTab.Text

        FormRules.General.BJBonuses.PayoffSuited = SuitedBJBoxBJTab.Text
        FormRules.General.BJBonuses.PayoffSpecificSuit = SpecificSuitBJBoxBJTab.Text
        If FormRules.General.BJBonuses.PayoffSuited <> 0 Then
            FormRules.General.BJBonuses.Suited = True
        Else
            FormRules.General.BJBonuses.Suited = False
        End If
        If FormRules.General.BJBonuses.PayoffSpecificSuit <> 0 Then
            FormRules.General.BJBonuses.SpecificSuit = True
            FormRules.General.BJBonuses.Suited = True
        Else
            FormRules.General.BJBonuses.SpecificSuit = False
        End If
        FormRules.General.BJBonuses.MustWin = SuitedBJMustWinCheckBJTab.Checked
        FormRules.General.BJBonuses.PayoffGeneralBJ = Spec10BoxlBJTab.Text
        FormRules.General.BJBonuses.PayoffSuitedBJ = Spec10SuitedBoxBJTab.Text
        FormRules.General.BJBonuses.PayoffSpecificSuitBJ = Spec10SpecSuitBoxBJTab.Text
        'PreSplit and PostSplit hold the Suited and Specific Suit values for the Specific Ten Bonuses
        If FormRules.General.BJBonuses.PayoffSuitedBJ <> 0 Then
            FormRules.General.BJBonuses.PreSplit = True
        Else
            FormRules.General.BJBonuses.PreSplit = False
        End If
        If FormRules.General.BJBonuses.PayoffSpecificSuitBJ <> 0 Then
            FormRules.General.BJBonuses.PostSplit = True
            FormRules.General.BJBonuses.PreSplit = True
        Else
            FormRules.General.BJBonuses.PostSplit = False
        End If
        FormRules.General.BJBonuses.SpecificTenFraction = Spec10FractionBoxBJTab.Text
        'HandContinues holds the MustWin value for Specific Ten Bonuses
        FormRules.General.BJBonuses.HandContinues = Spec10BJMustWinCheckBJTab.Checked

        If SpadesButtonBJTab.Checked Then
            FormRules.General.BJBonuses.SuitToWin = 0
        ElseIf HeartsButtonBJTab.Checked Then
            FormRules.General.BJBonuses.SuitToWin = 1
        ElseIf DiamondsButtonBJTab.Checked Then
            FormRules.General.BJBonuses.SuitToWin = 2
        Else
            FormRules.General.BJBonuses.SuitToWin = 3
        End If

        If Spec10SpadesButtonBJTab.Checked Then
            FormRules.General.BJBonuses.Upcard = 0
        ElseIf Spec10HeartsButtonBJTab.Checked Then
            FormRules.General.BJBonuses.Upcard = 1
        ElseIf Spec10DiamondsButtonBJTab.Checked Then
            FormRules.General.BJBonuses.Upcard = 2
        Else
            FormRules.General.BJBonuses.Upcard = 3
        End If

        FormRules.ForcedStrat.ForcednCD = ForcednCDBoxFSTab.Text
        FormRules.ForcedStrat.ForcedTablePreSplit = ForcedTablePreCheckFSTab.Checked
        FormRules.ForcedStrat.ForcedTablePostSplit = ForcedTablePostCheckFSTab.Checked

        FormRules.General.UCToggleAll = ToggleAllCheckOTab.Checked
        For i = 0 To 9
            FormRules.General.UCCheckValueArray(i) = UCChecksArray(i).Checked
        Next

        FormRules.General.ComputeTD = ComputeTDMenuItem.Checked
        FormRules.General.ComputeTC = ComputeTCMenuItem.Checked
        FormRules.General.ComputeForced = ComputeForcedMenuItem.Checked
        FormRules.General.PrintPSExceptions = PSExceptionsMenuItem.Checked
        FormRules.General.UseDPDictionary = UseDPDictionaryMenuItem.Checked
        FormRules.General.SaveDPDictionary = SaveDPDictionaryMenuItem.Checked
        FormRules.General.RTSPL1Est = RTSPL1EstMenuItem.Checked
        FormRules.General.RTSmall = RTSmallMenuItem.Checked
    End Sub

    Public Sub LoadFormGeneralRules()
        Dim i As Integer

        FiniteDecksButtonMTab.Checked = FormRules.General.FiniteDecks
        InfiniteDecksButtonMTab.Checked = FormRules.General.InfiniteDecks
        ForcedShoeCheckMTab.Checked = FormRules.General.UseForcedShoe
        NDecksBoxMTab.Text = CStr(FormRules.General.NDecks)
        NDecksBoxShTab.Text = NDecksBoxMTab.Text
        SpanishDecksCheckBoxMTab.Checked = FormRules.General.SpanishDecks

        BJPaysBoxMTab.Text = CStr(FormRules.General.BJPays)
        BJPaysBoxBJTab.Text = BJPaysBoxMTab.Text
        Stand17ButtonMTab.Checked = FormRules.General.S17
        Stand18ButtonMTab.Checked = FormRules.General.S18

        OBOButtonMTab.Checked = FormRules.General.OBO
        ENHCButtonMTab.Checked = FormRules.General.ENHC
        BBOButtonMTab.Checked = FormRules.General.BBO
        OBBOButtonMTab.Checked = FormRules.General.OBBO
        AOBBOButtonMTab.Checked = FormRules.General.AOBBO
        UseDefaultCheckMTab.Checked = FormRules.General.UseDefault
        CheckAceCheckMTab.Checked = FormRules.General.CheckAce
        CheckTenCheckMTab.Checked = FormRules.General.CheckTen

        DASCheckMTab.Checked = FormRules.General.DAS
        DANCheckMTab.Checked = FormRules.General.DAN
        DOAButtonMTab.Checked = FormRules.General.DOA
        D91011ButtonMTab.Checked = FormRules.General.D91011
        D1011ButtonMTab.Checked = FormRules.General.D1011
        DTableButtonMTab.Checked = FormRules.General.UseDTable

        NSButtonMTab.Checked = FormRules.General.NS
        LSButtonMTab.Checked = FormRules.General.LS
        ESButtonMTab.Checked = FormRules.General.ES
        ES10ButtonMTab.Checked = FormRules.General.ES10
        STableButtonMTab.Checked = FormRules.General.UseSTable
        SurrPaysBoxMTab.Text = CStr(FormRules.General.SurrPays)
        SurrPaysBoxSTab.Text = SurrPaysBoxMTab.Text
        SSACheckSTab.Checked = FormRules.General.SSA
        SurrDBJPaysBoxSTab.Text = CStr(FormRules.General.SurrDBJPays)
        If FormRules.General.SurrPays <> FormRules.General.SurrDBJPays Then
            SurrDBJCheckSTab.Checked = True
            SurrDBJPaysBoxSTab.Enabled = True
            SurrDBJPaysLabelSTab.Enabled = True
            To1DBJPaysLabelSTab.Enabled = True
        Else
            SurrDBJCheckSTab.Checked = False
            SurrDBJPaysBoxSTab.Enabled = False
            SurrDBJPaysLabelSTab.Enabled = False
            To1DBJPaysLabelSTab.Enabled = False
        End If

        SPL0ButtonMTab.Checked = FormRules.General.SPL0
        SPL1ButtonMTab.Checked = FormRules.General.SPL1
        SPL2ButtonMTab.Checked = FormRules.General.SPL2
        SPL3ButtonMTab.Checked = FormRules.General.SPL3
        HSACheckMTab.Checked = FormRules.General.HSA
        SMACheckMTab.Checked = FormRules.General.SMA

        DSoftAllHardCheckDTab.Checked = FormRules.General.DSoftAllHard
        DSoft19Hard9CheckDTab.Checked = FormRules.General.DSoft19Hard
        DSACheckDTab.Checked = FormRules.General.DSA
        DDRCheckDTab.Checked = FormRules.General.DDR
        DDRPSCheckDTab.Checked = FormRules.General.DDRPS
        If FormRules.General.DDR Then
            DDRPSCheckDTab.Enabled = True
            DDRPSCheckSTab.Enabled = True
            DDRPSCheckSpTab.Enabled = True
            DDRLateButtonDTab.Enabled = True
            DDRLateButtonSTab.Enabled = True
            If (FormRules.General.ENHC Or FormRules.General.BBO Or FormRules.General.OBBO Or FormRules.General.AOBBO) Then
                DDREarlyButtonDTab.Enabled = True
                DDREarlyButtonSTab.Enabled = True
            Else
                DDREarlyButtonDTab.Enabled = False
                DDREarlyButtonSTab.Enabled = False
            End If
        Else
            DDRPSCheckDTab.Enabled = False
            DDRPSCheckSTab.Enabled = False
            DDRPSCheckSpTab.Enabled = False
        End If

        If FormRules.General.DDRType = Constants.Surr.LS Then
            DDRLateButtonSTab.Checked = True
        Else
            DDREarlyButtonSTab.Checked = True
        End If

        RDACheckDTab.Checked = FormRules.General.RDA
        RDAPSCheckDTab.Checked = FormRules.General.RDAPS
        RDDepthBoxDTab.Text = CStr(FormRules.General.RDDepth)
        If FormRules.General.RDA Then
            RDDepthBoxDTab.Enabled = True
            RDDepthLabelDTab.Enabled = True
            RDAPSCheckDTab.Enabled = True
            RDAPSCheckSpTab.Enabled = True
        Else
            RDDepthBoxDTab.Enabled = False
            RDDepthLabelDTab.Enabled = False
            RDAPSCheckDTab.Enabled = False
            RDAPSCheckSpTab.Enabled = False
        End If

        SANCheckSTab.Checked = FormRules.General.SAN
        SASCheckSpTab.Checked = FormRules.General.SAS
        MacauSurrender2to10CheckSTab.Checked = FormRules.General.MacauSurrender2to10
        MacauSurrenderAceCheckSTab.Checked = FormRules.General.MacauSurrenderAce
        ToggleAllComboSTab.SelectedIndex = FormRules.General.SurrToggleAll
        For i = 0 To 9
            SComboArray(i).SelectedIndex = FormRules.General.SurrComboValueArray(i)
        Next i

        BJSPlitAcesCheckSpTab.Checked = FormRules.General.BJSPlitAces
        BJSplitTensCheckSpTab.Checked = FormRules.General.BJSplitTens
        BJSPlitAcesCheckBJTab.Checked = BJSPlitAcesCheckSpTab.Checked
        BJSPlitTensCheckBJTab.Checked = BJSplitTensCheckSpTab.Checked

        ToggleAllCheckSpTab.Checked = FormRules.General.SpToggleAll
        For i = 0 To 9
            PairChecksArray(i).Checked = FormRules.General.SpCheckValueArray(i)
        Next
        CDZButtonSpTab.Checked = FormRules.General.CDZ
        CDPButtonSpTab.Checked = FormRules.General.CDP
        CDPNButtonSpTab.Checked = FormRules.General.CDPN
        TDPlusCheckSpTab.Checked = FormRules.General.TDPlus
        TCPlusCheckSpTab.Checked = FormRules.General.TCPlus

        P21AutowinCheckBoxDTab.Checked = FormRules.General.P21Autowin
        PDTiesToggleAllBoxSRTab.SelectedIndex = FormRules.General.PDTiesToggleAll
        For i = 0 To 5
            PDTiesComboArray(i).SelectedIndex = FormRules.General.PDTiesComboValueArray(i)
            PDTiesArray(i).Text = CStr(FormRules.General.PDTiesValueArray(i))
            PDTiesArray(i).Index2 = FormRules.General.PDTiesValueArray(i)
        Next i

        RefDecksBoxShTab.Text = FormRules.General.RefDecks

        SuitedBJBoxBJTab.Text = CStr(FormRules.General.BJBonuses.PayoffSuited)
        SpecificSuitBJBoxBJTab.Text = CStr(FormRules.General.BJBonuses.PayoffSpecificSuit)
        If FormRules.General.BJBonuses.SuitToWin = 0 Then
            SpadesButtonBJTab.Checked = True
            SuitLabelBJTab.Text = "Spades Bonus"
        ElseIf FormRules.General.BJBonuses.SuitToWin = 1 Then
            HeartsButtonBJTab.Checked = True
            SuitLabelBJTab.Text = "Hearts Bonus"
        ElseIf FormRules.General.BJBonuses.SuitToWin = 2 Then
            DiamondsButtonBJTab.Checked = True
            SuitLabelBJTab.Text = "Diamonds Bonus"
        Else
            ClubsButtonBJTab.Checked = True
            SuitLabelBJTab.Text = "Clubs Bonus"
        End If
        If SpecificSuitBJBoxBJTab.Text = 0 Then
            SpadesButtonBJTab.Enabled = False
            HeartsButtonBJTab.Enabled = False
            DiamondsButtonBJTab.Enabled = False
            ClubsButtonBJTab.Enabled = False
        Else
            SpadesButtonBJTab.Enabled = True
            HeartsButtonBJTab.Enabled = True
            DiamondsButtonBJTab.Enabled = True
            ClubsButtonBJTab.Enabled = True
        End If

        Spec10BoxlBJTab.Text = CStr(FormRules.General.BJBonuses.PayoffGeneralBJ)
        Spec10SuitedBoxBJTab.Text = CStr(FormRules.General.BJBonuses.PayoffGeneral)
        Spec10SpecSuitBoxBJTab.Text = CStr(FormRules.General.BJBonuses.PayoffUCGeneral)
        Spec10FractionBoxBJTab.Text = CStr(FormRules.General.BJBonuses.SpecificTenFraction)
        If (Spec10BoxlBJTab.Text = 0 And Spec10SuitedBoxBJTab.Text = 0 And Spec10SpecSuitBoxBJTab.Text = 0) Then
            Spec10FractionBoxBJTab.Enabled = False
        Else
            Spec10FractionBoxBJTab.Enabled = True
        End If
        If FormRules.General.BJBonuses.Upcard = 0 Then
            Spec10SpadesButtonBJTab.Checked = True
            Spec10SuitLabelBJTab.Text = "Specific Ten Spades Bonus"
        ElseIf FormRules.General.BJBonuses.Upcard = 1 Then
            Spec10HeartsButtonBJTab.Checked = True
            Spec10SuitLabelBJTab.Text = "Specific Ten Hearts Bonus"
        ElseIf FormRules.General.BJBonuses.Upcard = 2 Then
            Spec10DiamondsButtonBJTab.Checked = True
            Spec10SuitLabelBJTab.Text = "Specific Ten Diamonds Bonus"
        Else
            Spec10ClubsButtonBJTab.Checked = True
            Spec10SuitLabelBJTab.Text = "Specific Ten Clubs Bonus"
        End If
        If Spec10SpecSuitBoxBJTab.Text = 0 Then
            Spec10SpadesButtonBJTab.Enabled = False
            Spec10HeartsButtonBJTab.Enabled = False
            Spec10DiamondsButtonBJTab.Enabled = False
            Spec10ClubsButtonBJTab.Enabled = False
        Else
            Spec10SpadesButtonBJTab.Enabled = True
            Spec10HeartsButtonBJTab.Enabled = True
            Spec10DiamondsButtonBJTab.Enabled = True
            Spec10ClubsButtonBJTab.Enabled = True
        End If

        ForcednCDBoxFSTab.Text = FormRules.ForcedStrat.ForcednCD
        ForcedTablePreCheckFSTab.Checked = FormRules.ForcedStrat.ForcedTablePreSplit
        ForcedTablePostCheckFSTab.Checked = FormRules.ForcedStrat.ForcedTablePostSplit

        ToggleAllCheckOTab.Checked = FormRules.General.UCToggleAll
        For i = 0 To 9
            UCChecksArray(i).Checked = FormRules.General.UCCheckValueArray(i)
        Next

        ComputeTDMenuItem.Checked = FormRules.General.ComputeTD
        ComputeTCMenuItem.Checked = FormRules.General.ComputeTC
        ComputeForcedMenuItem.Checked = FormRules.General.ComputeForced
        PSExceptionsMenuItem.Checked = FormRules.General.PrintPSExceptions
        UseDPDictionaryMenuItem.Checked = FormRules.General.UseDPDictionary
        SaveDPDictionaryMenuItem.Checked = FormRules.General.SaveDPDictionary
        RTSPL1EstMenuItem.Checked = FormRules.General.RTSPL1Est
        RTSmallMenuItem.Checked = FormRules.General.RTSmall

    End Sub

    Private Sub GetFormRules()
        Dim card As Integer
        Dim total As Integer
        Dim column As Integer
        Dim prePost As Integer

        GetFormGeneralRules()
        GetFormForcedTables()
        GetFormDoubleTable()
        GetFormForcedShoe()

        With Rules
            GetBonusRulesOn()
            .BonusRulesList = CloneObject(FormRules.BonusRulesList)
            .ForcedTableRulesList = CloneObject(FormRules.ForcedStrat.ForcedTableRulesList)
            GetForcedRulesOn()
            .ForcedRulesList = CloneObject(FormRules.ForcedStrat.ForcedRulesList)
            .ColorTable = CloneObject(FormRules.ColorTable)

            .InfiniteDecks = FormRules.General.InfiniteDecks
            If FormRules.General.UseForcedShoe Then
                .Shoe = CloneObject(FormRules.ForcedShoe)
                If FormRules.General.InfiniteDecks Then
                    .DeckType = "Inf Forced"
                    For card = 1 To 10
                        .Shoe.Cards(card) *= 1000
                        .Shoe.Suits(card, 0) *= 1000
                        .Shoe.Suits(card, 1) *= 1000
                        .Shoe.Suits(card, 2) *= 1000
                        .Shoe.Suits(card, 3) *= 1000
                    Next card
                    .Shoe.CardsLeft *= 1000
                Else
                    .DeckType = "Forced"
                End If
            Else
                If FormRules.General.SpanishDecks Then
                    If FormRules.General.InfiniteDecks Then
                        .Shoe.Reset(1000)
                        .Shoe.DealSuited(10, 0, 1000)
                        .Shoe.DealSuited(10, 1, 1000)
                        .Shoe.DealSuited(10, 2, 1000)
                        .Shoe.DealSuited(10, 3, 1000)
                        .DeckType = "Inf Sp"
                    Else
                        .Shoe.Reset(FormRules.General.NDecks)
                        .Shoe.DealSuited(10, 0, FormRules.General.NDecks)
                        .Shoe.DealSuited(10, 1, FormRules.General.NDecks)
                        .Shoe.DealSuited(10, 2, FormRules.General.NDecks)
                        .Shoe.DealSuited(10, 3, FormRules.General.NDecks)
                        .DeckType = CStr(FormRules.General.NDecks) + " Sp"
                    End If
                Else
                    If FormRules.General.InfiniteDecks Then
                        .Shoe.Reset(1000)
                        .DeckType = "Inf"
                    Else
                        .Shoe.Reset(FormRules.General.NDecks)
                        .DeckType = CStr(FormRules.General.NDecks)
                    End If
                End If
            End If

            .BJPays = FormRules.General.BJPays
            If FormRules.General.S17 Then
                .StandOnSoft = 17
            Else
                .StandOnSoft = 18
            End If
            .ENHC = FormRules.General.ENHC
            .BBO = FormRules.General.BBO
            .OBBO = FormRules.General.OBBO
            .AOBBO = FormRules.General.AOBBO
            .CheckAce = FormRules.General.CheckAce
            .CheckTen = FormRules.General.CheckTen

            If FormRules.General.DOA Then
                .DoubleType = "DOA"
            ElseIf FormRules.General.D91011 Then
                .DoubleType = "D9"
            ElseIf FormRules.General.D1011 Then
                .DoubleType = "D10"
            ElseIf FormRules.General.UseDTable Then
                .DoubleType = "Table"
            Else
                .DoubleType = "False"
            End If
            .DAN = FormRules.General.DAN

            For total = 4 To 21
                If FormRules.General.DOA Or (FormRules.General.D91011 And (total = 9 Or total = 10 Or total = 11)) Or (FormRules.General.D1011 And (total = 10 Or total = 11)) Then
                    .DoubleTable(total, 0, 0) = True
                    .DoubleTable(total, 2, 0) = True
                    If FormRules.General.DAS Then
                        .DoubleTable(total, 0, 1) = True
                        .DoubleTable(total, 2, 1) = True
                    Else
                        .DoubleTable(total, 0, 1) = False
                        .DoubleTable(total, 2, 1) = False
                    End If
                    If FormRules.General.DAN Then
                        .DoubleTable(total, 1, 0) = True
                        .DoubleTable(total, 3, 0) = True
                        If FormRules.General.DAS Then
                            .DoubleTable(total, 1, 1) = True
                            .DoubleTable(total, 3, 1) = True
                        Else
                            .DoubleTable(total, 1, 1) = False
                            .DoubleTable(total, 3, 1) = False
                        End If
                    Else
                        .DoubleTable(total, 1, 0) = False
                        .DoubleTable(total, 3, 0) = False
                        .DoubleTable(total, 1, 1) = False
                        .DoubleTable(total, 3, 1) = False
                    End If
                ElseIf FormRules.General.UseDTable Then
                    For column = 0 To 3
                        For prePost = 0 To 1
                            .DoubleTable(total, column, 0) = FormRules.DoubleTable.T(total - 4, column)
                            If FormRules.General.DAS Then
                                .DoubleTable(total, column, 1) = FormRules.DoubleTable.T(total - 4, column)
                            Else
                                .DoubleTable(total, column, 1) = False
                            End If
                        Next prePost
                    Next column
                Else
                    For column = 0 To 3
                        For prePost = 0 To 1
                            .DoubleTable(total, column, prePost) = False
                        Next prePost
                    Next column
                End If
            Next total

            .DAS = FormRules.General.DAS
            .DSoftAllHard = FormRules.General.DSoftAllHard
            .DSoft19Hard = FormRules.General.DSoft19Hard
            .DSA = FormRules.General.DSA
            .DDR = FormRules.General.DDR
            .DDRType = FormRules.General.DDRType
            .DDRPS = FormRules.General.DDRPS
            .RDA = FormRules.General.RDA
            .RDAPS = FormRules.General.RDAPS
            .RDDepth = FormRules.General.RDDepth

            If FormRules.General.SPL0 Then
                .SPL = 0
            ElseIf FormRules.General.SPL1 Then
                .SPL = 1
            ElseIf FormRules.General.SPL2 Then
                .SPL = 2
            Else   'SPL3
                .SPL = 3
            End If
            .HSA = FormRules.General.HSA
            .SMA = FormRules.General.SMA

            .SurrPays = FormRules.General.SurrPays
            .SurrDBJPays = FormRules.General.SurrDBJPays
            .SAN = FormRules.General.SAN
            .SAS = FormRules.General.SAS
            .SSA = FormRules.General.SSA
            .MacauSurrender2to10 = FormRules.General.MacauSurrender2to10
            .MacauSurrenderAce = FormRules.General.MacauSurrenderAce

            If FormRules.General.NS Then
                .SurrType = "None"
            ElseIf FormRules.General.LS Then
                .SurrType = "Late"
            ElseIf FormRules.General.ES Then
                .SurrType = "Early"
            ElseIf FormRules.General.ES10 Then
                .SurrType = "ES10"
            Else
                .SurrType = "Table"
            End If

            For card = 1 To 10
                If FormRules.General.NS Then
                    .SurrenderTable(card) = BJCAGlobalsClass.Surr.NS
                ElseIf FormRules.General.LS Then
                    .SurrenderTable(card) = BJCAGlobalsClass.Surr.LS
                ElseIf FormRules.General.ES Then
                    .SurrenderTable(card) = BJCAGlobalsClass.Surr.ES
                ElseIf FormRules.General.ES10 Then
                    If card = 10 Then
                        .SurrenderTable(card) = BJCAGlobalsClass.Surr.ES
                    ElseIf card = 1 Then
                        .SurrenderTable(card) = BJCAGlobalsClass.Surr.NS
                    Else
                        .SurrenderTable(card) = BJCAGlobalsClass.Surr.LS
                    End If
                Else    'Use SurrenderTable
                    .SurrenderTable(card) = FormRules.General.SurrComboValueArray(card - 1)
                End If
            Next card

            .BJSPlitAces = FormRules.General.BJSPlitAces
            .BJSplitTens = FormRules.General.BJSplitTens

            For card = 1 To 10
                .SplitAllowed(card) = FormRules.General.SpCheckValueArray(card - 1)
            Next card

            .CDP = FormRules.General.CDP
            .CDPN = FormRules.General.CDPN
            .TDPlus = FormRules.General.TDPlus
            .TCPlus = FormRules.General.TCPlus

            .P21Autowin = FormRules.General.P21Autowin
            For total = 17 To 22
                .PDTies(total) = FormRules.General.PDTiesValueArray(total - 17)
            Next total

            .BJBonuses = CloneObject(FormRules.General.BJBonuses)

            .ForcednCD = FormRules.ForcedStrat.ForcednCD
            .ForcedTablePreSplit = FormRules.ForcedStrat.ForcedTablePreSplit
            .ForcedTablePostSplit = FormRules.ForcedStrat.ForcedTablePostSplit

            For card = 1 To 10
                .UCAllowed(card) = FormRules.General.UCCheckValueArray(card - 1)
            Next card

            .ComputeTD = FormRules.General.ComputeTD
            .ComputeTC = FormRules.General.ComputeTC
            .ComputeForced = FormRules.General.ComputeForced
            .PrintPSExceptions = FormRules.General.PrintPSExceptions
            .UseDPDictionary = FormRules.General.UseDPDictionary
            .SaveDPDictionary = FormRules.General.SaveDPDictionary

            .OutputPath = FormRules.FileNames.OutputPath
            .ExcelFilePath = GetPath(INIPath) + "MGPs BJ CA Results.xls"
            .ProbsPath = ProbsPath

        End With
    End Sub

    Private Sub LoadFormRules()
        Dim card As Integer
        Dim total As Integer
        Dim column As Integer
        Dim prePost As Integer

        LoadFormGeneralRules()
        LoadFormForcedTables()
        LoadFormDoubleTable()
        LoadFormForcedShoe()
        LoadFormBonusRulesList(FormRules.BonusRulesList)
        LoadFormForcedRulesList(FormRules.ForcedStrat.ForcedRulesList)
        LoadFormColorTable()

    End Sub

#End Region

#Region " Main Rules Tab "

    Private Sub LoadResultsButtonMTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadResultsButtonMTab.Click
        Dim ResultsForm As New BJCAResultsForm

        ResultsForm.Show()
    End Sub

    Private Sub FiniteDecksButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiniteDecksButtonMTab.CheckedChanged
        If FiniteDecksButtonMTab.Checked = True And ForcedShoeCheckMTab.Checked = False Then
            NDecksLabelMTab.Enabled() = True
            NDecksBoxMTab.Enabled = True
        Else
            NDecksLabelMTab.Enabled() = False
            NDecksBoxMTab.Enabled = False
        End If
        If ForcedShoeCheckMTab.Checked Then
            ReferenceShoeGroupShTab.Enabled = True
            ForcedShoeGroupShTab.Enabled = True
            SpanishDecksCheckBoxMTab.Enabled = False
        Else
            ReferenceShoeGroupShTab.Enabled = False
            ForcedShoeGroupShTab.Enabled = False
            SpanishDecksCheckBoxMTab.Enabled = True
        End If
        If FiniteDecksButtonMTab.Checked <> FiniteDecksButtonShTab.Checked Then
            FiniteDecksButtonShTab.Checked = FiniteDecksButtonMTab.Checked
        End If
    End Sub

    Private Sub InfiniteDecksButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfiniteDecksButtonMTab.CheckedChanged
        If FiniteDecksButtonMTab.Checked = True And ForcedShoeCheckMTab.Checked = False Then
            NDecksLabelMTab.Enabled() = True
            NDecksBoxMTab.Enabled = True
        Else
            NDecksLabelMTab.Enabled() = False
            NDecksBoxMTab.Enabled = False
        End If
        If ForcedShoeCheckMTab.Checked Then
            ReferenceShoeGroupShTab.Enabled = True
            ForcedShoeGroupShTab.Enabled = True
            SpanishDecksCheckBoxMTab.Enabled = False
        Else
            ReferenceShoeGroupShTab.Enabled = False
            ForcedShoeGroupShTab.Enabled = False
            SpanishDecksCheckBoxMTab.Enabled = True
        End If
        If InfiniteDecksButtonMTab.Checked <> InfiniteDecksButtonShTab.Checked Then
            InfiniteDecksButtonShTab.Checked = InfiniteDecksButtonMTab.Checked
        End If
    End Sub

    Private Sub ForcedShoeCheckMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedShoeCheckMTab.CheckedChanged
        If FiniteDecksButtonMTab.Checked = True And ForcedShoeCheckMTab.Checked = False Then
            NDecksLabelMTab.Enabled() = True
            NDecksBoxMTab.Enabled = True
        Else
            NDecksLabelMTab.Enabled() = False
            NDecksBoxMTab.Enabled = False
        End If
        If ForcedShoeCheckMTab.Checked Then
            ReferenceShoeGroupShTab.Enabled = True
            ForcedShoeGroupShTab.Enabled = True
            SpanishDecksCheckBoxMTab.Enabled = False
        Else
            ReferenceShoeGroupShTab.Enabled = False
            ForcedShoeGroupShTab.Enabled = False
            SpanishDecksCheckBoxMTab.Enabled = True
        End If
        If ForcedShoeCheckMTab.Checked <> ForcedShoeCheckShTab.Checked Then
            ForcedShoeCheckShTab.Checked = ForcedShoeCheckMTab.Checked
        End If
    End Sub

    Private Sub NDecksBoxMTab_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NDecksBoxMTab.Validating
        If CheckValidInteger(NDecksBoxMTab.Text, 1, Constants.MaxDecks, True) Then
            NDecksBoxShTab.Text = NDecksBoxMTab.Text
        Else
            NDecksBoxMTab.Text = NDecksBoxShTab.Text
            e.Cancel = True
        End If
    End Sub

    Private Sub SpanishDecksCheckBoxMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpanishDecksCheckBoxMTab.CheckedChanged
        If SpanishDecksCheckBoxShTab.Checked <> SpanishDecksCheckBoxMTab.Checked Then
            SpanishDecksCheckBoxShTab.Checked = SpanishDecksCheckBoxMTab.Checked
        End If
    End Sub

    Private Sub OBOButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OBOButtonMTab.CheckedChanged
        If OBOButtonMTab.Checked Then
            UseDefaultCheckMTab.Checked = True
            CheckAceCheckMTab.Checked = True
            CheckTenCheckMTab.Checked = True
            DDRLateButtonDTab.Checked = True
            DDREarlyButtonDTab.Enabled = False
            DDREarlyButtonSTab.Enabled = False
        End If
        If (RDACheckDTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("Redoubling cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            RDACheckDTab.Checked = False
        End If
        If (CDPButtonSpTab.Checked Or CDPNButtonSpTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("CD-P and CD-PN cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            CDZButtonSpTab.Checked = True
        End If
    End Sub

    Private Sub ENHCButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENHCButtonMTab.CheckedChanged
        If ENHCButtonMTab.Checked And UseDefaultCheckMTab.Checked Then
            CheckAceCheckMTab.Checked = False
            CheckTenCheckMTab.Checked = False
        End If
        If (ENHCButtonMTab.Checked Or BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) And DDRCheckDTab.Checked Then
            DDREarlyButtonDTab.Enabled = True
            DDREarlyButtonSTab.Enabled = True
        End If
        If (RDACheckDTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("Redoubling cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            RDACheckDTab.Checked = False
        End If
        If (CDPButtonSpTab.Checked Or CDPNButtonSpTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("CD-P and CD-PN cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            CDZButtonSpTab.Checked = True
        End If
    End Sub

    Private Sub BBOButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BBOButtonMTab.CheckedChanged
        If BBOButtonMTab.Checked Then
            UseDefaultCheckMTab.Checked = True
            CheckAceCheckMTab.Checked = False
            CheckTenCheckMTab.Checked = False
        End If
        If (ENHCButtonMTab.Checked Or BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) And DDRCheckDTab.Checked Then
            DDREarlyButtonDTab.Enabled = True
            DDREarlyButtonSTab.Enabled = True
        End If
        If (RDACheckDTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("Redoubling cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            RDACheckDTab.Checked = False
        End If
        If (CDPButtonSpTab.Checked Or CDPNButtonSpTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("CD-P and CD-PN cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            CDZButtonSpTab.Checked = True
        End If
    End Sub

    Private Sub OBBOButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OBBOButtonMTab.CheckedChanged
        If OBBOButtonMTab.Checked Then
            UseDefaultCheckMTab.Checked = True
            CheckAceCheckMTab.Checked = False
            CheckTenCheckMTab.Checked = False
        End If
        If (ENHCButtonMTab.Checked Or BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) And DDRCheckDTab.Checked Then
            DDREarlyButtonDTab.Enabled = True
            DDREarlyButtonSTab.Enabled = True
        End If
        If (RDACheckDTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("Redoubling cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            RDACheckDTab.Checked = False
        End If
        If (CDPButtonSpTab.Checked Or CDPNButtonSpTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("CD-P and CD-PN cannot be used with BBO And OBBO.", MsgBoxStyle.OKOnly)
            CDZButtonSpTab.Checked = True
        End If
    End Sub

    Private Sub AOBBOButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AOBBOButtonMTab.CheckedChanged
        If BBOButtonMTab.Checked Then
            UseDefaultCheckMTab.Checked = True
            CheckAceCheckMTab.Checked = False
            CheckTenCheckMTab.Checked = False
        End If
        If (ENHCButtonMTab.Checked Or BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) And DDRCheckDTab.Checked Then
            DDREarlyButtonDTab.Enabled = True
            DDREarlyButtonSTab.Enabled = True
        End If
        If (RDACheckDTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("Redoubling cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            RDACheckDTab.Checked = False
        End If
        If (CDPButtonSpTab.Checked Or CDPNButtonSpTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            MsgBox("CD-P and CD-PN cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
            CDZButtonSpTab.Checked = True
        End If
    End Sub

    Private Sub UseDefaultCheckMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseDefaultCheckMTab.CheckedChanged
        If (OBOButtonMTab.Checked) Then
            CheckAceCheckMTab.Checked = True
            CheckTenCheckMTab.Checked = True
            UseDefaultCheckMTab.Checked = True
        ElseIf (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
            CheckAceCheckMTab.Checked = False
            CheckTenCheckMTab.Checked = False
            UseDefaultCheckMTab.Checked = True
        Else
            CheckAceCheckMTab.Checked = False
            CheckTenCheckMTab.Checked = False
        End If
        If UseDefaultCheckMTab.Checked Then
            CheckAceCheckMTab.Enabled = False
            CheckTenCheckMTab.Enabled = False
        Else
            CheckAceCheckMTab.Enabled = True
            CheckTenCheckMTab.Enabled = True
        End If
    End Sub

    Private Sub DASCheckMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DASCheckMTab.CheckedChanged
        If DASCheckMTab.Checked <> DASCheckDTab.Checked Then
            DASCheckDTab.Checked = DASCheckMTab.Checked
            DASCheckSpTab.Checked = DASCheckMTab.Checked
        End If
    End Sub

    Private Sub DANCheckMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DANCheckMTab.CheckedChanged
        If DANCheckMTab.Checked <> DANCheckDTab.Checked Then
            DANCheckDTab.Checked = DANCheckMTab.Checked
        End If
    End Sub

    Private Sub DOAButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DOAButtonMTab.CheckedChanged
        If DOAButtonMTab.Checked <> DOAButtonDTab.Checked Then
            DOAButtonDTab.Checked = DOAButtonMTab.Checked
        End If
        If DTableButtonMTab.Checked Then
            DoubleTableGroup.Enabled = True
            DANCheckMTab.Enabled = False
            DANCheckDTab.Enabled = False
        Else
            DoubleTableGroup.Enabled = False
            DANCheckMTab.Enabled = True
            DANCheckDTab.Enabled = True
        End If
    End Sub

    Private Sub D91011ButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles D91011ButtonMTab.CheckedChanged
        If D91011ButtonMTab.Checked <> D91011ButtonDTab.Checked Then
            D91011ButtonDTab.Checked = D91011ButtonMTab.Checked
        End If
        If DTableButtonMTab.Checked Then
            DoubleTableGroup.Enabled = True
            DANCheckMTab.Enabled = False
            DANCheckDTab.Enabled = False
        Else
            DoubleTableGroup.Enabled = False
            DANCheckMTab.Enabled = True
            DANCheckDTab.Enabled = True
        End If
    End Sub

    Private Sub D1011ButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles D1011ButtonMTab.CheckedChanged
        If D1011ButtonMTab.Checked <> D1011ButtonDTab.Checked Then
            D1011ButtonDTab.Checked = D1011ButtonMTab.Checked
        End If
        If DTableButtonMTab.Checked Then
            DoubleTableGroup.Enabled = True
            DANCheckMTab.Enabled = False
            DANCheckDTab.Enabled = False
        Else
            DoubleTableGroup.Enabled = False
            DANCheckMTab.Enabled = True
            DANCheckDTab.Enabled = True
        End If
    End Sub

    Private Sub DTableButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTableButtonMTab.CheckedChanged
        If DTableButtonMTab.Checked <> DTableButtonDTab.Checked Then
            DTableButtonDTab.Checked = DTableButtonMTab.Checked
        End If
        If DTableButtonMTab.Checked Then
            DoubleTableGroup.Enabled = True
            DANCheckMTab.Enabled = False
            DANCheckDTab.Enabled = False
        Else
            DoubleTableGroup.Enabled = False
            DANCheckMTab.Enabled = True
            DANCheckDTab.Enabled = True
        End If
    End Sub

    Private Sub BJPaysBoxMTab_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles BJPaysBoxMTab.Validating
        If Not CheckValidDecimal(BJPaysBoxMTab.Text, -1, 5, True) Then
            BJPaysBoxMTab.Text = BJPaysBoxBJTab.Text
            e.Cancel = True
        Else
            BJPaysBoxBJTab.Text = BJPaysBoxMTab.Text
        End If
        If PDTiesComboArray(5).SelectedIndex = 2 Then
            PDTiesArray(5).Text = BJPaysBoxMTab.Text
        End If
    End Sub

    Private Sub SurrPaysBoxMTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SurrPaysBoxMTab.Validating
        If CheckValidDecimal(SurrPaysBoxMTab.Text, -1, 1, True) Then
            If SurrPaysBoxMTab.Text <> SurrPaysBoxSTab.Text Then
                SurrPaysBoxSTab.Text = SurrPaysBoxMTab.Text
            End If
            If Not SurrDBJCheckSTab.Checked Then
                SurrDBJPaysBoxSTab.Text = SurrPaysBoxMTab.Text
            End If
        Else
            SurrPaysBoxMTab.Text = SurrPaysBoxSTab.Text
            e.Cancel = True
        End If
    End Sub

    Private Sub NSButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NSButtonMTab.CheckedChanged
        If NSButtonMTab.Checked <> NSButtonSTab.Checked Then
            NSButtonSTab.Checked = NSButtonMTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub LSButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LSButtonMTab.CheckedChanged
        If LSButtonMTab.Checked <> LSButtonSTab.Checked Then
            LSButtonSTab.Checked = LSButtonMTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub ESButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ESButtonMTab.CheckedChanged
        If ESButtonMTab.Checked <> ESButtonSTab.Checked Then
            ESButtonSTab.Checked = ESButtonMTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub STableButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STableButtonMTab.CheckedChanged
        If STableButtonMTab.Checked <> STableButtonSTab.Checked Then
            STableButtonSTab.Checked = STableButtonMTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub SPL0ButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPL0ButtonMTab.CheckedChanged
        If SPL0ButtonMTab.Checked <> SPL0ButtonSpTab.Checked Then
            SPL0ButtonSpTab.Checked = SPL0ButtonMTab.Checked
        End If
    End Sub

    Private Sub SPL1ButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPL1ButtonMTab.CheckedChanged
        If SPL1ButtonMTab.Checked <> SPL1ButtonSpTab.Checked Then
            SPL1ButtonSpTab.Checked = SPL1ButtonMTab.Checked
        End If
    End Sub

    Private Sub SPL2ButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPL2ButtonMTab.CheckedChanged
        If SPL2ButtonMTab.Checked <> SPL2ButtonSpTab.Checked Then
            SPL2ButtonSpTab.Checked = SPL2ButtonMTab.Checked
        End If
    End Sub

    Private Sub SPL3ButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPL3ButtonMTab.CheckedChanged
        If SPL3ButtonMTab.Checked <> SPL3ButtonSpTab.Checked Then
            SPL3ButtonSpTab.Checked = SPL3ButtonMTab.Checked
        End If
    End Sub

    Private Sub HSACheckMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HSACheckMTab.CheckedChanged
        If HSACheckMTab.Checked <> HSACheckSpTab.Checked Then
            HSACheckSpTab.Checked = HSACheckMTab.Checked
        End If
    End Sub

    Private Sub SMACheckMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMACheckMTab.CheckedChanged
        If SMACheckMTab.Checked <> SMACheckSpTab.Checked Then
            SMACheckSpTab.Checked = SMACheckMTab.Checked
        End If
    End Sub

#End Region

#Region " Double Tab "

    Private Sub PopulateDCheckTotalsTable()
        Dim row As Integer

        For row = 0 To 17
            Dim box As New IndexedCheckBox

            box.Location = New System.Drawing.Point(40, 88 + 20 * row)
            box.Size = New System.Drawing.Size(40, 20)
            box.Text = row + 4
            box.Index = row

            'Add the CheckBox to the array so it can be accessed by index.
            Me.DChecksTotalsArray(row) = box

            'Add the CheckBox to the Controls collection so it is visible.
            Me.DoubleTableGroup.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.CheckedChanged, AddressOf DChecksTotalsArrayHandler_CheckedChanged
        Next row
    End Sub

    Private Sub PopulateDCheckTable()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 17
            For column = 0 To 3
                If (row < 8 And column < 2) Or (row >= 8) Then
                    Dim box As New IndexedCheckBox

                    box.Index = row
                    box.Index2 = column
                    Select Case column
                        Case 0
                            box.Location = New System.Drawing.Point(112, 88 + 20 * row)
                            box.Text = ""
                            box.Size = New System.Drawing.Size(16, 20)
                        Case 1
                            box.Location = New System.Drawing.Point(168, 88 + 20 * row)
                            box.Text = ""
                            box.Size = New System.Drawing.Size(16, 20)
                        Case 2
                            box.Location = New System.Drawing.Point(224, 88 + 20 * row)
                            box.Text = ""
                            box.Size = New System.Drawing.Size(16, 20)
                        Case 3
                            box.Location = New System.Drawing.Point(280, 88 + 20 * row)
                            box.Text = ""
                            box.Size = New System.Drawing.Size(16, 20)
                    End Select

                    If ((row = 0 Or row = 1) And column = 1) Or (row = 8 And column = 3) Then
                        box.Visible = False
                    End If

                    'Add the CheckBox to the array so it can be accessed by index.
                    Me.DChecksArray(row, column) = box

                    'Add the CheckBox to the Controls collection so it is visible.
                    Me.DoubleTableGroup.Controls.Add(box)

                    'Add Handler to the general handler
                    AddHandler box.CheckedChanged, AddressOf DChecksArrayHandler_CheckedChanged
                End If
            Next column
        Next row
        LoadFormDoubleTable()
    End Sub

    Private Sub DChecksTotalsArrayHandler_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DChecksTotalsArrayHandler.CheckedChanged
        Dim row As Integer
        Dim column As Integer

        row = DirectCast(sender, IndexedCheckBox).Index
        For column = 0 To 3
            If ((row < 8 And column < 2) Or (row >= 8)) And Not (((row = 0 Or row = 1) And column = 1) Or (row = 8 And column = 3)) Then
                DChecksArray(row, column).Checked = DChecksTotalsArray(row).Checked
            End If
        Next column
    End Sub

    Private Sub DChecksArrayHandler_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DChecksArrayHandler.CheckedChanged
        Dim row As Integer

        row = DirectCast(sender, IndexedCheckBox).Index
        Select Case row
            Case Is < 2
                If (DChecksArray(row, 0).Checked) Then 'And DChecksArray(row, 1).Checked And DChecksArray(row, 2).Checked And DChecksArray(row, 3).Checked) Then
                    DChecksTotalsArray(row).Checked = True
                End If
            Case Is < 8
                If (DChecksArray(row, 0).Checked And DChecksArray(row, 1).Checked) Then
                    DChecksTotalsArray(row).Checked = True
                End If
            Case 8
                If (DChecksArray(row, 0).Checked And DChecksArray(row, 1).Checked And DChecksArray(row, 2).Checked) Then
                    DChecksTotalsArray(row).Checked = True
                End If
            Case Else
                If (DChecksArray(row, 0).Checked And DChecksArray(row, 1).Checked And DChecksArray(row, 2).Checked And DChecksArray(row, 3).Checked) Then
                    DChecksTotalsArray(row).Checked = True
                End If
        End Select
    End Sub

    Private Sub DToggleAllCheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DToggleAllCheckDTab.CheckedChanged
        Dim row As Integer

        For row = 0 To 17
            DChecksTotalsArray(row).Checked = DToggleAllCheckDTab.Checked
        Next row
    End Sub

    Private Sub GetFormDoubleTable()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 17
            For column = 0 To 3
                Try
                    FormRules.DoubleTable.T(row, column) = DChecksArray(row, column).Checked
                Catch
                End Try
            Next column
        Next row
    End Sub

    Private Sub LoadFormDoubleTable()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 17
            For column = 0 To 3
                Try
                    DChecksArray(row, column).Checked = FormRules.DoubleTable.T(row, column)
                Catch
                End Try
            Next column
        Next row
    End Sub

    Private Sub DASCheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DASCheckDTab.CheckedChanged
        If DASCheckMTab.Checked <> DASCheckDTab.Checked Then
            DASCheckMTab.Checked = DASCheckDTab.Checked
            DASCheckSpTab.Checked = DASCheckDTab.Checked
        End If
    End Sub

    Private Sub DANCheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DANCheckDTab.CheckedChanged
        If DANCheckMTab.Checked <> DANCheckDTab.Checked Then
            DANCheckMTab.Checked = DANCheckDTab.Checked
        End If
    End Sub

    Private Sub DOAButtonDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DOAButtonDTab.CheckedChanged
        If DOAButtonMTab.Checked <> DOAButtonDTab.Checked Then
            DOAButtonMTab.Checked = DOAButtonDTab.Checked
        End If
        If DTableButtonMTab.Checked Then
            DoubleTableGroup.Enabled = True
            DANCheckMTab.Enabled = False
            DANCheckDTab.Enabled = False
        Else
            DoubleTableGroup.Enabled = False
            DANCheckMTab.Enabled = True
            DANCheckDTab.Enabled = True
        End If
    End Sub

    Private Sub D91011ButtonDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles D91011ButtonDTab.CheckedChanged
        If D91011ButtonMTab.Checked <> D91011ButtonDTab.Checked Then
            D91011ButtonMTab.Checked = D91011ButtonDTab.Checked
        End If
        If DTableButtonMTab.Checked Then
            DoubleTableGroup.Enabled = True
            DANCheckMTab.Enabled = False
            DANCheckDTab.Enabled = False
        Else
            DoubleTableGroup.Enabled = False
            DANCheckMTab.Enabled = True
            DANCheckDTab.Enabled = True
        End If
    End Sub

    Private Sub D1011ButtonDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles D1011ButtonDTab.CheckedChanged
        If D1011ButtonMTab.Checked <> D1011ButtonDTab.Checked Then
            D1011ButtonMTab.Checked = D1011ButtonDTab.Checked
        End If
        If DTableButtonMTab.Checked Then
            DoubleTableGroup.Enabled = True
            DANCheckMTab.Enabled = False
            DANCheckDTab.Enabled = False
        Else
            DoubleTableGroup.Enabled = False
            DANCheckMTab.Enabled = True
            DANCheckDTab.Enabled = True
        End If
    End Sub

    Private Sub DTableButtonDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTableButtonDTab.CheckedChanged
        If DTableButtonMTab.Checked <> DTableButtonDTab.Checked Then
            DTableButtonMTab.Checked = DTableButtonDTab.Checked
        End If
        If DTableButtonMTab.Checked Then
            DoubleTableGroup.Enabled = True
            DANCheckMTab.Enabled = False
            DANCheckDTab.Enabled = False
        Else
            DoubleTableGroup.Enabled = False
            DANCheckMTab.Enabled = True
            DANCheckDTab.Enabled = True
        End If
    End Sub

    Private Sub DSACheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DSACheckDTab.CheckedChanged
        If DSACheckDTab.Checked <> DSACheckSpTab.Checked Then
            DSACheckSpTab.Checked = DSACheckDTab.Checked
        End If
    End Sub

    Private Sub DSoftAllHardCheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DSoftAllHardCheckDTab.CheckedChanged
        If DSoftAllHardCheckDTab.Checked Then
            DSoft19Hard9CheckDTab.Checked = True
            '            DDRCheckDTab.Checked = False
            RDACheckDTab.Checked = False
        Else
            DSoft19Hard9CheckDTab.Checked = False
        End If
    End Sub

    Private Sub DSoft19Hard9CheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DSoft19Hard9CheckDTab.CheckedChanged
        If DSoftAllHardCheckDTab.Checked Then
            DSoft19Hard9CheckDTab.Checked = True
        End If
        If DSoft19Hard9CheckDTab.Checked Then
            RDACheckDTab.Checked = False
        End If
    End Sub

    Private Sub P21AutowinCheckBoxDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P21AutowinCheckBoxDTab.CheckedChanged
        If P21AutowinCheckBoxDTab.Checked <> P21AutowinCheckBoxSRTab.Checked Then
            P21AutowinCheckBoxSRTab.Checked = P21AutowinCheckBoxDTab.Checked
        End If
    End Sub

    Private Sub DDRCheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDRCheckDTab.CheckedChanged
        If DDRCheckSTab.Checked <> DDRCheckDTab.Checked Then
            DDRCheckSTab.Checked = DDRCheckDTab.Checked
        End If
        If DDRCheckDTab.Checked Then
            CDZButtonSpTab.Checked = True
            DDRPSCheckDTab.Enabled = True
            DDRPSCheckSTab.Enabled = True
            DDRPSCheckSpTab.Enabled = True
            DDRLateButtonDTab.Enabled = True
            DDRLateButtonSTab.Enabled = True
            If (ENHCButtonMTab.Checked Or BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
                DDRLateButtonDTab.Checked = True
                DDRLateButtonSTab.Checked = True
                DDREarlyButtonDTab.Enabled = True
                DDREarlyButtonSTab.Enabled = True
            Else
                DDREarlyButtonDTab.Enabled = False
                DDREarlyButtonSTab.Enabled = False
            End If
        Else
            DDRPSCheckDTab.Enabled = False
            DDRPSCheckSTab.Enabled = False
            DDRPSCheckSpTab.Enabled = False
            DDRPSCheckDTab.Checked = False
            DDRLateButtonDTab.Enabled = False
            DDRLateButtonSTab.Enabled = False
            DDREarlyButtonDTab.Enabled = False
            DDREarlyButtonSTab.Enabled = False
        End If
    End Sub

    Private Sub DDRPSCheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDRPSCheckDTab.CheckedChanged
        If DDRPSCheckDTab.Checked <> DDRPSCheckSpTab.Checked Then
            DDRPSCheckSTab.Checked = DDRPSCheckDTab.Checked
            DDRPSCheckSpTab.Checked = DDRPSCheckDTab.Checked
        End If
    End Sub

    Private Sub DDRLateButtonDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDRLateButtonDTab.CheckedChanged
        If DDRLateButtonSTab.Checked <> DDRLateButtonDTab.Checked Then
            DDRLateButtonSTab.Checked = DDRLateButtonDTab.Checked
        End If
    End Sub

    Private Sub DDREarlyButtonDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDREarlyButtonDTab.CheckedChanged
        If DDREarlyButtonSTab.Checked <> DDREarlyButtonDTab.Checked Then
            DDREarlyButtonSTab.Checked = DDREarlyButtonDTab.Checked
        End If
    End Sub

    Private Sub RDAPSCheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDAPSCheckDTab.CheckedChanged
        If RDAPSCheckDTab.Checked <> RDAPSCheckSpTab.Checked Then
            RDAPSCheckSpTab.Checked = RDAPSCheckDTab.Checked
        End If
    End Sub

    Private Sub RDACheckDTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDACheckDTab.CheckedChanged
        If RDACheckDTab.Checked Then
            If (RDACheckDTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked) Then
                MsgBox("Redoubling cannot be used with BBO, OBBO or AOBBO.", MsgBoxStyle.OKOnly)
                RDACheckDTab.Checked = False
            Else
                RDDepthBoxDTab.Enabled = True
                RDDepthLabelDTab.Enabled = True
                RDDepthBoxDTab.Text = 1
                DSoftAllHardCheckDTab.Checked = False
                DSoft19Hard9CheckDTab.Checked = False
                CDZButtonSpTab.Checked = True
                RDAPSCheckDTab.Enabled = True
                RDAPSCheckSpTab.Enabled = True
            End If
        Else
            RDAPSCheckDTab.Enabled = False
            RDAPSCheckSpTab.Enabled = False
            RDAPSCheckDTab.Checked = False
            RDDepthBoxDTab.Enabled = False
            RDDepthLabelDTab.Enabled = False
            RDDepthBoxDTab.Text = 1
        End If
    End Sub

    Private Sub RDDepthBoxDTab_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles RDDepthBoxDTab.Validating
        If Not CheckValidInteger(RDDepthBoxDTab.Text, 0, 18, True) Then
            RDDepthBoxDTab.Text = 1
            e.Cancel = True
        End If
    End Sub

#End Region

#Region " Surrender Tab "

    Private Sub PopulateSComboTable()
        Dim row As Integer

        For row = 0 To 9 Step 1
            Dim box As New IndexedComboBox

            If row = 0 Then
                box.Location = New System.Drawing.Point(152, 104 + 32 * 9)
            Else
                box.Location = New System.Drawing.Point(152, 104 + 32 * (row - 1))
            End If
            box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            box.Items.AddRange(New Object() {"No Surrender", "Late Surrender", "Early Surrender"})
            box.Size = New System.Drawing.Size(124, 24)
            box.Index = row + 1

            'Add the CheckBox to the array so it can be accessed by index.
            Me.SComboArray(row) = box

            'Add Handler to the general handler
            AddHandler box.SelectionChangeCommitted, AddressOf SComboArrayHandler_SelectionChangeCommitted

            'Add the CheckBox to the Controls collection so it is visible.
            Me.SurrenderTableGroup.Controls.Add(box)
        Next row
    End Sub

    Private Sub SComboArrayHandler_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SComboArrayHandler.SelectionChangeCommitted
        SurrenderTableGroup.Focus()
    End Sub

    Private Sub ToggleAllComboSTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToggleAllComboSTab.SelectedIndexChanged
        Dim row As Integer

        For row = 0 To 9 Step 1
            SComboArray(row).SelectedIndex = ToggleAllComboSTab.SelectedIndex
        Next row
        SurrenderTableGroup.Focus()
    End Sub

    Private Sub SurrPaysBoxSTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SurrPaysBoxSTab.Validating
        If CheckValidDecimal(SurrPaysBoxSTab.Text, -1, 1, True) Then
            SurrPaysBoxMTab.Text = SurrPaysBoxSTab.Text
            If Not SurrDBJCheckSTab.Checked Then
                SurrDBJPaysBoxSTab.Text = SurrPaysBoxSTab.Text
            End If
        Else
            SurrPaysBoxSTab.Text = SurrPaysBoxMTab.Text
            e.Cancel = True
        End If
    End Sub

    Private Sub SurrDBJPaysBoxSTab_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SurrDBJPaysBoxSTab.Validating
        If Not CheckValidDecimal(SurrDBJPaysBoxSTab.Text, -1, 1, True) Then
            SurrDBJPaysBoxSTab.Text = SurrPaysBoxSTab.Text
            e.Cancel = True
        End If
    End Sub

    Private Sub SurrDBJBonusCheckSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SurrDBJCheckSTab.CheckedChanged
        If SurrDBJCheckSTab.Checked Then
            SurrDBJPaysBoxSTab.Enabled = True
            SurrDBJPaysLabelSTab.Enabled = True
            To1DBJPaysLabelSTab.Enabled = True
        Else
            SurrDBJPaysBoxSTab.Enabled = False
            SurrDBJPaysLabelSTab.Enabled = False
            To1DBJPaysLabelSTab.Enabled = False
            SurrDBJPaysBoxSTab.Text = SurrPaysBoxSTab.Text
        End If
    End Sub

    Private Sub NSButtonSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NSButtonSTab.CheckedChanged
        If NSButtonMTab.Checked <> NSButtonSTab.Checked Then
            NSButtonMTab.Checked = NSButtonSTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub LSButtonSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LSButtonSTab.CheckedChanged
        If LSButtonMTab.Checked <> LSButtonSTab.Checked Then
            LSButtonMTab.Checked = LSButtonSTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub ESButtonSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ESButtonSTab.CheckedChanged
        If ESButtonMTab.Checked <> ESButtonSTab.Checked Then
            ESButtonMTab.Checked = ESButtonSTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub ES10ButtonMTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ES10ButtonMTab.CheckedChanged
        If ES10ButtonMTab.Checked <> ES10ButtonSTab.Checked Then
            ES10ButtonSTab.Checked = ES10ButtonMTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub ES10ButtonSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ES10ButtonSTab.CheckedChanged
        If ES10ButtonMTab.Checked <> ES10ButtonSTab.Checked Then
            ES10ButtonMTab.Checked = ES10ButtonSTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub STableButtonSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STableButtonSTab.CheckedChanged
        If STableButtonMTab.Checked <> STableButtonSTab.Checked Then
            STableButtonMTab.Checked = STableButtonSTab.Checked
        End If
        If STableButtonMTab.Checked Then
            SurrenderTableGroup.Enabled = True
        Else
            SurrenderTableGroup.Enabled = False
        End If
    End Sub

    Private Sub SASCheckSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SASCheckSTab.CheckedChanged
        If SASCheckSTab.Checked <> SASCheckSpTab.Checked Then
            SASCheckSpTab.Checked = SASCheckSTab.Checked
        End If
    End Sub

    Private Sub SSACheckSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SSACheckSTab.CheckedChanged
        If SSACheckSTab.Checked <> SSACheckSpTab.Checked Then
            SSACheckSpTab.Checked = SSACheckSTab.Checked
        End If
    End Sub

    Private Sub DDRCheckSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDRCheckSTab.CheckedChanged
        If DDRCheckSTab.Checked <> DDRCheckDTab.Checked Then
            DDRCheckDTab.Checked = DDRCheckSTab.Checked
        End If
        If DDRCheckSTab.Checked Then
            CDZButtonSpTab.Checked = True
            DDRPSCheckDTab.Enabled = True
            DDRPSCheckSTab.Enabled = True
            DDRPSCheckSpTab.Enabled = True
            DDRLateButtonDTab.Enabled = True
            DDRLateButtonSTab.Enabled = True
            If (ENHCButtonMTab.Checked Or BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked) Then
                DDRLateButtonDTab.Checked = True
                DDRLateButtonSTab.Checked = True
                DDREarlyButtonDTab.Enabled = True
                DDREarlyButtonSTab.Enabled = True
            Else
                DDREarlyButtonDTab.Enabled = False
                DDREarlyButtonSTab.Enabled = False
            End If
        Else
            DDRPSCheckDTab.Enabled = False
            DDRPSCheckSTab.Enabled = False
            DDRPSCheckSpTab.Enabled = False
            DDRPSCheckDTab.Checked = False
            DDRLateButtonDTab.Enabled = False
            DDRLateButtonSTab.Enabled = False
            DDREarlyButtonDTab.Enabled = False
            DDREarlyButtonSTab.Enabled = False
        End If
    End Sub

    Private Sub DDRPSCheckSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDRPSCheckSTab.CheckedChanged
        If DDRPSCheckDTab.Checked <> DDRPSCheckSTab.Checked Then
            DDRPSCheckDTab.Checked = DDRPSCheckSTab.Checked
            DDRPSCheckSpTab.Checked = DDRPSCheckSTab.Checked
        End If
    End Sub

    Private Sub DDRLateButtonSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDRLateButtonSTab.CheckedChanged
        If DDRLateButtonSTab.Checked <> DDRLateButtonDTab.Checked Then
            DDRLateButtonDTab.Checked = DDRLateButtonSTab.Checked
        End If
    End Sub

    Private Sub DDREarlyButtonSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDREarlyButtonSTab.CheckedChanged
        If DDREarlyButtonSTab.Checked <> DDREarlyButtonDTab.Checked Then
            DDREarlyButtonDTab.Checked = DDREarlyButtonSTab.Checked
        End If
    End Sub

#End Region

#Region " Split Tab "

    Private Sub PopulateSpCheckTable()
        Dim row As Integer

        For row = 0 To 9 Step 1
            Dim box As New CheckBox

            box.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            box.Size = New System.Drawing.Size(48, 16)
            If row = 0 Then
                box.Text = "Ace"
                box.Location = New System.Drawing.Point(130, 95 + 20 * 9)
            ElseIf row = 9 Then
                box.Text = "Ten"
                box.Location = New System.Drawing.Point(130, 95 + 20 * (row - 1))
            Else
                box.Text = row + 1
                box.Location = New System.Drawing.Point(130, 95 + 20 * (row - 1))
            End If

            'Add the CheckBox to the array so it can be accessed by index.
            Me.PairChecksArray(row) = box

            'Add the CheckBox to the Controls collection so it is visible.
            Me.SplitPairsAllowedGroupSpTab.Controls.Add(box)

        Next row
    End Sub

    Private Sub ToggleAllCheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToggleAllCheckSpTab.CheckedChanged
        Dim row As Integer

        For row = 0 To 9 Step 1
            PairChecksArray(row).Checked = ToggleAllCheckSpTab.Checked
        Next row

    End Sub

    Private Sub SASCheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SASCheckSpTab.CheckedChanged
        If SASCheckSTab.Checked <> SASCheckSpTab.Checked Then
            SASCheckSTab.Checked = SASCheckSpTab.Checked
        End If
    End Sub

    Private Sub CDZButtonSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDZButtonSpTab.CheckedChanged
        If (CDPButtonSpTab.Checked Or CDPNButtonSpTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked Or DDRPSCheckDTab.Checked Or RDAPSCheckDTab.Checked) Then
            MsgBox("CD-P and CD-PN cannot be used with BBO, OBBO, AOBBO, DDR post-split or RDA post-split.", MsgBoxStyle.OKOnly)
            CDZButtonSpTab.Checked = True
        End If
        If Not CDZButtonSpTab.Checked Then
            DDRPSCheckDTab.Checked = False
            RDAPSCheckDTab.Checked = False
        End If
    End Sub

    Private Sub CDPButtonSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDPButtonSpTab.CheckedChanged
        If (CDPButtonSpTab.Checked Or CDPNButtonSpTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked Or DDRPSCheckDTab.Checked Or RDAPSCheckDTab.Checked) Then
            MsgBox("CD-P and CD-PN cannot be used with BBO, OBBO, AOBBO, DDR post-split or RDA post-split.", MsgBoxStyle.OKOnly)
            CDZButtonSpTab.Checked = True
        End If
    End Sub

    Private Sub CDPNButtonSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDPNButtonSpTab.CheckedChanged
        If (CDPButtonSpTab.Checked Or CDPNButtonSpTab.Checked) And (BBOButtonMTab.Checked Or OBBOButtonMTab.Checked Or AOBBOButtonMTab.Checked Or DDRPSCheckDTab.Checked Or RDAPSCheckDTab.Checked) Then
            MsgBox("CD-P and CD-PN cannot be used with BBO, OBBO, AOBBO, DDR post-split or RDA post-split.", MsgBoxStyle.OKOnly)
            CDZButtonSpTab.Checked = True
        End If
    End Sub

    Private Sub SPL0ButtonSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPL0ButtonSpTab.CheckedChanged
        If SPL0ButtonMTab.Checked <> SPL0ButtonSpTab.Checked Then
            SPL0ButtonMTab.Checked = SPL0ButtonSpTab.Checked
        End If
    End Sub

    Private Sub SPL1ButtonSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPL1ButtonSpTab.CheckedChanged
        If SPL1ButtonMTab.Checked <> SPL1ButtonSpTab.Checked Then
            SPL1ButtonMTab.Checked = SPL1ButtonSpTab.Checked
        End If
    End Sub

    Private Sub SPL2ButtonSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPL2ButtonSpTab.CheckedChanged
        If SPL2ButtonMTab.Checked <> SPL2ButtonSpTab.Checked Then
            SPL2ButtonMTab.Checked = SPL2ButtonSpTab.Checked
        End If
    End Sub

    Private Sub SPL3ButtonSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPL3ButtonSpTab.CheckedChanged
        If SPL3ButtonMTab.Checked <> SPL3ButtonSpTab.Checked Then
            SPL3ButtonMTab.Checked = SPL3ButtonSpTab.Checked
        End If
    End Sub

    Private Sub HSACheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HSACheckSpTab.CheckedChanged
        If HSACheckMTab.Checked <> HSACheckSpTab.Checked Then
            HSACheckMTab.Checked = HSACheckSpTab.Checked
        End If
    End Sub

    Private Sub DSACheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DSACheckSpTab.CheckedChanged
        If DSACheckDTab.Checked <> DSACheckSpTab.Checked Then
            DSACheckDTab.Checked = DSACheckSpTab.Checked
        End If
    End Sub

    Private Sub SSACheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SSACheckSpTab.CheckedChanged
        If SSACheckSTab.Checked <> SSACheckSpTab.Checked Then
            SSACheckSTab.Checked = SSACheckSpTab.Checked
        End If
    End Sub

    Private Sub SMACheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMACheckSpTab.CheckedChanged
        If SMACheckMTab.Checked <> SMACheckSpTab.Checked Then
            SMACheckMTab.Checked = SMACheckSpTab.Checked
        End If
    End Sub

    Private Sub BJSPlitAcesCheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BJSPlitAcesCheckSpTab.CheckedChanged
        BJSPlitAcesCheckBJTab.Checked = BJSPlitAcesCheckSpTab.Checked
    End Sub

    Private Sub BJSplitTensCheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BJSplitTensCheckSpTab.CheckedChanged
        BJSPlitTensCheckBJTab.Checked = BJSplitTensCheckSpTab.Checked
    End Sub

    Private Sub DASCheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DASCheckSpTab.CheckedChanged
        If DASCheckMTab.Checked <> DASCheckSpTab.Checked Then
            DASCheckMTab.Checked = DASCheckSpTab.Checked
            DASCheckDTab.Checked = DASCheckSpTab.Checked
        End If
    End Sub

    Private Sub RDAPSCheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDAPSCheckSpTab.CheckedChanged
        If RDAPSCheckDTab.Checked <> RDAPSCheckSpTab.Checked Then
            RDAPSCheckDTab.Checked = RDAPSCheckSpTab.Checked
        End If
    End Sub

    Private Sub DDRPSCheckSpTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDRPSCheckSpTab.CheckedChanged
        If DDRPSCheckDTab.Checked <> DDRPSCheckSpTab.Checked Then
            DDRPSCheckDTab.Checked = DDRPSCheckSpTab.Checked
            DDRPSCheckSTab.Checked = DDRPSCheckSpTab.Checked
        End If
    End Sub

#End Region

#Region " Special Rules Tab "

    Private Sub PopulatePDTiesArray()
        Dim row As Integer

        For row = 0 To 5 Step 1
            Dim box As New IndexedTextBox

            box.Size = New System.Drawing.Size(80, 24)
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Location = New System.Drawing.Point(230, 120 + row * 24)
            box.Enabled = False
            box.Text = 0
            box.Index = row
            box.Index2 = 0

            'Add the CheckBox to the array so it can be accessed by index.
            Me.PDTiesArray(row) = box

            'Add the CheckBox to the Controls collection so it is visible.
            Me.PDTiesGroupSRTab.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.Validating, AddressOf PDTiesArrayHandler_Validating

        Next row
    End Sub

    Private Sub PDTiesArrayHandler_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PDTiesArrayHandler.Validating
        If CheckValidDecimal(PDTiesArray(DirectCast(sender, IndexedTextBox).Index).Text, -5, 5, True) Then
            PDTiesArray(DirectCast(sender, IndexedTextBox).Index).Index2 = PDTiesArray(DirectCast(sender, IndexedTextBox).Index).Text
        Else
            PDTiesArray(DirectCast(sender, IndexedTextBox).Index).Text = 0
            e.Cancel = True
        End If
    End Sub

    Private Sub PopulatePDTiesListArray()
        Dim row As Integer

        For row = 0 To 5 Step 1
            Dim box As New IndexedComboBox

            box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            box.Items.AddRange(New Object() {"Push", "Dealer Wins", "Player Wins", "Other"})
            box.Location = New System.Drawing.Point(111, 120 + row * 24)
            box.Size = New System.Drawing.Size(112, 24)
            box.SelectedIndex = 0
            box.Index = row

            'Add the CheckBox to the array so it can be accessed by index.
            Me.PDTiesComboArray(row) = box

            'Add the CheckBox to the Controls collection so it is visible.
            Me.PDTiesGroupSRTab.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.SelectedIndexChanged, AddressOf PDTiesComboArray_SelectedIndexChanged

        Next row
    End Sub

    Private Sub PDTiesComboArray_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDTiesComboArrayHandler.SelectedIndexChanged
        Select Case DirectCast(sender, IndexedComboBox).SelectedIndex
            Case 0
                PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Text = 0
                PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Enabled = False
            Case 1
                PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Text = -1
                PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Enabled = False
            Case 2
                If DirectCast(sender, IndexedComboBox).Index = 5 Then
                    PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Text = BJPaysBoxMTab.Text
                Else
                    PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Text = 1
                End If
                PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Enabled = False
            Case Else
                PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Text = PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Index2
                PDTiesArray(DirectCast(sender, IndexedComboBox).Index).Enabled = True
        End Select
        PDTiesGroupSRTab.Focus()
    End Sub

    Private Sub PDTiesToggleAllBoxSRTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDTiesToggleAllBoxSRTab.SelectedIndexChanged
        Dim row As Integer

        For row = 0 To 5
            PDTiesComboArray(row).SelectedIndex = PDTiesToggleAllBoxSRTab.SelectedIndex
        Next row
    End Sub

    Private Sub P21AutowinCheckBoxSRTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P21AutowinCheckBoxSRTab.CheckedChanged
        If P21AutowinCheckBoxDTab.Checked <> P21AutowinCheckBoxSRTab.Checked Then
            P21AutowinCheckBoxDTab.Checked = P21AutowinCheckBoxSRTab.Checked
        End If
    End Sub

#End Region

#Region " Shoe Tab "

    Private Sub PopulateForcedShoeTables()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 6 Step 1
            For column = 0 To 9 Step 1
                Dim box As New IndexedTextBox

                Select Case row
                    Case 0      'Reference Shoe Row
                        box.ImeMode = System.Windows.Forms.ImeMode.On
                        box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                        box.Location = New System.Drawing.Point(152 + 56 * column, 65)
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Enabled = False
                        box.Index = column

                        Me.ReferenceShoeArray(column) = box
                        Me.ReferenceShoeGroupShTab.Controls.Add(box)

                    Case 1      'Forced Shoe Row
                        box.ImeMode = System.Windows.Forms.ImeMode.On
                        box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                        box.Location = New System.Drawing.Point(152 + 56 * column, 55)
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Index = column

                        Me.ForcedShoeArray(column) = box
                        Me.ForcedShoeGroupShTab.Controls.Add(box)

                        'Add Handler to the general handler
                        AddHandler box.Validating, AddressOf ForcedShoeArrayHandler_Validating

                    Case 2      'Net Suits Row
                        box.ImeMode = System.Windows.Forms.ImeMode.On
                        box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                        box.Location = New System.Drawing.Point(152 + 56 * column, 104 + 24 * (row - 2))
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Enabled = False
                        box.Index = column

                        Me.NetSuitsArray(column) = box
                        Me.ForcedShoeGroupShTab.Controls.Add(box)

                    Case 3      'Spades Row
                        box.ImeMode = System.Windows.Forms.ImeMode.On
                        box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                        box.Location = New System.Drawing.Point(152 + 56 * column, 104 + 24 * (row - 2))
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Index = column
                        box.Index2 = 0

                        Me.SpadesArray(column) = box
                        Me.ForcedShoeGroupShTab.Controls.Add(box)

                        'Add Handler to the general handler
                        AddHandler box.Validating, AddressOf SuitsArrayHandler_Validating

                    Case 4      'Hearts Row
                        box.ImeMode = System.Windows.Forms.ImeMode.On
                        box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                        box.Location = New System.Drawing.Point(152 + 56 * column, 104 + 24 * (row - 2))
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Index = column
                        box.Index2 = 1

                        Me.HeartsArray(column) = box
                        Me.ForcedShoeGroupShTab.Controls.Add(box)

                        'Add Handler to the general handler
                        AddHandler box.Validating, AddressOf SuitsArrayHandler_Validating

                    Case 5      'Diamonds Row
                        box.ImeMode = System.Windows.Forms.ImeMode.On
                        box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                        box.Location = New System.Drawing.Point(152 + 56 * column, 104 + 24 * (row - 2))
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Index = column
                        box.Index2 = 2

                        Me.DiamondsArray(column) = box
                        Me.ForcedShoeGroupShTab.Controls.Add(box)

                        'Add Handler to the general handler
                        AddHandler box.Validating, AddressOf SuitsArrayHandler_Validating

                    Case 6      'Clubs Row
                        box.ImeMode = System.Windows.Forms.ImeMode.On
                        box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
                        box.Location = New System.Drawing.Point(152 + 56 * column, 104 + 24 * (row - 2))
                        box.Size = New System.Drawing.Size(48, 20)
                        box.Index = column
                        box.Index2 = 3

                        Me.ClubsArray(column) = box
                        Me.ForcedShoeGroupShTab.Controls.Add(box)

                        'Add Handler to the general handler
                        AddHandler box.Validating, AddressOf SuitsArrayHandler_Validating
                End Select
            Next column
        Next row
    End Sub

    Private Function CalcNetForcedShoeCards() As Integer
        Dim card As Integer

        CalcNetForcedShoeCards = 0
        For card = 0 To 9
            CalcNetForcedShoeCards += ForcedShoeArray(card).Text
        Next card
    End Function

    Private Function CalcNetSuitCards(ByVal card As Integer) As Integer
        CalcNetSuitCards = SpadesArray(card).Text
        CalcNetSuitCards += HeartsArray(card).Text
        CalcNetSuitCards += DiamondsArray(card).Text
        CalcNetSuitCards += ClubsArray(card).Text

        If CalcNetSuitCards <> ForcedShoeArray(card).Text Then
            NetSuitsArray(card).BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(192, Byte))
        Else
            NetSuitsArray(card).BackColor = Nothing
        End If
    End Function

    Private Sub ForcedShoeArrayHandler_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ForcedShoeArrayHandler.Validating
        Dim max As Integer

        If DirectCast(sender, IndexedTextBox).Index = 9 Then
            max = Constants.MaxDecks * 16
        Else
            max = Constants.MaxDecks * 4
        End If
        If Not CheckValidInteger(DirectCast(sender, TextBox).Text, 0, max, True) Then
            DirectCast(sender, IndexedTextBox).Text = NetSuitsArray(DirectCast(sender, IndexedTextBox).Index).Text
            e.Cancel = True
        Else
            NetForcedCardsBoxShTab.Text = CalcNetForcedShoeCards()
            RestoreSuitDefaults(DirectCast(sender, IndexedTextBox).Index)
        End If
    End Sub

    Private Sub CopyRefShoe()
        Dim card As Integer

        For card = 0 To 9
            ForcedShoeArray(card).Text = ReferenceShoeArray(card).Text
        Next card
        NetForcedCardsBoxShTab.Text = CalcNetForcedShoeCards()
    End Sub

    Private Sub RestoreSuitDefaults(ByVal card As Integer)
        Dim defaultnum As Integer

        defaultnum = CInt(ForcedShoeArray(card).Text) \ 4
        If CInt(ForcedShoeArray(card).Text) Mod 4 > 0 Then
            SpadesArray(card).Text = defaultnum + 1
        Else
            SpadesArray(card).Text = defaultnum
        End If

        If CInt(ForcedShoeArray(card).Text) Mod 4 > 1 Then
            HeartsArray(card).Text = defaultnum + 1
        Else
            HeartsArray(card).Text = defaultnum
        End If

        If CInt(ForcedShoeArray(card).Text) Mod 4 > 2 Then
            DiamondsArray(card).Text = defaultnum + 1
        Else
            DiamondsArray(card).Text = defaultnum
        End If

        ClubsArray(card).Text = defaultnum
        NetSuitsArray(card).Text = CalcNetSuitCards(card)
    End Sub

    Private Sub RestoreSuitsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreSuitsButtonShTab.Click
        Dim card As Integer

        For card = 0 To 9
            RestoreSuitDefaults(card)
        Next card
    End Sub

    Private Sub SuitsArrayHandler_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SuitsArrayHandler.Validating
        Dim defaultnum As Integer

        If CheckValidInteger(DirectCast(sender, TextBox).Text, 0, ForcedShoeArray(DirectCast(sender, IndexedTextBox).Index).Text, True) Then
            NetSuitsArray(DirectCast(sender, IndexedTextBox).Index).Text = CalcNetSuitCards(DirectCast(sender, IndexedTextBox).Index)
        Else
            defaultnum = CInt(ForcedShoeArray(DirectCast(sender, IndexedTextBox).Index).Text) \ 4
            Select Case CInt(ForcedShoeArray(DirectCast(sender, IndexedTextBox).Index).Text) Mod 4
                Case 1
                    If DirectCast(sender, IndexedTextBox).Index2 < 1 Then
                        defaultnum += 1
                    End If
                Case 2
                    If DirectCast(sender, IndexedTextBox).Index2 < 2 Then
                        defaultnum += 1
                    End If
                Case 3
                    If DirectCast(sender, IndexedTextBox).Index2 < 3 Then
                        defaultnum += 1
                    End If
            End Select
            DirectCast(sender, IndexedTextBox).Text = defaultnum
            NetSuitsArray(DirectCast(sender, IndexedTextBox).Index).Text = CalcNetSuitCards(DirectCast(sender, IndexedTextBox).Index)

            e.Cancel = True
        End If
    End Sub

    Private Sub ForcedShoeGroupShTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ForcedShoeGroupShTab.Validating
        'Makes sure all the suits in the forced shoe are filled appropriately
        Dim card As Integer
        Dim valid As Boolean

        valid = True
        For card = 0 To 9
            If NetSuitsArray(card).Text <> ForcedShoeArray(card).Text Then
                valid = False
                Exit For
            End If
        Next card

        If Not valid Then
            MsgBox("The net forced shoe suit values must match the forced shoe values.", MsgBoxStyle.OKOnly)
            e.Cancel = True
        End If

    End Sub

    Private Sub GetFormForcedShoe()
        Dim card As Integer

        For card = 1 To 10
            FormRules.ForcedShoe.Cards(card) = ForcedShoeArray(card - 1).Text
            FormRules.ForcedShoe.Suits(card, BJCAGlobalsClass.Suits.Spades) = SpadesArray(card - 1).Text
            FormRules.ForcedShoe.Suits(card, BJCAGlobalsClass.Suits.Hearts) = HeartsArray(card - 1).Text
            FormRules.ForcedShoe.Suits(card, BJCAGlobalsClass.Suits.Diamonds) = DiamondsArray(card - 1).Text
            FormRules.ForcedShoe.Suits(card, BJCAGlobalsClass.Suits.Clubs) = ClubsArray(card - 1).Text
        Next card
        FormRules.ForcedShoe.CardsLeft = CInt(NetForcedCardsBoxShTab.Text)
    End Sub

    Private Sub LoadFormForcedShoe()
        Dim card As Integer

        For card = 1 To 10
            ForcedShoeArray(card - 1).Text = FormRules.ForcedShoe.Cards(card)
            SpadesArray(card - 1).Text = FormRules.ForcedShoe.Suits(card, BJCAGlobalsClass.Suits.Spades)
            HeartsArray(card - 1).Text = FormRules.ForcedShoe.Suits(card, BJCAGlobalsClass.Suits.Hearts)
            DiamondsArray(card - 1).Text = FormRules.ForcedShoe.Suits(card, BJCAGlobalsClass.Suits.Diamonds)
            ClubsArray(card - 1).Text = FormRules.ForcedShoe.Suits(card, BJCAGlobalsClass.Suits.Clubs)
            NetSuitsArray(card - 1).Text = CalcNetSuitCards(card - 1)
        Next card
        NetForcedCardsBoxShTab.Text = FormRules.ForcedShoe.CardsLeft
    End Sub

    Private Sub FiniteDeckButtonShTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiniteDecksButtonShTab.CheckedChanged
        If FiniteDecksButtonShTab.Checked = True And ForcedShoeCheckShTab.Checked = False Then
            NDecksLabelShTab.Enabled() = True
            NDecksBoxShTab.Enabled = True
        Else
            NDecksLabelShTab.Enabled() = False
            NDecksBoxShTab.Enabled = False
        End If
        If ForcedShoeCheckShTab.Checked Then
            ReferenceShoeGroupShTab.Enabled = True
            ForcedShoeGroupShTab.Enabled = True
            SpanishDecksCheckBoxShTab.Enabled = False
        Else
            ReferenceShoeGroupShTab.Enabled = False
            ForcedShoeGroupShTab.Enabled = False
            SpanishDecksCheckBoxShTab.Enabled = True
        End If
        If FiniteDecksButtonMTab.Checked <> FiniteDecksButtonShTab.Checked Then
            FiniteDecksButtonMTab.Checked = FiniteDecksButtonShTab.Checked
        End If
    End Sub

    Private Sub InfiniteDecksButtonShTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfiniteDecksButtonShTab.CheckedChanged
        If FiniteDecksButtonShTab.Checked = True And ForcedShoeCheckShTab.Checked = False Then
            NDecksLabelShTab.Enabled = True
            NDecksBoxShTab.Enabled = True
        Else
            NDecksLabelShTab.Enabled = False
            NDecksBoxShTab.Enabled = False
        End If
        If ForcedShoeCheckShTab.Checked Then
            ReferenceShoeGroupShTab.Enabled = True
            ForcedShoeGroupShTab.Enabled = True
            SpanishDecksCheckBoxShTab.Enabled = False
        Else
            ReferenceShoeGroupShTab.Enabled = False
            ForcedShoeGroupShTab.Enabled = False
            SpanishDecksCheckBoxShTab.Enabled = True
        End If
        If InfiniteDecksButtonMTab.Checked <> InfiniteDecksButtonShTab.Checked Then
            InfiniteDecksButtonMTab.Checked = InfiniteDecksButtonShTab.Checked
        End If
    End Sub

    Private Sub ForcedShoeCheckShTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcedShoeCheckShTab.CheckedChanged
        If FiniteDecksButtonShTab.Checked = True And ForcedShoeCheckShTab.Checked = False Then
            NDecksLabelShTab.Enabled = True
            NDecksBoxShTab.Enabled = True
        Else
            NDecksLabelShTab.Enabled = False
            NDecksBoxShTab.Enabled = False
        End If
        If ForcedShoeCheckShTab.Checked Then
            ReferenceShoeGroupShTab.Enabled = True
            ForcedShoeGroupShTab.Enabled = True
            SpanishDecksCheckBoxShTab.Enabled = False
        Else
            ReferenceShoeGroupShTab.Enabled = False
            ForcedShoeGroupShTab.Enabled = False
            SpanishDecksCheckBoxShTab.Enabled = True
        End If
        If ForcedShoeCheckMTab.Checked <> ForcedShoeCheckShTab.Checked Then
            ForcedShoeCheckMTab.Checked = ForcedShoeCheckShTab.Checked
        End If
    End Sub

    Private Sub NDecksBoxShTab_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NDecksBoxShTab.Validating
        If CheckValidInteger(NDecksBoxShTab.Text, 1, Constants.MaxDecks, True) Then
            NDecksBoxMTab.Text = NDecksBoxShTab.Text
        Else
            NDecksBoxShTab.Text = NDecksBoxMTab.Text
            e.Cancel = True
        End If
    End Sub

    Private Sub SpanishDecksCheckBoxShTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpanishDecksCheckBoxShTab.CheckedChanged
        If SpanishDecksCheckBoxShTab.Checked <> SpanishDecksCheckBoxMTab.Checked Then
            SpanishDecksCheckBoxMTab.Checked = SpanishDecksCheckBoxShTab.Checked
        End If
    End Sub

    Private Sub RefDecksBoxShTabShTab_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RefDecksBoxShTab.KeyPress
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub RefDecksBoxShTabShTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefDecksBoxShTab.TextChanged
        If CheckValidInteger(RefDecksBoxShTab.Text, 0, Constants.MaxDecks, True) Then
            If Not SpanishDecksRefCheckBoxShTab.Checked Then
                ReferenceShoeArray(0).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(1).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(2).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(3).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(4).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(5).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(6).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(7).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(8).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(9).Text = RefDecksBoxShTab.Text * 16
                NetRefCardsBoxShTab.Text = RefDecksBoxShTab.Text * 52
            Else
                ReferenceShoeArray(0).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(1).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(2).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(3).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(4).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(5).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(6).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(7).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(8).Text = RefDecksBoxShTab.Text * 4
                ReferenceShoeArray(9).Text = RefDecksBoxShTab.Text * 12
                NetRefCardsBoxShTab.Text = RefDecksBoxShTab.Text * 48
            End If
        Else
            RefDecksBoxShTab.Text = 6
            ShoeOptionsTab.Select()
            RefDecksBoxShTab.Select()
        End If
    End Sub

    Private Sub SpanishDecksRefCheckBoxShTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpanishDecksRefCheckBoxShTab.CheckedChanged
        If Not SpanishDecksRefCheckBoxShTab.Checked Then
            ReferenceShoeArray(0).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(1).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(2).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(3).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(4).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(5).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(6).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(7).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(8).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(9).Text = RefDecksBoxShTab.Text * 16
            NetRefCardsBoxShTab.Text = RefDecksBoxShTab.Text * 52
        Else
            ReferenceShoeArray(0).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(1).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(2).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(3).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(4).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(5).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(6).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(7).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(8).Text = RefDecksBoxShTab.Text * 4
            ReferenceShoeArray(9).Text = RefDecksBoxShTab.Text * 12
            NetRefCardsBoxShTab.Text = RefDecksBoxShTab.Text * 48
        End If
    End Sub

    Private Sub CopyRefShoeButtonShTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyRefShoeButtonShTab.Click
        Dim card As Integer

        CopyRefShoe()
        For card = 0 To 9
            RestoreSuitDefaults(card)
        Next card
    End Sub

#End Region

#Region " Bonus Rules Tab "

#Region " Bonus Rules List "

    Private Sub GetFormCurrentBonusRule()
        Dim i As Integer

        With BonusRule
            .Name = RuleNameBoxBTab.Text
            .RuleOn = False
            .UseSpecificHand = HandCompCheckBTab.Checked

            For i = 0 To 9
                .Hand.Cards(i + 1) = CInt(BonusRulesHandArray(i).Text)
            Next i
            .HardSoftOnly = Not EitherCheckBTab.Checked
            If Not HandCompCheckBTab.Checked Then
                If HandTotalComboBoxBTab.SelectedIndex = 0 Then
                    .Hand.Total = 0
                Else
                    .Hand.Total = HandTotalComboBoxBTab.SelectedIndex + 3
                End If
                If HandNCardsComboBoxBTab.SelectedIndex = 0 Then
                    .Hand.NumCards = 0
                Else
                    .Hand.NumCards = HandNCardsComboBoxBTab.SelectedIndex + 1
                End If
                .Hand.Soft = SoftOnlyCheckBTab.Checked
            Else
                .Hand.Total = TotalBoxBTab.Text
                .Hand.NumCards = NCardsBoxBTab.Text
                .Hand.Soft = SoftCheckBTab.Checked
            End If
            .OrMore = OrMoreCheckBTab.Checked
            .OrLess = OrLessCheckBTab.Checked

            .ExactMatch = ExactMatchCheckBTab.Checked
            .SpecificTen = SpecificTenCheckBTab.Checked
            .SpecificTenFraction = SpecificTenFractionBoxBTab.Text

            .PayoffGeneral = PayoffGeneralBoxBTab.Text
            .PayoffSuited = PayoffSuitedBoxBTab.Text
            .PayoffSpecificSuit = PayoffSpecificSuitBoxBTab.Text
            .PayoffGeneralBJ = PayoffGeneralBJBoxBTab.Text
            .PayoffSuitedBJ = PayoffSuitedBJBoxBTab.Text
            .PayoffSpecificSuitBJ = PayoffSpecificSuitBJBoxBTab.Text
            .BJAUp = AceUpCheckBTab.Checked
            .BJTUp = TenUpCheckBTab.Checked
            .PayoffUCGeneral = PayoffUCGeneralBoxBTab.Text
            .PayoffUCSuited = PayoffUCSuitedBoxBTab.Text
            .PayoffUCSpecificSuit = PayoffUCSpecificSuitBoxBTab.Text
            .Upcard = DealerUpcardComboBoxBTab.SelectedIndex + 1

            .Suited = SuitedCheckBTab.Checked
            .SpecificSuit = SpecificSuitCheckBTab.Checked
            If SpadesButtonBTab.Checked Then
                .SuitToWin = 0
            ElseIf HeartsButtonBTab.Checked Then
                .SuitToWin = 1
            ElseIf DiamondsButtonBTab.Checked Then
                .SuitToWin = 2
            Else
                .SuitToWin = 3
            End If

            .MustWin = MustWinCheckBTab.Checked
            .HandContinues = HandContinuesCheckBTab.Checked

            .PreSplit = PreSplitCheckBTab.Checked
            .PostSplit = PostSplitCheckBTab.Checked
        End With
    End Sub

    Private Sub LoadFormBonusRule(ByVal index As Integer)
        Dim i As Integer

        RuleNameBoxBTab.Text = BonusRule.Name
        If Not BonusRule.UseSpecificHand Then
            HandTotalSizeCheckBTab.Checked = True
            HandCompCheckBTab.Checked = False
        Else
            HandTotalSizeCheckBTab.Checked = False
            HandCompCheckBTab.Checked = True
        End If
        If Not HandCompCheckBTab.Checked Then
            If BonusRule.Hand.Total = 0 Then
                HandTotalComboBoxBTab.SelectedIndex = BonusRule.Hand.Total
            Else
                HandTotalComboBoxBTab.SelectedIndex = BonusRule.Hand.Total - 3
            End If
            If BonusRule.Hand.Soft Then
                HardOnlyCheckBTab.Checked = False
                SoftOnlyCheckBTab.Checked = True
            Else
                HardOnlyCheckBTab.Checked = True
                SoftOnlyCheckBTab.Checked = False
            End If
            If BonusRule.Hand.NumCards = 0 Then
                HandNCardsComboBoxBTab.SelectedIndex = BonusRule.Hand.NumCards
            Else
                HandNCardsComboBoxBTab.SelectedIndex = BonusRule.Hand.NumCards - 1
            End If

            TotalBoxBTab.Text = 0
            SoftCheckBTab.Checked = False
            NCardsBoxBTab.Text = 0
            If BonusRule.Hand.NumCards = 0 Then
                OrMoreCheckBTab.Enabled = False
                OrLessCheckBTab.Enabled = False
                OrMoreCheckBTab.Checked = False
                OrLessCheckBTab.Checked = False
            ElseIf BonusRule.Hand.NumCards = 2 Then
                OrMoreCheckBTab.Enabled = True
                OrLessCheckBTab.Enabled = False
                OrLessCheckBTab.Checked = False
            ElseIf BonusRule.Hand.NumCards = 21 Then
                OrMoreCheckBTab.Enabled = False
                OrLessCheckBTab.Enabled = True
                OrMoreCheckBTab.Checked = False
            Else
                OrMoreCheckBTab.Enabled = True
                OrLessCheckBTab.Enabled = True
            End If
        Else
            TotalBoxBTab.Text = BonusRule.Hand.Total
            SoftCheckBTab.Checked = BonusRule.Hand.Soft
            NCardsBoxBTab.Text = BonusRule.Hand.NumCards

            HandTotalComboBoxBTab.SelectedIndex = 0
            HardOnlyCheckBTab.Checked = True
            SoftOnlyCheckBTab.Checked = False
            HandNCardsComboBoxBTab.SelectedIndex = 0
        End If

        For i = 1 To 10
            BonusRulesHandArray(i - 1).Text = BonusRule.Hand.Cards(i)
        Next i

        EitherCheckBTab.Checked = Not BonusRule.HardSoftOnly
        OrMoreCheckBTab.Checked = BonusRule.OrMore
        OrLessCheckBTab.Checked = BonusRule.OrLess

        ExactMatchCheckBTab.Checked = BonusRule.ExactMatch
        SpecificTenCheckBTab.Checked = BonusRule.SpecificTen
        SpecificTenFractionBoxBTab.Text = BonusRule.SpecificTenFraction
        If ExactMatchCheckBTab.Checked And HandCompCheckBTab.Checked Then
            SpecificTenCheckBTab.Enabled = True
            SpecificTenFractionBoxBTab.Enabled = True
            SpecificTenFractionLabelBTab.Enabled = True
        Else
            SpecificTenCheckBTab.Enabled = False
            SpecificTenFractionBoxBTab.Enabled = False
            SpecificTenFractionLabelBTab.Enabled = False
        End If

        PayoffGeneralBoxBTab.Text = BonusRule.PayoffGeneral
        PayoffSuitedBoxBTab.Text = BonusRule.PayoffSuited
        PayoffSpecificSuitBoxBTab.Text = BonusRule.PayoffSpecificSuit
        PayoffGeneralBJBoxBTab.Text = BonusRule.PayoffGeneralBJ
        PayoffSuitedBJBoxBTab.Text = BonusRule.PayoffSuitedBJ
        PayoffSpecificSuitBJBoxBTab.Text = BonusRule.PayoffSpecificSuitBJ
        AceUpCheckBTab.Checked = BonusRule.BJAUp
        TenUpCheckBTab.Checked = BonusRule.BJTUp
        PayoffUCGeneralBoxBTab.Text = BonusRule.PayoffUCGeneral
        PayoffUCSuitedBoxBTab.Text = BonusRule.PayoffUCSuited
        PayoffUCSpecificSuitBoxBTab.Text = BonusRule.PayoffUCSpecificSuit
        DealerUpcardComboBoxBTab.SelectedIndex = BonusRule.Upcard - 1

        If (PayoffGeneralBJBoxBTab.Text <> -1) Or (PayoffSuitedBJBoxBTab.Text <> -1) Or (PayoffSpecificSuitBJBoxBTab.Text <> -1) Then
            AceUpCheckBTab.Enabled = True
            TenUpCheckBTab.Enabled = True
        Else
            AceUpCheckBTab.Enabled = False
            TenUpCheckBTab.Enabled = False
        End If

        If (BonusRule.PayoffUCGeneral = 0 And BonusRule.PayoffUCSuited = 0 And BonusRule.PayoffUCSpecificSuit = 0) Then
            DealerUpcardComboBoxBTab.Enabled = False
            DealerUCLabelBTab.Enabled = False
        Else
            DealerUpcardComboBoxBTab.Enabled = True
            DealerUCLabelBTab.Enabled = True
        End If

        SuitedCheckBTab.Checked = BonusRule.Suited

        SpecificSuitCheckBTab.Checked = BonusRule.SpecificSuit
        If Not SpecificSuitCheckBTab.Checked Then
            PayoffSpecificSuitBoxBTab.Text = 0
        End If
        If BonusRule.SuitToWin = 0 Then
            SpadesButtonBTab.Checked = True
            SuitLabelBTab.Text = "Spades Bonus"
        ElseIf BonusRule.SuitToWin = 1 Then
            HeartsButtonBTab.Checked = True
            SuitLabelBTab.Text = "Hearts Bonus"
        ElseIf BonusRule.SuitToWin = 2 Then
            DiamondsButtonBTab.Checked = True
            SuitLabelBTab.Text = "Diamonds Bonus"
        Else
            ClubsButtonBTab.Checked = True
            SuitLabelBTab.Text = "Clubs Bonus"
        End If
        If SuitedCheckBTab.Checked Then
            SpecificSuitCheckBTab.Enabled = True
            PayoffSuitedBoxBTab.Enabled = True
            PayoffSuitedBJBoxBTab.Enabled = True
            PayoffUCSuitedBoxBTab.Enabled = True
            If Not SpecificSuitCheckBTab.Checked Then
                SpadesButtonBTab.Enabled = False
                DiamondsButtonBTab.Enabled = False
                HeartsButtonBTab.Enabled = False
                ClubsButtonBTab.Enabled = False
                PayoffSpecificSuitBoxBTab.Enabled = False
                PayoffSpecificSuitBoxBTab.Text = 0
                PayoffSpecificSuitBJBoxBTab.Enabled = False
                PayoffSpecificSuitBJBoxBTab.Text = -1
                PayoffUCSpecificSuitBoxBTab.Enabled = False
                PayoffUCSpecificSuitBoxBTab.Text = 0
            Else
                SpadesButtonBTab.Enabled = True
                DiamondsButtonBTab.Enabled = True
                HeartsButtonBTab.Enabled = True
                ClubsButtonBTab.Enabled = True
                PayoffSpecificSuitBoxBTab.Enabled = True
                PayoffSpecificSuitBJBoxBTab.Enabled = True
                PayoffUCSpecificSuitBoxBTab.Enabled = True
            End If
        Else
            SpecificSuitCheckBTab.Enabled = False
            SpecificSuitCheckBTab.Checked = False
            SpadesButtonBTab.Enabled = False
            DiamondsButtonBTab.Enabled = False
            HeartsButtonBTab.Enabled = False
            ClubsButtonBTab.Enabled = False

            PayoffSuitedBoxBTab.Enabled = False
            PayoffSuitedBoxBTab.Text = 0
            PayoffSuitedBJBoxBTab.Enabled = False
            PayoffSuitedBJBoxBTab.Text = -1
            PayoffUCSuitedBoxBTab.Enabled = False
            PayoffUCSuitedBoxBTab.Text = 0
            PayoffSpecificSuitBoxBTab.Enabled = False
            PayoffSpecificSuitBoxBTab.Text = 0
            PayoffSpecificSuitBJBoxBTab.Enabled = False
            PayoffSpecificSuitBJBoxBTab.Text = -1
            PayoffUCSpecificSuitBoxBTab.Enabled = False
            PayoffUCSpecificSuitBoxBTab.Text = 0
        End If

        MustWinCheckBTab.Checked = BonusRule.MustWin
        HandContinuesCheckBTab.Checked = BonusRule.HandContinues

        PreSplitCheckBTab.Checked = BonusRule.PreSplit
        PostSplitCheckBTab.Checked = BonusRule.PostSplit

        BonusRulesCheckListBoxBTab.SelectedIndex = index
    End Sub

    Private Sub PopulateBonusRulesTable()
        Dim column As Integer

        For column = 0 To 9 Step 1
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Location = New System.Drawing.Point(296 + 32 * column, 120)
            box.Size = New System.Drawing.Size(32, 22)
            box.Index = column

            Me.BonusRulesHandArray(column) = box
            Me.BonusRuleDetailsGroupBTab.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.Validating, AddressOf BonusRulesHandArrayHandler_Validating

        Next column
    End Sub

    Private Sub MoveRuleUpButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveRuleUpButtonBTab.Click
        Dim currentRule As Integer
        Dim numRules As Integer
        Dim tempbox As New Windows.Forms.CheckedListBox
        Dim rule As Integer

        numRules = BonusRulesCheckListBoxBTab.Items.Count()
        currentRule = BonusRulesCheckListBoxBTab.SelectedIndex()

        If currentRule > 0 And numRules > 1 Then
            'First move the actual rule in the list
            FormRules.BonusRulesList.MoveBonusRuleUp(currentRule)

            'Then move the name of the rule in the list
            For rule = 0 To currentRule - 2
                tempbox.Items.Add(BonusRulesCheckListBoxBTab.Items(rule), BonusRulesCheckListBoxBTab.GetItemChecked(rule))
            Next
            tempbox.Items.Add(BonusRulesCheckListBoxBTab.Items(currentRule), BonusRulesCheckListBoxBTab.GetItemChecked(currentRule))
            tempbox.Items.Add(BonusRulesCheckListBoxBTab.Items(currentRule - 1), BonusRulesCheckListBoxBTab.GetItemChecked(currentRule - 1))
            For rule = currentRule + 1 To numRules - 1
                tempbox.Items.Add(BonusRulesCheckListBoxBTab.Items(rule), BonusRulesCheckListBoxBTab.GetItemChecked(rule))
            Next
            For rule = numRules - 1 To 0 Step -1
                BonusRulesCheckListBoxBTab.Items.RemoveAt(rule)
            Next
            For rule = 0 To numRules - 1
                BonusRulesCheckListBoxBTab.Items.Add(tempbox.Items(rule), tempbox.GetItemChecked(rule))
            Next
            BonusRulesCheckListBoxBTab.SelectedIndex = currentRule - 1
        End If
    End Sub

    Private Sub MoveRuleDownButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveRuleDownButtonBTab.Click
        Dim currentRule As Integer
        Dim numRules As Integer
        Dim tempBox As New Windows.Forms.CheckedListBox
        Dim rule As Integer

        numRules = BonusRulesCheckListBoxBTab.Items.Count()
        currentRule = BonusRulesCheckListBoxBTab.SelectedIndex

        If currentRule < numRules - 1 And numRules > 1 Then
            'First move the actual rule in the list
            FormRules.BonusRulesList.MoveBonusRuleDown(currentRule)

            'Then move the name of the rule in the list
            For rule = 0 To currentRule - 1
                tempBox.Items.Add(BonusRulesCheckListBoxBTab.Items(rule), BonusRulesCheckListBoxBTab.GetItemChecked(rule))
            Next
            tempBox.Items.Add(BonusRulesCheckListBoxBTab.Items(currentRule + 1), BonusRulesCheckListBoxBTab.GetItemChecked(currentRule + 1))
            tempBox.Items.Add(BonusRulesCheckListBoxBTab.Items(currentRule), BonusRulesCheckListBoxBTab.GetItemChecked(currentRule))
            For rule = currentRule + 2 To numRules - 1
                tempBox.Items.Add(BonusRulesCheckListBoxBTab.Items(rule), BonusRulesCheckListBoxBTab.GetItemChecked(rule))
            Next
            For rule = numRules - 1 To 0 Step -1
                BonusRulesCheckListBoxBTab.Items.RemoveAt(rule)
            Next
            For rule = 0 To numRules - 1
                BonusRulesCheckListBoxBTab.Items.Add(tempBox.Items(rule), tempBox.GetItemChecked(rule))
            Next
            BonusRulesCheckListBoxBTab.SelectedIndex = currentRule + 1

        End If
    End Sub

    Private Sub DeleteRuleButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteRuleButtonBTab.Click
        Dim currentRule As Integer

        currentRule = BonusRulesCheckListBoxBTab.SelectedIndex
        If currentRule = -1 Then
            MsgBox("Please select a bonus rule you would like to delete.", MsgBoxStyle.OKOnly)
        Else
            If MsgBox("Are you sure you would like to delete the Bonus Rule: " + FormRules.BonusRulesList.L(currentRule).Name + "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                FormRules.BonusRulesList.DeleteBonusRule(currentRule)
                BonusRulesCheckListBoxBTab.Items.RemoveAt(currentRule)
                BonusRulesCheckListBoxBTab.SelectedIndex() = -1
            End If
        End If
    End Sub

    Private Sub UpdateRuleButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateRuleButtonBTab.Click
        Dim i As Integer
        Dim rulePresent As Boolean
        Dim currentRule As Integer

        rulePresent = False
        For i = 0 To FormRules.BonusRulesList.NumRules - 1
            If FormRules.BonusRulesList.L(i).Name = RuleNameBoxBTab.Text Then
                rulePresent = True
                currentRule = i
            End If
        Next i
        If rulePresent Then
            GetFormCurrentBonusRule()
            If Not (BonusRule.Hand.NumCards > 0 Or BonusRule.Hand.Total > 0 Or (Not EitherCheckBTab.Checked And HardOnlyCheckBTab.Checked) Or SoftOnlyCheckBTab.Checked Or SuitedCheckBTab.Checked) Then
                MsgBox("This rule is empty.", MsgBoxStyle.OKOnly)
            Else
                FormRules.BonusRulesList.L(currentRule) = CType(CloneObject(BonusRule), BJCABonusRulesClass)
                BonusRulesCheckListBoxBTab.SelectedIndex = currentRule
            End If
        Else
            BonusRulesCheckListBoxBTab.SelectedIndex = -1
            MsgBox("The rule name does not match any names in the current rules list.", MsgBoxStyle.OKOnly)
        End If
    End Sub

    Private Sub AddRuleButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRuleButtonBTab.Click
        Dim i As Integer
        Dim rulePresent As Boolean

        rulePresent = False
        For i = 0 To FormRules.BonusRulesList.NumRules - 1
            If FormRules.BonusRulesList.L(i).Name = RuleNameBoxBTab.Text Then
                rulePresent = True
                Exit For
            End If
        Next i
        If rulePresent Then
            BonusRulesCheckListBoxBTab.SelectedIndex = -1
            MsgBox("A rule by this name already exists.", MsgBoxStyle.OKOnly)
        Else
            GetFormCurrentBonusRule()
            If (BonusRule.PayoffGeneral = 0 And BonusRule.PayoffSuited = 0 And BonusRule.PayoffSpecificSuit = 0 And PayoffGeneralBJBoxBTab.Text = -1 And PayoffSuitedBJBoxBTab.Text = -1 And PayoffSpecificSuitBJBoxBTab.Text = -1) Then
                MsgBox("This rule is empty.", MsgBoxStyle.OKOnly)
            ElseIf Not (BonusRule.Hand.NumCards > 0 Or BonusRule.Hand.Total > 0 Or (Not EitherCheckBTab.Checked And HardOnlyCheckBTab.Checked) Or SoftOnlyCheckBTab.Checked Or SuitedCheckBTab.Checked) Then
                MsgBox("This rule is empty.", MsgBoxStyle.OKOnly)
            ElseIf BonusRule.Name = "" Then
                MsgBox("Please enter a name for this rule.", MsgBoxStyle.OKOnly)
            Else
                BonusRule.RuleOn = False
                FormRules.BonusRulesList.AddBonusRule(BonusRule)
                BonusRulesCheckListBoxBTab.Items.Add(BonusRule.Name, BonusRule.RuleOn)
                BonusRulesCheckListBoxBTab.SelectedIndex = FormRules.BonusRulesList.NumRules - 1
            End If
        End If
    End Sub

    Private Sub RestoreDefaultRulesButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreDefaultRulesButtonBTab.Click
        Dim i As Integer
        Dim j As Integer
        Dim rulePresent As Boolean

        rulePresent = False
        For i = 0 To FormRules.DefaultBonusRulesList.NumRules - 1
            rulePresent = False
            For j = 0 To FormRules.BonusRulesList.NumRules - 1
                If FormRules.DefaultBonusRulesList.L(i).Name = FormRules.BonusRulesList.L(j).Name Then
                    rulePresent = True
                    Exit For
                End If
            Next j
            If Not rulePresent Then
                FormRules.BonusRulesList.AddBonusRule(FormRules.DefaultBonusRulesList.L(i))
                BonusRulesCheckListBoxBTab.Items.Add(FormRules.DefaultBonusRulesList.L(i).Name, FormRules.DefaultBonusRulesList.L(i).RuleOn)
                BonusRulesCheckListBoxBTab.SelectedIndex = FormRules.BonusRulesList.NumRules - 1
            End If
        Next i
        ClearBonusRule()

    End Sub

    Private Sub ClearBonusRule()
        BonusRule.Empty()
        LoadFormBonusRule(-1)
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub LoadFormBonusRulesList(ByRef brList As BJCABonusRulesListClass, Optional ByVal append As Boolean = False)
        Dim rule As Integer
        Dim tempList As BJCABonusRulesListClass

        tempList = CloneObject(brList)
        If Not append Then
            DeleteAllBonusRules()
        End If
        FormRules.BonusRulesList = CloneObject(tempList)
        For rule = 0 To FormRules.BonusRulesList.NumRules - 1
            BonusRulesCheckListBoxBTab.Items.Add(FormRules.BonusRulesList.L(rule).Name, FormRules.BonusRulesList.L(rule).RuleOn)
        Next rule
    End Sub

    Private Sub UncheckBonusRulesButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UncheckBonusRulesButtonBTab.Click
        Dim i As Integer

        For i = 0 To FormRules.BonusRulesList.NumRules - 1
            BonusRulesCheckListBoxBTab.SetItemChecked(i, False)
            FormRules.BonusRulesList.L(i).RuleOn = False
        Next i
    End Sub

    Private Sub BonusRulesCheckListBoxBTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BonusRulesCheckListBoxBTab.SelectedIndexChanged
        If BonusRulesCheckListBoxBTab.SelectedIndex >= 0 Then
            BonusRule = CType(CloneObject(FormRules.BonusRulesList.L(BonusRulesCheckListBoxBTab.SelectedIndex)), BJCABonusRulesClass)
            LoadFormBonusRule(BonusRulesCheckListBoxBTab.SelectedIndex)
        End If
    End Sub

    Private Sub ClearRuleButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearRuleButtonBTab.Click
        ClearBonusRule()
    End Sub

    Private Sub DeleteAllBonusRules()
        If BonusRulesCheckListBoxBTab.Items.Count > 0 Then
            Dim rule As Integer

            For rule = FormRules.BonusRulesList.NumRules - 1 To 0 Step -1
                FormRules.BonusRulesList.DeleteBonusRule(rule)
                BonusRulesCheckListBoxBTab.Items.RemoveAt(rule)
            Next rule
        End If
    End Sub

    Private Sub DeleteAllBonusRulesButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAllBonusRulesButtonBTab.Click
        If MsgBox("Are you sure you would like to delete all the Bonus Rules in the list?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DeleteAllBonusRules()
        End If
    End Sub

    Private Sub RenameRuleButtonBTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameRuleButtonBTab.Click
        Dim rule As Integer
        Dim renameForm As New BJCARenameForm

        rule = BonusRulesCheckListBoxBTab.SelectedIndex()
        If rule = -1 Then
            MsgBox("Please select a rule to rename")
        Else
            renameForm.CurrentNameBoxRForm.Text = FormRules.BonusRulesList.L(rule).Name
            renameForm.ShowDialog()
            If renameForm.RenameOK Then
                BonusRulesCheckListBoxBTab.Items.Item(rule) = renameForm.NewNameBoxRForm.Text
                FormRules.BonusRulesList.L(rule).Name = renameForm.NewNameBoxRForm.Text
                BonusRule = CType(CloneObject(FormRules.BonusRulesList.L(BonusRulesCheckListBoxBTab.SelectedIndex)), BJCABonusRulesClass)
                LoadFormBonusRule(BonusRulesCheckListBoxBTab.SelectedIndex)
            End If
        End If
        renameForm.Dispose()
    End Sub

    Private Sub GetBonusRulesOn()
        Dim rule As Integer

        For rule = 0 To FormRules.BonusRulesList.NumRules - 1
            FormRules.BonusRulesList.L(rule).RuleOn = BonusRulesCheckListBoxBTab.GetItemChecked(rule)
        Next rule
    End Sub


#End Region

#Region " Bonus Rules Form "

    Private Sub BonusRulesHandArrayHandler_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles BonusRulesHandArrayHandler.Validating
        Dim card As Integer
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
        For card = 1 To 10
            tempNum = BonusRulesHandArray(card - 1).Text
            tempTotal += tempNum * card
            tempNumCards += tempNum
        Next
        If tempTotal < 12 And BonusRulesHandArray(0).Text > 0 Then
            tempSoft = True
            tempTotal += 10
        End If
        If tempTotal > 21 Then
            MsgBox("Total of hand must be <=21.", MsgBoxStyle.OKOnly)
            DirectCast(sender, IndexedTextBox).Text = 0
            e.Cancel = True
        ElseIf tempTotal = 21 And tempNumCards = 2 Then
            MsgBox("Blackjacks are handled under the BJ Bonuses Tab.", MsgBoxStyle.OKOnly)
            DirectCast(sender, IndexedTextBox).Text = 0
            e.Cancel = True
        Else
            With BonusRule.Hand
                For card = 1 To 10
                    .Cards(card) = BonusRulesHandArray(card - 1).Text
                Next
                .Total = tempTotal
                .Soft = tempSoft
                .NumCards = tempNumCards
            End With
            TotalBoxBTab.Text = tempTotal
            SoftCheckBTab.Checked = tempSoft
            NCardsBoxBTab.Text = tempNumCards
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub RuleNameBoxBTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RuleNameBoxBTab.TextChanged
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub HandTotalSizeCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandTotalSizeCheckBTab.CheckedChanged
        Dim i As Integer

        If HandTotalSizeCheckBTab.Checked Then
            HandCompCheckBTab.Checked = False
            HandTotalLabelBTab.Enabled = True
            HandSoftLabelBTab.Enabled = True
            HandNCardsLabelBTab.Enabled = True
            HandTotalComboBoxBTab.Enabled = True
            EitherCheckBTab.Enabled = True
            If EitherCheckBTab.Checked Then
                HardOnlyCheckBTab.Enabled = False
                SoftOnlyCheckBTab.Enabled = False
                HardOnlyCheckBTab.Checked = False
                SoftOnlyCheckBTab.Checked = False
            Else
                HardOnlyCheckBTab.Enabled = True
                SoftOnlyCheckBTab.Enabled = True
            End If
            HandNCardsComboBoxBTab.Enabled = True
            If HandNCardsComboBoxBTab.SelectedIndex = 0 Then
                OrMoreCheckBTab.Enabled = False
                OrLessCheckBTab.Enabled = False
                OrMoreCheckBTab.Checked = False
                OrLessCheckBTab.Checked = False
            ElseIf HandNCardsComboBoxBTab.SelectedIndex = 1 Then
                OrMoreCheckBTab.Enabled = True
                OrLessCheckBTab.Enabled = False
                OrLessCheckBTab.Checked = False
            ElseIf HandNCardsComboBoxBTab.SelectedIndex = 20 Then
                OrMoreCheckBTab.Enabled = False
                OrLessCheckBTab.Enabled = True
                OrMoreCheckBTab.Checked = False
            Else
                OrMoreCheckBTab.Enabled = True
                OrLessCheckBTab.Enabled = True
            End If

            CAceLabelBTab.Enabled = False
            C2LabelBTab.Enabled = False
            C3LabelBTab.Enabled = False
            C4LabelBTab.Enabled = False
            C5LabelBTab.Enabled = False
            C6LabelBTab.Enabled = False
            C7LabelBTab.Enabled = False
            C8LabelBTab.Enabled = False
            C9LabelBTab.Enabled = False
            C10LabelBTab.Enabled = False
            TotalLabelBTab.Enabled = False
            SoftLabelBTab.Enabled = False
            NCardsLabelBTab.Enabled = False
            For i = 0 To 9
                BonusRulesHandArray(i).Enabled = False
            Next i
            ExactMatchCheckBTab.Enabled = False
            SpecificTenCheckBTab.Enabled = False
            SpecificTenFractionBoxBTab.Enabled = False
            SpecificTenFractionLabelBTab.Enabled = False
        Else
            HandCompCheckBTab.Checked = True
            HandTotalLabelBTab.Enabled = False
            HandSoftLabelBTab.Enabled = False
            HandNCardsLabelBTab.Enabled = False
            HandTotalComboBoxBTab.Enabled = False
            EitherCheckBTab.Enabled = False
            SoftOnlyCheckBTab.Enabled = False
            HardOnlyCheckBTab.Enabled = False
            HandNCardsComboBoxBTab.Enabled = False
            OrMoreCheckBTab.Enabled = False
            OrLessCheckBTab.Enabled = False

            CAceLabelBTab.Enabled = True
            C2LabelBTab.Enabled = True
            C3LabelBTab.Enabled = True
            C4LabelBTab.Enabled = True
            C5LabelBTab.Enabled = True
            C6LabelBTab.Enabled = True
            C7LabelBTab.Enabled = True
            C8LabelBTab.Enabled = True
            C9LabelBTab.Enabled = True
            C10LabelBTab.Enabled = True
            TotalLabelBTab.Enabled = True
            SoftLabelBTab.Enabled = True
            NCardsLabelBTab.Enabled = True
            For i = 0 To 9
                BonusRulesHandArray(i).Enabled = True
            Next i
            ExactMatchCheckBTab.Enabled = True
            If ExactMatchCheckBTab.Checked Then
                SpecificTenCheckBTab.Enabled = True
                SpecificTenFractionBoxBTab.Enabled = True
                SpecificTenFractionLabelBTab.Enabled = True
            Else
                SpecificTenCheckBTab.Enabled = False
                SpecificTenFractionBoxBTab.Enabled = False
                SpecificTenFractionLabelBTab.Enabled = False
            End If
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub ExactMatchCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExactMatchCheckBTab.CheckedChanged
        If ExactMatchCheckBTab.Checked = False Then
            SpecificTenCheckBTab.Enabled = False
            SpecificTenFractionBoxBTab.Enabled = False
            SpecificTenFractionLabelBTab.Enabled = False
        Else
            SpecificTenCheckBTab.Enabled = True
            SpecificTenFractionBoxBTab.Enabled = True
            SpecificTenFractionLabelBTab.Enabled = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub HandCompCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandCompCheckBTab.CheckedChanged
        Dim i As Integer

        If HandCompCheckBTab.Checked Then
            HandTotalSizeCheckBTab.Checked = False
            HandTotalLabelBTab.Enabled = False
            HandSoftLabelBTab.Enabled = False
            HandNCardsLabelBTab.Enabled = False
            HandTotalComboBoxBTab.Enabled = False
            EitherCheckBTab.Enabled = False
            SoftOnlyCheckBTab.Enabled = False
            HardOnlyCheckBTab.Enabled = False
            HandNCardsComboBoxBTab.Enabled = False
            OrMoreCheckBTab.Enabled = False
            OrLessCheckBTab.Enabled = False

            CAceLabelBTab.Enabled = True
            C2LabelBTab.Enabled = True
            C3LabelBTab.Enabled = True
            C4LabelBTab.Enabled = True
            C5LabelBTab.Enabled = True
            C6LabelBTab.Enabled = True
            C7LabelBTab.Enabled = True
            C8LabelBTab.Enabled = True
            C9LabelBTab.Enabled = True
            C10LabelBTab.Enabled = True
            TotalLabelBTab.Enabled = True
            SoftLabelBTab.Enabled = True
            NCardsLabelBTab.Enabled = True
            For i = 0 To 9
                BonusRulesHandArray(i).Enabled = True
            Next i
            ExactMatchCheckBTab.Enabled = True
        Else
            HandTotalSizeCheckBTab.Checked = True
            HandTotalLabelBTab.Enabled = True
            HandSoftLabelBTab.Enabled = True
            HandNCardsLabelBTab.Enabled = True
            HandTotalComboBoxBTab.Enabled = True
            EitherCheckBTab.Enabled = True
            If EitherCheckBTab.Checked Then
                HardOnlyCheckBTab.Enabled = False
                SoftOnlyCheckBTab.Enabled = False
                HardOnlyCheckBTab.Checked = False
                SoftOnlyCheckBTab.Checked = False
            Else
                HardOnlyCheckBTab.Enabled = True
                SoftOnlyCheckBTab.Enabled = True
            End If
            HandNCardsComboBoxBTab.Enabled = True
            If HandNCardsComboBoxBTab.SelectedIndex = 0 Then
                OrMoreCheckBTab.Enabled = False
                OrLessCheckBTab.Enabled = False
                OrMoreCheckBTab.Checked = False
                OrLessCheckBTab.Checked = False
            ElseIf HandNCardsComboBoxBTab.SelectedIndex = 1 Then
                OrMoreCheckBTab.Enabled = True
                OrLessCheckBTab.Enabled = False
                OrLessCheckBTab.Checked = False
            ElseIf HandNCardsComboBoxBTab.SelectedIndex = 20 Then
                OrMoreCheckBTab.Enabled = False
                OrLessCheckBTab.Enabled = True
                OrMoreCheckBTab.Checked = False
            Else
                OrMoreCheckBTab.Enabled = True
                OrLessCheckBTab.Enabled = True
            End If

            CAceLabelBTab.Enabled = False
            C2LabelBTab.Enabled = False
            C3LabelBTab.Enabled = False
            C4LabelBTab.Enabled = False
            C5LabelBTab.Enabled = False
            C6LabelBTab.Enabled = False
            C7LabelBTab.Enabled = False
            C8LabelBTab.Enabled = False
            C9LabelBTab.Enabled = False
            C10LabelBTab.Enabled = False
            TotalLabelBTab.Enabled = False
            SoftLabelBTab.Enabled = False
            NCardsLabelBTab.Enabled = False
            For i = 0 To 9
                BonusRulesHandArray(i).Enabled = False
            Next i
            ExactMatchCheckBTab.Enabled = False
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub EitherCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EitherCheckBTab.CheckedChanged
        If EitherCheckBTab.Checked Then
            HardOnlyCheckBTab.Enabled = False
            SoftOnlyCheckBTab.Enabled = False
            HardOnlyCheckBTab.Checked = True
            SoftOnlyCheckBTab.Checked = False
        Else
            HardOnlyCheckBTab.Enabled = True
            SoftOnlyCheckBTab.Enabled = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub HardOnlyCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardOnlyCheckBTab.CheckedChanged
        If HardOnlyCheckBTab.Checked Then
            SoftOnlyCheckBTab.Checked = False
        Else
            SoftOnlyCheckBTab.Checked = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub SoftOnlyCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoftOnlyCheckBTab.CheckedChanged
        If SoftOnlyCheckBTab.Checked Then
            HardOnlyCheckBTab.Checked = False
        Else
            HardOnlyCheckBTab.Checked = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub OrMoreCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrMoreCheckBTab.CheckedChanged
        If OrMoreCheckBTab.Checked Then
            OrLessCheckBTab.Checked = False
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub OrLessCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrLessCheckBTab.CheckedChanged
        If OrLessCheckBTab.Checked Then
            OrMoreCheckBTab.Checked = False
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub HandNCardsComboBoxBTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandNCardsComboBoxBTab.SelectedIndexChanged
        If HandNCardsComboBoxBTab.SelectedIndex = 0 Then
            OrMoreCheckBTab.Enabled = False
            OrLessCheckBTab.Enabled = False
            OrMoreCheckBTab.Checked = False
            OrLessCheckBTab.Checked = False
        ElseIf HandNCardsComboBoxBTab.SelectedIndex = 1 Then
            OrMoreCheckBTab.Enabled = True
            OrLessCheckBTab.Enabled = False
            OrLessCheckBTab.Checked = False
        ElseIf HandNCardsComboBoxBTab.SelectedIndex = 20 Then
            OrMoreCheckBTab.Enabled = False
            OrLessCheckBTab.Enabled = True
            OrMoreCheckBTab.Checked = False
        Else
            OrMoreCheckBTab.Enabled = True
            OrLessCheckBTab.Enabled = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub SpecificTenCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpecificTenCheckBTab.CheckedChanged
        If SpecificTenCheckBTab.Checked = False Then
            SpecificTenFractionBoxBTab.Enabled = False
            SpecificTenFractionLabelBTab.Enabled = False
        Else
            SpecificTenFractionBoxBTab.Enabled = True
            SpecificTenFractionLabelBTab.Enabled = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub SpecificTenFractionBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SpecificTenFractionBoxBTab.Validating
        If Not CheckValidDecimal(SpecificTenFractionBoxBTab.Text, 0, 1, True) Then
            SpecificTenFractionBoxBTab.Text = 0.25
            e.Cancel = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub SpecificTenFractionBoxBTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpecificTenFractionBoxBTab.TextChanged
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffGeneralBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffGeneralBoxBTab.Validating
        If Not CheckValidDecimal(PayoffGeneralBoxBTab.Text, -1, 1000000, True) Then
            PayoffGeneralBoxBTab.Text = 0
            e.Cancel = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffSuitedBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffSuitedBoxBTab.Validating
        If Not CheckValidDecimal(PayoffSuitedBoxBTab.Text, -1, 1000000, True) Then
            PayoffSuitedBoxBTab.Text = 1
            e.Cancel = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffSpecificSuitBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffSpecificSuitBoxBTab.Validating
        If Not CheckValidDecimal(PayoffSpecificSuitBoxBTab.Text, -1, 1000000, True) Then
            PayoffSpecificSuitBoxBTab.Text = 1
            e.Cancel = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffGeneralBJBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffGeneralBJBoxBTab.Validating
        If Not CheckValidDecimal(PayoffGeneralBJBoxBTab.Text, -1, 1000000, True) Then
            PayoffGeneralBJBoxBTab.Text = -1
            e.Cancel = True
        End If
        If (MustWinCheckBTab.Checked Or HandContinuesCheckBTab.Checked) Then
            PayoffGeneralBJBoxBTab.Text = -1
        End If
        If (PayoffGeneralBJBoxBTab.Text <> -1 Or PayoffSuitedBJBoxBTab.Text <> -1 Or PayoffSpecificSuitBJBoxBTab.Text <> -1) Then
            AceUpCheckBTab.Enabled = True
            TenUpCheckBTab.Enabled = True
        Else
            AceUpCheckBTab.Enabled = False
            TenUpCheckBTab.Enabled = False
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffSuitedBJBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffSuitedBJBoxBTab.Validating
        If Not CheckValidDecimal(PayoffSuitedBJBoxBTab.Text, -1, 1000000, True) Then
            PayoffSuitedBJBoxBTab.Text = -1
            e.Cancel = True
        End If
        If (MustWinCheckBTab.Checked Or HandContinuesCheckBTab.Checked) Then
            PayoffSuitedBJBoxBTab.Text = -1
        End If
        If (PayoffGeneralBJBoxBTab.Text <> -1 Or PayoffSuitedBJBoxBTab.Text <> -1 Or PayoffSpecificSuitBJBoxBTab.Text <> -1) Then
            AceUpCheckBTab.Enabled = True
            TenUpCheckBTab.Enabled = True
        Else
            AceUpCheckBTab.Enabled = False
            TenUpCheckBTab.Enabled = False
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffSpecificSuitBJBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffSpecificSuitBJBoxBTab.Validating
        If Not CheckValidDecimal(PayoffSpecificSuitBJBoxBTab.Text, -1, 1000000, True) Then
            PayoffSpecificSuitBJBoxBTab.Text = -1
            e.Cancel = True
        End If
        If (MustWinCheckBTab.Checked Or HandContinuesCheckBTab.Checked) Then
            PayoffSpecificSuitBJBoxBTab.Text = -1
        End If
        If (PayoffGeneralBJBoxBTab.Text <> -1 Or PayoffSuitedBJBoxBTab.Text <> -1 Or PayoffSpecificSuitBJBoxBTab.Text <> -1) Then
            AceUpCheckBTab.Enabled = True
            TenUpCheckBTab.Enabled = True
        Else
            AceUpCheckBTab.Enabled = False
            TenUpCheckBTab.Enabled = False
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffUCGeneralBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffUCGeneralBoxBTab.Validating
        If Not CheckValidDecimal(PayoffUCGeneralBoxBTab.Text, -1, 1000000, True) Then
            PayoffUCGeneralBoxBTab.Text = 0
            e.Cancel = True
        End If
        If (PayoffUCGeneralBoxBTab.Text = 0 And PayoffUCSuitedBoxBTab.Text = 0 And PayoffUCSpecificSuitBoxBTab.Text = 0) Then
            DealerUpcardComboBoxBTab.SelectedIndex = 0
            DealerUpcardComboBoxBTab.Enabled = False
            DealerUCLabelBTab.Enabled = False
        Else
            DealerUpcardComboBoxBTab.SelectedIndex = 0
            DealerUpcardComboBoxBTab.Enabled = True
            DealerUCLabelBTab.Enabled = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffUCSuitedBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffUCSuitedBoxBTab.Validating
        If Not CheckValidDecimal(PayoffUCSuitedBoxBTab.Text, -1, 1000000, True) Then
            PayoffUCSuitedBoxBTab.Text = 0
            e.Cancel = True
        End If
        If (PayoffUCGeneralBoxBTab.Text = 0 And PayoffUCSuitedBoxBTab.Text = 0 And PayoffUCSpecificSuitBoxBTab.Text = 0) Then
            DealerUpcardComboBoxBTab.SelectedIndex = 0
            DealerUpcardComboBoxBTab.Enabled = False
            DealerUCLabelBTab.Enabled = False
        Else
            DealerUpcardComboBoxBTab.SelectedIndex = 0
            DealerUpcardComboBoxBTab.Enabled = True
            DealerUCLabelBTab.Enabled = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffUCSpecificSuitBoxBTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PayoffUCSpecificSuitBoxBTab.Validating
        If Not CheckValidDecimal(PayoffUCSpecificSuitBoxBTab.Text, -1, 1000000, True) Then
            PayoffUCSpecificSuitBoxBTab.Text = 0
            e.Cancel = True
        End If
        If (PayoffUCGeneralBoxBTab.Text = 0 And PayoffUCSuitedBoxBTab.Text = 0 And PayoffUCSpecificSuitBoxBTab.Text = 0) Then
            DealerUpcardComboBoxBTab.SelectedIndex = 0
            DealerUpcardComboBoxBTab.Enabled = False
            DealerUCLabelBTab.Enabled = False
        Else
            DealerUpcardComboBoxBTab.SelectedIndex = 0
            DealerUpcardComboBoxBTab.Enabled = True
            DealerUCLabelBTab.Enabled = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffGeneralBoxBTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayoffGeneralBoxBTab.TextChanged
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffSuitedBoxBTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayoffSuitedBoxBTab.TextChanged
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffSuitBoxBTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayoffSpecificSuitBoxBTab.TextChanged
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffDealerBJBTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayoffGeneralBJBoxBTab.TextChanged
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub PayoffDealerUCBTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayoffUCGeneralBoxBTab.TextChanged
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub DealerUpcardBoxBTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub MustWinCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MustWinCheckBTab.CheckedChanged
        If MustWinCheckBTab.Checked Then
            HandContinuesCheckBTab.Checked = False
            PayoffGeneralBJBoxBTab.Text = -1
            PayoffSuitedBJBoxBTab.Text = -1
            PayoffSpecificSuitBJBoxBTab.Text = -1
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub HandContinuesCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandContinuesCheckBTab.CheckedChanged
        If HandContinuesCheckBTab.Checked Then
            MustWinCheckBTab.Checked = False
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub SuitedCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuitedCheckBTab.CheckedChanged
        If SuitedCheckBTab.Checked Then
            SpecificSuitCheckBTab.Enabled = True
            PayoffSuitedBoxBTab.Enabled = True
            PayoffSuitedBJBoxBTab.Enabled = True
            PayoffUCSuitedBoxBTab.Enabled = True
            If Not SpecificSuitCheckBTab.Checked Then
                SpadesButtonBTab.Enabled = False
                DiamondsButtonBTab.Enabled = False
                HeartsButtonBTab.Enabled = False
                ClubsButtonBTab.Enabled = False
                PayoffSpecificSuitBoxBTab.Enabled = False
                PayoffSpecificSuitBoxBTab.Text = 0
                PayoffSpecificSuitBJBoxBTab.Enabled = False
                PayoffSpecificSuitBJBoxBTab.Text = -1
                PayoffUCSpecificSuitBoxBTab.Enabled = False
                PayoffUCSpecificSuitBoxBTab.Text = 0
            Else
                SpadesButtonBTab.Enabled = True
                DiamondsButtonBTab.Enabled = True
                HeartsButtonBTab.Enabled = True
                ClubsButtonBTab.Enabled = True
                PayoffSpecificSuitBoxBTab.Enabled = True
                PayoffSpecificSuitBJBoxBTab.Enabled = True
                PayoffUCSpecificSuitBoxBTab.Enabled = True
            End If
        Else
            SpecificSuitCheckBTab.Enabled = False
            SpecificSuitCheckBTab.Checked = False
            SpadesButtonBTab.Enabled = False
            DiamondsButtonBTab.Enabled = False
            HeartsButtonBTab.Enabled = False
            ClubsButtonBTab.Enabled = False
            PayoffSuitedBoxBTab.Enabled = False
            PayoffSuitedBoxBTab.Text = 0
            PayoffSuitedBJBoxBTab.Enabled = False
            PayoffSuitedBJBoxBTab.Text = -1
            PayoffUCSuitedBoxBTab.Enabled = False
            PayoffUCSuitedBoxBTab.Text = 0
            PayoffSpecificSuitBoxBTab.Enabled = False
            PayoffSpecificSuitBoxBTab.Text = 0
            PayoffSpecificSuitBJBoxBTab.Enabled = False
            PayoffSpecificSuitBJBoxBTab.Text = -1
            PayoffUCSpecificSuitBoxBTab.Enabled = False
            PayoffUCSpecificSuitBoxBTab.Text = 0
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub SpecificSuitCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpecificSuitCheckBTab.CheckedChanged
        If Not SpecificSuitCheckBTab.Checked Then
            SpadesButtonBTab.Enabled = False
            DiamondsButtonBTab.Enabled = False
            HeartsButtonBTab.Enabled = False
            ClubsButtonBTab.Enabled = False
            PayoffSpecificSuitBoxBTab.Enabled = False
            PayoffSpecificSuitBoxBTab.Text = 0
            PayoffSpecificSuitBJBoxBTab.Enabled = False
            PayoffSpecificSuitBJBoxBTab.Text = -1
            PayoffUCSpecificSuitBoxBTab.Enabled = False
            PayoffUCSpecificSuitBoxBTab.Text = 0
        Else
            SpadesButtonBTab.Enabled = True
            DiamondsButtonBTab.Enabled = True
            HeartsButtonBTab.Enabled = True
            ClubsButtonBTab.Enabled = True
            PayoffSpecificSuitBoxBTab.Enabled = True
            PayoffSpecificSuitBJBoxBTab.Enabled = True
            PayoffUCSpecificSuitBoxBTab.Enabled = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

    Private Sub SpadesButtonBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpadesButtonBTab.CheckedChanged
        If SpadesButtonBTab.Checked Then
            SuitLabelBTab.Text = "Spades Bonus"
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub HeartsButtonBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeartsButtonBTab.CheckedChanged
        If HeartsButtonBTab.Checked Then
            SuitLabelBTab.Text = "Hearts Bonus"
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub DiamondsButtonBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiamondsButtonBTab.CheckedChanged
        If DiamondsButtonBTab.Checked Then
            SuitLabelBTab.Text = "Diamonds Bonus"
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub ClubsButtonBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClubsButtonBTab.CheckedChanged
        If ClubsButtonBTab.Checked Then
            SuitLabelBTab.Text = "Clubs Bonus"
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub PreSplitCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreSplitCheckBTab.CheckedChanged
        If Not PreSplitCheckBTab.Checked And Not PostSplitCheckBTab.Checked Then
            MsgBox("The rule must be applied pre-split, post-split or both pre- and post-split", MsgBoxStyle.OKOnly)
            PreSplitCheckBTab.Checked = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub PostSplitCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostSplitCheckBTab.CheckedChanged
        If Not PreSplitCheckBTab.Checked And Not PostSplitCheckBTab.Checked Then
            MsgBox("The rule must be applied pre-split, post-split or both pre- and post-split", MsgBoxStyle.OKOnly)
            PostSplitCheckBTab.Checked = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub AceUpCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AceUpCheckBTab.CheckedChanged
        If (PayoffGeneralBJBoxBTab.Text <> -1 Or PayoffSuitedBJBoxBTab.Text <> -1 Or PayoffSpecificSuitBJBoxBTab.Text <> -1) And Not AceUpCheckBTab.Checked And Not TenUpCheckBTab.Checked Then
            MsgBox("The Dealer BJ Payoff must apply to either Ace up, Ten up or both.", MsgBoxStyle.OKOnly)
            AceUpCheckBTab.Checked = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub TenUpCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TenUpCheckBTab.CheckedChanged
        If (PayoffGeneralBJBoxBTab.Text <> -1 Or PayoffSuitedBJBoxBTab.Text <> -1 Or PayoffSpecificSuitBJBoxBTab.Text <> -1) And Not AceUpCheckBTab.Checked And Not TenUpCheckBTab.Checked Then
            MsgBox("The Dealer BJ Payoff must apply to either Ace up, Ten up or both.", MsgBoxStyle.OKOnly)
            TenUpCheckBTab.Checked = True
        End If
        BonusRulesCheckListBoxBTab.SelectedIndex() = -1
    End Sub

    Private Sub HandTotalComboBoxBTab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandTotalComboBoxBTab.SelectedIndexChanged
        BonusRulesCheckListBoxBTab.SelectedIndex = -1
    End Sub

#End Region

#Region " BJ Bonuses "

    Private Sub BJPaysBoxBJTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles BJPaysBoxBJTab.Validating
        If Not CheckValidDecimal(BJPaysBoxBJTab.Text, -1, 5, True) Then
            BJPaysBoxBJTab.Text = BJPaysBoxMTab.Text
            e.Cancel = True
        Else
            BJPaysBoxMTab.Text = BJPaysBoxBJTab.Text
        End If
        If PDTiesComboArray(5).SelectedIndex = 2 Then
            PDTiesArray(5).Text = BJPaysBoxBJTab.Text
        End If
    End Sub

    Private Sub BJSPlitAcesCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BJSPlitAcesCheckBJTab.CheckedChanged
        BJSPlitAcesCheckSpTab.Checked = BJSPlitAcesCheckBJTab.Checked
    End Sub

    Private Sub BJSPlitTensCheckBTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BJSPlitTensCheckBJTab.CheckedChanged
        BJSplitTensCheckSpTab.Checked = BJSPlitTensCheckBJTab.Checked
    End Sub

    Private Sub SuitedBJBoxBJTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SuitedBJBoxBJTab.Validating
        If Not CheckValidDecimal(SuitedBJBoxBJTab.Text, -1, 1000000, True) Then
            SuitedBJBoxBJTab.Text = 0
            e.Cancel = True
        End If
    End Sub

    Private Sub SpecificSuitBJBoxBJTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SpecificSuitBJBoxBJTab.Validating
        If Not CheckValidDecimal(SpecificSuitBJBoxBJTab.Text, -1, 1000000, True) Then
            SpecificSuitBJBoxBJTab.Text = 0
            e.Cancel = True
        End If
        If SpecificSuitBJBoxBJTab.Text = 0 Then
            SpadesButtonBJTab.Enabled = False
            HeartsButtonBJTab.Enabled = False
            DiamondsButtonBJTab.Enabled = False
            ClubsButtonBJTab.Enabled = False
        Else
            SpadesButtonBJTab.Enabled = True
            HeartsButtonBJTab.Enabled = True
            DiamondsButtonBJTab.Enabled = True
            ClubsButtonBJTab.Enabled = True
        End If
    End Sub

    Private Sub SpadesButtonBJTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpadesButtonBJTab.CheckedChanged
        If SpadesButtonBJTab.Checked Then
            SuitLabelBJTab.Text = "Spades Bonus"
        End If
    End Sub

    Private Sub HeartsButtonBJTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeartsButtonBJTab.CheckedChanged
        If HeartsButtonBJTab.Checked Then
            SuitLabelBJTab.Text = "Hearts Bonus"
        End If
    End Sub

    Private Sub DiamondsButtonBJTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiamondsButtonBJTab.CheckedChanged
        If DiamondsButtonBJTab.Checked Then
            SuitLabelBJTab.Text = "Diamonds Bonus"
        End If
    End Sub

    Private Sub ClubsButtonBJTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClubsButtonBJTab.CheckedChanged
        If ClubsButtonBJTab.Checked Then
            SuitLabelBJTab.Text = "Clubs Bonus"
        End If
    End Sub

    Private Sub Spec10SpadesButtonBJTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Spec10SpadesButtonBJTab.CheckedChanged
        If Spec10SpadesButtonBJTab.Checked Then
            Spec10SuitLabelBJTab.Text = "Specific Ten Spades Bonus"
        End If
    End Sub

    Private Sub Spec10HeartsButtonBJTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Spec10HeartsButtonBJTab.CheckedChanged
        If Spec10HeartsButtonBJTab.Checked Then
            Spec10SuitLabelBJTab.Text = "Specific Ten Hearts Bonus"
        End If
    End Sub

    Private Sub Spec10DiamondsButtonBJTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Spec10DiamondsButtonBJTab.CheckedChanged
        If Spec10DiamondsButtonBJTab.Checked Then
            Spec10SuitLabelBJTab.Text = "Specific Ten Diamonds Bonus"
        End If
    End Sub

    Private Sub Spec10ClubsButtonBJTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Spec10ClubsButtonBJTab.CheckedChanged
        If Spec10ClubsButtonBJTab.Checked Then
            Spec10SuitLabelBJTab.Text = "Specific Ten Clubs Bonus"
        End If
    End Sub

    Private Sub Spec10BoxlBJTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Spec10BoxlBJTab.Validating
        If Not CheckValidDecimal(Spec10BoxlBJTab.Text, -1, 1000000, True) Then
            Spec10BoxlBJTab.Text = 0
            e.Cancel = True
        End If
        If (Spec10BoxlBJTab.Text = 0 And Spec10SuitedBoxBJTab.Text = 0 And Spec10SpecSuitBoxBJTab.Text = 0) Then
            Spec10FractionBoxBJTab.Enabled = False
        Else
            Spec10FractionBoxBJTab.Enabled = True
        End If
    End Sub

    Private Sub Spec10SuitedBoxBJTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Spec10SuitedBoxBJTab.Validating
        If Not CheckValidDecimal(Spec10SuitedBoxBJTab.Text, -1, 1000000, True) Then
            Spec10SuitedBoxBJTab.Text = 0
            e.Cancel = True
        End If
        If (Spec10BoxlBJTab.Text = 0 And Spec10SuitedBoxBJTab.Text = 0 And Spec10SpecSuitBoxBJTab.Text = 0) Then
            Spec10FractionBoxBJTab.Enabled = False
        Else
            Spec10FractionBoxBJTab.Enabled = True
        End If
    End Sub

    Private Sub Spec10SpecSuitBoxBJTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Spec10SpecSuitBoxBJTab.Validating
        If Not CheckValidDecimal(Spec10SpecSuitBoxBJTab.Text, -1, 1000000, True) Then
            Spec10SpecSuitBoxBJTab.Text = 0
            e.Cancel = True
        End If
        If (Spec10BoxlBJTab.Text = 0 And Spec10SuitedBoxBJTab.Text = 0 And Spec10SpecSuitBoxBJTab.Text = 0) Then
            Spec10FractionBoxBJTab.Enabled = False
        Else
            Spec10FractionBoxBJTab.Enabled = True
        End If
        If Spec10SpecSuitBoxBJTab.Text = 0 Then
            Spec10SpadesButtonBJTab.Enabled = False
            Spec10HeartsButtonBJTab.Enabled = False
            Spec10DiamondsButtonBJTab.Enabled = False
            Spec10ClubsButtonBJTab.Enabled = False
        Else
            Spec10SpadesButtonBJTab.Enabled = True
            Spec10HeartsButtonBJTab.Enabled = True
            Spec10DiamondsButtonBJTab.Enabled = True
            Spec10ClubsButtonBJTab.Enabled = True
        End If
    End Sub

    Private Sub Spec10FractionBoxBJTab_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Spec10FractionBoxBJTab.Validating
        If Not CheckValidDecimal(Spec10FractionBoxBJTab.Text, 0, 1, True) Then
            Spec10FractionBoxBJTab.Text = 0.25
            e.Cancel = True
        End If
    End Sub

#End Region

#End Region

#Region " Forced Tab "

#Region " Forced General "

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
                    If (row = 0 Or row = 1) And tempForcedRule.Strat = Constants.Strat.P Then
                        tempForcedRule.Strat = Constants.Strat.PH
                    End If
                    tempForcedRule.UseSpecificHand = True
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
        Dim tempList As New BJCAForcedRulesListClass

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

            HandTotalComboBoxFSTab.Text = 0
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

    Private Sub AddForcedRuleButtonFSTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddForcedRuleButtonFSTab.Click
        Dim rule As Integer
        Dim rulePresent As Boolean

        rulePresent = False
        For rule = 0 To FormRules.ForcedStrat.ForcedRulesList.NumRules - 1
            If FormRules.ForcedStrat.ForcedRulesList.L(rule).Name = ForcedRuleNameBoxFSTab.Text Then
                rulePresent = True
                Exit For
            End If
        Next rule
        If rulePresent Then
            ForcedRulesCheckListBoxFSTab.SelectedIndex = -1
            MsgBox("A rule by this name already exists.", MsgBoxStyle.OKOnly)
        Else
            GetFormCurrentForcedRule()
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
        If ForcedStratTabControlFSTab.SelectedIndex = 4 Then
            ClearForcedTablesButtonFSTab.Visible = False
            SaveForcedTablesButtonFSTab.Visible = False
            LoadForcedTablesButtonFSTab.Visible = False
        Else
            ClearForcedTablesButtonFSTab.Visible = True
            SaveForcedTablesButtonFSTab.Visible = True
            LoadForcedTablesButtonFSTab.Visible = True
        End If
    End Sub

    Private Sub DeleteAllForcedRules()
        If ForcedRulesCheckListBoxFSTab.Items.Count > 0 Then
            Dim rule As Integer

            ForcedRulesCheckListBoxFSTab.Items.Clear()

            For rule = FormRules.ForcedStrat.ForcedRulesList.NumRules - 1 To 0 Step -1
                FormRules.ForcedStrat.ForcedRulesList.DeleteForcedRule(rule)
            Next rule
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
        If Not PreSplitCheckFSTab.Checked And Not PostSplitCheckFSTab.Checked And Not PostDoubleCheckFSTab.Checked Then
            MsgBox("The rule must be applied to at least some hands.", MsgBoxStyle.OKOnly)
            PreSplitCheckFSTab.Checked = True
        End If
        If PreSplitCheckFSTab.Checked Then
            PostDoubleCheckFSTab.Checked = False
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub PostSplitCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostSplitCheckFSTab.CheckedChanged
        If Not PreSplitCheckFSTab.Checked And Not PostSplitCheckFSTab.Checked And Not PostDoubleCheckFSTab.Checked Then
            MsgBox("The rule must be applied to at least some hands.", MsgBoxStyle.OKOnly)
            PostSplitCheckFSTab.Checked = True
        End If
        If PostSplitCheckFSTab.Checked Then
            PostDoubleCheckFSTab.Checked = False
        End If
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub PostDoubleCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostDoubleCheckFSTab.CheckedChanged
        If Not PreSplitCheckFSTab.Checked And Not PostSplitCheckFSTab.Checked And Not PostDoubleCheckFSTab.Checked Then
            MsgBox("The rule must be applied to at least some hands.", MsgBoxStyle.OKOnly)
            PostDoubleCheckFSTab.Checked = True
        End If
        If PostDoubleCheckFSTab.Checked Then
            PreSplitCheckFSTab.Checked = False
            PostSplitCheckFSTab.Checked = False
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

    Private Sub TDCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ForcedRulesCheckListBoxFSTab.SelectedIndex() = -1
    End Sub

    Private Sub TCCheckFSTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

#End Region

#End Region

#Region " Other Tab "

    Private Sub PopulateUCCheckTable()
        Dim row As Integer

        For row = 0 To 9
            Dim box As New CheckBox

            box.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            box.Size = New System.Drawing.Size(48, 16)
            If row = 0 Then
                box.Text = "Ace"
                box.Location = New System.Drawing.Point(130, 95 + 20 * 9)
            ElseIf row = 9 Then
                box.Text = "Ten"
                box.Location = New System.Drawing.Point(130, 95 + 20 * (row - 1))
            Else
                box.Text = row + 1
                box.Location = New System.Drawing.Point(130, 95 + 20 * (row - 1))
            End If

            'Add the CheckBox to the array so it can be accessed by index.
            Me.UCChecksArray(row) = box

            'Add the CheckBox to the Controls collection so it is visible.
            Me.UCAllowedGroupOTab.Controls.Add(box)

        Next row
    End Sub

    Private Sub ToggleAllCheckOTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToggleAllCheckOTab.CheckedChanged
        Dim row As Integer

        For row = 0 To 9
            UCChecksArray(row).Checked = ToggleAllCheckOTab.Checked
        Next row

    End Sub

    Private Sub UpdateAllColors()
        Dim row As Integer
        Dim upcard As Integer

        For upcard = 0 To 9
            'Recolor Hard TD Table
            For row = 0 To 16
                HardTDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(HardTDForcedTableArray(row, upcard).Index)
            Next row
            'Recolor Hard CD Table
            For row = 0 To 35
                HardCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(HardCDForcedTableArray(row, upcard).Index)
            Next row
            'Recolor Soft Tables
            For row = 0 To 8
                SoftTDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(SoftTDForcedTableArray(row, upcard).Index)
                SoftCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(SoftCDForcedTableArray(row, upcard).Index)
            Next row
            'Recolor Pair Tables
            For row = 0 To 9
                PairCDForcedTableArray(row, upcard).BackColor = FormRules.ColorTable.C(PairCDForcedTableArray(row, upcard).Index)
            Next row
        Next upcard

        ForcedRuleStratBoxFSTab.BackColor = FormRules.ColorTable.C(CurrentForcedRuleStrat)
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
            Me.StratColorBoxArray(strat) = box

            'Add the CheckBox to the Controls collection so it is visible.
            Me.ColorTableGroupOTab.Controls.Add(box)

        Next strat
    End Sub

    Private Sub StratColorBoxArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StratColorBoxArrayHandler.Click
        Dim colorBox As New System.Windows.Forms.ColorDialog

        If colorBox.ShowDialog() = Windows.Forms.DialogResult.OK Then
            FormRules.ColorTable.C(DirectCast(sender, IndexedTextBox).Index) = colorBox.Color
            StratColorBoxArray(DirectCast(sender, IndexedTextBox).Index).BackColor = colorBox.Color
            UpdateAllColors()
        End If

        'UpdateForcedColorTable.C()
        colorBox.Dispose()
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

#End Region

#Region " File Methods "

    Private Sub LoadINI()

        INIPath = CurDir() + "\BJCA.ini"
        ProbsPath = CurDir()

        'The INI File is actually just a file set file.
        'See if INI File is valid
        If System.IO.File.Exists(INIPath) Then
            Try
                FormRules = CType(LoadObjectFile("BJCA.ini"), BJCAFormRulesClass)
                DefaultFormRules = CType(LoadObjectFile("BJCA.ini"), BJCAFormRulesClass)
            Catch
                MsgBox("The INI File is invalid - rewriting.")
                SaveINI(True)
            End Try
        Else
            MsgBox("The INI File is missing - rewriting.")
            SaveINI(True)
        End If

        LoadFormRules()

    End Sub

    Private Sub SaveINI(Optional ByVal useAllDefaults As Boolean = False)

        If useAllDefaults Then
            RestoreOriginalDefaults()
            FormRules.FileNames.DefaultPath = "."
            FormRules.FileNames.OutputPath = "."
        End If

        SaveObjectFile(INIPath, FormRules)
    End Sub

    Private Sub RestoreDefaultGeneralRulesButtonFTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Restoring the default General Rules will undo all selections EXCEPT for those that can be saved separately." + Chr(13) + Chr(9) + Chr(9) + Chr(9) + Chr(9) + "Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreDefaultGeneralRules()
        End If
    End Sub

    Private Sub RestoreDefaultBonusRulesButtonFTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Restoring the default Bonus Rules will erase any user defined bonus rules." + Chr(13) + Chr(9) + Chr(9) + "      Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreDefaultBonusRules()
        End If
    End Sub

    Private Sub RestoreDefaultForcedTablesButtonFTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Restoring the default Forced Tables will clear the tables." + Chr(13) + Chr(9) + "       Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreDefaultForcedTables()
        End If
    End Sub

    Private Sub RestoreDefaultForcedRulesButtonFTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Restoring the default Forced Rules will erase any user defined forced rules." + Chr(13) + Chr(9) + Chr(9) + "       Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreDefaultForcedRules()
        End If
    End Sub

    Private Sub RestoreDefaultDoubleTableButtonFTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Restoring the default Double Table will clear the table." + Chr(13) + Chr(9) + "      Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreDefaultDoubleTable()
        End If
    End Sub

    Private Sub RestoreDefaultForcedShoeButtonFTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Restoring the default Forced Shoe will erase the current shoe." + Chr(13) + Chr(9) + "           Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreDefaultForcedShoe()
        End If
    End Sub

    Private Sub SaveBonusRulesFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBonusRulesButtonBTab.Click
        SaveBonusRulesFile()
    End Sub

    Private Sub LoadBonusRulesFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadBonusRulesButtonBTab.Click
        LoadBonusRulesFile()
    End Sub

    Private Sub SaveForcedRulesFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavedForcedRulesButtonFSTab.Click
        SaveForcedRulesFile()
    End Sub

    Private Sub LoadForcedRulesFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadForcedRulesButtonFSTab.Click
        LoadForcedRulesFile()
    End Sub

    Private Sub SaveColorTableFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveColorTableFileButtonOTab.Click
        SaveColorTableFile()
    End Sub

    Private Sub LoadColorTableFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadColorTableFileButtonOTab.Click
        LoadColorTableFile()
    End Sub

    Private Sub SaveDoubleTableFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveDoubleTableFileButtonDTab.Click
        SaveDoubleTableFile()
    End Sub

    Private Sub LoadDoubleTableFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadDoubleTableFileButtonDTab.Click
        LoadDoubleTableFile()
    End Sub

    Private Sub SaveForcedShoeFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveForcedShoeFileButtonShTab.Click
        SaveForcedShoeFile()
    End Sub

    Private Sub LoadForcedShoeFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadForcedShoeFileButtonShTab.Click
        LoadForcedShoeFile()
    End Sub

    Private Sub SaveForcedTablesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveForcedTablesButtonFSTab.Click
        SaveForcedTablesFile()
    End Sub

    Private Sub LoadForcedTablesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadForcedTablesButtonFSTab.Click
        LoadForcedTablesFile()
    End Sub

    Private Sub SaveBonusRulesFile()
        Dim sfd As New SaveFileDialog

        GetBonusRulesOn()
        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = FormRules.FileNames.BonusRulesFileExt
        sfd.FileName = GetFileName(FormRules.FileNames.BonusRulesFileName)
        sfd.InitialDirectory = FormRules.FileNames.DefaultPath
        sfd.Filter = ("Bonus Rules Files (*" + FormRules.FileNames.BonusRulesFileExt + ")|*" + FormRules.FileNames.BonusRulesFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SaveObjectFile(sfd.FileName, FormRules.BonusRulesList)
            FormRules.FileNames.BonusRulesFileName = sfd.FileName
            FormRules.FileNames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadBonusRulesFile(Optional ByVal getName As Boolean = True)
        Dim tempList As New BJCABonusRulesListClass

        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                FormRules.BonusRulesList = CType(LoadObjectFile(FormRules.FileNames.BonusRulesFileName), BJCABonusRulesListClass)
                LoadFormBonusRulesList(FormRules.BonusRulesList)
            Catch
                MsgBox("The file: " + GetFileName(FormRules.FileNames.BonusRulesFileName) + " is not a valid Bonus Rules file.")
                RestoreDefaultBonusRules()
            Finally
            End Try
        Else
            Dim ofd As New OpenFileDialog
            Dim fileOK As Boolean

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = FormRules.FileNames.BonusRulesFileExt
            ofd.FileName = GetFileName(FormRules.FileNames.BonusRulesFileName)
            ofd.InitialDirectory = FormRules.FileNames.DefaultPath
            ofd.Filter = ("Bonus Rules Files (*" + FormRules.FileNames.BonusRulesFileExt + ")|*" + FormRules.FileNames.BonusRulesFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    tempList = CType(LoadObjectFile(ofd.FileName), BJCABonusRulesListClass)
                    DeleteAllBonusRules()
                    FormRules.BonusRulesList = CloneObject(tempList)
                    FormRules.FileNames.BonusRulesFileName = ofd.FileName
                    FormRules.FileNames.DefaultPath = GetPath(ofd.FileName)
                    fileOK = True
                Catch
                    MsgBox("The file: " + GetFileName(ofd.FileName) + " is not a valid Bonus Rules file.")
                    fileOK = False
                End Try
                If fileOK Then
                    LoadFormBonusRulesList(FormRules.BonusRulesList)
                End If
            End If
            ofd.Dispose()
        End If
    End Sub

    Private Sub SaveForcedRulesFile()
        Dim sfd As New SaveFileDialog

        GetForcedRulesOn()
        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = FormRules.FileNames.ForcedRulesFileExt
        sfd.FileName = GetFileName(FormRules.FileNames.ForcedRulesFileName)
        sfd.InitialDirectory = FormRules.FileNames.DefaultPath
        sfd.Filter = ("Forced Rules Files (*" + FormRules.FileNames.ForcedRulesFileExt + ")|*" + FormRules.FileNames.ForcedRulesFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SaveObjectFile(sfd.FileName, FormRules.ForcedStrat.ForcedRulesList)
            FormRules.FileNames.ForcedRulesFileName = sfd.FileName
            FormRules.FileNames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadForcedRulesFile(Optional ByVal getName As Boolean = True)
        Dim tempList As New BJCAForcedRulesListClass

        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                FormRules.ForcedStrat.ForcedRulesList = CType(LoadObjectFile(FormRules.FileNames.ForcedRulesFileName), BJCAForcedRulesListClass)
                LoadFormForcedRulesList(FormRules.ForcedStrat.ForcedRulesList)
            Catch
                MsgBox("The file: " + GetFileName(FormRules.FileNames.ForcedRulesFileName) + " is not a valid Forced Rules file.")
                RestoreDefaultForcedRules()
            Finally
            End Try
        Else
            Dim ofd As New OpenFileDialog
            Dim fileOK As Boolean

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = FormRules.FileNames.ForcedRulesFileExt
            ofd.FileName = GetFileName(FormRules.FileNames.ForcedRulesFileName)
            ofd.InitialDirectory = FormRules.FileNames.DefaultPath
            ofd.Filter = ("Forced Rules Files (*" + FormRules.FileNames.ForcedRulesFileExt + ")|*" + FormRules.FileNames.ForcedRulesFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    templist = CType(LoadObjectFile(ofd.FileName), BJCAForcedRulesListClass)
                    DeleteAllForcedRules()
                    FormRules.ForcedStrat.ForcedRulesList = CloneObject(tempList)
                    FormRules.FileNames.ForcedRulesFileName = ofd.FileName
                    FormRules.FileNames.DefaultPath = GetPath(ofd.FileName)
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

    Private Sub SaveColorTableFile()
        Dim sfd As New SaveFileDialog

        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = FormRules.FileNames.ColorTableFileExt
        sfd.FileName = GetFileName(FormRules.FileNames.ColorTableFileName)
        sfd.InitialDirectory = FormRules.FileNames.DefaultPath
        sfd.Filter = ("Color Table Files (*" + FormRules.FileNames.ColorTableFileExt + ")|*" + FormRules.FileNames.ColorTableFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SaveObjectFile(sfd.FileName, FormRules.ColorTable)
            FormRules.FileNames.ColorTableFileName = sfd.FileName
            FormRules.FileNames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadColorTableFile(Optional ByVal getName As Boolean = True)
        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                FormRules.ColorTable = CType(LoadObjectFile(FormRules.FileNames.ColorTableFileName), BJCAColorTableClass)
                LoadFormColorTable()
            Catch
                MsgBox("The file: " + GetFileName(FormRules.FileNames.ColorTableFileName) + " is not a valid Color Table file.")
                RestoreDefaultColorTable()
            Finally
            End Try
        Else
            Dim ofd As New OpenFileDialog
            Dim fileOK As Boolean

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = FormRules.FileNames.ColorTableFileExt
            ofd.FileName = GetFileName(FormRules.FileNames.ColorTableFileName)
            ofd.InitialDirectory = FormRules.FileNames.DefaultPath
            ofd.Filter = ("Color Table Files (*" + FormRules.FileNames.ColorTableFileExt + ")|*" + FormRules.FileNames.ColorTableFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    FormRules.ColorTable = CType(LoadObjectFile(ofd.FileName), BJCAColorTableClass)
                    FormRules.FileNames.ColorTableFileName = ofd.FileName
                    FormRules.FileNames.DefaultPath = GetPath(ofd.FileName)
                    fileOK = True
                Catch
                    MsgBox("The file: " + GetFileName(ofd.FileName) + " is not a valid Color Table file.")
                    fileOK = False
                End Try
                If fileOK Then
                    LoadFormColorTable()
                End If
            End If
            ofd.Dispose()
        End If
    End Sub

    Private Sub SaveDoubleTableFile()
        Dim sfd As New SaveFileDialog

        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = FormRules.FileNames.DoubleTableFileExt
        sfd.FileName = GetFileName(FormRules.FileNames.DoubleTableFileName)
        sfd.InitialDirectory = FormRules.FileNames.DefaultPath
        sfd.Filter = ("Double Table Files (*" + FormRules.FileNames.DoubleTableFileExt + ")|*" + FormRules.FileNames.DoubleTableFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            GetFormDoubleTable()
            SaveObjectFile(sfd.FileName, FormRules.DoubleTable)
            FormRules.FileNames.DoubleTableFileName = sfd.FileName
            FormRules.FileNames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadDoubleTableFile(Optional ByVal getName As Boolean = True)
        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                FormRules.DoubleTable = CType(LoadObjectFile(FormRules.FileNames.DoubleTableFileName), BJCADoubleTableClass)
                LoadFormDoubleTable()
            Catch
                MsgBox("The file: " + GetFileName(FormRules.FileNames.DoubleTableFileName) + " is not a valid Double Table file.")
                RestoreDefaultDoubleTable()
            Finally
            End Try
        Else
            Dim ofd As New OpenFileDialog
            Dim fileOK As Boolean

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = FormRules.FileNames.DoubleTableFileExt
            ofd.FileName = GetFileName(FormRules.FileNames.DoubleTableFileName)
            ofd.InitialDirectory = FormRules.FileNames.DefaultPath
            ofd.Filter = ("Double Table Files (*" + FormRules.FileNames.DoubleTableFileExt + ")|*" + FormRules.FileNames.DoubleTableFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    FormRules.DoubleTable = CType(LoadObjectFile(ofd.FileName), BJCADoubleTableClass)
                    FormRules.FileNames.DoubleTableFileName = ofd.FileName
                    FormRules.FileNames.DefaultPath = GetPath(ofd.FileName)
                    fileOK = True
                Catch
                    MsgBox("The file: " + GetFileName(ofd.FileName) + " is not a valid Double Table file.")
                    fileOK = False
                End Try
                If fileOK Then
                    LoadFormDoubleTable()
                End If
            End If
            ofd.Dispose()
        End If
    End Sub

    Private Sub SaveForcedShoeFile()
        Dim sfd As New SaveFileDialog

        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = FormRules.FileNames.ForcedShoeFileExt
        sfd.FileName = GetFileName(FormRules.FileNames.ForcedShoeFileName)
        sfd.InitialDirectory = FormRules.FileNames.DefaultPath
        sfd.Filter = ("Forced Shoe Files (*" + FormRules.FileNames.ForcedShoeFileExt + ")|*" + FormRules.FileNames.ForcedShoeFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            GetFormForcedShoe()
            SaveObjectFile(sfd.FileName, FormRules.ForcedShoe)
            FormRules.FileNames.ForcedShoeFileName = sfd.FileName
            FormRules.FileNames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadForcedShoeFile(Optional ByVal getName As Boolean = True)
        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                FormRules.ForcedShoe = CType(LoadObjectFile(FormRules.FileNames.ForcedShoeFileName), BJCAShoeClass)
                LoadFormForcedShoe()
            Catch
                MsgBox("The file: " + GetFileName(FormRules.FileNames.ForcedShoeFileName) + " is not a valid Forced Shoe file.")
                RestoreDefaultForcedShoe()
            Finally
            End Try
        Else
            Dim ofd As New OpenFileDialog
            Dim fileOK As Boolean

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = FormRules.FileNames.ForcedShoeFileExt
            ofd.FileName = GetFileName(FormRules.FileNames.ForcedShoeFileName)
            ofd.InitialDirectory = FormRules.FileNames.DefaultPath
            ofd.Filter = ("Forced Shoe Files (*" + FormRules.FileNames.ForcedShoeFileExt + ")|*" + FormRules.FileNames.ForcedShoeFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    FormRules.ForcedShoe = CType(LoadObjectFile(ofd.FileName), BJCAShoeClass)
                    FormRules.FileNames.ForcedShoeFileName = ofd.FileName
                    FormRules.FileNames.DefaultPath = GetPath(ofd.FileName)
                    fileOK = True
                Catch
                    MsgBox("The file: " + GetFileName(ofd.FileName) + " is not a valid Forced Shoe file.")
                    fileOK = False
                End Try
                If fileOK Then
                    LoadFormForcedShoe()
                End If
            End If
            ofd.Dispose()
        End If
    End Sub

    Private Sub SaveForcedTablesFile()
        Dim sfd As New SaveFileDialog
        Dim forcedtable As New BJCAForcedRulesTableClass

        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = FormRules.FileNames.ForcedTablesFileExt
        sfd.FileName = GetFileName(FormRules.FileNames.ForcedTablesFileName)
        sfd.InitialDirectory = FormRules.FileNames.DefaultPath
        sfd.Filter = ("Forced Tables Files (*" + FormRules.FileNames.ForcedTablesFileExt + ")|*" + FormRules.FileNames.ForcedTablesFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            GetFormForcedTables()
            forcedtable.ForcedTableRulesList = CloneObject(FormRules.ForcedStrat.ForcedTableRulesList)
            forcedtable.ForcednCD = CInt(ForcednCDBoxFSTab.Text)
            forcedtable.ForcedTablePreSplit = ForcedTablePreCheckFSTab.Checked
            forcedtable.ForcedTablePostSplit = ForcedTablePostCheckFSTab.Checked
            SaveObjectFile(sfd.FileName, forcedtable)
            FormRules.FileNames.ForcedTablesFileName = sfd.FileName
            FormRules.FileNames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadForcedTablesFile(Optional ByVal getName As Boolean = True)
        Dim forcedtable As BJCAForcedRulesTableClass

        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                FormRules.ForcedStrat.ForcedTableRulesList = CType(LoadObjectFile(FormRules.FileNames.ForcedTablesFileName), BJCAForcedRulesListClass)
                LoadFormForcedTables()
            Catch
                MsgBox("The file: " + GetFileName(FormRules.FileNames.ForcedTablesFileName) + " is not a valid Forced Tables file.")
                RestoreDefaultForcedTables()
            Finally
            End Try
        Else
            Dim ofd As New OpenFileDialog
            Dim fileOK As Boolean

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = FormRules.FileNames.ForcedTablesFileExt
            ofd.FileName = GetFileName(FormRules.FileNames.ForcedTablesFileName)
            ofd.InitialDirectory = FormRules.FileNames.DefaultPath
            ofd.Filter = ("Forced Tables Files (*" + FormRules.FileNames.ForcedTablesFileExt + ")|*" + FormRules.FileNames.ForcedTablesFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    forcedtable = CType(LoadObjectFile(ofd.FileName), BJCAForcedRulesTableClass)
                    FormRules.ForcedStrat.ForcedTableRulesList = CloneObject(forcedtable.ForcedTableRulesList)
                    ForcednCDBoxFSTab.Text = forcedtable.ForcednCD
                    ForcedTablePreCheckFSTab.Checked = forcedtable.ForcedTablePreSplit
                    ForcedTablePostCheckFSTab.Checked = forcedtable.ForcedTablePostSplit
                    FormRules.FileNames.ForcedTablesFileName = ofd.FileName
                    FormRules.FileNames.DefaultPath = GetPath(ofd.FileName)
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

    Private Sub SaveRuleSetFile()
        Dim sfd As New SaveFileDialog

        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True
        sfd.AddExtension = True
        sfd.DefaultExt = FormRules.FileNames.RuleSetFileExt
        sfd.FileName = GetFileName(FormRules.FileNames.RuleSetFileName)
        sfd.InitialDirectory = FormRules.FileNames.DefaultPath
        sfd.Filter = ("Rule Set Files (*" + FormRules.FileNames.RuleSetFileExt + ")|*" + FormRules.FileNames.RuleSetFileExt)
        sfd.ValidateNames = True
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            GetFormRules()
            SaveObjectFile(sfd.FileName, FormRules)
            FormRules.FileNames.RuleSetFileName = sfd.FileName
            FormRules.FileNames.DefaultPath = GetPath(sfd.FileName)
        End If
        sfd.Dispose()
    End Sub

    Private Sub LoadRuleSetFile(Optional ByVal getName As Boolean = True)
        Dim tempRules As BJCAFormRulesClass

        'Use defaults if file is present or it is not valid
        If getName = False Then
            Try
                tempRules = CType(LoadObjectFile(FormRules.FileNames.RuleSetFileName), BJCAFormRulesClass)
                DeleteAllBonusRules()
                DeleteAllForcedRules()
                FormRules = CloneObject(tempRules)
            Catch
                MsgBox("The file: " + GetFileName(FormRules.FileNames.RuleSetFileName) + " is not a valid File Set file.")
                RestoreOriginalDefaults()
            End Try
            LoadFormRules()
        Else
            Dim ofd As New OpenFileDialog

            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.AddExtension = True
            ofd.DefaultExt = FormRules.FileNames.RuleSetFileExt
            ofd.FileName = GetFileName(FormRules.FileNames.RuleSetFileName)
            ofd.InitialDirectory = FormRules.FileNames.DefaultPath
            ofd.Filter = ("Rule Set Files (*" + FormRules.FileNames.RuleSetFileExt + ")|*" + FormRules.FileNames.RuleSetFileExt)
            ofd.ValidateNames = True
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    tempRules = CType(LoadObjectFile(ofd.FileName), BJCAFormRulesClass)
                    DeleteAllBonusRules()
                    DeleteAllForcedRules()
                    FormRules = CloneObject(tempRules)
                    FormRules.FileNames.RuleSetFileName = ofd.FileName
                    FormRules.FileNames.DefaultPath = GetPath(ofd.FileName)
                    LoadFormRules()
                Catch
                    MsgBox("The file: " + GetFileName(ofd.FileName) + " is not a valid Rule Set file.")
                End Try
            End If
            ofd.Dispose()
        End If

    End Sub

#End Region

#Region " Realtime Analysis "

    Private Sub StartRealtime()
        Dim RealtimeForm As New BJCARealtimeForm
        Dim SmallRealtimeForm As New BJCASmallRealTimeForm
        Dim response As MsgBoxResult
        Dim useEstimate As Boolean

        Try
            GetFormRules()
            If FormRules.General.RTSPL1Est Then
                useEstimate = True
            Else
                useEstimate = False
            End If
            If FormRules.General.RTSmall Then
                SmallRealtimeForm.UseSPL1Estimate = useEstimate
                SmallRealtimeForm.FileNames = FormRules.FileNames
                SmallRealtimeForm.OriginalRules = CloneObject(Rules)
                SmallRealtimeForm.Rules = CloneObject(Rules)
                SmallRealtimeForm.OriginalShoe.Reset(Rules.Shoe)
                SmallRealtimeForm.LoadFormRealtime()

                Me.Visible = False
                SmallRealtimeForm.ShowDialog()
                Me.Visible = True

                SmallRealtimeForm.Dispose()
            Else
                RealtimeForm.UseSPL1Estimate = useEstimate
                RealtimeForm.FileNames = FormRules.FileNames
                RealtimeForm.OriginalRules = CloneObject(Rules)
                RealtimeForm.Rules = CloneObject(Rules)
                RealtimeForm.OriginalShoe.Reset(Rules.Shoe)
                RealtimeForm.LoadFormRealtime()

                Me.Visible = False
                RealtimeForm.ShowDialog()
                Me.Visible = True

                RealtimeForm.Dispose()
            End If
        Catch
            MsgBox("An error has occurred - please restart the program.")
            Me.Close()
        End Try
    End Sub

#End Region

#Region " Main Form Menu Methods "

    Private Sub CalcNowMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalcNowMenuItem.Click
        CalculateNow()
    End Sub

#Region " Help Menu Item Methods"

    Private Sub HelpFileMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpFileMenuItem.Click
        Dim helpfile As String

        Try
            helpfile = GetPath(INIPath) + "\MGP's BJ CA Help.pdf"

            Start(helpfile)
        Catch
            MsgBox("The help file could not be located")
        End Try
    End Sub

    Private Sub AboutMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutMenuItem.Click
        Dim aboutDlg As New BJCAAboutForm

        aboutDlg.ShowDialog()
        aboutDlg.Dispose()
    End Sub

#End Region

#Region " Realtime Menu Item Methods "

    Private Sub RTSPL1EstMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RTSPL1EstMenuItem.Click
        If RTSPL1EstMenuItem.Checked Then
            RTSPL1EstMenuItem.Checked = False
        Else
            RTSPL1EstMenuItem.Checked = True
        End If
    End Sub

    Private Sub RTSmallMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RTSmallMenuItem.Click
        If RTSmallMenuItem.Checked Then
            RTSmallMenuItem.Checked = False
        Else
            RTSmallMenuItem.Checked = True
        End If
    End Sub

    Private Sub StartRTMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartRTMenuItem.Click
        StartRealtime()
    End Sub

#End Region

#Region " Options Menu Item Methods "

    Private Sub SetDefaultsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetDefaultsMenuItem.Click
        If MsgBox("Would you like to overwrite the current default settings?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            GetFormRules()
            DefaultFormRules = CloneObject(FormRules)
            SaveINI()
        End If
    End Sub

    Private Sub RestoreDefaultsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreDefaultsMenuItem.Click
        If MsgBox("Restoring the original default rules will undo all selections INCLUDING those that can be saved separately." + Chr(13) + Chr(9) + Chr(9) + Chr(9) + "           Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreOriginalDefaults()
        End If
    End Sub

    Private Sub UseDPDictionaryMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseDPDictionaryMenuItem.Click
        If UseDPDictionaryMenuItem.Checked Then
            UseDPDictionaryMenuItem.Checked = False
        Else
            UseDPDictionaryMenuItem.Checked = True
        End If
        If UseDPDictionaryMenuItem.Checked Then
            SaveDPDictionaryMenuItem.Enabled = True
        Else
            SaveDPDictionaryMenuItem.Checked = False
            SaveDPDictionaryMenuItem.Enabled = False
        End If

    End Sub

    Private Sub SaveDPDictionaryMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveDPDictionaryMenuItem.Click
        If SaveDPDictionaryMenuItem.Checked Then
            SaveDPDictionaryMenuItem.Checked = False
        Else
            SaveDPDictionaryMenuItem.Checked = True
        End If
    End Sub

    Private Sub PSExceptionsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PSExceptionsMenuItem.Click
        If PSExceptionsMenuItem.Checked Then
            PSExceptionsMenuItem.Checked = False
        Else
            PSExceptionsMenuItem.Checked = True
        End If
    End Sub

#End Region

#Region " Strategy Menu Item Methods "

    Private Sub ComputeTDMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComputeTDMenuItem.Click
        If ComputeTDMenuItem.Checked Then
            ComputeTDMenuItem.Checked = False
        Else
            ComputeTDMenuItem.Checked = True
        End If
    End Sub

    Private Sub ComputeTCMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComputeTCMenuItem.Click
        If ComputeTCMenuItem.Checked Then
            ComputeTCMenuItem.Checked = False
        Else
            ComputeTCMenuItem.Checked = True
        End If
    End Sub

    Private Sub ComputeForcedMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComputeForcedMenuItem.Click
        If ComputeForcedMenuItem.Checked Then
            ComputeForcedMenuItem.Checked = False
        Else
            ComputeForcedMenuItem.Checked = True
        End If
    End Sub

#End Region

#Region " File Menu Item Methods "

    Private Sub NewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewMenuItem.Click
        If MsgBox("This will undo all selections INCLUDING those that can be saved separately." + Chr(13) + Chr(9) + Chr(9) + Chr(9) + "           Do you still wish to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            RestoreCurrentDefaults()
        End If
    End Sub

    Private Sub OpenRuleSetMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenRuleSetMenuItem.Click
        LoadRuleSetFile()
    End Sub

    Private Sub SaveRuleSetMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveRuleSetMenuItem.Click
        SaveRuleSetFile()
    End Sub

#End Region

#End Region







End Class


