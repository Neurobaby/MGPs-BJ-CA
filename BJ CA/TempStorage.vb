    'Finding "My Documents"
    '   sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
    'Including "All Files
    '   ofd.Filter = ("Test files (*.test)|*.test|All files (*.*)|*.*")


        '        Dim pform As New BJCAProgressForm

        '        pform.Visible = True
        '        pform.PBarPForm.Minimum = 1
        '        pform.PBarPForm.Maximum = NumPHands
        '        pform.PBarPForm.Text = "Computing Stand Values"
        '            pform.PBarPForm.PerformStep()
        '        pform.Visible = False


        '       Dim oexcel As New Excel.Application
        '       Dim oexcelbook As Excel.Workbook
        '       Dim oexcelsheet As Excel.Worksheet

        '        oexcelbook = oexcel.Workbooks.Open("C:\Documents and Settings\Anon\My Documents\Blackjack\VB\BJ CA.xls", , False)
        '        oexcelsheet = oexcelbook.Worksheets("Rules")
        '        oexcelsheet.Select()
        '        oexcelsheet.Range("A30").Formula = "Finally!!!"
        '        oexcel.Visible = True
        '        oexcelbook.Close()
        '        oexcel.Quit()

'Change Excel Cell Color
'        worksheet.Range("A1").Interior.Color = RGB(ColorTable.C(1).R, ColorTable.C(1).G, ColorTable.C(1).B)




        'Now we need to fill in any strategies that may be overlooked because
        '    of post-split strategy changes
        For Total = 4 To 21
            For Soft = 0 To 1
                If cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.None Then
                    'First see if any hands actually exist with the Total/Soft
                    cStrat.StratTD(Total, Soft).NetProb(upcard) = 0
                    index = PlayerHandTotal(Total, Soft)
                    Do While index
                        If cStrat.HandEVs(index).Multiplier(upcard) > 0 Then
                            cStrat.StratTD(Total, Soft).NetProb(upcard) += cStrat.HandEVs(index).Multiplier(upcard)
                            Exit Do
                        End If
                        index = PlayerHands(index).NextHand
                    Loop
                    If cStrat.StratTD(Total, Soft).NetProb(upcard) <> 0 Then
                        pairMultiplier = 0
                        'If yes, then all hands are either forced or a pair so check pairs first
                        If Total = 12 And Soft = True + 1 Then
                            'The TD strat will be .None only when A,A is split
                            'These are taken care of with 2-Card check below when not TD
                            If SplitIndex(1, upcard) > 0 And cStrat.NCardsCD = 0 Then
                                Select Case cStrat.HandEVs(SplitIndex(1, upcard)).EVs.Strat(upcard)
                                    Case C.Strat.P
                                        'No hands are available to pick a strategy
                                        cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.None
                                    Case C.Strat.PS
                                        cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.S
                                    Case C.Strat.PH
                                        cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.H
                                    Case C.Strat.PD
                                        cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.D
                                    Case C.Strat.PR
                                        cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.R
                                End Select
                                pairMultiplier = cStrat.HandEVs(SplitIndex(1, upcard)).Multiplier(upcard)
                            End If
                        ElseIf Soft = False + 1 Then
                            For paircard = 2 To 10
                                If SplitIndex(paircard, upcard) > 0 Then
                                    If PlayerHands(SplitIndex(paircard, upcard)).Hand.Total = Total Then
                                        'Check and see if the pair is split and if there are
                                        ' post-split cards to allow a strategy
                                        Select Case cStrat.HandEVs(SplitIndex(paircard, upcard)).EVs.Strat(upcard)
                                            Case C.Strat.PS, C.Strat.PH, C.Strat.PD, C.Strat.PR
                                                If (CurrentShoe.Cards(paircard) >= (SPL + 1)) Then
                                                    CurrentShoe.Deal(paircard, (SPL + 1))
                                                    If CardProb(paircard, 0) > 0 Then
                                                        'All splits should have a 2nd strat at this point except A,A
                                                        Select Case cStrat.HandEVs(SplitIndex(paircard, upcard)).EVs.Strat(upcard)
                                                            Case C.Strat.PS
                                                                cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.S
                                                            Case C.Strat.PH
                                                                cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.H
                                                            Case C.Strat.PD
                                                                cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.D
                                                            Case C.Strat.PR
                                                                cStrat.StratTD(Total, Soft).Strat(upcard) = C.Strat.R
                                                        End Select
                                                    End If
                                                    CurrentShoe.Undeal(paircard, (SPL + 1))
                                                End If
                                                pairMultiplier = cStrat.HandEVs(SplitIndex(paircard, upcard)).Multiplier(upcard)
                                        End Select
                                    End If
                                End If
                            Next
                        End If

                        If cStrat.StratTD(Total, Soft).NetProb(upcard) <> pairMultiplier Then
                            'Only non-split pair hands and other forced hands need be included
                            index = PlayerHandTotal(Total, Soft)
                            forcedStrat = cStrat.HandEVs(index).EVs.Strat(upcard)
                            forcedAllSame = True
                            index = PlayerHands(index).NextHand
                            Do While index
                                If (forcedStrat <> cStrat.HandEVs(index).EVs.Strat(upcard)) Then
                                    forcedAllSame = False
                                End If
                                index = PlayerHands(index).NextHand
                            Loop
                            If forcedAllSame Then
                                cStrat.StratTD(Total, Soft).Strat(upcard) = forcedStrat
                            End If
                        End If
                    End If
                End If
            Next Soft
        Next Total

    Private Function OldApplyBonusRulesHand(ByVal pHand As BJCAPlayerHandClass, ByVal upcard As Integer, ByVal bjprob As Double, ByVal postSplit As Boolean, ByVal postDouble As Boolean) As BJCAHandEVsClass
        Dim apply As Boolean
        Dim winEV As Double
        Dim loseEV As Double
        Dim pushEV As Double
        Dim netEV As Double
        Dim tempBJPayoff As Double

        Dim suit As Integer
        Dim card As Integer
        Dim i As Integer

        Dim probSuit(3) As Double
        Dim pGeneral As Double
        Dim pSuited As Double
        Dim pSpecificSuit As Double
        '        Dim bjStandProb As Double

        Dim rule As Integer
        Dim newHandEVs As New BJCAHandEVsClass

        Dim noRulesApply As Boolean

        noRulesApply = True
        'Make sure the rule isn't applied to player BJ
        If Not (pHand.Hand.Total = 21 And pHand.Hand.NumCards = 2) Then
            'netEV = winEV - loseEV
            'winEV = netEV + loseEV
            'push = 1 - winEV - loseEV = 1 - (netEV + loseEV) - loseEV = 1 - netEV - 2*loseEV
            'loseEV = (1 - netEV - push) / 2
            loseEV = (1 - pHand.HandEVs.StandEV(upcard) - pHand.HandEVs.StandPushEV(upcard)) / 2
            winEV = pHand.HandEVs.StandEV(upcard) + loseEV
            pushEV = pHand.HandEVs.StandPushEV(upcard)

            For rule = 0 To BonusRulesList.NumRules - 1
                If BonusRulesList.L(rule).RuleOn Then
                    'First see if the hand qualifies to have the rule applied to it
                    apply = False

                    'If post-split bonuses are added then add the following below:      ((Not postSplit And BonusRulesList.L(rule).PreSplit) Or (postSplit And BonusRulesList.L(rule).PostSplit)) And 

                    If BonusRulesList.L(rule).UseSpecificHand Then
                        If (BonusRulesList.L(rule).ExactMatch And pHand.Hand.SameAs(BonusRulesList.L(rule).Hand)) Or (Not BonusRulesList.L(rule).ExactMatch And pHand.Hand.Includes(BonusRulesList.L(rule).Hand)) Then
                            apply = True
                        End If
                    Else
                        If pHand.Hand.Total = BonusRulesList.L(rule).Hand.Total Or BonusRulesList.L(rule).Hand.Total = 0 Then
                            If (Not BonusRulesList.L(rule).HardSoftOnly) Or (BonusRulesList.L(rule).HardOnly And Not pHand.Hand.Soft) Or (BonusRulesList.L(rule).SoftOnly And pHand.Hand.Soft) Then
                                If (BonusRulesList.L(rule).Hand.NumCards = 0) Or (BonusRulesList.L(rule).Hand.NumCards = pHand.Hand.NumCards) Or (BonusRulesList.L(rule).OrLess And pHand.Hand.NumCards <= BonusRulesList.L(rule).Hand.NumCards) Or (BonusRulesList.L(rule).OrMore And pHand.Hand.NumCards >= BonusRulesList.L(rule).Hand.NumCards) Then
                                    apply = True
                                End If
                            End If
                        End If
                    End If

                    If apply Then
                        noRulesApply = False
                        newHandEVs.Empty()
                        pGeneral = 0
                        pSuited = 0
                        pSpecificSuit = 0

                        If BonusRulesList.L(rule).Suited Then
                            'First just figure out the chances of any suited hand
                            'Note that these probs are given that the hand occurring is a given
                            If InfiniteDeck Then
                                CurrentShoe.Reset(OriginalShoe)
                            End If
                            pSuited = 0
                            For suit = 0 To 3
                                probSuit(suit) = 1
                                For card = 1 To 10
                                    If probSuit(suit) > 0 Then
                                        For i = 1 To pHand.Hand.Cards(card)
                                            If CurrentShoe.Suits(card, suit) > 0 Then
                                                probSuit(suit) *= CurrentShoe.Suits(card, suit) / CurrentShoe.Cards(card)
                                                If Not InfiniteDeck Then CurrentShoe.DealSuited(card, suit)
                                            Else
                                                probSuit(suit) = 0
                                                Exit For
                                            End If
                                        Next i
                                    End If
                                Next card
                                If BonusRulesList.L(rule).SpecificSuit And BonusRulesList.L(rule).SuitToWin = suit Then
                                    pSpecificSuit = probSuit(suit)
                                Else
                                    pSuited += probSuit(suit)
                                End If
                                CurrentShoe.Reset(OriginalShoe)
                            Next suit
                        End If  'Suited

                        'Need to finalize probs here
                        pGeneral = 1 - pSuited - pSpecificSuit
                        If BonusRulesList.L(rule).PayoffSuit = 0 Then
                            pSuited += pSpecificSuit
                            pSpecificSuit = 0
                        End If
                        If BonusRulesList.L(rule).PayoffSuited = 0 Then
                            pGeneral += pSuited
                            pSuited = 0
                        End If

                        'Apply the upcard limitation on BJ Payoffs here
                        '   No need to check for other UC's since bjprob is 0 for them
                        If (upcard = 1 And BonusRulesList.L(rule).BJAUp) Or (upcard = 10 And BonusRulesList.L(rule).BJTUp) Then
                            tempBJPayoff = BonusRulesList.L(rule).PayoffBJ
                        Else
                            tempBJPayoff = -1
                        End If

                        'Now adjust the evs
                        'The upcard can only be non-zero and therefore only match if UCPayoff <> 0
                        If (upcard <> 1 And upcard <> 10) Or (upcard = 1 And CheckAce And pHand.Hand.NumCards > 2) Or (upcard = 10 And CheckTen And pHand.Hand.NumCards > 2) Then
                            'No BJ Possible if UC<>(1 or 10) or if Checking and >2 cards in hand
                            If Not BonusRulesList.L(rule).MustWin And Not BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * BonusRulesList.L(rule).PayoffUC
                                Else
                                    newHandEVs.StandEV(upcard) += pGeneral * BonusRulesList.L(rule).PayoffGeneral
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * BonusRulesList.L(rule).PayoffUC
                                Else
                                    newHandEVs.StandEV(upcard) += pSuited * BonusRulesList.L(rule).PayoffSuited
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * BonusRulesList.L(rule).PayoffUC
                                Else
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * BonusRulesList.L(rule).PayoffSuit
                                End If
                            ElseIf BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                Else
                                    newHandEVs.StandEV(upcard) += pGeneral * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffGeneral)
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                Else
                                    newHandEVs.StandEV(upcard) += pSuited * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuited)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                Else
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuit)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                End If
                            Else
                                'Since hand Must Win, pushev is unaffected
                                newHandEVs.StandPushEV(upcard) = pushEV
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pGeneral * (BonusRulesList.L(rule).PayoffGeneral * winEV - loseEV)
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pSuited * (BonusRulesList.L(rule).PayoffSuited * winEV - loseEV)
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (BonusRulesList.L(rule).PayoffSuit * winEV - loseEV)
                                End If
                            End If


                        ElseIf (upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen) Then
                            'No checking for BJ but BJ possible
                            If Not BonusRulesList.L(rule).MustWin And Not BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * ((1 - bjprob) * BonusRulesList.L(rule).PayoffUC + bjprob * tempBJPayoff)
                                Else
                                    newHandEVs.StandEV(upcard) += pGeneral * ((1 - bjprob) * BonusRulesList.L(rule).PayoffGeneral + bjprob * tempBJPayoff)
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * ((1 - bjprob) * BonusRulesList.L(rule).PayoffUC + bjprob * tempBJPayoff)
                                Else
                                    newHandEVs.StandEV(upcard) += pSuited * ((1 - bjprob) * BonusRulesList.L(rule).PayoffSuited + bjprob * tempBJPayoff)
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * ((1 - bjprob) * BonusRulesList.L(rule).PayoffUC + bjprob * tempBJPayoff)
                                Else
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * ((1 - bjprob) * BonusRulesList.L(rule).PayoffSuit + bjprob * tempBJPayoff)
                                End If
                            ElseIf BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * ((1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC) + bjprob * tempBJPayoff)
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                Else
                                    newHandEVs.StandEV(upcard) += pGeneral * ((1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffGeneral) + bjprob * tempBJPayoff)
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * ((1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC) + bjprob * tempBJPayoff)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                Else
                                    newHandEVs.StandEV(upcard) += pSuited * ((1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuited) + bjprob * tempBJPayoff)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * ((1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC) + bjprob * tempBJPayoff)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                Else
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * ((1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuit) + bjprob * tempBJPayoff)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                End If
                            Else
                                'Since hand Must Win, pushev is unaffected and hand cannot beat BJ
                                newHandEVs.StandPushEV(upcard) = pushEV
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pGeneral * (BonusRulesList.L(rule).PayoffGeneral * winEV - loseEV)
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pSuited * (BonusRulesList.L(rule).PayoffSuited * winEV - loseEV)
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (BonusRulesList.L(rule).PayoffSuit * winEV - loseEV)
                                End If
                            End If


                        Else
                            'BJ Possible and dealer checking and player's hand has 2 cards
                            'No BJ adjustments need to be made when the hand doesn't beat BJ
                            'I.e. if PayoffBJ is -1, then the hand is already appropriately conditioned
                            If Not BonusRulesList.L(rule).MustWin And Not BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    'NetPayoff = pBJ*PayoffBJ + (1-pBJ)*Payoff
                                    'NetPayoff = pBJ*(-1) + (1-pBJ)*CEV
                                    'CEV = (NetPayoff + pBJ)/(1-pBJ)
                                    '                                   netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffUC
                                    '                                   newHandEVs.StandEV(upcard) += pGeneral * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pGeneral * BonusRulesList.L(rule).PayoffUC
                                    newHandEVs.BJStandEV(upcard) += pGeneral * tempBJPayoff
                                Else
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffGeneral
                                    '                                    newHandEVs.StandEV(upcard) += pGeneral * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pGeneral * BonusRulesList.L(rule).PayoffGeneral
                                    newHandEVs.BJStandEV(upcard) += pGeneral * tempBJPayoff
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffUC
                                    '                                    newHandEVs.StandEV(upcard) += pSuited * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pSuited * BonusRulesList.L(rule).PayoffUC
                                    newHandEVs.BJStandEV(upcard) += pSuited * tempBJPayoff
                                Else
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffSuited
                                    '                                    newHandEVs.StandEV(upcard) += pSuited * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pSuited * BonusRulesList.L(rule).PayoffSuited
                                    newHandEVs.BJStandEV(upcard) += pSuited * tempBJPayoff
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffUC
                                    '                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * BonusRulesList.L(rule).PayoffUC
                                    newHandEVs.BJStandEV(upcard) += pSpecificSuit * tempBJPayoff
                                Else
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffSuit
                                    '                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * BonusRulesList.L(rule).PayoffSuit
                                    newHandEVs.BJStandEV(upcard) += pSpecificSuit * tempBJPayoff
                                End If
                            ElseIf BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    'NetPayoff = pBJ*PayoffBJ + (1-pBJ)*Payoff
                                    'NetPayoff = pBJ*(-1) + (1-pBJ)*CEV
                                    'CEV = (NetPayoff + pBJ)/(1-pBJ)
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    '                                    newHandEVs.StandEV(upcard) += pGeneral * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pGeneral * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    newHandEVs.BJStandEV(upcard) += pGeneral * tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                Else
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffGeneral)
                                    '                                    newHandEVs.StandEV(upcard) += pGeneral * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pGeneral * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffGeneral)
                                    newHandEVs.BJStandEV(upcard) += pGeneral * tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) += pGeneral * pushEV
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    '                                    newHandEVs.StandEV(upcard) += pSuited * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pSuited * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    newHandEVs.BJStandEV(upcard) += pSuited * tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                Else
                                    '                                   netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuited)
                                    '                                   newHandEVs.StandEV(upcard) += pSuited * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pSuited * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuited)
                                    newHandEVs.BJStandEV(upcard) += pSuited * tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) += pSuited * pushEV
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    '                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    newHandEVs.BJStandEV(upcard) += pSpecificSuit * tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                Else
                                    '                                   netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuit)
                                    '                                   newHandEVs.StandEV(upcard) += pSpecificSuit * (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuit)
                                    newHandEVs.BJStandEV(upcard) += pSpecificSuit * tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) += pSpecificSuit * pushEV
                                End If
                            Else
                                'Since hand Must Win, pushev is unaffected and BJ hand cannot beat BJ
                                newHandEVs.StandPushEV(upcard) = pushEV
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (winEV - loseEV)
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And Not BonusRulesList.L(rule).HandSuitedUC) Then
                                    newHandEVs.StandEV(upcard) += pGeneral * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pGeneral * (BonusRulesList.L(rule).PayoffGeneral * winEV - loseEV)
                                End If

                                If BonusRulesList.L(rule).PayoffSuited = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (winEV - loseEV)
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSuited * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pSuited * (BonusRulesList.L(rule).PayoffSuited * winEV - loseEV)
                                End If

                                If BonusRulesList.L(rule).PayoffSuit = 0 And Not BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (winEV - loseEV)
                                ElseIf BonusRulesList.L(rule).Upcard = upcard Then
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (BonusRulesList.L(rule).PayoffUC * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) += pSpecificSuit * (BonusRulesList.L(rule).PayoffSuit * winEV - loseEV)
                                End If
                            End If
                        End If
                    End If  'Apply
                End If  'Rule On
            Next rule
        End If  'Not BJ

        If noRulesApply Then
            newHandEVs.StandEV(upcard) = pHand.HandEVs.StandEV(upcard)
            newHandEVs.StandPushEV(upcard) = pHand.HandEVs.StandPushEV(upcard)
        End If

        Return newHandEVs
    End Function

    Private Sub OldApplyBonusRulesPreSplit()
        'Apply both the Dealer-Player Push rules as well as the bonus Rules.
        'Bonus Rules override Dealer-Player push rules 
        Dim index As Integer
        Dim upcard As Integer
        Dim bjprob As Double
        Dim newEVs As New BJCAHandEVsClass

        If BonusRulesList.NumRules > 0 Then
            CurrentShoe.Reset(OriginalShoe)
            For index = 1 To NumPHands
                'Adjustment for player blackjack is separate
                If PlayerHands(index).Hand.NumCards > 1 And Not PlayerHands(index).Hand.IsBJ Then
                    For upcard = 1 To 10
                        If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                            If upcard = 1 Then
                                bjprob = BJHandNumerator(10, PlayerHands(index).Hand.Cards(10), 1) / BJHandDivisor(PlayerHands(index).Hand.NumCards + 1, 1)
                            ElseIf upcard = 10 Then
                                bjprob = BJHandNumerator(1, PlayerHands(index).Hand.Cards(1), 1) / BJHandDivisor(PlayerHands(index).Hand.NumCards + 1, 1)
                            End If

                            'Rules will be applied in sequence and override the previous rules.
                            newEVs.Empty()
                            newEVs = ApplyBonusRulesNonSuitedHand(PlayerHands(index), upcard, bjprob, False, False)
                            PlayerHands(index).HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                            PlayerHands(index).HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                            PlayerHands(index).HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                        End If
                    Next
                End If
            Next index
        End If
    End Sub


    Private Sub xApplyForcedRuleHandUpcard(ByRef cStrat As BJCAStrategyClass, ByVal index As Integer, ByVal rule As BJCAForcedRulesClass, ByVal upcard As Integer)
        Dim apply As Boolean
        Dim card As Integer

        'First see if the forced rule applies to the hand
        apply = False

        If rule.Index <> 0 Then
            'This takes care of all ExactMatch hands
            If rule.Index = index Then
                apply = True
            End If
        Else
            'The hand does not require an exact match
            If rule.UseSpecificHand And (Not rule.ExactMatch And PlayerHands(index).Hand.Includes(rule.Hand)) Then
                'This will take care of specific hand rules
                apply = True
            Else
                'Finally check for rules that don't require specific hands
                If (PlayerHands(index).Hand.Total = rule.Hand.Total) Or (rule.Hand.Total = 0) Then
                    If ((Not rule.Hand.Soft And Not PlayerHands(index).Hand.Soft) Or (rule.Hand.Soft And PlayerHands(index).Hand.Soft)) Then
                        If (rule.Hand.NumCards = 0) Or (rule.Hand.NumCards = PlayerHands(index).Hand.NumCards) Or (rule.OrLess And PlayerHands(index).Hand.NumCards <= rule.Hand.NumCards) Or (rule.OrMore And PlayerHands(index).Hand.NumCards >= rule.Hand.NumCards) Then
                            apply = True
                        End If
                    End If
                End If
            End If
        End If

        If apply Then
            'If the strategy is to do nothing then it resets any previous forced strat
            Select Case rule.Strat
                Case BJCAGlobalsClass.Strat.None
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).SPreallowed(upcard) = True
                        cStrat.HandEVs(index).HPreallowed(upcard) = True
                        cStrat.HandEVs(index).PAllowed(upcard) = True
                        cStrat.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
                        cStrat.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
                        cStrat.HandEVs(index).PreForced(upcard) = False
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).SPostallowed(upcard) = True
                        cStrat.HandEVs(index).HPostallowed(upcard) = True
                        cStrat.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                        cStrat.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
                        cStrat.HandEVs(index).PostForced(upcard) = False
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                Case BJCAGlobalsClass.Strat.S, BJCAGlobalsClass.Strat.H
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).PreForced(upcard) = True
                        cStrat.HandEVs(index).EVs.Strat(upcard) = rule.Strat
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).PostForced(upcard) = True
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = rule.Strat
                    End If
                Case BJCAGlobalsClass.Strat.D
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).PreForced(upcard) = True
                        If PlayerHands(index).DPreallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = rule.Strat
                        Else
                            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.H
                        End If
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).PostForced(upcard) = True
                        If PlayerHands(index).DPostallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = rule.Strat
                        Else
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.H
                        End If
                    End If
                Case BJCAGlobalsClass.Strat.DS
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).PreForced(upcard) = True
                        If PlayerHands(index).DPreallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = rule.Strat
                        Else
                            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.S
                        End If
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).PostForced(upcard) = True
                        If PlayerHands(index).DPostallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = rule.Strat
                        Else
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.S
                        End If
                    End If
                Case BJCAGlobalsClass.Strat.R
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).PreForced(upcard) = True
                        If PlayerHands(index).RPreallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = rule.Strat
                        Else
                            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.H
                        End If
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).PostForced(upcard) = True
                        If PlayerHands(index).RPostallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = rule.Strat
                        Else
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.H
                        End If
                    End If
                Case BJCAGlobalsClass.Strat.RS
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).PreForced(upcard) = True
                        If PlayerHands(index).RPreallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = rule.Strat
                        Else
                            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.S
                        End If
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).PostForced(upcard) = True
                        If PlayerHands(index).RPostallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = rule.Strat
                        Else
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.S
                        End If
                    End If
                Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR
                    For card = 1 To 10
                        If rule.Hand.Cards(card) = 2 Then
                            Exit For
                        End If
                    Next card
                    If SplitIndex(card, upcard) > 0 Then
                        cStrat.HandEVs(index).EVs.Strat(upcard) = rule.Strat
                        Select Case rule.Strat
                            Case BJCAGlobalsClass.Strat.P
                                cStrat.HandEVs(index).PostForced(upcard) = False
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                            Case BJCAGlobalsClass.Strat.PS
                                cStrat.HandEVs(index).PostForced(upcard) = True
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.S
                            Case BJCAGlobalsClass.Strat.PH
                                cStrat.HandEVs(index).PostForced(upcard) = True
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.H
                            Case BJCAGlobalsClass.Strat.PD
                                If PlayerHands(index).DPostallowed(upcard) Then
                                    cStrat.HandEVs(index).PostForced(upcard) = True
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.D
                                Else
                                    cStrat.HandEVs(index).PostForced(upcard) = False
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                                End If
                            Case BJCAGlobalsClass.Strat.PR
                                If PlayerHands(index).RPostallowed(upcard) Then
                                    cStrat.HandEVs(index).PostForced(upcard) = True
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.R
                                Else
                                    cStrat.HandEVs(index).PostForced(upcard) = False
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                                End If
                        End Select
                    End If
                Case BJCAGlobalsClass.Strat.xS
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).SPreallowed(upcard) = False
                        cStrat.HandEVs(index).PreForced(upcard) = False
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).SPostallowed(upcard) = False
                        cStrat.HandEVs(index).PostForced(upcard) = False
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                Case BJCAGlobalsClass.Strat.xH
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).HPreallowed(upcard) = False
                        cStrat.HandEVs(index).PreForced(upcard) = False
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).HPostallowed(upcard) = False
                        cStrat.HandEVs(index).PostForced(upcard) = False
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                Case BJCAGlobalsClass.Strat.xD
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).DPreallowed(upcard) = False
                        cStrat.HandEVs(index).PreForced(upcard) = False
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).DPostallowed(upcard) = False
                        cStrat.HandEVs(index).PostForced(upcard) = False
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                Case BJCAGlobalsClass.Strat.xR
                    If rule.PreSplit Then
                        cStrat.HandEVs(index).RPreallowed(upcard) = False
                        cStrat.HandEVs(index).PreForced(upcard) = False
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                    If rule.PostSplit Then
                        cStrat.HandEVs(index).RPostallowed(upcard) = False
                        cStrat.HandEVs(index).PostForced(upcard) = False
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                    End If
                Case BJCAGlobalsClass.Strat.xP
                    cStrat.HandEVs(index).PAllowed(upcard) = False
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
            End Select
        End If
    End Sub

    Private Sub xApplyForcedRulesHand(ByRef cStrat As BJCAStrategyClass, ByVal index As Integer)
        Dim rule As Integer
        Dim upcard As Integer

        'First apply the rules from the Forced Tables
        For rule = 0 To ForcedTableRulesList.NumRules - 1
            upcard = ForcedTableRulesList.L(rule).Upcard
            If UCAllowed(upcard) And PlayerHands(index).Hand.NumCards > cStrat.NCardsCD And PlayerHands(index).Hand.NumCards > 1 And ForcedTableRulesList.L(rule).RuleOn And Not (ForcedTableRulesList.L(rule).UseSpecificHand And ForcedTableRulesList.L(rule).ExactMatch And index = 0) Then
                ApplyForcedRuleHandUpcard(Forced, index, ForcedTableRulesList.L(rule), upcard)
            End If
        Next rule

        'Now apply the individual Forced Rules
        For rule = 0 To ForcedRulesList.NumRules - 1
            If ForcedRulesList.L(rule).Upcard = 0 Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) And PlayerHands(index).Hand.NumCards > 1 And ForcedRulesList.L(rule).RuleOn And Not (ForcedRulesList.L(rule).UseSpecificHand And ForcedRulesList.L(rule).ExactMatch And index = 0) Then
                        ApplyForcedRuleHandUpcard(Forced, index, ForcedRulesList.L(rule), upcard)
                    End If
                Next upcard
            Else
                upcard = ForcedRulesList.L(rule).Upcard
                If UCAllowed(upcard) And PlayerHands(index).Hand.NumCards > 1 And ForcedRulesList.L(rule).RuleOn And Not (ForcedRulesList.L(rule).UseSpecificHand And ForcedRulesList.L(rule).ExactMatch And index = 0) Then
                    ApplyForcedRuleHandUpcard(Forced, index, ForcedRulesList.L(rule), upcard)
                End If
            End If
        Next rule

        For upcard = 1 To 10
            'Make sure at least one strategy is possible, if not then reset the whole strategy
            If cStrat.HandEVs(index).SPreallowed(upcard) = False And _
                cStrat.HandEVs(index).HPreallowed(upcard) = False And _
                cStrat.HandEVs(index).PAllowed(upcard) = False And _
                cStrat.HandEVs(index).DPreallowed(upcard) = False And _
                cStrat.HandEVs(index).RPreallowed(upcard) = False And _
                cStrat.HandEVs(index).PreForced(upcard) = True Then

                cStrat.HandEVs(index).SPreallowed(upcard) = True
                cStrat.HandEVs(index).HPreallowed(upcard) = True
                cStrat.HandEVs(index).PAllowed(upcard) = True
                cStrat.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
                cStrat.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
                cStrat.HandEVs(index).PreForced(upcard) = False
                cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
            End If
            If cStrat.HandEVs(index).SPostallowed(upcard) = False And _
                cStrat.HandEVs(index).HPostallowed(upcard) = False And _
                cStrat.HandEVs(index).DPostallowed(upcard) = False And _
                cStrat.HandEVs(index).RPostallowed(upcard) = False And _
                cStrat.HandEVs(index).PostForced(upcard) = True Then

                cStrat.HandEVs(index).SPostallowed(upcard) = True
                cStrat.HandEVs(index).HPostallowed(upcard) = True
                cStrat.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                cStrat.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
                cStrat.HandEVs(index).PostForced(upcard) = False
                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
            End If
        Next upcard

    End Sub

    Public Sub xApplyForcedRules()
        Dim rule As Integer
        Dim index As Integer
        Dim total As Integer
        Dim upcard As Integer


        If Forced.ComputeStrat Then
            'Clear out the Forced TD Strategies which are needed for the NCard strategies
            For total = 4 To 21
                For upcard = 1 To 10
                    If Forced.StratTD(total, False + 1) Is Nothing Then
                        Forced.StratTD(total, False + 1) = New BJCATDStratClass
                    End If
                    If Forced.StratTD(total, True + 1) Is Nothing Then
                        Forced.StratTD(total, True + 1) = New BJCATDStratClass
                    End If
                    '                    Forced.StratTD(total, False + 1).Forced(upcard) = False
                    '                    Forced.StratTD(total, True + 1).Forced(upcard) = False
                Next upcard
            Next total

            'Find the hand indices for the table's CD Hands
            For rule = 0 To ForcedTableRulesList.NumRules - 1
                If ForcedTableRulesList.L(rule).UseSpecificHand And ForcedTableRulesList.L(rule).ExactMatch Then
                    ForcedTableRulesList.L(rule).Index = FindPlayerHand(ForcedTableRulesList.L(rule).Hand)
                Else
                    Forced.StratTD(ForcedTableRulesList.L(rule).Hand.Total, ForcedTableRulesList.L(rule).Hand.Soft + 1).Strat(ForcedTableRulesList.L(rule).Upcard) = ForcedTableRulesList.L(rule).Strat
                    '                    Forced.StratTD(ForcedTableRulesList.L(rule).Hand.Total, ForcedTableRulesList.L(rule).Hand.Soft + 1).Forced(ForcedTableRulesList.L(rule).Upcard) = True
                    ForcedTableRulesList.L(rule).Index = 0
                End If
            Next rule

            'Find the hand indices for the the non-table CD Hands
            For rule = 0 To ForcedRulesList.NumRules - 1
                If ForcedRulesList.L(rule).UseSpecificHand And ForcedRulesList.L(rule).ExactMatch Then
                    ForcedRulesList.L(rule).Index = FindPlayerHand(ForcedRulesList.L(rule).Hand)
                Else
                    ForcedRulesList.L(rule).Index = 0
                End If
            Next rule

            'Reset the Forced Strategy before applying the rules
            ResetForcedRulesHands(Forced)

            For index = 1 To NumPHands
                If PlayerHands(index).Hand.NumCards > 1 Then
                    ApplyForcedRulesHand(Forced, index)
                End If
            Next
        End If

    End Sub


    Private Sub oldComputeStratHitTotal(ByRef cStrat As BJCAStrategyClass, ByVal Total As Integer, ByVal Soft As Boolean, ByVal upcard As Integer)
        Dim index As Integer
        Dim card As Integer
        Dim Strat As Integer
        Dim newStrat As Integer
        Dim prob As Double

        index = PlayerHandTotal(Total, Soft + 1)
        Do While index
            If PlayerHands(index).Hand.NumCards > 1 And HandPossible(PlayerHands(index).Hand) Then
                CurrentShoe.Deal(PlayerHands(index).Hand)
                cStrat.HandEVs(index).EVs.HitEV(upcard) = 0
                For card = 1 To 10
                    If CardProb(card, 0) > 0 Then
                        prob = CardProb(card, upcard)
                        If PlayerHands(index).HitHand(card) Then
                            If cStrat.HandEVs(PlayerHands(index).HitHand(card)).PreForced(upcard) Then
                                Strat = cStrat.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)
                            ElseIf cStrat.NCardsCD <> 0 And PlayerHands(PlayerHands(index).HitHand(card)).Hand.NumCards <= cStrat.NCardsCD Then
                                Strat = cStrat.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)
                            Else
                                Strat = cStrat.StratTD(PlayerHands(PlayerHands(index).HitHand(card)).Hand.Total, PlayerHands(PlayerHands(index).HitHand(card)).Hand.Soft + 1).Strat(upcard)
                            End If
                            If (Strat = BJCAGlobalsClass.Strat.DS And Not cStrat.HandEVs(PlayerHands(index).HitHand(card)).DPreallowed(upcard)) Or (Strat = BJCAGlobalsClass.Strat.RS And Not cStrat.HandEVs(PlayerHands(index).HitHand(card)).RPreallowed(upcard) > 0) Then
                                newStrat = BJCAGlobalsClass.Strat.S
                            ElseIf (Strat = BJCAGlobalsClass.Strat.D And Not cStrat.HandEVs(PlayerHands(index).HitHand(card)).DPreallowed(upcard)) Or (Strat = BJCAGlobalsClass.Strat.R And Not cStrat.HandEVs(PlayerHands(index).HitHand(card)).RPreallowed(upcard) > 0) Then
                                newStrat = BJCAGlobalsClass.Strat.H
                            Else
                                newStrat = Strat
                            End If
                            Select Case newStrat
                                Case BJCAGlobalsClass.Strat.S
                                    cStrat.HandEVs(index).EVs.HitEV(upcard) += prob * (PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.StandEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BonusEV(upcard))
                                Case BJCAGlobalsClass.Strat.D, BJCAGlobalsClass.Strat.DS
                                    cStrat.HandEVs(index).EVs.HitEV(upcard) += prob * (PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.DEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BonusEV(upcard))
                                Case BJCAGlobalsClass.Strat.H
                                    cStrat.HandEVs(index).EVs.HitEV(upcard) += prob * (cStrat.HandEVs(PlayerHands(index).HitHand(card)).EVs.HitEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BonusEV(upcard))
                                Case BJCAGlobalsClass.Strat.R, BJCAGlobalsClass.Strat.RS
                                    cStrat.HandEVs(index).EVs.HitEV(upcard) += prob * (PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.SurrEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BonusEV(upcard))
                            End Select
                        Else    'Bust
                            cStrat.HandEVs(index).EVs.HitEV(upcard) -= prob
                        End If
                    End If
                Next card
                CurrentShoe.Undeal(PlayerHands(index).Hand)
            End If
            index = PlayerHands(index).NextHand
        Loop
    End Sub

